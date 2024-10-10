// <copyright file="News.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// In-game news.
    /// </summary>
    [IsAutomaticallySerializable]
    public class News
    {
        /// <summary>
        /// Gets or sets the list of in-game news articles.
        /// </summary>
        public List<NewsArticle>? NewsArticles { get; set; }
    }
}
