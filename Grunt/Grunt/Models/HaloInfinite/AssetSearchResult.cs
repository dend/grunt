// <copyright file="AssetSearchResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for an asset search result.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AssetSearchResult : AssetBase
    {
        /// <summary>
        /// Gets or sets the asset thumbnail URL.
        /// </summary>
        public string? ThumbnailUrl { get; set; }

        /// <summary>
        /// Gets or sets the list of referenced assets.
        /// </summary>
        public List<string>? ReferencedAssets { get; set; }

        /// <summary>
        /// Gets or sets the asset original author.
        /// </summary>
        public string? OriginalAuthor { get; set; }

        /// <summary>
        /// Gets or sets the number of likes for an asset.
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Gets or sets the number of bookmarks for an asset.
        /// </summary>
        public int Bookmarks { get; set; }

        /// <summary>
        /// Gets or sets the number of recent plays for an asset.
        /// </summary>
        public int PlaysRecent { get; set; }

        /// <summary>
        /// Gets or sets the number of objects associated with an asset.
        /// </summary>
        public int NumberOfObjects { get; set; }

        /// <summary>
        /// Gets or sets the asset creation date.
        /// </summary>
        public APIFormattedDate? DateCreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the asset modification date.
        /// </summary>
        public APIFormattedDate? DateModifiedUtc { get; set; }

        /// <summary>
        /// Gets or sets the asset publication date.
        /// </summary>
        public APIFormattedDate? DatePublishedUtc { get; set; }

        /// <summary>
        /// Gets or sets whether an asset has a node graph.
        /// </summary>
        public bool? HasNodeGraph { get; set; }

        /// <summary>
        /// Gets or sets whether asset clones are read-only.
        /// </summary>
        public bool? ReadOnlyClones { get; set; }

        /// <summary>
        /// Gets or sets the number of all-time plays for an asset.
        /// </summary>
        public int PlaysAllTime { get; set; }

        /// <summary>
        /// Gets or sets the number of assets parented to the current asset.
        /// </summary>
        public int ParentAssetCount { get; set; }

        /// <summary>
        /// Gets or sets the average asset rating.
        /// </summary>
        public float AverageRating { get; set; }

        /// <summary>
        /// Gets or sets the number of ratings.
        /// </summary>
        public int NumberOfRatings { get; set; }

        /// <summary>
        /// Gets or sets the published asset version.
        /// </summary>
        public Guid? PublishedVersion { get; set; }

        /// <summary>
        /// Gets or sets whether the asset is banned.
        /// </summary>
        public bool? IsBanned { get; set; }

        /// <summary>
        /// Gets or sets the number of times the asset has been favorited.
        /// </summary>
        public int FavoritesCount { get; set; }

        /// <summary>
        /// Gets or sets the asset rating score.
        /// </summary>
        public float RatingScore { get; set; }

        /// <summary>
        /// Gets or sets the number of ratings the asset has received.
        /// </summary>
        public int RatingCount { get; set; }

        /// <summary>
        /// Gets or sets the asset state.
        /// </summary>
        public int AssetState { get; set; }

        /// <summary>
        /// Gets or sets the date the asset was created on.
        /// </summary>
        public APIFormattedDate? AssetCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the date the asset was modified on.
        /// </summary>
        public APIFormattedDate? AssetLastModified { get; set; }
    }
}
