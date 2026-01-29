// <copyright file="EndpointTestConfig.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Den.Dev.Grunt.Auditor.Models
{
    /// <summary>
    /// Root configuration for endpoint testing.
    /// </summary>
    public class EndpointTestConfig
    {
        /// <summary>
        /// Gets or sets the discovery chain - endpoints called in order to discover parameters.
        /// </summary>
        [JsonPropertyName("discoveryChain")]
        public List<DiscoveryStep> DiscoveryChain { get; set; } = new();

        /// <summary>
        /// Gets or sets the validation targets - endpoints to validate.
        /// </summary>
        [JsonPropertyName("validationTargets")]
        public List<ValidationTarget> ValidationTargets { get; set; } = new();

        /// <summary>
        /// Gets or sets patterns for endpoints to skip.
        /// </summary>
        [JsonPropertyName("skipEndpoints")]
        public List<SkipPattern> SkipEndpoints { get; set; } = new();

        /// <summary>
        /// Gets or sets HTTP methods to skip (e.g., PUT, POST, DELETE, PATCH).
        /// Any endpoint with these methods will be automatically skipped.
        /// </summary>
        [JsonPropertyName("skipHttpMethods")]
        public List<string> SkipHttpMethods { get; set; } = new();
    }

    /// <summary>
    /// A step in the discovery chain that calls an endpoint to extract parameters.
    /// </summary>
    public class DiscoveryStep
    {
        /// <summary>
        /// Gets or sets the step number (for ordering).
        /// </summary>
        [JsonPropertyName("step")]
        public int Step { get; set; }

        /// <summary>
        /// Gets or sets the endpoint identifier.
        /// </summary>
        [JsonPropertyName("endpointId")]
        public string EndpointId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the module and method path (e.g., "Stats.GetMatchHistory").
        /// </summary>
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the arguments to pass to the method.
        /// Values starting with "$" are parameter references.
        /// </summary>
        [JsonPropertyName("args")]
        public Dictionary<string, object> Args { get; set; } = new();

        /// <summary>
        /// Gets or sets the extractors that pull values from the response.
        /// Key is the parameter name to store, value is a JSONPath expression.
        /// </summary>
        [JsonPropertyName("extractors")]
        public Dictionary<string, string> Extractors { get; set; } = new();
    }

    /// <summary>
    /// An endpoint to validate against its model.
    /// </summary>
    public class ValidationTarget
    {
        /// <summary>
        /// Gets or sets the endpoint identifier.
        /// </summary>
        [JsonPropertyName("endpointId")]
        public string EndpointId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the module and method path.
        /// </summary>
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the arguments to pass.
        /// </summary>
        [JsonPropertyName("args")]
        public Dictionary<string, object> Args { get; set; } = new();

        /// <summary>
        /// Gets or sets the expected model type name.
        /// </summary>
        [JsonPropertyName("expectedModel")]
        public string ExpectedModel { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the HTTP method used by this endpoint (GET, POST, PUT, DELETE, PATCH).
        /// Used to determine if the endpoint should be skipped based on skipHttpMethods config.
        /// </summary>
        [JsonPropertyName("httpMethod")]
        public string? HttpMethod { get; set; }

        /// <summary>
        /// Gets or sets whether to skip this endpoint.
        /// </summary>
        [JsonPropertyName("skip")]
        public bool Skip { get; set; }

        /// <summary>
        /// Gets or sets the reason for skipping.
        /// </summary>
        [JsonPropertyName("skipReason")]
        public string? SkipReason { get; set; }
    }

    /// <summary>
    /// Pattern for skipping endpoints.
    /// </summary>
    public class SkipPattern
    {
        /// <summary>
        /// Gets or sets the pattern to match (supports * wildcard).
        /// </summary>
        [JsonPropertyName("pattern")]
        public string Pattern { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reason for skipping.
        /// </summary>
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
    }
}
