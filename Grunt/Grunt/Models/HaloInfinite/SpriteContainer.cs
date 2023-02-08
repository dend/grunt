// <copyright file="SpriteContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for sprite information. Primarily used for medals.
    /// </summary>
    public class SpriteContainer
    {
        /// <summary>
        /// Gets or sets the contents for the small sprite. Size is 72x72px.
        /// </summary>
        public Sprite? Small { get; set; }

        /// <summary>
        /// Gets or sets the contents for the medium sprite. Size is 128x128px.
        /// </summary>
        public Sprite? Medium { get; set; }

        /// <summary>
        /// Gets or sets the contents for the extra large sprite. Size is 256x256px.
        /// </summary>
        [JsonPropertyName("extra-large")]
        public Sprite? ExtraLarge { get; set; }
    }
}
