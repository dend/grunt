import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { MatchSkillInfo, PlaylistCsrResultContainer } from '../../models/halo-infinite/skill';

/**
 * Skill module for CSR (Competitive Skill Rank) queries.
 *
 * Provides access to:
 * - Match skill results (CSR changes after a match)
 * - Playlist CSR for players
 *
 * @example
 * ```typescript
 * // Get CSR for a player in a playlist
 * const csr = await client.skill.getPlaylistCsr('playlist-id', ['xuid1', 'xuid2']);
 *
 * // Get skill results for a match
 * const matchSkill = await client.skill.getMatchPlayerResult('match-id', ['xuid1']);
 * ```
 */
export class SkillModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.SKILL_ORIGIN);
  }

  /**
   * Get skill/CSR changes for players in a specific match.
   *
   * @param matchId - Match ID in GUID format
   * @param playerIds - List of player XUIDs to query
   * @returns Skill info for each player
   */
  getMatchPlayerResult(
    matchId: string,
    playerIds: string[]
  ): Promise<HaloApiResult<MatchSkillInfo>> {
    this.assertNotEmpty(matchId, 'matchId');
    if (!playerIds.length) {
      throw new Error('playerIds cannot be empty');
    }

    const players = playerIds.map((id) => `xuid(${id})`).join(',');
    return this.get<MatchSkillInfo>(
      `/hi/matches/${matchId}/skill?players=${players}`,
      { useClearance: true }
    );
  }

  /**
   * Get current CSR for players in a specific playlist.
   *
   * @param playlistId - Playlist ID in GUID format
   * @param playerIds - List of player XUIDs to query
   * @param seasonId - Optional season ID for season-specific CSR
   * @returns CSR results for each player
   */
  getPlaylistCsr(
    playlistId: string,
    playerIds: string[],
    seasonId?: string
  ): Promise<HaloApiResult<PlaylistCsrResultContainer>> {
    this.assertNotEmpty(playlistId, 'playlistId');
    if (!playerIds.length) {
      throw new Error('playerIds cannot be empty');
    }

    const players = playerIds.map((id) => `xuid(${id})`).join(',');
    const seasonParam = seasonId ? `&seasonId=${seasonId}` : '';
    return this.get<PlaylistCsrResultContainer>(
      `/hi/playlist/${playlistId}/csrs?players=${players}${seasonParam}`,
      { useClearance: true }
    );
  }
}
