// <copyright file="FavoriteAsset.cs" company="Den Delimarsky">
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
    /// Asset favorited by the player.
    /// </summary>
    [IsAutomaticallySerializable]
    public class FavoriteAsset : AssetBase
    {
        /// <summary>
        /// Gets or sets links associated with the asset.
        /// </summary>
        public dynamic? Links { get; set; }

        /// <summary>
        /// Gets or sets custom data associated with the asset.
        /// </summary>
        public dynamic? CustomData { get; set; }

        /// <summary>
        /// Gets or sets a list of version ratings associated with the asset.
        /// </summary>
        public List<VersionRating>? VersionRatings { get; set; }
    }
}
