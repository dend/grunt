// <copyright file="SkillModule.cs" company="Den Delimarsky">
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
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules
{
    /// <summary>
    /// Module for skill-related API operations including CSR and match skill information.
    /// </summary>
    public class SkillModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkillModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal SkillModule(ClientBase client)
            : base(client, HaloCoreEndpoints.SkillOrigin)
        {
        }

        /// <summary>
        /// Returns individual player stats for a given match.
        /// </summary>
        /// <remarks>
        /// Method supports returning results in XML behind the scenes. Class names map to XML data model.
        /// </remarks>
        /// <include file='../../APIDocsExamples/HaloInfinite/Skill_GetMatchPlayerResult.xml' path='example'/>
        /// <param name="matchId">The unique match ID.</param>
        /// <param name="playerIds">Array of player IDs. Each ID string should be in the format of "xuid(XUID_VALUE)".</param>
        /// <returns>An instance of <see cref="MatchSkillInfo"/> representing player skills if the request was successful. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<MatchSkillInfo, RawResponseContainer>> GetMatchPlayerResult(string matchId, List<string> playerIds)
        {
            var formattedPlayerList = string.Join(",", playerIds);
            return await this.GetAsync<MatchSkillInfo>(
                $"/hi/matches/{matchId}/skill?players={formattedPlayerList}",
                useClearance: true);
        }

        /// <summary>
        /// Gets playlist Competitive Skill Rank (CSR) for a player or a set of players.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Skill_GetPlaylistCsr.xml' path='example'/>
        /// <param name="playlistId">Unique ID for the playlist.</param>
        /// <param name="playerIds">Array of player IDs. Each ID string should be in the format of "xuid(XUID_VALUE)".</param>
        /// <param name="seasonId">Season identifier. Example value is "CsrSeason2-3".</param>
        /// <returns>If successful, an instance of <see cref="PlaylistCsrResultContainer"/> representing player CSRs. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlaylistCsrResultContainer, RawResponseContainer>> GetPlaylistCsr(string playlistId, List<string> playerIds, string seasonId = "")
        {
            var formattedPlayerList = string.Join(",", playerIds);
            return await this.GetAsync<PlaylistCsrResultContainer>(
                $"/hi/playlist/{playlistId}/csrs?players={formattedPlayerList}&season={seasonId}",
                useClearance: true);
        }
    }
}
