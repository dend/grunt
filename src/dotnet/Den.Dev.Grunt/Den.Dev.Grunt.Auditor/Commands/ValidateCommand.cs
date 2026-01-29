// <copyright file="ValidateCommand.cs" company="Den Delimarsky">
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
    /// Command that validates API endpoints against their models.
    /// </summary>
    public class ValidateCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateCommand"/> class.
        /// </summary>
        public ValidateCommand()
            : base("validate", "Validate API endpoints against their C# models")
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
                description: "Specific endpoint to validate (optional)");

            var outputOption = new Option<string?>(
                aliases: new[] { "-o", "--output" },
                description: "Output path for JSON report");

            var htmlOutputOption = new Option<string?>(
                aliases: new[] { "--html", "--html-output" },
                description: "Output path for HTML report");

            AddOption(configOption);
            AddOption(testConfigOption);
            AddOption(endpointOption);
            AddOption(outputOption);
            AddOption(htmlOutputOption);

            this.SetHandler(ExecuteAsync, configOption, testConfigOption, endpointOption, outputOption, htmlOutputOption);
        }

        private async Task ExecuteAsync(string configPath, string testConfigPath, string? endpointFilter, string? outputPath, string? htmlOutputPath)
        {
            AnsiConsole.Write(new FigletText("Auditor").Color(Color.Cyan1));
            AnsiConsole.MarkupLine("[dim]Halo Infinite API Model Validation Tool[/]");
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
            var validator = new ResponseValidator();
            var reportGenerator = new ReportGenerator();

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

            // Validate endpoints
            var report = new ValidationReport();

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
                    var task = ctx.AddTask("[cyan]Validating endpoints[/]", maxValue: targets.Count);

                    foreach (var target in targets)
                    {
                        task.Description = $"[cyan]{target.EndpointId}[/]";

                        var result = await ValidateEndpointAsync(target, testConfig.SkipEndpoints, testConfig.SkipHttpMethods, executor, validator, registry);
                        report.Results.Add(result);

                        task.Increment(1);
                    }
                });

            // Display report
            reportGenerator.DisplayConsoleReport(report);

            // Save JSON report if requested
            if (!string.IsNullOrEmpty(outputPath))
            {
                reportGenerator.GenerateJsonReport(report, outputPath);
            }

            // Save HTML report if requested
            if (!string.IsNullOrEmpty(htmlOutputPath))
            {
                reportGenerator.GenerateHtmlReport(report, htmlOutputPath);
            }
        }

        private async Task<EndpointValidationResult> ValidateEndpointAsync(
            ValidationTarget target,
            List<SkipPattern> skipPatterns,
            List<string> skipHttpMethods,
            EndpointExecutor executor,
            ResponseValidator validator,
            ParameterRegistry registry)
        {
            var result = new EndpointValidationResult
            {
                EndpointId = target.EndpointId,
                ModelType = target.ExpectedModel,
            };

            // Check if should skip
            if (target.Skip)
            {
                result.Status = ValidationStatus.Skipped;
                result.SkipReason = target.SkipReason ?? "Marked as skip";
                return result;
            }

            // Check if HTTP method should be skipped
            if (!string.IsNullOrEmpty(target.HttpMethod) &&
                skipHttpMethods.Any(m => m.Equals(target.HttpMethod, StringComparison.OrdinalIgnoreCase)))
            {
                result.Status = ValidationStatus.Skipped;
                result.SkipReason = $"HTTP method {target.HttpMethod} is a mutation (skipped by config)";
                return result;
            }

            // Check skip patterns
            foreach (var pattern in skipPatterns)
            {
                if (MatchesPattern(target.EndpointId, pattern.Pattern))
                {
                    result.Status = ValidationStatus.Skipped;
                    result.SkipReason = pattern.Reason;
                    return result;
                }
            }

            // Check if required parameters are available
            var resolvedArgs = registry.ResolveArguments(target.Args);
            foreach (var kvp in resolvedArgs)
            {
                if (kvp.Value is string strVal && strVal.StartsWith("$"))
                {
                    result.Status = ValidationStatus.Skipped;
                    result.SkipReason = $"Missing parameter: {strVal}";
                    return result;
                }
            }

            try
            {
                // Execute the endpoint
                var execResult = await executor.ExecuteAsync(target.Method, target.Args);

                // Always capture request details for debugging
                result.RequestUrl = execResult.RequestUrl;
                result.RequestMethod = execResult.RequestMethod;
                result.RequestHeaders = execResult.RequestHeaders;
                result.HttpStatusCode = execResult.HttpStatusCode;
                result.RawJson = execResult.RawJson;

                if (!execResult.Success)
                {
                    result.Status = ValidationStatus.Error;
                    result.ErrorMessage = execResult.ErrorMessage;
                    return result;
                }

                // Validate the response
                if (!string.IsNullOrEmpty(execResult.RawJson) && !string.IsNullOrEmpty(target.ExpectedModel))
                {
                    var discrepancies = validator.Validate(execResult.RawJson, target.ExpectedModel);
                    result.Discrepancies = discrepancies;

                    // Determine status based on discrepancies
                    if (discrepancies.Count == 0)
                    {
                        result.Status = ValidationStatus.Pass;
                    }
                    else if (discrepancies.Any(d =>
                        d.Type == DiscrepancyType.DeserializationFailure ||
                        d.Type == DiscrepancyType.TypeMismatch))
                    {
                        result.Status = ValidationStatus.Fail;
                    }
                    else
                    {
                        result.Status = ValidationStatus.Warning;
                    }
                }
                else
                {
                    result.Status = ValidationStatus.Pass;
                }
            }
            catch (Exception ex)
            {
                result.Status = ValidationStatus.Error;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        private static bool MatchesPattern(string value, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return false;
            }

            // Convert wildcard pattern to regex
            var regexPattern = "^" + Regex.Escape(pattern).Replace("\\*", ".*") + "$";
            return Regex.IsMatch(value, regexPattern, RegexOptions.IgnoreCase);
        }
    }
}
