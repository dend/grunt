import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';

/**
 * Text moderation key response.
 */
export interface ModerationKey {
  /** The moderation key */
  key?: string;
  /** Expiration time (ISO 8601) */
  expiresAt?: string;
}

/**
 * Text Moderation module for moderation-related APIs.
 *
 * @example
 * ```typescript
 * // Get moderation key
 * const key = await client.textModeration.getModerationKey();
 * ```
 */
export class TextModerationModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.TEXT_ORIGIN);
  }

  /**
   * Get a moderation key for text validation.
   *
   * @returns Moderation key
   */
  getModerationKey(): Promise<HaloApiResult<ModerationKey>> {
    return this.get<ModerationKey>('/hi/moderation/key');
  }
}
