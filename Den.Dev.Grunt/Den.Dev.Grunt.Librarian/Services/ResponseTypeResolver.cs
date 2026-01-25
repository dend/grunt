// <copyright file="ResponseTypeResolver.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service for resolving response types for API endpoints.
    /// Uses a JSON mapping file for explicit mappings with fallback to "object".
    /// </summary>
    public class ResponseTypeResolver
    {
        private readonly Dictionary<string, string> typeMappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseTypeResolver"/> class.
        /// </summary>
        /// <param name="mappingFilePath">Path to the response-types.json file. Can be null to use empty mappings.</param>
        public ResponseTypeResolver(string? mappingFilePath = null)
        {
            this.typeMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (!string.IsNullOrEmpty(mappingFilePath) && File.Exists(mappingFilePath))
            {
                try
                {
                    var json = File.ReadAllText(mappingFilePath);
                    var mappings = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                    if (mappings != null)
                    {
                        foreach (var kvp in mappings)
                        {
                            this.typeMappings[kvp.Key] = kvp.Value;
                        }
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Warning: Failed to parse response types mapping file: {ex.Message}");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Warning: Failed to read response types mapping file: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Resolves the response type for a given endpoint ID.
        /// </summary>
        /// <param name="endpointId">The endpoint identifier (e.g., "Economy_GetActiveBoosts").</param>
        /// <returns>The response type name, or "object" if no mapping exists.</returns>
        public string ResolveType(string endpointId)
        {
            if (string.IsNullOrEmpty(endpointId))
            {
                return "object";
            }

            return this.typeMappings.TryGetValue(endpointId, out var typeName)
                ? typeName
                : "object";
        }

        /// <summary>
        /// Gets whether an explicit mapping exists for the given endpoint ID.
        /// </summary>
        /// <param name="endpointId">The endpoint identifier.</param>
        /// <returns>True if an explicit mapping exists.</returns>
        public bool HasExplicitMapping(string endpointId)
        {
            return !string.IsNullOrEmpty(endpointId) && this.typeMappings.ContainsKey(endpointId);
        }

        /// <summary>
        /// Gets the count of explicit type mappings loaded.
        /// </summary>
        public int MappingCount => this.typeMappings.Count;
    }
}
