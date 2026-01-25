// <copyright file="SettingsModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules
{
    /// <summary>
    /// Module for settings-related API operations including clearances and flights.
    /// </summary>
    public class SettingsModule : ModuleBase
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
        /// Get a list of features enabled for a given flight.
        /// </summary>
        /// <param name="flightId">Clearance ID/flight that is being used.</param>
        /// <returns>An instance of FlightedFeatureFlags containing a list of enabled and disabled features if the request is successful. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<FlightedFeatureFlags, RawResponseContainer>> GetFlightedFeatureFlags(string flightId)
        {
            return await this.GetAsync<FlightedFeatureFlags>(
                $"/featureflags/hi?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Returns the currently active clearance.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Settings_ActiveClearance.xml' path='example'/>
        /// <param name="release">Release identifier. Examples seen are 1.4, 1.5, and 1.6.</param>
        /// <returns>If successful, returns an instance of <see cref="PlayerClearance"/>. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> ActiveClearance(string release)
        {
            return await this.GetAsync<PlayerClearance>(
                $"/hi/clearances/active?release={release}",
                useSpartanToken: false);
        }

        /// <summary>
        /// Returns the currently active flight.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Settings_ActiveFlight.xml' path='example'/>
        /// <param name="sandbox">Identifier associated with the sandbox. Typical value is UNUSED.</param>
        /// <param name="buildNumber">Number of the game build the data is requested for. Example value is 211755.22.01.23.0549-0.</param>
        /// <param name="release">Release identifier. Examples seen are 1.4 and 1.5.</param>
        /// <returns>If successful, returns an instance of <see cref="PlayerClearance"/>. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> ActiveFlight(string sandbox, string buildNumber, string release)
        {
            return await this.GetAsync<PlayerClearance>(
                $"/oban/flight-configurations/titles/hi/audiences/RETAIL/active?sandbox={sandbox}&build={buildNumber}&release={release}");
        }

        /// <summary>
        /// Gets the currently assigned clearance/flight ID.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Settings_GetClearance.xml' path='example'/>
        /// <param name="audience">Audience that the request is targeting. Standard value is RETAIL.</param>
        /// <param name="sandbox">Identifier associated with the sandbox. Typical value is UNUSED.</param>
        /// <param name="buildNumber">Number of the game build the data is requested for. Example value is 211755.22.01.23.0549-0.</param>
        /// <param name="release">Release identifier. Examples seen are 1.4 and 1.5.</param>
        /// <returns>An instance of PlayerClearance if the request is successful. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> GetClearance(string audience, string sandbox, string buildNumber, string release)
        {
            return await this.GetAsync<PlayerClearance>(
                $"/oban/flight-configurations/titles/hi/audiences/{audience}/active?sandbox={sandbox}&build={buildNumber}&release={release}");
        }

        /// <summary>
        /// Gets the player clearance/flight ID.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Settings_GetPlayerClearance.xml' path='example'/>
        /// <param name="audience">Audience that the request is targeting. Standard value is RETAIL.</param>
        /// <param name="player">The player identifier in the format "xuid(PLAYER_XUID_HERE)".</param>
        /// <param name="sandbox">Identifier associated with the sandbox. Typical value is UNUSED.</param>
        /// <param name="buildNumber">Number of the game build the data is requested for. Example value is 211755.22.01.23.0549-0.</param>
        /// <param name="release">Release identifier. Examples seen are 1.4 and 1.5.</param>
        /// <returns>An instance of PlayerClearance if the request is successful. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> GetPlayerClearance(string audience, string player, string sandbox, string buildNumber, string release)
        {
            return await this.GetAsync<PlayerClearance>(
                $"/oban/flight-configurations/titles/hi/audiences/{audience}/players/{player}/active?sandbox={sandbox}&build={buildNumber}&release={release}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the player clearance/flight ID for RETAIL audience.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Settings_PlayerClearance.xml' path='example'/>
        /// <param name="player">The player identifier in the format "xuid(PLAYER_XUID_HERE)".</param>
        /// <param name="sandbox">Identifier associated with the sandbox. Typical value is UNUSED.</param>
        /// <param name="buildNumber">Number of the game build the data is requested for. Example value is 211755.22.01.23.0549-0.</param>
        /// <param name="release">Release identifier. Examples seen are 1.4 and 1.5.</param>
        /// <returns>An instance of PlayerClearance if the request is successful. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlayerClearance, RawResponseContainer>> PlayerClearance(string player, string sandbox, string buildNumber, string release)
        {
            return await this.GetAsync<PlayerClearance>(
                $"/oban/flight-configurations/titles/hi/audiences/RETAIL/players/{player}/active?sandbox={sandbox}&build={buildNumber}&release={release}");
        }
    }
}
