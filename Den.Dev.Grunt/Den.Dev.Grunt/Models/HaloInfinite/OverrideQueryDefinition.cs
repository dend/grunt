// <copyright file="OverrideQueryDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for a graphics override query definition.
    /// </summary>
    [IsAutomaticallySerializable]
    public class OverrideQueryDefinition
    {
        /// <summary>
        /// Gets or sets the schema version.
        /// </summary>
        [JsonPropertyName("schema_version")]
        public int SchemaVersion { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the list of override queries.
        /// </summary>
        public List<OverrideQuery>? Overrides { get; set; }
    }
}
