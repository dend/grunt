// <copyright file="CoverageAnalyzer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using Den.Dev.Grunt.Librarian.Models;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service for analyzing API coverage gaps.
    /// </summary>
    public class CoverageAnalyzer
    {
        private readonly ResponseTypeResolver typeResolver;
        private readonly ModuleScanner moduleScanner;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoverageAnalyzer"/> class.
        /// </summary>
        /// <param name="typeResolver">The response type resolver to use.</param>
        public CoverageAnalyzer(ResponseTypeResolver typeResolver)
        {
            this.typeResolver = typeResolver ?? throw new ArgumentNullException(nameof(typeResolver));
            this.moduleScanner = new ModuleScanner();
        }

        /// <summary>
        /// Analyzes endpoint coverage and generates a gap report.
        /// </summary>
        /// <param name="endpoints">The list of endpoint metadata to analyze.</param>
        /// <param name="modulesPath">Optional path to scan for module implementations.</param>
        /// <returns>A coverage report detailing gaps in the API coverage.</returns>
        public CoverageReport Analyze(List<EndpointMetadata> endpoints, string? modulesPath = null)
        {
            var report = new CoverageReport
            {
                TotalEndpoints = endpoints.Count,
            };

            // Scan module implementations if path provided
            Dictionary<string, HashSet<string>>? scannedModules = null;
            if (!string.IsNullOrEmpty(modulesPath))
            {
                scannedModules = moduleScanner.ScanModules(modulesPath);
            }

            foreach (var endpoint in endpoints)
            {
                // Check Layer 1: Response type coverage
                bool hasResponseType = typeResolver.HasExplicitMapping(endpoint.EndpointId);
                if (hasResponseType)
                {
                    report.WithResponseTypes++;
                }
                else
                {
                    report.MissingResponseTypes.Add(new EndpointGap
                    {
                        EndpointId = endpoint.EndpointId,
                        ModuleName = endpoint.ModuleName,
                        MethodName = endpoint.MethodName,
                        Type = GapType.NoResponseType,
                    });
                }

                // Check Layer 2: Implementation coverage (if modules path provided)
                if (scannedModules != null)
                {
                    bool hasImplementation = ModuleScanner.HasImplementation(
                        scannedModules,
                        endpoint.ModuleName,
                        endpoint.MethodName);

                    if (hasImplementation)
                    {
                        report.ImplementedInModules++;
                    }
                    else
                    {
                        report.MissingImplementations.Add(new EndpointGap
                        {
                            EndpointId = endpoint.EndpointId,
                            ModuleName = endpoint.ModuleName,
                            MethodName = endpoint.MethodName,
                            Type = GapType.NoImplementation,
                        });
                    }
                }
            }

            return report;
        }
    }
}
