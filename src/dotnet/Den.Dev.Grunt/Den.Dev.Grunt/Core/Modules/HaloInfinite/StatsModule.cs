// <copyright file="StatsModule.cs" company="Den Delimarsky">
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

namespace Den.Dev.Grunt.Core.Modules.HaloInfinite
{
    /// <summary>
    /// Module for stats-related API operations including match history and service records.
    /// </summary>
    public class StatsModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatsModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal StatsModule(ClientBase client)
            : base(client, HaloCoreEndpoints.StatsOrigin)
        {
        }

        /// <summary>
        /// Gets challenge decks that are available for a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_GetChallengeDecks.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>An instance of ChallengeDecksResponse containing deck information if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<ChallengeDecksResponse, RawResponseContainer>> GetChallengeDecks(string player)
        {
            return await this.GetAsync<ChallengeDecksResponse>($"/hi/players/xuid({player})/decks");
        }

        /// <summary>
        /// Gets the summary on number of played matches.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_GetMatchCount.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>An instance of PlayerMatchCount containing match counts if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<PlayerMatchCount, RawResponseContainer>> GetMatchCount(string player)
        {
            return await this.GetAsync<PlayerMatchCount>($"/hi/players/xuid({player})/matches/count");
        }

        /// <summary>
        /// Gets match history for a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_GetMatchHistory.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="start">Start value for the counter, from which data should be returned.</param>
        /// <param name="count">Number of matches to return. Maximum is 25. Going beyond 25 will result in only 25 values being returned.</param>
        /// <param name="type">Type of matches to query.</param>
        /// <returns>An instance of <see cref="MatchHistoryResponse"/> containing match metadata if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<MatchHistoryResponse, RawResponseContainer>> GetMatchHistory(string player, int start, int count, MatchType type)
        {
            return await this.GetAsync<MatchHistoryResponse>(
                $"/hi/players/xuid({player})/matches?start={start}&count={count}&type={type}");
        }

        /// <summary>
        /// Gets stats for a specific match.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_GetMatchStats.xml' path='example'/>
        /// <param name="matchId">Match ID in GUID format.</param>
        /// <returns>An instance of MatchStats containing match metadata if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<MatchStats, RawResponseContainer>> GetMatchStats(string matchId)
        {
            return await this.GetAsync<MatchStats>($"/hi/matches/{matchId}/stats");
        }

        /// <summary>
        /// Get challenge progression associated with a given match.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_GetPlayerMatchProgression.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="matchId">Match ID in GUID format.</param>
        /// <returns>An instance of MatchProgression containing match challenge progression metadata if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<MatchProgression, RawResponseContainer>> GetPlayerMatchProgression(string player, string matchId)
        {
            return await this.GetAsync<MatchProgression>(
                $"/hi/players/xuid({player})/matches/{matchId}/progression");
        }

        /// <summary>
        /// Gets match privacy settings for a given player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_MatchPrivacy.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>An instance of MatchesPrivacy containing match privacy metadata if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<MatchesPrivacy, RawResponseContainer>?> MatchPrivacy(string player)
        {
            return await this.GetAsync<MatchesPrivacy>($"/hi/players/xuid({player})/matches-privacy");
        }

        /// <summary>
        /// Gets the service record for a player by their XUID.
        /// </summary>
        /// <remarks>By tweaking season IDs, you can obtain season-specific information such as number of matches played, wins, losses, and others.</remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_GetPlayerServiceRecord.xml' path='example'/>
        /// <param name="xuid">The player's numeric XUID. Example value is "2533274855333605".</param>
        /// <param name="mode">Type of games for which to get the service record.</param>
        /// <param name="seasonId">The ID of the season for which additional stats are pulled. Example value is "Seasons/Season7.json".</param>
        /// <returns>If successful, an instance of <see cref="PlayerServiceRecord"/> containing service record information. Otherwise, returns null with additional details about the error.</returns>
        public async Task<HaloApiResultContainer<PlayerServiceRecord, RawResponseContainer>?> GetPlayerServiceRecordByXuid(string xuid, LifecycleMode mode, string seasonId = "")
        {
            var seasonMarker = !string.IsNullOrWhiteSpace(seasonId) ? $"?seasonId={seasonId}" : string.Empty;

            return await this.GetAsync<PlayerServiceRecord>(
                $"/hi/players/xuid({xuid})/{mode}/servicerecord{seasonMarker}");
        }

        /// <summary>
        /// Gets the service record for a player by their gamertag.
        /// </summary>
        /// <remarks>By tweaking season IDs, you can obtain season-specific information such as number of matches played, wins, losses, and others.</remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_GetPlayerServiceRecord.xml' path='example'/>
        /// <param name="gamertag">The player's gamertag. Example value is "BreadKrtek".</param>
        /// <param name="mode">Type of games for which to get the service record.</param>
        /// <param name="seasonId">The ID of the season for which additional stats are pulled. Example value is "Seasons/Season7.json".</param>
        /// <returns>If successful, an instance of <see cref="PlayerServiceRecord"/> containing service record information. Otherwise, returns null with additional details about the error.</returns>
        public async Task<HaloApiResultContainer<PlayerServiceRecord, RawResponseContainer>?> GetPlayerServiceRecordByGamertag(string gamertag, LifecycleMode mode, string seasonId = "")
        {
            var seasonMarker = !string.IsNullOrWhiteSpace(seasonId) ? $"?seasonId={seasonId}" : string.Empty;

            return await this.GetAsync<PlayerServiceRecord>(
                $"/hi/players/{gamertag}/{mode}/servicerecord{seasonMarker}");
        }

        /// <summary>
        /// Gets the daily custom experience for a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Stats_GetPlayerDailyCustomExperience.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>An instance of <see cref="PlayerDailyCustomExperience"/> containing daily experience if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<PlayerDailyCustomExperience, RawResponseContainer>> GetPlayerDailyCustomExperience(string player)
        {
            return await this.GetAsync<PlayerDailyCustomExperience>($"/hi/players/xuid({player})/customexperience");
        }
    }
}
