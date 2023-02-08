// <copyright file="WaypointClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OpenSpartan.Grunt.Core.Foundation;
using OpenSpartan.Grunt.Endpoints;
using OpenSpartan.Grunt.Models;
using OpenSpartan.Grunt.Models.Waypoint;
using OpenSpartan.Grunt.Util;

namespace OpenSpartan.Grunt.Core
{
    /// <summary>
    /// Client for interacting with the Halo Waypoint APIs.
    /// </summary>
    public class WaypointClient : ClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointClient"/> class, used to access the Halo Waypoint API.
        /// </summary>
        /// <param name="spartanToken">The Spartan token used to authenticate against the Halo Infinite API.</param>
        /// <param name="xuid">The player identifier in the format "xuid(XUID_VALUE)".</param>
        /// <param name="clearanceToken">ID of the flight/clearance currently active for the player. Optional when first instantiating the client.</param>
        public WaypointClient(string spartanToken, string xuid = "", string clearanceToken = "")
        {
            this.SpartanToken = spartanToken;
            this.Xuid = xuid;
            this.ClearanceToken = clearanceToken;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointClient"/> class, used to access the Halo Waypoint API.
        /// </summary>
        public WaypointClient()
        {
        }

        /// <summary>
        /// Redeems a Halo Waypoint code.
        /// </summary>
        /// <remarks>
        /// The codes redeemable here can be those that are obtained through Xbox Game Pass perks, but can also be outside the scope of that particular program.
        /// </remarks>
        /// <include file='../APIDocsExamples/Waypoint/RedeemCode.xml' path='//example'/>
        /// <param name="code">Code to be redeemed.</param>
        /// <returns>If call is successful, returns an instance of <see cref="CodeRedemptionResult"/> that contains information about the redeemed code. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<CodeRedemptionResult, HaloApiErrorContainer>> RedeemCode(string code)
        {
            RedeemableCode container = new()
            {
                Code = code,
            };

            return await this.ExecuteAPIRequest<CodeRedemptionResult>(
                $"https://{WaypointEndpoints.VoucherEndpoint}.{WaypointEndpoints.ServiceDomain}/users/me/codes",
                HttpMethod.Post,
                true,
                false,
                GlobalConstants.WEB_USER_AGENT,
                JsonSerializer.Serialize(container));
        }

        /// <summary>
        /// Gets information about a user's Halo Waypoint settings.
        /// </summary>
        /// <remarks>
        /// Settings are obtained for the user associated with the Spartan token passed to the request.
        /// </remarks>
        /// <include file='../APIDocsExamples/Waypoint/GetUserSettings.xml' path='//example'/>
        /// <returns>If successful, returns an instance of <see cref="UserSettings"/> containing user configuration information. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<UserSettings, HaloApiErrorContainer>> GetUserSettings()
        {
            return await this.ExecuteAPIRequest<UserSettings>(
                $"https://{WaypointEndpoints.ProfileEndpoint}.{WaypointEndpoints.ServiceDomain}/users/me/settings",
                HttpMethod.Post,
                true,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }

        /// <summary>
        /// Gets information about your own Halo Waypoint profile.
        /// </summary>
        /// <remarks>
        /// Profile is obtained for the user associated with the Spartan token passed to the request.
        /// </remarks>
        /// <include file='../APIDocsExamples/Waypoint/GetMyProfile.xml' path='//example'/>
        /// <returns>If successful, returns an instance of <see cref="UserProfile"/> containing profile information. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<UserProfile, HaloApiErrorContainer>> GetMyProfile()
        {
            return await this.ExecuteAPIRequest<UserProfile>(
                $"https://{WaypointEndpoints.ProfileEndpoint}.{WaypointEndpoints.ServiceDomain}/users/me",
                HttpMethod.Post,
                true,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }

        /// <summary>
        /// Gets information about a user's Halo Waypoint profile.
        /// </summary>
        /// <include file='../APIDocsExamples/Waypoint/GetUserProfile.xml' path='//example'/>
        /// <param name="userId">User identifier. Can be a XUID or Gamertag. If XUID is used, then <paramref name="isXuid"/> should be set to true.</param>
        /// <param name="isXuid">Determines whether the user ID specified in <paramref name="userId"/> is a XUID or not.</param>
        /// <returns>If successful, returns an instance of <see cref="UserProfile"/> containing profile information. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<UserProfile, HaloApiErrorContainer>> GetUserProfile(string userId, bool isXuid)
        {
            string composedId = isXuid ? $"xuid({userId})" : $"gt({userId})";

            return await this.ExecuteAPIRequest<UserProfile>(
                $"https://{WaypointEndpoints.ProfileEndpoint}.{WaypointEndpoints.ServiceDomain}/users/me/{composedId}",
                HttpMethod.Post,
                true,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }

        /// <summary>
        /// Gets the list of articles published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <include file='../APIDocsExamples/Waypoint/GetArticles.xml' path='//example'/>
        /// <param name="language">Article language. Example value is "en".</param>
        /// <param name="offset">Offset (number of articles to skip) from which to start the query.</param>
        /// <param name="count">Number of articles to retrieve.</param>
        /// <param name="order">Order in which articles are returned. Example values are "asc" or "desc".</param>
        /// <param name="categories">List of categories for which to return the articles.</param>
        /// <returns>If successful, returns the list of articles, each represented as <see cref="Article"/>. Otherwise, returns the details about the error.</returns>
        public async Task<HaloApiResultContainer<List<Article>, HaloApiErrorContainer>> GetArticles(string language = "", int offset = -1, int count = -1, string order = "", List<int>? categories = null)
        {
            string urlBase = $"https://{WaypointEndpoints.WPContentEndpoint}.{WaypointEndpoints.ServiceDomain}/articles?";

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

            return await this.ExecuteAPIRequest<List<Article>>(
                urlBase,
                HttpMethod.Get,
                false,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }

        /// <summary>
        /// Gets a single article published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <include file='../APIDocsExamples/Waypoint/GetArticle.xml' path='//example'/>
        /// <param name="slug">Slug associated with the article. Example value is "halo-waypoint-content-browser".</param>
        /// <returns>If successful, returns an instance of <see cref="Article"/>. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<Article, HaloApiErrorContainer>> GetArticle(string slug)
        {
            return await this.ExecuteAPIRequest<Article>(
                $"https://{WaypointEndpoints.WPContentEndpoint}.{WaypointEndpoints.ServiceDomain}/articles/{slug}",
                HttpMethod.Get,
                false,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }

        /// <summary>
        /// Gets a list of article categories that are available on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <include file='../APIDocsExamples/Waypoint/GetArticleCategories.xml' path='//example'/>
        /// <param name="language">Language in which the categories should be displayed. Example value is "en".</param>
        /// <returns>If successful, returns a list of <see cref="ArticleCategory"/> containing publishing categories. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<List<ArticleCategory>, HaloApiErrorContainer>> GetArticleCategories(string language = "")
        {
            return await this.ExecuteAPIRequest<List<ArticleCategory>>(
                $"https://{WaypointEndpoints.WPContentEndpoint}.{WaypointEndpoints.ServiceDomain}/taxonomy/article_category?" + (!string.IsNullOrEmpty(language) ? $"lang={language}" : string.Empty),
                HttpMethod.Get,
                false,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }

        /// <summary>
        /// Gets the details on a single article category published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <remarks>
        /// If you specify a category that does not exist, the response will be a HTTP 200 OK but with a `null` body.
        /// </remarks>
        /// <include file='../APIDocsExamples/Waypoint/GetArticleCategory.xml' path='//example'/>
        /// <param name="id">ID of the category. Must be an integer.</param>
        /// <param name="language">Language in which the category should be displayed. Example value is "en".</param>
        /// <returns>If successful, returns an instance of <see cref="ArticleCategory"/> containing category information. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<ArticleCategory, HaloApiErrorContainer>> GetArticleCategory(int id, string language = "")
        {
            return await this.ExecuteAPIRequest<ArticleCategory>(
                $"https://{WaypointEndpoints.WPContentEndpoint}.{WaypointEndpoints.ServiceDomain}/taxonomy/article_category/{id}?" + (!string.IsNullOrEmpty(language) ? $"lang={language}" : string.Empty),
                HttpMethod.Get,
                false,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }

        /// <summary>
        /// Gets the list of a player's service awards associated with <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <include file='../APIDocsExamples/Waypoint/GetServiceAwards.xml' path='//example'/>
        /// <returns>If successful, returns an instance of <see cref="ServiceAwardSnapshot"/> containing service award information. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<ServiceAwardSnapshot, HaloApiErrorContainer>> GetServiceAwards()
        {
            return await this.ExecuteAPIRequest<ServiceAwardSnapshot>(
                $"https://{WaypointEndpoints.ProfileEndpoint}.{WaypointEndpoints.ServiceDomain}/users/me/service-awards",
                HttpMethod.Get,
                true,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }

        /// <summary>
        /// Sets featured <see href="https://www.halowaypoint.com/">Halo Waypoint</see> service awards in a user's profile.
        /// </summary>
        /// <remarks>
        /// When passing an instance of <see cref="ServiceAwardSnapshot"/> ensure that only the <see cref="ServiceAwardSnapshot.FeaturedAwards"/> property is set. Setting other properties will result in a HTTP 400 Bad Request response.
        /// </remarks>
        /// <include file='../APIDocsExamples/Waypoint/PutFeaturedServiceAwards.xml' path='//example'/>
        /// <param name="awards">Instance of <see cref="ServiceAwardSnapshot"/> containing the list of service awards to feature.</param>
        /// <returns>If successful, returns an instance of <see cref="ServiceAwardSnapshot"/> confirming the setting. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<ServiceAwardSnapshot, HaloApiErrorContainer>> PutFeaturedServiceAwards(ServiceAwardSnapshot awards)
        {
            string requestBody = JsonSerializer.Serialize(awards);

            return await this.ExecuteAPIRequest<ServiceAwardSnapshot>(
                $"https://{WaypointEndpoints.ProfileEndpoint}.{WaypointEndpoints.ServiceDomain}/users/me/service-awards/featured-awards",
                HttpMethod.Put,
                true,
                false,
                GlobalConstants.WEB_USER_AGENT,
                requestBody,
                ApiContentType.Json);
        }

        /// <summary>
        /// Gets <see href="https://www.halowaypoint.com/">Halo Waypoint</see> service award details.
        /// </summary>
        /// <include file='../APIDocsExamples/Waypoint/GetServiceAward.xml' path='//example'/>
        /// <param name="slug">Service award slug.</param>
        /// <returns>If successful, returns an instance of <see cref="ServiceAward"/>. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<ServiceAward, HaloApiErrorContainer>> GetServiceAward(string slug)
        {
            return await this.ExecuteAPIRequest<ServiceAward>(
                $"https://{WaypointEndpoints.WPContentEndpoint}.{WaypointEndpoints.ServiceDomain}/service-awards/{slug}",
                HttpMethod.Get,
                false,
                false,
                GlobalConstants.WEB_USER_AGENT);
        }
    }
}
