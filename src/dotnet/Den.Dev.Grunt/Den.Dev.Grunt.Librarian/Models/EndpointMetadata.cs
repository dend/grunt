// <copyright file="EndpointMetadata.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System.Collections.Generic;
using System.Net.Http;

namespace Den.Dev.Grunt.Librarian.Models
{
    /// <summary>
    /// Represents parsed metadata for a single API endpoint.
    /// </summary>
    public class EndpointMetadata
    {
        /// <summary>
        /// Gets or sets the full endpoint identifier (e.g., "Economy_GetActiveBoosts").
        /// </summary>
        public string EndpointId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the module name derived from the endpoint prefix (e.g., "Economy").
        /// </summary>
        public string ModuleName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the method name derived from the endpoint suffix (e.g., "GetActiveBoosts").
        /// </summary>
        public string MethodName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL path template (e.g., "/hi/players/{player}/boosts").
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the query string portion (e.g., "?flight={flightId}").
        /// </summary>
        public string QueryString { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this endpoint requires clearance.
        /// </summary>
        public bool ClearanceAware { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this endpoint requires a Spartan token.
        /// </summary>
        public bool RequiresSpartanToken { get; set; }

        /// <summary>
        /// Gets or sets the authority ID from the endpoint configuration.
        /// </summary>
        public string AuthorityId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the inferred HTTP method for this endpoint.
        /// </summary>
        public HttpMethod InferredMethod { get; set; } = HttpMethod.Get;

        /// <summary>
        /// Gets or sets a value indicating whether HTTP method inference requires manual review.
        /// </summary>
        public bool NeedsMethodReview { get; set; }

        /// <summary>
        /// Gets or sets the list of parameters extracted from the path and query string.
        /// </summary>
        public List<ParameterInfo> Parameters { get; set; } = new();

        /// <summary>
        /// Gets or sets the response type name from mapping (or "object" as fallback).
        /// </summary>
        public string ResponseTypeName { get; set; } = "object";

        /// <summary>
        /// Gets the combined URL template including path and query string.
        /// </summary>
        public string FullUrlTemplate => string.IsNullOrEmpty(QueryString) ? Path : $"{Path}{QueryString}";
    }
}
