// <copyright file="ParameterDiscovery.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Den.Dev.Grunt.Auditor.Models;
using Den.Dev.Grunt.Core;
using Spectre.Console;

namespace Den.Dev.Grunt.Auditor.Services
{
    /// <summary>
    /// Discovers parameters by calling seed endpoints and extracting values from responses.
    /// </summary>
    public class ParameterDiscovery
    {
        private readonly HaloInfiniteClient _client;
        private readonly ParameterRegistry _registry;
        private readonly EndpointExecutor _executor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterDiscovery"/> class.
        /// </summary>
        /// <param name="client">Authenticated HaloInfiniteClient.</param>
        /// <param name="registry">Parameter registry to populate.</param>
        /// <param name="executor">Endpoint executor for calling discovery endpoints.</param>
        public ParameterDiscovery(HaloInfiniteClient client, ParameterRegistry registry, EndpointExecutor executor)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
            _executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        /// <summary>
        /// Runs the discovery chain to populate the parameter registry.
        /// </summary>
        /// <param name="config">Endpoint test configuration.</param>
        /// <returns>True if discovery completed successfully, false otherwise.</returns>
        public async Task<bool> RunDiscoveryChainAsync(EndpointTestConfig config)
        {
            var orderedSteps = config.DiscoveryChain.OrderBy(s => s.Step).ToList();

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[bold]Running parameter discovery chain...[/]");
            AnsiConsole.WriteLine();

            foreach (var step in orderedSteps)
            {
                AnsiConsole.MarkupLine($"  [dim]Step {step.Step}:[/] [cyan]{step.EndpointId}[/]");

                try
                {
                    var result = await _executor.ExecuteAsync(step.Method, step.Args);

                    if (result.Success && !string.IsNullOrEmpty(result.RawJson))
                    {
                        // Extract values using the defined extractors
                        foreach (var extractor in step.Extractors)
                        {
                            var values = _registry.ExtractFromJson(result.RawJson, extractor.Value);

                            if (values.Count > 0)
                            {
                                StoreExtractedValue(extractor.Key, values);
                                AnsiConsole.MarkupLine($"    [green]+[/] {extractor.Key}: [dim]{TruncateForDisplay(values)}[/]");
                            }
                            else
                            {
                                AnsiConsole.MarkupLine($"    [yellow]-[/] {extractor.Key}: [dim]not found[/]");
                            }
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"    [red]Failed:[/] {result.ErrorMessage ?? "Unknown error"}");
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"    [red]Error:[/] {ex.Message}");
                }

                AnsiConsole.WriteLine();
            }

            // Run built-in discovery for common parameters
            await RunBuiltInDiscoveryAsync();

            return true;
        }

        /// <summary>
        /// Runs built-in discovery for common parameters that aren't in the config.
        /// </summary>
        private async Task RunBuiltInDiscoveryAsync()
        {
            AnsiConsole.MarkupLine("[bold]Running built-in discovery...[/]");
            AnsiConsole.WriteLine();

            // Get match history if we don't have match IDs
            if (_registry.Parameters.MatchIds.Count == 0 && !string.IsNullOrEmpty(_registry.Parameters.PlayerXuid))
            {
                AnsiConsole.MarkupLine("  [dim]Discovering match IDs from match history...[/]");
                try
                {
                    var historyResult = await _client.Stats.GetMatchHistoryAsync(
                        _registry.Parameters.PlayerXuid,
                        0,
                        10,
                        Den.Dev.Grunt.Models.HaloInfinite.MatchType.All);

                    if (historyResult?.Result?.Results != null)
                    {
                        foreach (var match in historyResult.Result.Results.Take(5))
                        {
                            if (match.MatchId != Guid.Empty)
                            {
                                _registry.AddMatchId(match.MatchId.ToString());
                            }
                        }

                        AnsiConsole.MarkupLine($"    [green]+[/] Discovered {_registry.Parameters.MatchIds.Count} match IDs");
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"    [yellow]Warning:[/] Could not discover match IDs: {ex.Message}");
                }
            }

            // Get clearance/flight ID if we don't have it
            if (string.IsNullOrEmpty(_registry.Parameters.FlightId))
            {
                AnsiConsole.MarkupLine("  [dim]Discovering flight ID from clearance...[/]");
                try
                {
                    var clearanceResult = await _client.Settings.GetClearanceAsync("RETAIL", "UNUSED", "268411.25.10.26.1801-0", "1.13");

                    if (clearanceResult?.Result?.FlightConfigurationId != null)
                    {
                        _registry.SetFlightId(clearanceResult.Result.FlightConfigurationId);
                        AnsiConsole.MarkupLine($"    [green]+[/] Flight ID: [dim]{clearanceResult.Result.FlightConfigurationId}[/]");
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"    [yellow]Warning:[/] Could not discover flight ID: {ex.Message}");
                }
            }

            // Get asset IDs from match stats if we have match IDs
            if (_registry.Parameters.AssetIds.Count == 0 && _registry.Parameters.MatchIds.Count > 0)
            {
                AnsiConsole.MarkupLine("  [dim]Discovering asset IDs from match stats...[/]");
                try
                {
                    var matchId = _registry.Parameters.MatchIds[0];
                    var matchResult = await _client.Stats.GetMatchStatsAsync(matchId);

                    if (matchResult?.Result?.MatchInfo != null)
                    {
                        var matchInfo = matchResult.Result.MatchInfo;

                        // Extract map asset ID
                        if (matchInfo.MapVariant?.AssetId != null && matchInfo.MapVariant.AssetId != Guid.Empty)
                        {
                            _registry.AddMapAssetId(matchInfo.MapVariant.AssetId.ToString()!);
                            AnsiConsole.MarkupLine($"    [green]+[/] Map Asset ID: [dim]{matchInfo.MapVariant.AssetId}[/]");
                        }

                        // Extract playlist asset ID
                        if (matchInfo.Playlist?.AssetId != null && matchInfo.Playlist.AssetId != Guid.Empty)
                        {
                            _registry.AddPlaylistAssetId(matchInfo.Playlist.AssetId.ToString()!);
                            AnsiConsole.MarkupLine($"    [green]+[/] Playlist Asset ID: [dim]{matchInfo.Playlist.AssetId}[/]");
                        }

                        // Extract UGC game variant asset ID
                        if (matchInfo.UgcGameVariant?.AssetId != null && matchInfo.UgcGameVariant.AssetId != Guid.Empty)
                        {
                            _registry.AddAssetId(matchInfo.UgcGameVariant.AssetId.ToString()!);
                            AnsiConsole.MarkupLine($"    [green]+[/] Game Variant Asset ID: [dim]{matchInfo.UgcGameVariant.AssetId}[/]");
                        }
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"    [yellow]Warning:[/] Could not discover asset IDs: {ex.Message}");
                }
            }

            AnsiConsole.WriteLine();
        }

        /// <summary>
        /// Stores an extracted value in the appropriate registry location.
        /// </summary>
        private void StoreExtractedValue(string parameterName, List<string> values)
        {
            var lowerName = parameterName.ToLowerInvariant();

            switch (lowerName)
            {
                case "flightid":
                case "flight":
                    if (values.Count > 0)
                    {
                        _registry.SetFlightId(values[0]);
                    }

                    break;

                case "matchid":
                    if (values.Count > 0)
                    {
                        _registry.AddMatchId(values[0]);
                    }

                    break;

                case "matchids":
                    _registry.AddMatchIds(values);
                    break;

                case "assetid":
                    if (values.Count > 0)
                    {
                        _registry.AddAssetId(values[0]);
                    }

                    break;

                case "assetids":
                    foreach (var v in values)
                    {
                        _registry.AddAssetId(v);
                    }

                    break;

                case "versionid":
                    if (values.Count > 0)
                    {
                        _registry.AddVersionId(values[0]);
                    }

                    break;

                case "mapassetid":
                    if (values.Count > 0)
                    {
                        _registry.AddMapAssetId(values[0]);
                    }

                    break;

                case "playlistassetid":
                    if (values.Count > 0)
                    {
                        _registry.AddPlaylistAssetId(values[0]);
                    }

                    break;

                default:
                    // Store as custom parameter
                    if (values.Count > 0)
                    {
                        _registry.SetCustomParameter(parameterName, values[0]);
                    }

                    break;
            }
        }

        /// <summary>
        /// Truncates a list of values for display.
        /// </summary>
        private static string TruncateForDisplay(List<string> values)
        {
            if (values.Count == 0)
            {
                return "(none)";
            }

            if (values.Count == 1)
            {
                var v = values[0];
                return v.Length > 50 ? v.Substring(0, 47) + "..." : v;
            }

            var first = values[0];
            if (first.Length > 30)
            {
                first = first.Substring(0, 27) + "...";
            }

            return $"{first} (+{values.Count - 1} more)";
        }
    }
}
