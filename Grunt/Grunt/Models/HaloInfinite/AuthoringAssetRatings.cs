// <copyright file="AuthoringAssetRatings.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for authoring asset ratings.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringAssetRatings
    {
        /// <summary>
        /// Gets or sets the average rating for an authoring asset.
        /// </summary>
        public float AverageRating { get; set; }

        /// <summary>
        /// Gets or sets the number of ratings for an authoring asset.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the collection of ratings.
        /// </summary>
        /// <remarks>
        /// Additional research is needed to figure out the underlying data model.
        /// </remarks>
        public List<dynamic>? Ratings { get; set; }
    }
}
