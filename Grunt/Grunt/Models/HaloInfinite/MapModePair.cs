// <copyright file="MapModePair.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
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
