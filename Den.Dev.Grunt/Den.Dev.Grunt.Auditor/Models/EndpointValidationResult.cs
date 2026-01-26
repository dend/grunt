// <copyright file="EndpointValidationResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;

namespace Den.Dev.Grunt.Auditor.Models
{
    /// <summary>
    /// Result of validating a single endpoint.
    /// </summary>
    public class EndpointValidationResult
    {
        /// <summary>
        /// Gets or sets the endpoint identifier (e.g., "Stats_GetMatchStats").
        /// </summary>
        public string EndpointId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the C# model type name.
        /// </summary>
        public string ModelType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the validation status.
        /// </summary>
        public ValidationStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code from the API call.
        /// </summary>
        public int? HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets the list of discrepancies found.
        /// </summary>
        public List<FieldDiscrepancy> Discrepancies { get; set; } = new();

        /// <summary>
        /// Gets or sets the error message if the call failed.
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the reason if the endpoint was skipped.
        /// </summary>
        public string? SkipReason { get; set; }

        /// <summary>
        /// Gets or sets the raw JSON response (for snapshot updates).
        /// </summary>
        public string? RawJson { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the validation.
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the full URL of the HTTP request.
        /// </summary>
        public string? RequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method used for the request.
        /// </summary>
        public string? RequestMethod { get; set; }

        /// <summary>
        /// Gets or sets the HTTP headers sent with the request.
        /// </summary>
        public Dictionary<string, string>? RequestHeaders { get; set; }
    }
}

