// <copyright file="ModuleGrouper.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Den.Dev.Grunt.Librarian.Models;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service for grouping endpoints into modules based on their naming patterns.
    /// </summary>
    public static class ModuleGrouper
    {
        /// <summary>
        /// Mapping of endpoint prefixes to module names and their origin constants.
        /// </summary>
        private static readonly Dictionary<string, (string ModuleName, string Origin)> ModuleMappings = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Academy", ("Academy", "GameCmsOrigin") },
            { "BanProcessor", ("BanProcessor", "BanProcessorOrigin") },
            { "Crashes", ("Crashes", "CrashesOrigin") },
            { "Economy", ("Economy", "EconomyOrigin") },
            { "GameCms", ("GameCms", "GameCmsOrigin") },
            { "HIUGC_Authoring", ("UgcAuthoring", "AuthoringOrigin") },
            { "HIUGC_Discovery", ("UgcDiscovery", "DiscoveryOrigin") },
            { "HIUGC", ("Ugc", "AuthoringOrigin") },
            { "Lobby", ("Lobby", "HaloInfiniteLobbyOrigin") },
            { "Settings", ("Settings", "SettingsOrigin") },
            { "Skill", ("Skill", "SkillOrigin") },
            { "Stats", ("Stats", "StatsOrigin") },
            { "TextModeration", ("TextModeration", "TextOrigin") },
            { "Telemetry", ("Telemetry", "TelemetryOrigin") },
        };

        /// <summary>
        /// Gets the module name and origin for a given endpoint ID.
        /// </summary>
        /// <param name="endpointId">The full endpoint ID (e.g., "Economy_GetActiveBoosts").</param>
        /// <returns>A tuple containing the module name and origin constant name.</returns>
        public static (string ModuleName, string Origin) GetModuleInfo(string endpointId)
        {
            if (string.IsNullOrEmpty(endpointId))
            {
                return ("Unknown", "UnknownOrigin");
            }

            // Try matching the longest prefix first (for cases like HIUGC_Discovery vs HIUGC)
            var orderedMappings = ModuleMappings.OrderByDescending(m => m.Key.Length);

            foreach (var mapping in orderedMappings)
            {
                if (endpointId.StartsWith(mapping.Key, StringComparison.OrdinalIgnoreCase))
                {
                    return mapping.Value;
                }
            }

            // If no mapping found, extract module name from first part of endpoint ID
            var parts = endpointId.Split('_');
            if (parts.Length > 0)
            {
                var moduleName = parts[0];
                return (moduleName, $"{moduleName}Origin");
            }

            return ("Unknown", "UnknownOrigin");
        }

        /// <summary>
        /// Extracts the method name from an endpoint ID.
        /// </summary>
        /// <param name="endpointId">The full endpoint ID.</param>
        /// <returns>The method name portion.</returns>
        public static string GetMethodName(string endpointId)
        {
            if (string.IsNullOrEmpty(endpointId))
            {
                return "Unknown";
            }

            // Handle HIUGC_Discovery_ and HIUGC_Authoring_ patterns
            if (endpointId.StartsWith("HIUGC_Discovery_", StringComparison.OrdinalIgnoreCase))
            {
                return endpointId.Substring("HIUGC_Discovery_".Length);
            }

            if (endpointId.StartsWith("HIUGC_Authoring_", StringComparison.OrdinalIgnoreCase))
            {
                return endpointId.Substring("HIUGC_Authoring_".Length);
            }

            // Standard pattern: ModuleName_MethodName
            var underscoreIndex = endpointId.IndexOf('_');
            if (underscoreIndex >= 0 && underscoreIndex < endpointId.Length - 1)
            {
                return endpointId.Substring(underscoreIndex + 1);
            }

            return endpointId;
        }

        /// <summary>
        /// Groups a collection of endpoints into module definitions.
        /// </summary>
        /// <param name="endpoints">The endpoints to group.</param>
        /// <returns>A dictionary of module name to module definition.</returns>
        public static Dictionary<string, ModuleDefinition> GroupByModule(IEnumerable<EndpointMetadata> endpoints)
        {
            var modules = new Dictionary<string, ModuleDefinition>(StringComparer.OrdinalIgnoreCase);

            foreach (var endpoint in endpoints)
            {
                var (moduleName, origin) = GetModuleInfo(endpoint.EndpointId);

                if (!modules.TryGetValue(moduleName, out var module))
                {
                    module = new ModuleDefinition
                    {
                        Name = moduleName,
                        Origin = origin,
                    };
                    modules[moduleName] = module;
                }

                var method = new MethodDefinition
                {
                    EndpointId = endpoint.EndpointId,
                    Name = endpoint.MethodName,
                    HttpMethod = HttpMethodInferrer.GetMethodName(endpoint.InferredMethod),
                    UrlTemplate = endpoint.FullUrlTemplate,
                    ResponseType = endpoint.ResponseTypeName,
                    Parameters = endpoint.Parameters,
                    UseClearance = endpoint.ClearanceAware,
                    UseSpartanToken = endpoint.RequiresSpartanToken,
                    NeedsReview = endpoint.NeedsMethodReview,
                };

                module.Methods.Add(method);
            }

            // Sort methods within each module
            foreach (var module in modules.Values)
            {
                module.Methods = module.Methods.OrderBy(m => m.Name).ToList();
            }

            return modules;
        }
    }
}
