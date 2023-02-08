// <copyright file="MedalMetadata.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Metadata for player-awarded medals.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MedalMetadata
    {
        /// <summary>
        /// Gets or sets the list of difficulties associated with medals.
        /// </summary>
        public List<string>? Difficulties { get; set; }

        /// <summary>
        /// Gets or sets the medal types.
        /// </summary>
        public List<string>? Types { get; set; }

        /// <summary>
        /// Gets or sets the medal sprite configuration.
        /// </summary>
        public SpriteContainer? Sprites { get; set; }

        /// <summary>
        /// Gets or sets the collection of medals.
        /// </summary>
        public List<Medal>? Medals { get; set; }
    }
}
