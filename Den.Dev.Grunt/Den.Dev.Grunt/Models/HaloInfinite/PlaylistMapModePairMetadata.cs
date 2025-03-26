// <copyright file="PlaylistMapModePairMetadata.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Metadata associated with a map-mode pair.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlaylistMapModePairMetadata
    {
        /// <summary>
        /// Gets or sets the weight for the map-mode pair in a playlist.
        /// </summary>
        public float Weight { get; set; }
    }
}
