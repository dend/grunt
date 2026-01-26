// <copyright file="ParameterRegistry.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text.Json;
using Den.Dev.Grunt.Auditor.Models;

namespace Den.Dev.Grunt.Auditor.Services
{
    /// <summary>
    /// Central registry for discovered parameters used in API calls.
    /// </summary>
    public class ParameterRegistry
    {
        private readonly DiscoveredParameters _parameters = new();
        private readonly Dictionary<string, string> _customParams = new();

        /// <summary>
        /// Gets the discovered parameters.
        /// </summary>
        public DiscoveredParameters Parameters => _parameters;

        /// <summary>
        /// Sets authentication-derived parameters.
        /// </summary>
        /// <param name="xuid">Player XUID.</param>
        /// <param name="gamertag">Player gamertag.</param>
        /// <param name="clearanceToken">Clearance token.</param>
        public void SetAuthParameters(string xuid, string? gamertag, string? clearanceToken)
        {
            _parameters.PlayerXuid = xuid;
            _parameters.Gamertag = gamertag ?? string.Empty;
            _parameters.ClearanceToken = clearanceToken ?? string.Empty;
        }

        /// <summary>
        /// Sets the flight ID.
        /// </summary>
        /// <param name="flightId">Flight configuration ID.</param>
        public void SetFlightId(string flightId)
        {
            _parameters.FlightId = flightId;
        }

        /// <summary>
        /// Adds a match ID to the registry.
        /// </summary>
        /// <param name="matchId">Match ID.</param>
        public void AddMatchId(string matchId)
        {
            if (!string.IsNullOrEmpty(matchId) && !_parameters.MatchIds.Contains(matchId))
            {
                _parameters.MatchIds.Add(matchId);
            }
        }

        /// <summary>
        /// Adds multiple match IDs to the registry.
        /// </summary>
        /// <param name="matchIds">Match IDs.</param>
        public void AddMatchIds(IEnumerable<string> matchIds)
        {
            foreach (var id in matchIds)
            {
                AddMatchId(id);
            }
        }

        /// <summary>
        /// Adds an asset ID to the registry.
        /// </summary>
        /// <param name="assetId">Asset ID.</param>
        public void AddAssetId(string assetId)
        {
            if (!string.IsNullOrEmpty(assetId) && !_parameters.AssetIds.Contains(assetId))
            {
                _parameters.AssetIds.Add(assetId);
            }
        }

        /// <summary>
        /// Adds a version ID to the registry.
        /// </summary>
        /// <param name="versionId">Version ID.</param>
        public void AddVersionId(string versionId)
        {
            if (!string.IsNullOrEmpty(versionId) && !_parameters.VersionIds.Contains(versionId))
            {
                _parameters.VersionIds.Add(versionId);
            }
        }

        /// <summary>
        /// Adds a map asset ID to the registry.
        /// </summary>
        /// <param name="mapAssetId">Map asset ID.</param>
        public void AddMapAssetId(string mapAssetId)
        {
            if (!string.IsNullOrEmpty(mapAssetId) && !_parameters.MapAssetIds.Contains(mapAssetId))
            {
                _parameters.MapAssetIds.Add(mapAssetId);
            }
        }

        /// <summary>
        /// Adds a playlist asset ID to the registry.
        /// </summary>
        /// <param name="playlistAssetId">Playlist asset ID.</param>
        public void AddPlaylistAssetId(string playlistAssetId)
        {
            if (!string.IsNullOrEmpty(playlistAssetId) && !_parameters.PlaylistAssetIds.Contains(playlistAssetId))
            {
                _parameters.PlaylistAssetIds.Add(playlistAssetId);
            }
        }

        /// <summary>
        /// Sets a custom parameter value.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <param name="value">Parameter value.</param>
        public void SetCustomParameter(string name, string value)
        {
            _customParams[name] = value;
            _parameters.Custom[name] = value;
        }

        /// <summary>
        /// Resolves a parameter reference to its value.
        /// </summary>
        /// <param name="reference">Parameter reference (e.g., "$playerXuid", "$matchId").</param>
        /// <returns>The resolved value, or null if not found.</returns>
        public string? Resolve(string reference)
        {
            if (string.IsNullOrEmpty(reference))
            {
                return null;
            }

            // Remove $ prefix if present
            var paramName = reference.StartsWith("$") ? reference.Substring(1) : reference;

            return paramName.ToLowerInvariant() switch
            {
                "playerxuid" or "player" or "xuid" => _parameters.PlayerXuid,
                "gamertag" => _parameters.Gamertag,
                "clearancetoken" or "clearance" => _parameters.ClearanceToken,
                "flightid" or "flight" => _parameters.FlightId,
                "matchid" => _parameters.FirstMatchId,
                "assetid" => _parameters.FirstAssetId,
                "versionid" => _parameters.FirstVersionId,
                "mapassetid" => _parameters.FirstMapAssetId,
                _ => _customParams.TryGetValue(paramName, out var value) ? value : null,
            };
        }

        /// <summary>
        /// Resolves all parameter references in a dictionary.
        /// </summary>
        /// <param name="args">Arguments with potential parameter references.</param>
        /// <returns>Resolved arguments.</returns>
        public Dictionary<string, object> ResolveArguments(Dictionary<string, object> args)
        {
            var resolved = new Dictionary<string, object>();

            foreach (var kvp in args)
            {
                if (kvp.Value is string strValue && strValue.StartsWith("$"))
                {
                    var resolvedValue = Resolve(strValue);
                    if (resolvedValue != null)
                    {
                        resolved[kvp.Key] = resolvedValue;
                    }
                    else
                    {
                        // Keep original if not resolved
                        resolved[kvp.Key] = strValue;
                    }
                }
                else if (kvp.Value is JsonElement element)
                {
                    // Handle JSON elements from config
                    resolved[kvp.Key] = ResolveJsonElement(element);
                }
                else
                {
                    resolved[kvp.Key] = kvp.Value;
                }
            }

            return resolved;
        }

        /// <summary>
        /// Extracts a value from a JSON response using a simple path expression.
        /// Supports basic JSONPath-like syntax: $.Property, $.Array[0], $.Array[*].
        /// </summary>
        /// <param name="json">Raw JSON string.</param>
        /// <param name="path">Path expression (e.g., "$.Results[0].MatchId").</param>
        /// <returns>Extracted value(s) as a list of strings.</returns>
        public List<string> ExtractFromJson(string json, string path)
        {
            var results = new List<string>();

            try
            {
                using var doc = JsonDocument.Parse(json);
                var tokens = ParsePath(path);
                ExtractValues(doc.RootElement, tokens, 0, results);
            }
            catch
            {
                // Return empty list on parse errors
            }

            return results;
        }

        private object ResolveJsonElement(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.String:
                    var strValue = element.GetString() ?? string.Empty;
                    if (strValue.StartsWith("$"))
                    {
                        return Resolve(strValue) ?? strValue;
                    }

                    return strValue;
                case JsonValueKind.Number:
                    if (element.TryGetInt32(out var intValue))
                    {
                        return intValue;
                    }

                    return element.GetDouble();
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                default:
                    return element.ToString();
            }
        }

        private List<string> ParsePath(string path)
        {
            var tokens = new List<string>();
            var current = path.TrimStart('$', '.');

            while (!string.IsNullOrEmpty(current))
            {
                // Handle array access
                var bracketIndex = current.IndexOf('[');
                var dotIndex = current.IndexOf('.');

                if (bracketIndex == -1 && dotIndex == -1)
                {
                    tokens.Add(current);
                    break;
                }

                if (bracketIndex != -1 && (dotIndex == -1 || bracketIndex < dotIndex))
                {
                    if (bracketIndex > 0)
                    {
                        tokens.Add(current.Substring(0, bracketIndex));
                    }

                    var endBracket = current.IndexOf(']');
                    if (endBracket > bracketIndex)
                    {
                        tokens.Add(current.Substring(bracketIndex, endBracket - bracketIndex + 1));
                        current = current.Substring(endBracket + 1).TrimStart('.');
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    tokens.Add(current.Substring(0, dotIndex));
                    current = current.Substring(dotIndex + 1);
                }
            }

            return tokens;
        }

        private void ExtractValues(JsonElement element, List<string> tokens, int tokenIndex, List<string> results)
        {
            if (tokenIndex >= tokens.Count)
            {
                // Reached the end, extract value
                var value = element.ValueKind switch
                {
                    JsonValueKind.String => element.GetString(),
                    JsonValueKind.Number => element.GetRawText(),
                    JsonValueKind.True => "true",
                    JsonValueKind.False => "false",
                    _ => element.GetRawText(),
                };

                if (!string.IsNullOrEmpty(value))
                {
                    results.Add(value);
                }

                return;
            }

            var token = tokens[tokenIndex];

            if (token.StartsWith("["))
            {
                // Array access
                var indexStr = token.Trim('[', ']');

                if (indexStr == "*")
                {
                    // All elements
                    if (element.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in element.EnumerateArray())
                        {
                            ExtractValues(item, tokens, tokenIndex + 1, results);
                        }
                    }
                }
                else if (int.TryParse(indexStr, out var index))
                {
                    // Specific index
                    if (element.ValueKind == JsonValueKind.Array && index < element.GetArrayLength())
                    {
                        ExtractValues(element[index], tokens, tokenIndex + 1, results);
                    }
                }
            }
            else
            {
                // Property access
                if (element.ValueKind == JsonValueKind.Object && element.TryGetProperty(token, out var property))
                {
                    ExtractValues(property, tokens, tokenIndex + 1, results);
                }
            }
        }
    }
}
