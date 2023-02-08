// <copyright file="AuthoringAssetRating.cs" company="Den Delimarsky">
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
    /// Container for authoring asset ratings.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringAssetRating : AssetBase
    {
        /// <summary>
        /// Gets or sets the links associated with an authored asset rating.
        /// </summary>
        /// <remarks>
        /// Additional research is needed to figure out the underlying data model.
        /// </remarks>
        public dynamic? Links { get; set; }

        /// <summary>
        /// Gets or sets authoring asset rating custom data.
        /// </summary>
        public AuthoringAssetRatingCustomData? CustomData { get; set; }

        /// <summary>
        /// Gets or sets the list of version ratings for an authoring asset.
        /// </summary>
        public List<VersionRating>? VersionRatings { get; set; }
    }
}
