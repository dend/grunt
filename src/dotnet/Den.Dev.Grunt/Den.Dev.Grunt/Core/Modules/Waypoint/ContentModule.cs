// <copyright file="ContentModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.Waypoint;

namespace Den.Dev.Grunt.Core.Modules.Waypoint
{
    /// <summary>
    /// Module for Halo Waypoint article and content APIs.
    /// </summary>
    public class ContentModule : WaypointModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal ContentModule(ClientBase client)
            : base(client, WaypointEndpoints.WPContentEndpoint)
        {
        }

        /// <summary>
        /// Gets the list of articles published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <param name="language">Article language. Example value is "en".</param>
        /// <param name="offset">Offset (number of articles to skip) from which to start the query.</param>
        /// <param name="count">Number of articles to retrieve.</param>
        /// <param name="order">Order in which articles are returned. Example values are "asc" or "desc".</param>
        /// <param name="categories">List of categories for which to return the articles.</param>
        /// <returns>If successful, returns the list of articles, each represented as <see cref="Article"/>. Otherwise, returns the details about the error.</returns>
        public async Task<HaloApiResultContainer<List<Article>, RawResponseContainer>> GetArticles(
            string language = "",
            int offset = -1,
            int count = -1,
            string order = "",
            List<int>? categories = null)
        {
            string urlBase = "/articles?";

            if (!string.IsNullOrWhiteSpace(language))
            {
                urlBase += $"lang={language}&";
            }

            if (offset > 0)
            {
                urlBase += $"offset={offset}&";
            }

            if (count > 0)
            {
                urlBase += $"count={count}&";
            }

            if (!string.IsNullOrWhiteSpace(order))
            {
                urlBase += $"order={order}&";
            }

            if (categories != null && categories.Count > 0)
            {
                urlBase += $"categories={string.Join(",", categories)}&";
            }

            return await this.GetAsync<List<Article>>(urlBase, useSpartanToken: false);
        }

        /// <summary>
        /// Gets a single article published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <param name="slug">Slug associated with the article. Example value is "halo-waypoint-content-browser".</param>
        /// <returns>If successful, returns an instance of <see cref="Article"/>. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<Article, RawResponseContainer>> GetArticle(string slug)
        {
            return await this.GetAsync<Article>($"/articles/{slug}", useSpartanToken: false);
        }

        /// <summary>
        /// Gets a list of article categories that are available on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <param name="language">Language in which the categories should be displayed. Example value is "en".</param>
        /// <returns>If successful, returns a list of <see cref="ArticleCategory"/> containing publishing categories. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<List<ArticleCategory>, RawResponseContainer>> GetArticleCategories(string language = "")
        {
            string path = "/taxonomy/article_category";
            if (!string.IsNullOrEmpty(language))
            {
                path += $"?lang={language}";
            }

            return await this.GetAsync<List<ArticleCategory>>(path, useSpartanToken: false);
        }

        /// <summary>
        /// Gets the details on a single article category published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <remarks>
        /// If you specify a category that does not exist, the response will be a HTTP 200 OK but with a `null` body.
        /// </remarks>
        /// <param name="id">ID of the category. Must be an integer.</param>
        /// <param name="language">Language in which the category should be displayed. Example value is "en".</param>
        /// <returns>If successful, returns an instance of <see cref="ArticleCategory"/> containing category information. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<ArticleCategory, RawResponseContainer>> GetArticleCategory(int id, string language = "")
        {
            string path = $"/taxonomy/article_category/{id}";
            if (!string.IsNullOrEmpty(language))
            {
                path += $"?lang={language}";
            }

            return await this.GetAsync<ArticleCategory>(path, useSpartanToken: false);
        }

        /// <summary>
        /// Gets <see href="https://www.halowaypoint.com/">Halo Waypoint</see> service award details.
        /// </summary>
        /// <param name="slug">Service award slug.</param>
        /// <returns>If successful, returns an instance of <see cref="ServiceAward"/>. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<ServiceAward, RawResponseContainer>> GetServiceAward(string slug)
        {
            return await this.GetAsync<ServiceAward>($"/service-awards/{slug}", useSpartanToken: false);
        }
    }
}
