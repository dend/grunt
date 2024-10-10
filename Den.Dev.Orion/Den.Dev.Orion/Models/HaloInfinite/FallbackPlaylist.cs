// <copyright file="FallbackPlaylist.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container class for a fallback playlist.
    /// </summary>
    [IsAutomaticallySerializable]
    public class FallbackPlaylist
    {
        /// <summary>
        /// Gets or sets the fallback playlist ID.
        /// </summary>
        public string? FallbackPlaylistId { get; set; }
    }
}
