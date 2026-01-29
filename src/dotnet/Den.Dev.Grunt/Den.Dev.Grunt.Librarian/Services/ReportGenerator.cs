// <copyright file="ReportGenerator.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Den.Dev.Grunt.Librarian.Models;

namespace Den.Dev.Grunt.Librarian.Services
{
    /// <summary>
    /// Service for generating formatted coverage reports.
    /// </summary>
    public class ReportGenerator
    {
        /// <summary>
        /// Generates a formatted report string from the coverage report.
        /// </summary>
        /// <param name="report">The coverage report to format.</param>
        /// <param name="includeImplementationAnalysis">Whether to include implementation analysis section.</param>
        /// <returns>A formatted report string.</returns>
        public string GenerateReport(CoverageReport report, bool includeImplementationAnalysis)
        {
            var sb = new StringBuilder();

            sb.AppendLine("=== Halo API Coverage Analysis ===");
            sb.AppendLine();

            // Summary statistics
            sb.AppendLine($"Total endpoints: {report.TotalEndpoints}");
            sb.AppendLine($"With response type mappings: {report.WithResponseTypes} ({report.ResponseTypeCoveragePercent:F1}%)");

            if (includeImplementationAnalysis)
            {
                sb.AppendLine($"Implemented in modules: {report.ImplementedInModules} ({report.ImplementationCoveragePercent:F1}%)");
            }

            sb.AppendLine();

            // Missing response types section
            if (report.MissingResponseTypes.Count > 0)
            {
                sb.AppendLine($"=== Missing Response Type Mappings ({report.MissingResponseTypes.Count}) ===");
                AppendGapsGroupedByModule(sb, report.MissingResponseTypes);
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("=== All endpoints have response type mappings ===");
                sb.AppendLine();
            }

            // Missing implementations section
            if (includeImplementationAnalysis)
            {
                if (report.MissingImplementations.Count > 0)
                {
                    sb.AppendLine($"=== Not Implemented in Modules ({report.MissingImplementations.Count}) ===");
                    AppendGapsGroupedByModule(sb, report.MissingImplementations);
                    sb.AppendLine();
                }
                else
                {
                    sb.AppendLine("=== All endpoints are implemented in modules ===");
                    sb.AppendLine();
                }
            }

            // Detailed summary
            if (includeImplementationAnalysis)
            {
                sb.AppendLine("=== Summary ===");

                int fullyCovered = report.TotalEndpoints - report.MissingResponseTypes.Count;
                int missingTypeOnly = 0;
                int missingImplOnly = 0;
                int missingBoth = 0;

                var missingTypeIds = new HashSet<string>(report.MissingResponseTypes.Select(g => g.EndpointId));
                var missingImplIds = new HashSet<string>(report.MissingImplementations.Select(g => g.EndpointId));

                foreach (var id in missingTypeIds)
                {
                    if (missingImplIds.Contains(id))
                    {
                        missingBoth++;
                    }
                    else
                    {
                        missingTypeOnly++;
                    }
                }

                foreach (var id in missingImplIds)
                {
                    if (!missingTypeIds.Contains(id))
                    {
                        missingImplOnly++;
                    }
                }

                fullyCovered = report.TotalEndpoints - missingTypeOnly - missingImplOnly - missingBoth;

                sb.AppendLine($"Fully covered (type + implementation): {fullyCovered}");
                sb.AppendLine($"Missing response type only: {missingTypeOnly}");
                sb.AppendLine($"Missing implementation only: {missingImplOnly}");
                sb.AppendLine($"Missing both: {missingBoth}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Writes the report to a file.
        /// </summary>
        /// <param name="report">The coverage report.</param>
        /// <param name="outputPath">The output file path.</param>
        /// <param name="includeImplementationAnalysis">Whether to include implementation analysis.</param>
        public void WriteToFile(CoverageReport report, string outputPath, bool includeImplementationAnalysis)
        {
            var content = GenerateReport(report, includeImplementationAnalysis);
            var directory = Path.GetDirectoryName(outputPath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(outputPath, content);
        }

        /// <summary>
        /// Outputs the report to the console.
        /// </summary>
        /// <param name="report">The coverage report.</param>
        /// <param name="includeImplementationAnalysis">Whether to include implementation analysis.</param>
        public void WriteToConsole(CoverageReport report, bool includeImplementationAnalysis)
        {
            var content = GenerateReport(report, includeImplementationAnalysis);
            Console.WriteLine(content);
        }

        /// <summary>
        /// Appends gaps grouped by module name to the string builder.
        /// </summary>
        private static void AppendGapsGroupedByModule(StringBuilder sb, List<EndpointGap> gaps)
        {
            var groupedByModule = gaps
                .GroupBy(g => g.ModuleName)
                .OrderBy(g => g.Key);

            foreach (var moduleGroup in groupedByModule)
            {
                sb.AppendLine($"Module: {moduleGroup.Key}");

                foreach (var gap in moduleGroup.OrderBy(g => g.MethodName))
                {
                    sb.AppendLine($"  - {gap.EndpointId}");
                }
            }
        }
    }
}
