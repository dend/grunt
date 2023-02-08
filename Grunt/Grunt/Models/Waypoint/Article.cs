// <copyright file="Article.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace OpenSpartan.Grunt.Models.Waypoint
{
    /// <summary>
    /// Article published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Article
    {
        /// <summary>
        /// Gets or sets the article ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the URI to the featured image.
        /// </summary>
        public string? FeaturedImageUri { get; set; }

        /// <summary>
        /// Gets or sets the featured image alternative text.
        /// </summary>
        public string? FeaturedImageAlt { get; set; }

        /// <summary>
        /// Gets or sets the article title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the article subtitle.
        /// </summary>
        public string? Subtitle { get; set; }

        /// <summary>
        /// Gets or sets the article content.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Gets or sets the article excerpt.
        /// </summary>
        public string? Excerpt { get; set; }

        /// <summary>
        /// Gets or sets the article slug.
        /// </summary>
        public string? Slug { get; set; }

        /// <summary>
        /// Gets or sets the article creator slug.
        /// </summary>
        public string? CreatorSlug { get; set; }

        /// <summary>
        /// Gets or sets the article creator title.
        /// </summary>
        public string? CreatorTitle { get; set; }

        /// <summary>
        /// Gets or sets the categories associated with the article.
        /// </summary>
        public List<int>? Categories { get; set; }

        /// <summary>
        /// Gets or sets the tags associated with the article.
        /// </summary>
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Gets or sets article blocks that represent individual paragraphs.
        /// </summary>
        public List<ArticleBlock>? Blocks { get; set; }

        /// <summary>
        /// Gets or sets the article publish date and time.
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the medium-sized featured image.
        /// </summary>
        public string? FeaturedImageUriMedium { get; set; }

        /// <summary>
        /// Gets or sets the medium-sized featured image alternative text.
        /// </summary>
        public string? FeaturedImageAltMedium { get; set; }

        /// <summary>
        /// Gets or sets the msmall-sized featured image.
        /// </summary>
        public string? FeaturedImageUriSmall { get; set; }

        /// <summary>
        /// Gets or sets the small-sized featured image alternative text.
        /// </summary>
        public string? FeaturedImageAltSmall { get; set; }
    }
}
