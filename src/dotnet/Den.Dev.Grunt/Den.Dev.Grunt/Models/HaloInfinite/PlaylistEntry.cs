// <copyright file="PlaylistEntry.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration entry into a game playlist.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlaylistEntry
    {
        /// <summary>
        /// Gets or sets the asset ID for the map-mode pair for a playlist.
        /// </summary>
        public string? MapModePairAssetId { get; set; }

        /// <summary>
        /// Gets or sets the map-mode pair metadata for a playlist.
        /// </summary>
        public PlaylistMapModePairMetadata? Metadata { get; set; }
    }
}
