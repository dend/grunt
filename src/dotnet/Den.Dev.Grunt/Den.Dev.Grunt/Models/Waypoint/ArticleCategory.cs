// <copyright file="ArticleCategory.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.Waypoint
{
    /// <summary>
    /// Categories for articles published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ArticleCategory
    {
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the category name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the category description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the category slug.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Gets or sets the count of articles in a given category.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the parent category ID.
        /// </summary>
        /// <remarks>
        /// The category ID is 0 for categories that are top-level (have no parent/are the parent themselves).
        /// </remarks>
        public int Parent { get; set; }
    }
}
