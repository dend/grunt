// <copyright file="ValidationReport.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace Den.Dev.Grunt.Auditor.Models
{
    /// <summary>
    /// Complete validation report for all tested endpoints.
    /// </summary>
    public class ValidationReport
    {
        /// <summary>
        /// Gets or sets the timestamp when the report was generated.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the list of endpoint validation results.
        /// </summary>
        public List<EndpointValidationResult> Results { get; set; } = new();

        /// <summary>
        /// Gets the count of endpoints that passed validation.
        /// </summary>
        public int PassedCount => Results.Count(r => r.Status == ValidationStatus.Pass);

        /// <summary>
        /// Gets the count of endpoints with warnings.
        /// </summary>
        public int WarningCount => Results.Count(r => r.Status == ValidationStatus.Warning);

        /// <summary>
        /// Gets the count of endpoints that failed validation.
        /// </summary>
        public int FailedCount => Results.Count(r => r.Status == ValidationStatus.Fail);

        /// <summary>
        /// Gets the count of endpoints that were skipped.
        /// </summary>
        public int SkippedCount => Results.Count(r => r.Status == ValidationStatus.Skipped);

        /// <summary>
        /// Gets the count of endpoints that encountered errors.
        /// </summary>
        public int ErrorCount => Results.Count(r => r.Status == ValidationStatus.Error);

        /// <summary>
        /// Gets the total count of endpoints tested.
        /// </summary>
        public int TotalCount => Results.Count;
    }
}
