// <copyright file="FieldDiscrepancy.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Auditor.Models
{
    /// <summary>
    /// Represents a discrepancy found during response validation.
    /// </summary>
    public class FieldDiscrepancy
    {
        /// <summary>
        /// Gets or sets the JSON path to the discrepancy (e.g., "$.Players[0].Score").
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of discrepancy.
        /// </summary>
        public DiscrepancyType Type { get; set; }

        /// <summary>
        /// Gets or sets the JSON type that was found (if applicable).
        /// </summary>
        public string? JsonType { get; set; }

        /// <summary>
        /// Gets or sets the expected C# type (if applicable).
        /// </summary>
        public string? ExpectedType { get; set; }

        /// <summary>
        /// Gets or sets a human-readable message describing the discrepancy.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the actual value found (for debugging, may be truncated).
        /// </summary>
        public string? ActualValue { get; set; }
    }
}
