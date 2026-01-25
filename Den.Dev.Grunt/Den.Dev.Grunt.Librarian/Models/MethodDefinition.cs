// <copyright file="MethodDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace Den.Dev.Grunt.Librarian.Models
{
    /// <summary>
    /// Represents a method definition ready for template rendering.
    /// </summary>
    public class MethodDefinition
    {
        /// <summary>
        /// Gets or sets the endpoint ID for reference.
        /// </summary>
        public string EndpointId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the method name (e.g., "GetActiveBoosts").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the HTTP method name in PascalCase (e.g., "Get", "Post").
        /// </summary>
        public string HttpMethod { get; set; } = "Get";

        /// <summary>
        /// Gets or sets the URL template with interpolation syntax (e.g., "/hi/players/{player}/boosts").
        /// </summary>
        public string UrlTemplate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the response type name.
        /// </summary>
        public string ResponseType { get; set; } = "object";

        /// <summary>
        /// Gets or sets the list of method parameters.
        /// </summary>
        public List<ParameterInfo> Parameters { get; set; } = new();

        /// <summary>
        /// Gets or sets a value indicating whether to include useClearance: true.
        /// </summary>
        public bool UseClearance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include useSpartanToken: false.
        /// </summary>
        public bool UseSpartanToken { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether this method needs manual review for HTTP method.
        /// </summary>
        public bool NeedsReview { get; set; }

        /// <summary>
        /// Gets the parameter signature for the method declaration.
        /// </summary>
        public string ParameterSignature
        {
            get
            {
                if (Parameters.Count == 0)
                {
                    return string.Empty;
                }

                return string.Join(", ", Parameters.Select(p => $"{p.Type} {p.Name}"));
            }
        }
    }
}
