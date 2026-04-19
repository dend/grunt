// <copyright file="ProfileModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Threading;
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
    public sealed class ProfileModule : WaypointModuleBase
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
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="UserSettings"/> containing user configuration information. Otherwise, returns a null object and error details.</returns>
        public Task<HaloApiResultContainer<UserSettings, RawResponseContainer>> GetUserSettingsAsync(CancellationToken cancellationToken = default)
        {
            return this.PostAsync<UserSettings>("/users/me/settings", useSpartanToken: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about your own Halo Waypoint profile.
        /// </summary>
        /// <remarks>
        /// Profile is obtained for the user associated with the Spartan token passed to the request.
        /// </remarks>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="UserProfile"/> containing profile information. Otherwise, returns a null object and error details.</returns>
        public Task<HaloApiResultContainer<UserProfile, RawResponseContainer>> GetMyProfileAsync(CancellationToken cancellationToken = default)
        {
            return this.PostAsync<UserProfile>("/users/me", useSpartanToken: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about a user's Halo Waypoint profile.
        /// </summary>
        /// <param name="userId">User identifier. Can be a XUID or Gamertag. If XUID is used, then <paramref name="isXuid"/> should be set to true.</param>
        /// <param name="isXuid">Determines whether the user ID specified in <paramref name="userId"/> is a XUID or not.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="UserProfile"/> containing profile information. Otherwise, returns a null object and error details.</returns>
        public Task<HaloApiResultContainer<UserProfile, RawResponseContainer>> GetUserProfileAsync(string userId, bool isXuid, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(userId);

            string composedId = isXuid ? $"xuid({userId})" : $"gt({userId})";
            return this.PostAsync<UserProfile>($"/users/me/{composedId}", useSpartanToken: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the list of a player's service awards associated with <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="ServiceAwardSnapshot"/> containing service award information. Otherwise, returns a null object and the error details.</returns>
        public Task<HaloApiResultContainer<ServiceAwardSnapshot, RawResponseContainer>> GetServiceAwardsAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<ServiceAwardSnapshot>("/users/me/service-awards", useSpartanToken: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Sets featured <see href="https://www.halowaypoint.com/">Halo Waypoint</see> service awards in a user's profile.
        /// </summary>
        /// <remarks>
        /// When passing an instance of <see cref="ServiceAwardSnapshot"/> ensure that only the <see cref="ServiceAwardSnapshot.FeaturedAwards"/> property is set. Setting other properties will result in a HTTP 400 Bad Request response.
        /// </remarks>
        /// <param name="awards">Instance of <see cref="ServiceAwardSnapshot"/> containing the list of service awards to feature.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="ServiceAwardSnapshot"/> confirming the setting. Otherwise, returns a null object and the error details.</returns>
        public Task<HaloApiResultContainer<ServiceAwardSnapshot, RawResponseContainer>> PutFeaturedServiceAwardsAsync(ServiceAwardSnapshot awards, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(awards);

            return this.PutJsonAsync<ServiceAwardSnapshot, ServiceAwardSnapshot>("/users/me/service-awards/featured-awards", awards, useSpartanToken: true, cancellationToken: cancellationToken);
        }
    }
}
