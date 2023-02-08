// <copyright file="Map.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
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
