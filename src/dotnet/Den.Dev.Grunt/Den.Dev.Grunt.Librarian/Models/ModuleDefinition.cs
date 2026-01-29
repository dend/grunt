// <copyright file="ModuleDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Librarian.Models
{
    /// <summary>
    /// Represents a module definition containing grouped methods for template rendering.
    /// </summary>
    public class ModuleDefinition
    {
        /// <summary>
        /// Gets or sets the module name (e.g., "Economy", "GameCms").
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the origin constant name from HaloCoreEndpoints (e.g., "EconomyOrigin").
        /// </summary>
        public string Origin { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of methods belonging to this module.
        /// </summary>
        public List<MethodDefinition> Methods { get; set; } = new();

        /// <summary>
        /// Gets the generated file name for this module.
        /// </summary>
        public string FileName => $"{Name}Module.Generated.cs";
    }
}
