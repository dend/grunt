// <copyright file="ParameterInfo.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

namespace Den.Dev.Grunt.Librarian.Models
{
    /// <summary>
    /// Represents a parameter extracted from an API endpoint path or query string.
    /// </summary>
    public class ParameterInfo
    {
        /// <summary>
        /// Gets or sets the parameter name as it appears in the URL template (e.g., "player", "coreId").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the C# type for this parameter. Defaults to "string".
        /// </summary>
        public string Type { get; set; } = "string";

        /// <summary>
        /// Gets or sets the description for XML documentation.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this parameter is from the query string (vs path).
        /// </summary>
        public bool IsQueryParameter { get; set; }
    }
}
