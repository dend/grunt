// <copyright file="AssetKind.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Types of assets available through the User-Generated Content discovery API.
    /// </summary>
    public enum AssetKind
    {
        /// <summary>
        /// Film asset.
        /// </summary>
        Film = 1,

        /// <summary>
        /// Map asset.
        /// </summary>
        Map = 2,

        /// <summary>
        /// Playlist asset.
        /// </summary>
        Playlist = 3,

        /// <summary>
        /// Prefab asset.
        /// </summary>
        Prefab = 4,

        /// <summary>
        /// Test asset.
        /// </summary>
        TestAsset,

        /// <summary>
        /// Game variant asset.
        /// </summary>
        UgcGameVariant = 6,

        /// <summary>
        /// Map mode pair asset.
        /// </summary>
        MapModePair = 7,

        /// <summary>
        /// Project asset.
        /// </summary>
        Project = 8,

        /// <summary>
        /// Manifest asset.
        /// </summary>
        Manifest,

        /// <summary>
        /// Engine game variant asset.
        /// </summary>
        EngineGameVariant,
    }
}
