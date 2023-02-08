// <copyright file="PlaylistCsrResults.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using Den.Dev.Orion.Models.HaloInfinite.Foundation;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Playlist CSR snapshot.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlaylistCsrResults : ResultContainerBase
    {
        /// <summary>
        /// Gets or sets the playlist CSR data container.
        /// </summary>
        public PlaylistCsrContainer? Result { get; set; }
    }
}
