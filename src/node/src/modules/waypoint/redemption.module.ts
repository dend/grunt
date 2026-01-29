import { WaypointModuleBase } from '../base/waypoint-module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import type { CodeRedemptionResult } from '../../models/waypoint';

/**
 * Redemption module for code redemption APIs.
 *
 * @example
 * ```typescript
 * // Redeem a code
 * const result = await client.redemption.redeemCode('XXXX-XXXX-XXXX');
 * ```
 */
export class RedemptionModule extends WaypointModuleBase {
  constructor(client: ClientBase) {
    super(client);
  }

  /**
   * Redeem a promotional code.
   *
   * @param code - The code to redeem
   * @returns Redemption result
   */
  redeemCode(code: string): Promise<HaloApiResult<CodeRedemptionResult>> {
    this.assertNotEmpty(code, 'code');

    return this.postJson<CodeRedemptionResult, { code: string }>(
      '/hi/redemption/code',
      { code }
    );
  }
}
