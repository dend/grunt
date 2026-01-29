import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { Configuration } from '../../models/halo-infinite/misc';

/**
 * Configuration module for discovering API endpoints.
 *
 * @example
 * ```typescript
 * // Get API configuration
 * const config = await client.configuration.getApiSettingsContainer();
 * console.log(config.result?.endpoints);
 * ```
 */
export class ConfigurationModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.SETTINGS_ORIGIN);
  }

  /**
   * Get the API settings/configuration container.
   *
   * Returns the list of all available API endpoints and their configurations.
   *
   * @returns API configuration
   */
  getApiSettingsContainer(): Promise<HaloApiResult<Configuration>> {
    return this.getFullUrl<Configuration>(HALO_CORE_ENDPOINTS.HALO_INFINITE_SETTINGS, {
      useSpartanToken: false,
    });
  }
}
