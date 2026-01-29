import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { MatchType } from '../../models/halo-infinite/enums/match-type';
import type { LifecycleMode } from '../../models/halo-infinite/enums/lifecycle-mode';
import type {
  MatchStats,
  MatchHistoryResponse,
  PlayerMatchCount,
  PlayerServiceRecord,
} from '../../models/halo-infinite/match';
import type { ChallengeDecksResponse, MatchProgression } from '../../models/halo-infinite/progression';
import type { MatchesPrivacy, PlayerDailyCustomExperience } from '../../models/halo-infinite/misc';

/**
 * Stats module for match history and service records.
 *
 * Provides access to:
 * - Match history for players
 * - Individual match statistics
 * - Player service records (career stats)
 * - Challenge decks and progression
 *
 * @example
 * ```typescript
 * // Get match history
 * const history = await client.stats.getMatchHistory('2533274855333605', 0, 25, MatchType.All);
 *
 * // Get specific match details
 * const match = await client.stats.getMatchStats('match-guid-here');
 *
 * // Get player service record
 * const record = await client.stats.getPlayerServiceRecordByXuid('2533274855333605', LifecycleMode.Matchmade);
 * ```
 */
export class StatsModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.STATS_ORIGIN);
  }

  /**
   * Get challenge decks available for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Challenge decks response
   */
  getChallengeDecks(player: string): Promise<HaloApiResult<ChallengeDecksResponse>> {
    this.assertNotEmpty(player, 'player');
    return this.get<ChallengeDecksResponse>(`/hi/players/xuid(${player})/decks`);
  }

  /**
   * Get match count summary for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Match count breakdown by type
   */
  getMatchCount(player: string): Promise<HaloApiResult<PlayerMatchCount>> {
    this.assertNotEmpty(player, 'player');
    return this.get<PlayerMatchCount>(`/hi/players/xuid(${player})/matches/count`);
  }

  /**
   * Get match history for a player.
   *
   * @param player - Player's numeric XUID
   * @param start - Starting index for pagination (0-based)
   * @param count - Number of matches to return (max 25)
   * @param type - Type of matches to query
   * @returns Paginated match history
   */
  getMatchHistory(
    player: string,
    start: number,
    count: number,
    type: MatchType
  ): Promise<HaloApiResult<MatchHistoryResponse>> {
    this.assertNotEmpty(player, 'player');
    this.assertRange(count, 1, 25, 'count');
    this.assertRange(start, 0, Number.MAX_SAFE_INTEGER, 'start');

    return this.get<MatchHistoryResponse>(
      `/hi/players/xuid(${player})/matches?start=${start}&count=${count}&type=${type}`
    );
  }

  /**
   * Get detailed statistics for a specific match.
   *
   * @param matchId - Match ID in GUID format
   * @returns Complete match statistics
   */
  getMatchStats(matchId: string): Promise<HaloApiResult<MatchStats>> {
    this.assertNotEmpty(matchId, 'matchId');
    return this.get<MatchStats>(`/hi/matches/${matchId}/stats`);
  }

  /**
   * Get challenge progression for a player in a specific match.
   *
   * @param player - Player's numeric XUID
   * @param matchId - Match ID in GUID format
   * @returns Match progression details
   */
  getPlayerMatchProgression(
    player: string,
    matchId: string
  ): Promise<HaloApiResult<MatchProgression>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(matchId, 'matchId');

    return this.get<MatchProgression>(
      `/hi/players/xuid(${player})/matches/${matchId}/progression`
    );
  }

  /**
   * Get match privacy settings for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Privacy settings
   */
  getMatchPrivacy(player: string): Promise<HaloApiResult<MatchesPrivacy>> {
    this.assertNotEmpty(player, 'player');
    return this.get<MatchesPrivacy>(`/hi/players/xuid(${player})/matches-privacy`);
  }

  /**
   * Get service record for a player by XUID.
   *
   * The service record contains aggregate career statistics.
   *
   * @param xuid - Player's numeric XUID
   * @param mode - Lifecycle mode (matchmade, custom, local)
   * @param seasonId - Optional season ID for season-specific stats
   * @returns Player service record
   */
  getPlayerServiceRecordByXuid(
    xuid: string,
    mode: LifecycleMode,
    seasonId?: string
  ): Promise<HaloApiResult<PlayerServiceRecord>> {
    this.assertNotEmpty(xuid, 'xuid');

    const seasonParam = seasonId ? `?seasonId=${seasonId}` : '';
    return this.get<PlayerServiceRecord>(
      `/hi/players/xuid(${xuid})/${mode}/servicerecord${seasonParam}`
    );
  }

  /**
   * Get service record for a player by gamertag.
   *
   * @param gamertag - Player's gamertag
   * @param mode - Lifecycle mode (matchmade, custom, local)
   * @param seasonId - Optional season ID for season-specific stats
   * @returns Player service record
   */
  getPlayerServiceRecordByGamertag(
    gamertag: string,
    mode: LifecycleMode,
    seasonId?: string
  ): Promise<HaloApiResult<PlayerServiceRecord>> {
    this.assertNotEmpty(gamertag, 'gamertag');

    const encodedGamertag = encodeURIComponent(gamertag);
    const seasonParam = seasonId ? `?seasonId=${seasonId}` : '';
    return this.get<PlayerServiceRecord>(
      `/hi/players/${encodedGamertag}/${mode}/servicerecord${seasonParam}`
    );
  }

  /**
   * Get daily custom game XP for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Daily custom experience info
   */
  getPlayerDailyCustomExperience(
    player: string
  ): Promise<HaloApiResult<PlayerDailyCustomExperience>> {
    this.assertNotEmpty(player, 'player');
    return this.get<PlayerDailyCustomExperience>(
      `/hi/players/xuid(${player})/customexperience`
    );
  }
}
