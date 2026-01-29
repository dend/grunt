// <copyright file="DiscoverCommand.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Grunt.Auditor.Models;
using Den.Dev.Grunt.Auditor.Services;
using Spectre.Console;

namespace Den.Dev.Grunt.Auditor.Commands
{
    /// <summary>
    /// Command that authenticates and discovers available test parameters.
    /// </summary>
    public class DiscoverCommand : Command
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoverCommand"/> class.
        /// </summary>
        public DiscoverCommand()
            : base("discover", "Authenticate and discover available test parameters")
        {
            var configOption = new Option<string>(
                aliases: new[] { "-c", "--config" },
                description: "Path to client.json configuration file",
                getDefaultValue: () => "client.json");

            var testConfigOption = new Option<string>(
                aliases: new[] { "-t", "--test-config" },
                description: "Path to endpoint-test-config.json",
                getDefaultValue: () => "Config/endpoint-test-config.json");

            AddOption(configOption);
            AddOption(testConfigOption);

            this.SetHandler(ExecuteAsync, configOption, testConfigOption);
        }

        private async Task ExecuteAsync(string configPath, string testConfigPath)
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

            // Load test config
            EndpointTestConfig? testConfig = null;
            if (File.Exists(testConfigPath))
            {
                var json = File.ReadAllText(testConfigPath);
                testConfig = JsonSerializer.Deserialize<EndpointTestConfig>(json);
            }

            // Run discovery
            if (testConfig != null)
            {
                await discovery.RunDiscoveryChainAsync(testConfig);
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]No test config found, running built-in discovery only.[/]");
                AnsiConsole.WriteLine();
            }

            // Display discovered parameters
            DisplayDiscoveredParameters(registry.Parameters);
        }

        private static void DisplayDiscoveredParameters(DiscoveredParameters parameters)
        {
            AnsiConsole.MarkupLine("[bold]Discovered Parameters:[/]");
            AnsiConsole.WriteLine();

            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn(new TableColumn("[bold]Parameter[/]"))
                .AddColumn(new TableColumn("[bold]Value[/]"));

            table.AddRow("Player XUID", parameters.PlayerXuid);
            table.AddRow("Gamertag", parameters.Gamertag);
            table.AddRow("Clearance Token", TruncateValue(parameters.ClearanceToken, 40));
            table.AddRow("Flight ID", TruncateValue(parameters.FlightId, 40));

            if (parameters.MatchIds.Count > 0)
            {
                table.AddRow("Match IDs", $"{parameters.MatchIds.Count} discovered");
                foreach (var id in parameters.MatchIds)
                {
                    table.AddRow("", $"  {id}");
                }
            }
            else
            {
                table.AddRow("Match IDs", "[dim](none)[/]");
            }

            if (parameters.AssetIds.Count > 0)
            {
                table.AddRow("Asset IDs", $"{parameters.AssetIds.Count} discovered");
            }
            else
            {
                table.AddRow("Asset IDs", "[dim](none)[/]");
            }

            if (parameters.Custom.Count > 0)
            {
                table.AddRow("[bold]Custom Parameters[/]", string.Empty);
                foreach (var kvp in parameters.Custom)
                {
                    table.AddRow($"  {kvp.Key}", TruncateValue(kvp.Value, 40));
                }
            }

            AnsiConsole.Write(table);
        }

        private static string TruncateValue(string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "[dim](empty)[/]";
            }

            if (value.Length > maxLength)
            {
                return value.Substring(0, maxLength - 3) + "...";
            }

            return value;
        }
    }
}
