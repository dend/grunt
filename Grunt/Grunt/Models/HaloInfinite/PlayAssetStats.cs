// <copyright file="PlayAssetStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Class containing information about asset statistics.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayAssetStats
    {
        /// <summary>
        /// Gets or sets the number of recent plays for an asset.
        /// </summary>
        public long? PlaysRecent { get; set; }

        /// <summary>
        /// Gets or sets the number of all-time plays for an asset.
        /// </summary>
        public long? PlaysAllTime { get; set; }

        /// <summary>
        /// Gets or sets the number of times an asset has been favorited.
        /// </summary>
        public long? Favorites { get; set; }

        /// <summary>
        /// Gets or sets the number of times an asset has been liked.
        /// </summary>
        public long? Likes { get; set; }

        /// <summary>
        /// Gets or sets the number of times an asset has been bookmarked.
        /// </summary>
        public long? Bookmarks { get; set; }

        /// <summary>
        /// Gets or sets the number of assets that are parented from the current asset.
        /// </summary>
        public long? ParentAssetCount { get; set; }

        /// <summary>
        /// Gets or sets the average rating for an asset.
        /// </summary>
        public float? AverageRating { get; set; }

        /// <summary>
        /// Gets or sets the number of ratings for an asset.
        /// </summary>
        public long? NumberOfRatings { get; set; }
    }
}
