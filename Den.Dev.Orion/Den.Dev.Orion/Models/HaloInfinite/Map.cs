// <copyright file="Map.cs" company="Den Delimarsky">
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
    /// Halo Infinite game map.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Map : AssetBase
    {
        /// <summary>
        /// Gets or sets custom data associated with a map.
        /// </summary>
        public CustomMapData? CustomData { get; set; }

        /// <summary>
        /// Gets or sets a list of links to prefabs associated with a map.
        /// </summary>
        public List<AssetLink>? PrefabLinks { get; set; }
    }
}
