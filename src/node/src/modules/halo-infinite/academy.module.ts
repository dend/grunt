import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type {
  AcademyClientManifest,
  AcademyStarDefinitions,
  BotCustomizationData,
  TestAcademyClientManifest,
} from '../../models/halo-infinite/misc';

/**
 * Academy module for bot customization and drill-related APIs.
 *
 * Provides access to:
 * - Academy content manifest
 * - Bot customization options
 * - Star/scoring definitions for drills
 *
 * @example
 * ```typescript
 * // Get academy content
 * const content = await client.academy.getContent();
 *
 * // Get bot customization options
 * const bots = await client.academy.getBotCustomization('flight-id');
 * ```
 */
export class AcademyModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.GAME_CMS_ORIGIN);
  }

  /**
   * Get bot customization data.
   *
   * @param flightId - Flight/clearance ID
   * @returns Bot customization options
   */
  getBotCustomization(flightId?: string): Promise<HaloApiResult<BotCustomizationData>> {
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<BotCustomizationData>(`/hi/academy/botcustomization${flightParam}`, {
      useClearance: !!flightId,
    });
  }

  /**
   * Get academy content manifest.
   *
   * @returns Academy client manifest
   */
  getContent(): Promise<HaloApiResult<AcademyClientManifest>> {
    return this.get<AcademyClientManifest>('/hi/academy/content');
  }

  /**
   * Get test academy content (for flighted builds).
   *
   * @param clearanceId - Clearance identifier
   * @returns Test academy manifest
   */
  getContentTest(clearanceId: string): Promise<HaloApiResult<TestAcademyClientManifest>> {
    this.assertNotEmpty(clearanceId, 'clearanceId');
    return this.get<TestAcademyClientManifest>(`/hi/academy/content/test?clearanceId=${clearanceId}`, {
      useClearance: true,
    });
  }

  /**
   * Get star/scoring definitions for academy drills.
   *
   * @returns Star definitions
   */
  getStarDefinitions(): Promise<HaloApiResult<AcademyStarDefinitions>> {
    return this.get<AcademyStarDefinitions>('/hi/Progression/file/academy/stars');
  }
}
