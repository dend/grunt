import { ClientBase } from './base/client-base';
import type { WaypointClientOptions } from '../models/common/client-options';

// Module imports
import { ProfileModule } from '../modules/waypoint/profile.module';
import { RedemptionModule } from '../modules/waypoint/redemption.module';
import { ContentModule } from '../modules/waypoint/content.module';
import { CommsModule } from '../modules/waypoint/comms.module';

/**
 * Client for interacting with Halo Waypoint APIs.
 *
 * Provides access to user profiles, code redemption, news content,
 * and notifications through domain-specific modules.
 *
 * @example
 * ```typescript
 * import { WaypointClient, isSuccess } from '@dendev/grunt';
 *
 * // Create client with authentication
 * const client = new WaypointClient({
 *   spartanToken: 'your-spartan-token',
 *   xuid: '2533274855333605',
 * });
 *
 * // Get my profile
 * const profile = await client.profile.getMyProfile();
 * if (isSuccess(profile)) {
 *   console.log(`Hello, ${profile.result.gamertag}!`);
 * }
 *
 * // Get news articles (no auth required)
 * const unauthClient = new WaypointClient();
 * const articles = await unauthClient.content.getArticles(1, 10);
 *
 * // Redeem a code
 * const result = await client.redemption.redeemCode('XXXX-XXXX-XXXX');
 * ```
 */
export class WaypointClient extends ClientBase {
  // Lazy-loaded module instances
  private _profile?: ProfileModule;
  private _redemption?: RedemptionModule;
  private _content?: ContentModule;
  private _comms?: CommsModule;

  /**
   * Creates a new WaypointClient instance.
   *
   * Some endpoints (like news articles) don't require authentication,
   * so spartanToken is optional.
   *
   * @param options - Client configuration options (optional)
   *
   * @example
   * ```typescript
   * // With authentication
   * const authClient = new WaypointClient({
   *   spartanToken: 'your-spartan-token',
   * });
   *
   * // Without authentication (for public endpoints)
   * const publicClient = new WaypointClient();
   * ```
   */
  constructor(options: WaypointClientOptions = {}) {
    super({
      fetchFn: options.fetchFn,
      cacheTtlMs: options.cacheTtlMs,
      maxRetries: options.maxRetries,
    });

    this.spartanToken = options.spartanToken ?? '';
    this.xuid = options.xuid ?? '';
    this.clearanceToken = options.clearanceToken ?? '';
    this.userAgent = options.userAgent ?? '';
  }

  /**
   * Profile module for user profile and settings APIs.
   *
   * Provides access to:
   * - User profiles (self and others)
   * - User settings
   * - Service awards
   */
  get profile(): ProfileModule {
    return (this._profile ??= new ProfileModule(this));
  }

  /**
   * Redemption module for code redemption APIs.
   *
   * Provides access to:
   * - Promotional code redemption
   */
  get redemption(): RedemptionModule {
    return (this._redemption ??= new RedemptionModule(this));
  }

  /**
   * Content module for articles and news APIs.
   *
   * Provides access to:
   * - News articles
   * - Article categories
   *
   * Note: Most content endpoints don't require authentication.
   */
  get content(): ContentModule {
    return (this._content ??= new ContentModule(this));
  }

  /**
   * Comms module for notifications and communications APIs.
   *
   * Provides access to:
   * - User notifications
   * - Marking notifications as read
   */
  get comms(): CommsModule {
    return (this._comms ??= new CommsModule(this));
  }
}
