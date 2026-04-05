// <copyright file="SkillModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules.HaloInfinite
{
    /// <summary>
    /// Module for skill-related API operations including CSR and match skill information.
    /// </summary>
    public sealed class SkillModule : ModuleBase
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
        /// <include file='../../../APIDocsExamples/HaloInfinite/Skill_GetMatchPlayerResult.xml' path='example'/>
        /// <param name="matchId">The unique match ID.</param>
        /// <param name="playerIds">List of numeric XUIDs for the players.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of <see cref="MatchSkillInfo"/> representing player skills if the request was successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<MatchSkillInfo, RawResponseContainer>> GetMatchPlayerResultAsync(string matchId, List<string> playerIds, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(matchId);
            ArgumentNullException.ThrowIfNull(playerIds);

            var formattedPlayerList = string.Join(",", playerIds.Select(id => $"xuid({id})"));
            return this.GetAsync<MatchSkillInfo>(
                $"/hi/matches/{matchId}/skill?players={formattedPlayerList}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets playlist Competitive Skill Rank (CSR) for a player or a set of players.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Skill_GetPlaylistCsr.xml' path='example'/>
        /// <param name="playlistId">Unique ID for the playlist.</param>
        /// <param name="playerIds">List of numeric XUIDs for the players.</param>
        /// <param name="seasonId">Season identifier. Example value is "CsrSeason2-3".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of <see cref="PlaylistCsrResultContainer"/> representing player CSRs. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<PlaylistCsrResultContainer, RawResponseContainer>> GetPlaylistCsrAsync(string playlistId, List<string> playerIds, string seasonId = "", CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(playlistId);
            ArgumentNullException.ThrowIfNull(playerIds);

            var formattedPlayerList = string.Join(",", playerIds.Select(id => $"xuid({id})"));
            return this.GetAsync<PlaylistCsrResultContainer>(
                $"/hi/playlist/{playlistId}/csrs?players={formattedPlayerList}&season={seasonId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }
    }
}
