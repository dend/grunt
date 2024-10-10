// <copyright file="Manifest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using Den.Dev.Orion.Models.HaloInfinite.Foundation;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Class representing a game configuration manifest.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Manifest : AssetBase
    {
        /// <summary>
        /// Gets or sets custom data associated with a manifest.
        /// </summary>
        public ManifestCustomData? CustomData { get; set; }

        /// <summary>
        /// Gets or sets a list of maps available for the game through the selected manifest.
        /// </summary>
        public List<AssetLink>? MapLinks { get; set; }

        /// <summary>
        /// Gets or sets a list of game variants available for the game through the selected manifest.
        /// </summary>
        public List<AssetLink>? UgcGameVariantLinks { get; set; }

        /// <summary>
        /// Gets or sets a list of playlists available for the game through the selected manifest.
        /// </summary>
        public List<AssetLink>? PlaylistLinks { get; set; }

        /// <summary>
        /// Gets or sets a list of engine game variants available for the game through the selected manifest.
        /// </summary>
        public List<AssetLink>? EngineGameVariantLinks { get; set; }
    }
}
