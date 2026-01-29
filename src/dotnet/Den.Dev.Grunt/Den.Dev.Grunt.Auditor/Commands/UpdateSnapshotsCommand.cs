// <copyright file="UpdateSnapshotsCommand.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Den.Dev.Grunt.Auditor.Models;
using Den.Dev.Grunt.Auditor.Services;
using Spectre.Console;

namespace Den.Dev.Grunt.Auditor.Commands
{
    /// <summary>
    /// Command that updates XMLDocsExamples with fresh API responses.
    /// </summary>
    public class UpdateSnapshotsCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSnapshotsCommand"/> class.
        /// </summary>
        public UpdateSnapshotsCommand()
            : base("update-snapshots", "Update XMLDocsExamples with fresh API responses")
        {
            var configOption = new Option<string>(
                aliases: new[] { "-c", "--config" },
                description: "Path to client.json configuration file",
                getDefaultValue: () => "client.json");

            var testConfigOption = new Option<string>(
                aliases: new[] { "-t", "--test-config" },
                description: "Path to endpoint-test-config.json",
                getDefaultValue: () => "Config/endpoint-test-config.json");

            var endpointOption = new Option<string?>(
                aliases: new[] { "-e", "--endpoint" },
                description: "Specific endpoint to update (optional)");

            var basePathOption = new Option<string?>(
                aliases: new[] { "-b", "--base-path" },
                description: "Base path to Den.Dev.Grunt directory");

            var noSanitizeOption = new Option<bool>(
                aliases: new[] { "--no-sanitize" },
                description: "Don't sanitize sensitive data (like XUIDs)");

            AddOption(configOption);
            AddOption(testConfigOption);
            AddOption(endpointOption);
            AddOption(basePathOption);
            AddOption(noSanitizeOption);

            this.SetHandler(ExecuteAsync, configOption, testConfigOption, endpointOption, basePathOption, noSanitizeOption);
        }

        private async Task ExecuteAsync(string configPath, string testConfigPath, string? endpointFilter, string? basePath, bool noSanitize)
        {
            AnsiConsole.Write(new FigletText("Auditor").Color(Color.Cyan1));
            AnsiConsole.MarkupLine("[dim]Updating XMLDocsExamples with fresh API responses[/]");
            AnsiConsole.WriteLine();

            // Authenticate
            var authManager = new AuthenticationManager(configPath);
            if (!await authManager.AuthenticateAsync())
            {
                AnsiConsole.MarkupLine("[red]Authentication failed.[/]");
                return;
            }

            if (authManager.Client == null)
            {
                AnsiConsole.MarkupLine("[red]Client not initialized.[/]");
                return;
            }

            // Initialize services
            var registry = new ParameterRegistry();
            registry.SetAuthParameters(
                authManager.Xuid ?? string.Empty,
                authManager.Gamertag,
                authManager.ClearanceToken);

            var executor = new EndpointExecutor(authManager.Client, registry);
            var discovery = new ParameterDiscovery(authManager.Client, registry, executor);
            var snapshotUpdater = new SnapshotUpdater(basePath);

            // Load test config
            EndpointTestConfig? testConfig = null;
            if (File.Exists(testConfigPath))
            {
                var json = File.ReadAllText(testConfigPath);
                testConfig = JsonSerializer.Deserialize<EndpointTestConfig>(json);
            }

            if (testConfig == null)
            {
                AnsiConsole.MarkupLine("[red]Could not load test configuration.[/]");
                return;
            }

            // Run discovery
            await discovery.RunDiscoveryChainAsync(testConfig);

            // Filter validation targets
            var targets = testConfig.ValidationTargets;
            if (!string.IsNullOrEmpty(endpointFilter))
            {
                targets = targets.Where(t =>
                    t.EndpointId.Contains(endpointFilter, StringComparison.OrdinalIgnoreCase)).ToList();

                if (targets.Count == 0)
                {
                    AnsiConsole.MarkupLine($"[yellow]No endpoints matching '{endpointFilter}' found.[/]");
                    return;
                }
            }

            // Update snapshots
            var updatedCount = 0;
            var skippedCount = 0;
            var failedCount = 0;

            await AnsiConsole.Progress()
                .AutoRefresh(true)
                .AutoClear(false)
                .HideCompleted(false)
                .Columns(new ProgressColumn[]
                {
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new SpinnerColumn(),
                })
                .StartAsync(async ctx =>
                {
                    var task = ctx.AddTask("[cyan]Updating snapshots[/]", maxValue: targets.Count);

                    foreach (var target in targets)
                    {
                        task.Description = $"[cyan]{target.EndpointId}[/]";

                        // Skip if marked to skip or matches skip pattern
                        if (target.Skip || ShouldSkip(target.EndpointId, testConfig.SkipEndpoints))
                        {
                            skippedCount++;
                            task.Increment(1);
                            continue;
                        }

                        // Check parameters
                        var resolvedArgs = registry.ResolveArguments(target.Args);
                        var hasMissingParams = resolvedArgs.Any(kvp =>
                            kvp.Value is string s && s.StartsWith("$"));

                        if (hasMissingParams)
                        {
                            skippedCount++;
                            task.Increment(1);
                            continue;
                        }

                        try
                        {
                            var execResult = await executor.ExecuteAsync(target.Method, target.Args);

                            if (execResult.Success && !string.IsNullOrEmpty(execResult.RawJson))
                            {
                                var sanitize = !noSanitize;
                                if (snapshotUpdater.UpdateSnapshot(target.EndpointId, execResult.RawJson, sanitize))
                                {
                                    updatedCount++;
                                }
                                else
                                {
                                    failedCount++;
                                }
                            }
                            else
                            {
                                failedCount++;
                            }
                        }
                        catch
                        {
                            failedCount++;
                        }

                        task.Increment(1);
                    }
                });

            // Summary
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold]Snapshot Update Summary:[/]");
            AnsiConsole.WriteLine();

            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("[bold]Status[/]"))
                .AddColumn(new TableColumn("[bold]Count[/]").Centered());

            table.AddRow("[green]Updated[/]", updatedCount.ToString());
            table.AddRow("[dim]Skipped[/]", skippedCount.ToString());
            table.AddRow("[red]Failed[/]", failedCount.ToString());

            AnsiConsole.Write(table);
        }

        private static bool ShouldSkip(string endpointId, List<SkipPattern> patterns)
        {
            foreach (var pattern in patterns)
            {
                var regexPattern = "^" + Regex.Escape(pattern.Pattern).Replace("\\*", ".*") + "$";
                if (Regex.IsMatch(endpointId, regexPattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
