import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { BansSummaryQueryResult } from '../../models/halo-infinite/misc';

/**
 * Ban Processor module for querying ban information.
 *
 * @example
 * ```typescript
 * // Check if players are banned
 * const bans = await client.banProcessor.banSummary(['xuid1', 'xuid2']);
 * ```
 */
export class BanProcessorModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.BAN_PROCESSOR_ORIGIN);
  }

  /**
   * Get ban summary for a list of players.
   *
   * @param targetList - List of player XUIDs to check
   * @returns Ban summary results
   */
  banSummary(targetList: string[]): Promise<HaloApiResult<BansSummaryQueryResult>> {
    if (!targetList.length) {
      throw new Error('targetList cannot be empty');
    }

    const targets = targetList.map((id) => `xuid(${id})`).join(',');
    return this.get<BansSummaryQueryResult>(`/hi/bansummary?auth=st&targets=${targets}`);
  }
}
