// <copyright file="AuthoringAssetStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Stats associated with an authoring asset.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringAssetStats
    {
        /// <summary>
        /// Gets or sets the number of favorites for an authoring asset.
        /// </summary>
        public int Favorites { get; set; }

        /// <summary>
        /// Gets or sets the number of film bookmarks for an authoring asset.
        /// </summary>
        public int FilmBookmarks { get; set; }

        /// <summary>
        /// Gets or sets the number of likes for an authoring asset.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Gets or sets the ratings for an authoring asset.
        /// </summary>
        public AuthoringAssetRatings? Ratings { get; set; }

        /// <summary>
        /// Gets or sets the number of assets parented to the current authoring asset.
        /// </summary>
        public int ParentAssetCount { get; set; }

        /// <summary>
        /// Gets or sets the last modified date for the authoring asset.
        /// </summary>
        public APIFormattedDate? LastModifiedDateUtc { get; set; }

        /// <summary>
        /// Gets or sets whether the authoring assets should ingore incoming reports.
        /// </summary>
        public bool? IgnoreReports { get; set; }
    }
}
