// <copyright file="AssetActionResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using OpenSpartan.Grunt.Models.ApiIngress;
using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Result representing an action on an asset.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AssetActionResult : AssetBase
    {
        /// <summary>
        /// Gets or sets the list of links referenced by the asset.
        /// </summary>
        public Dictionary<string, OnlineUriReference>? Links { get; set; }

        /// <summary>
        /// Gets or sets custom data associated with an asset.
        /// </summary>
        public dynamic? CustomData { get; set; }

        /// <summary>
        /// Gets or sets the version ratings for an asset.
        /// </summary>
        public dynamic? VersionRatings { get; set; }

        /// <summary>
        /// Gets or sets output searchable data for an asset.
        /// </summary>
        public AssetSearchResult? SearchableData { get; set; }
    }
}
