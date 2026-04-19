// <copyright file="SettingsModule.cs" company="Den Delimarsky">
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
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules.HaloInfinite
{
    /// <summary>
    /// Module for settings-related API operations including clearances and flights.
    /// </summary>
    public sealed class SettingsModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal SettingsModule(ClientBase client)
            : base(client, HaloCoreEndpoints.SettingsOrigin)
        {
        }

        /// <summary>
        /// Gets a list of features enabled for a given flight.
        /// </summary>
        /// <param name="flightId">Clearance ID/flight that is being used.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of FlightedFeatureFlags containing a list of enabled and disabled features if the request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<FlightedFeatureFlags, RawResponseContainer>> GetFlightedFeatureFlagsAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<FlightedFeatureFlags>(
                $"/featureflags/hi?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns the currently active clearance.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Settings_ActiveClearance.xml' path='example'/>
        /// <param name="release">Release identifier. Examples seen are 1.4, 1.5, and 1.6.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="PlayerClearance"/>. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> GetActiveClearanceAsync(string release, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(release);

            return this.GetAsync<PlayerClearance>(
                $"/hi/clearances/active?release={release}",
                useSpartanToken: false,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns the currently active flight.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Settings_ActiveFlight.xml' path='example'/>
        /// <param name="sandbox">Identifier associated with the sandbox. Typical value is UNUSED.</param>
        /// <param name="buildNumber">Number of the game build the data is requested for. Example value is 211755.22.01.23.0549-0.</param>
        /// <param name="release">Release identifier. Examples seen are 1.4 and 1.5.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="PlayerClearance"/>. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> GetActiveFlightAsync(string sandbox, string buildNumber, string release, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(sandbox);
            ArgumentException.ThrowIfNullOrEmpty(buildNumber);
            ArgumentException.ThrowIfNullOrEmpty(release);

            return this.GetAsync<PlayerClearance>(
                $"/oban/flight-configurations/titles/hi/audiences/RETAIL/active?sandbox={sandbox}&build={buildNumber}&release={release}",
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the currently assigned clearance/flight ID.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Settings_GetClearance.xml' path='example'/>
        /// <param name="audience">Audience that the request is targeting. Standard value is RETAIL.</param>
        /// <param name="sandbox">Identifier associated with the sandbox. Typical value is UNUSED.</param>
        /// <param name="buildNumber">Number of the game build the data is requested for. Example value is 211755.22.01.23.0549-0.</param>
        /// <param name="release">Release identifier. Examples seen are 1.4 and 1.5.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of PlayerClearance if the request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> GetClearanceAsync(string audience, string sandbox, string buildNumber, string release, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(audience);
            ArgumentException.ThrowIfNullOrEmpty(sandbox);
            ArgumentException.ThrowIfNullOrEmpty(buildNumber);
            ArgumentException.ThrowIfNullOrEmpty(release);

            return this.GetAsync<PlayerClearance>(
                $"/oban/flight-configurations/titles/hi/audiences/{audience}/active?sandbox={sandbox}&build={buildNumber}&release={release}",
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the player clearance/flight ID.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Settings_GetPlayerClearance.xml' path='example'/>
        /// <param name="audience">Audience that the request is targeting. Standard value is RETAIL.</param>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="sandbox">Identifier associated with the sandbox. Typical value is UNUSED.</param>
        /// <param name="buildNumber">Number of the game build the data is requested for. Example value is 211755.22.01.23.0549-0.</param>
        /// <param name="release">Release identifier. Examples seen are 1.4 and 1.5.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of PlayerClearance if the request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> GetPlayerClearanceAsync(string audience, string player, string sandbox, string buildNumber, string release, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(audience);
            ArgumentException.ThrowIfNullOrEmpty(player);
            ArgumentException.ThrowIfNullOrEmpty(sandbox);
            ArgumentException.ThrowIfNullOrEmpty(buildNumber);
            ArgumentException.ThrowIfNullOrEmpty(release);

            return this.GetAsync<PlayerClearance>(
                $"/oban/flight-configurations/titles/hi/audiences/{audience}/players/xuid({player})/active?sandbox={sandbox}&build={buildNumber}&release={release}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the player clearance/flight ID for the RETAIL audience.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Settings_PlayerClearance.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="sandbox">Identifier associated with the sandbox. Typical value is UNUSED.</param>
        /// <param name="buildNumber">Number of the game build the data is requested for. Example value is 211755.22.01.23.0549-0.</param>
        /// <param name="release">Release identifier. Examples seen are 1.4 and 1.5.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of PlayerClearance if the request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> GetRetailPlayerClearanceAsync(string player, string sandbox, string buildNumber, string release, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(player);
            ArgumentException.ThrowIfNullOrEmpty(sandbox);
            ArgumentException.ThrowIfNullOrEmpty(buildNumber);
            ArgumentException.ThrowIfNullOrEmpty(release);

            return this.GetAsync<PlayerClearance>(
                $"/oban/flight-configurations/titles/hi/audiences/RETAIL/players/xuid({player})/active?sandbox={sandbox}&build={buildNumber}&release={release}",
                cancellationToken: cancellationToken);
        }
    }
}
