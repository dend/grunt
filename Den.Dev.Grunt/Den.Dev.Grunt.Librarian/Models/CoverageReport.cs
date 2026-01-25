// <copyright file="CoverageReport.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Librarian.Models
{
    /// <summary>
    /// Represents a coverage analysis report for the Halo API.
    /// </summary>
    public class CoverageReport
    {
        /// <summary>
        /// Gets or sets the total number of endpoints in the API.
        /// </summary>
        public int TotalEndpoints { get; set; }

        /// <summary>
        /// Gets or sets the number of endpoints with response type mappings.
        /// </summary>
        public int WithResponseTypes { get; set; }

        /// <summary>
        /// Gets or sets the number of endpoints implemented in module files.
        /// </summary>
        public int ImplementedInModules { get; set; }

        /// <summary>
        /// Gets or sets the list of endpoints missing response type mappings.
        /// </summary>
        public List<EndpointGap> MissingResponseTypes { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of endpoints missing implementations.
        /// </summary>
        public List<EndpointGap> MissingImplementations { get; set; } = new();

        /// <summary>
        /// Gets the percentage of endpoints with response type coverage.
        /// </summary>
        public double ResponseTypeCoveragePercent =>
            TotalEndpoints > 0 ? (double)WithResponseTypes / TotalEndpoints * 100 : 0;

        /// <summary>
        /// Gets the percentage of endpoints with implementation coverage.
        /// </summary>
        public double ImplementationCoveragePercent =>
            TotalEndpoints > 0 ? (double)ImplementedInModules / TotalEndpoints * 100 : 0;
    }

    /// <summary>
    /// Represents a gap in API coverage.
    /// </summary>
    public class EndpointGap
    {
        /// <summary>
        /// Gets or sets the endpoint identifier.
        /// </summary>
        public string EndpointId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the module name this endpoint belongs to.
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the expected method name for this endpoint.
        /// </summary>
        public string MethodName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of gap.
        /// </summary>
        public GapType Type { get; set; }
    }

    /// <summary>
    /// Represents the type of coverage gap.
    /// </summary>
    public enum GapType
    {
        /// <summary>
        /// Endpoint has no response type mapping in response-types.json.
        /// </summary>
        NoResponseType,

        /// <summary>
        /// Endpoint has no implementation in module source files.
        /// </summary>
        NoImplementation,
    }
}
