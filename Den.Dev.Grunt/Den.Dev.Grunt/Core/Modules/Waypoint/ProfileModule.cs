// <copyright file="ProfileModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.Waypoint;

namespace Den.Dev.Grunt.Core.Modules.Waypoint
{
    /// <summary>
    /// Module for Halo Waypoint profile and user settings APIs.
    /// </summary>
    public class ProfileModule : WaypointModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal ProfileModule(ClientBase client)
            : base(client, WaypointEndpoints.ProfileEndpoint)
        {
        }

        /// <summary>
        /// Gets information about a user's Halo Waypoint settings.
        /// </summary>
        /// <remarks>
        /// Settings are obtained for the user associated with the Spartan token passed to the request.
        /// </remarks>
        /// <returns>If successful, returns an instance of <see cref="UserSettings"/> containing user configuration information. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<UserSettings, RawResponseContainer>> GetUserSettings()
        {
            return await this.PostAsync<UserSettings>("/users/me/settings", useSpartanToken: true);
        }

        /// <summary>
        /// Gets information about your own Halo Waypoint profile.
        /// </summary>
        /// <remarks>
        /// Profile is obtained for the user associated with the Spartan token passed to the request.
        /// </remarks>
        /// <returns>If successful, returns an instance of <see cref="UserProfile"/> containing profile information. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<UserProfile, RawResponseContainer>> GetMyProfile()
        {
            return await this.PostAsync<UserProfile>("/users/me", useSpartanToken: true);
        }

        /// <summary>
        /// Gets information about a user's Halo Waypoint profile.
        /// </summary>
        /// <param name="userId">User identifier. Can be a XUID or Gamertag. If XUID is used, then <paramref name="isXuid"/> should be set to true.</param>
        /// <param name="isXuid">Determines whether the user ID specified in <paramref name="userId"/> is a XUID or not.</param>
        /// <returns>If successful, returns an instance of <see cref="UserProfile"/> containing profile information. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<UserProfile, RawResponseContainer>> GetUserProfile(string userId, bool isXuid)
        {
            string composedId = isXuid ? $"xuid({userId})" : $"gt({userId})";
            return await this.PostAsync<UserProfile>($"/users/me/{composedId}", useSpartanToken: true);
        }

        /// <summary>
        /// Gets the list of a player's service awards associated with <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <returns>If successful, returns an instance of <see cref="ServiceAwardSnapshot"/> containing service award information. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<ServiceAwardSnapshot, RawResponseContainer>> GetServiceAwards()
        {
            return await this.GetAsync<ServiceAwardSnapshot>("/users/me/service-awards", useSpartanToken: true);
        }

        /// <summary>
        /// Sets featured <see href="https://www.halowaypoint.com/">Halo Waypoint</see> service awards in a user's profile.
        /// </summary>
        /// <remarks>
        /// When passing an instance of <see cref="ServiceAwardSnapshot"/> ensure that only the <see cref="ServiceAwardSnapshot.FeaturedAwards"/> property is set. Setting other properties will result in a HTTP 400 Bad Request response.
        /// </remarks>
        /// <param name="awards">Instance of <see cref="ServiceAwardSnapshot"/> containing the list of service awards to feature.</param>
        /// <returns>If successful, returns an instance of <see cref="ServiceAwardSnapshot"/> confirming the setting. Otherwise, returns a null object and the error details.</returns>
        public async Task<HaloApiResultContainer<ServiceAwardSnapshot, RawResponseContainer>> PutFeaturedServiceAwards(ServiceAwardSnapshot awards)
        {
            return await this.PutJsonAsync<ServiceAwardSnapshot, ServiceAwardSnapshot>("/users/me/service-awards/featured-awards", awards, useSpartanToken: true);
        }
    }
}
