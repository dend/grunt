import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { FlightedFeatureFlags } from '../../models/halo-infinite/misc';

/**
 * Settings module for clearance and flight configuration.
 *
 * @example
 * ```typescript
 * // Get clearance level
 * const clearance = await client.settings.getClearanceLevel();
 * ```
 */
export class SettingsModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.SETTINGS_ORIGIN);
  }

  /**
   * Get the clearance level for the current player.
   *
   * Returns feature flags and flight IDs the player has access to.
   *
   * @returns Flighted feature flags
   */
  getClearanceLevel(): Promise<HaloApiResult<FlightedFeatureFlags>> {
    return this.get<FlightedFeatureFlags>('/hi/clearance', {
      useSpartanToken: true,
    });
  }
}
