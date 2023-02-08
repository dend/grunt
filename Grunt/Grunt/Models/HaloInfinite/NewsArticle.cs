// <copyright file="NewsArticle.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// In-game news article.
    /// </summary>
    [IsAutomaticallySerializable]
    public class NewsArticle
    {
        /// <summary>
        /// Gets or sets the short headline. Includes translation strings.
        /// </summary>
        public DisplayString? ShortHeadline { get; set; }

        /// <summary>
        /// Gets or sets the full headline. Includes translation strings.
        /// </summary>
        public DisplayString? FullHeadline { get; set; }

        /// <summary>
        /// Gets or sets the news body. Inscludes translated strings.
        /// </summary>
        public DisplayString? Body { get; set; }

        /// <summary>
        /// Gets or sets the article image.
        /// </summary>
        public ArticleImage? ArticleImage { get; set; }

        /// <summary>
        /// Gets or sets the list of available article actions that the player can take from within the game.
        /// </summary>
        public List<ArticleAction>? ArticleActions { get; set; }
    }
}
