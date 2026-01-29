// <copyright file="SearchableData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Searchable metadata for an asset.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SearchableData
    {
        /// <summary>
        /// Gets or sets the public name.
        /// </summary>
        public string? PublicName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the list of tags.
        /// </summary>
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the asset has a node graph.
        /// </summary>
        public bool HasNodeGraph { get; set; }

        /// <summary>
        /// Gets or sets the clone behavior.
        /// </summary>
        public int CloneBehavior { get; set; }

        /// <summary>
        /// Gets or sets the published version ID.
        /// </summary>
        public string? PublishedVersion { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the asset is banned.
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// Gets or sets the favorites count.
        /// </summary>
        public int FavoritesCount { get; set; }

        /// <summary>
        /// Gets or sets the rating score.
        /// </summary>
        public double RatingScore { get; set; }

        /// <summary>
        /// Gets or sets the rating count.
        /// </summary>
        public int RatingCount { get; set; }

        /// <summary>
        /// Gets or sets the asset state.
        /// </summary>
        public int AssetState { get; set; }

        /// <summary>
        /// Gets or sets the asset creation date.
        /// </summary>
        public APIFormattedDate? AssetCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the asset last modified date.
        /// </summary>
        public APIFormattedDate? AssetLastModified { get; set; }
    }
}
