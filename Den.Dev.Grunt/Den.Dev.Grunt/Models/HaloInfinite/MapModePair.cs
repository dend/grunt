// <copyright file="MapModePair.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Grunt.Models.HaloInfinite.Foundation;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Halo Infinite map-mode pairing configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MapModePair : AssetBase
    {
        /// <summary>
        /// Gets or sets custom data associated with the map-mode pair.
        /// </summary>
        public dynamic? CustomData { get; set; }

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        public Map? MapLink { get; set; }

        /// <summary>
        /// Gets or sets the game variant.
        /// </summary>
        public UGCGameVariant? UgcGameVariantLink { get; set; }
    }
}
