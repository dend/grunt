import { WaypointModuleBase } from '../base/waypoint-module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import type {
  UserProfile,
  UserSettings,
  ServiceAwardSnapshot,
} from '../../models/waypoint';

/**
 * Profile module for user profile and settings APIs.
 *
 * @example
 * ```typescript
 * // Get my profile
 * const profile = await client.profile.getMyProfile();
 *
 * // Get another user's profile
 * const otherProfile = await client.profile.getUserProfile('xuid', true);
 * ```
 */
export class ProfileModule extends WaypointModuleBase {
  constructor(client: ClientBase) {
    super(client);
  }

  /**
   * Get the current user's settings.
   *
   * @returns User settings
   */
  getUserSettings(): Promise<HaloApiResult<UserSettings>> {
    return this.get<UserSettings>('/hi/users/me/settings');
  }

  /**
   * Get the current user's profile.
   *
   * @returns User profile
   */
  getMyProfile(): Promise<HaloApiResult<UserProfile>> {
    return this.get<UserProfile>('/hi/users/me');
  }

  /**
   * Get a user's profile by XUID or gamertag.
   *
   * @param userId - XUID or gamertag
   * @param isXuid - Whether userId is an XUID (true) or gamertag (false)
   * @returns User profile
   */
  getUserProfile(
    userId: string,
    isXuid: boolean = false
  ): Promise<HaloApiResult<UserProfile>> {
    this.assertNotEmpty(userId, 'userId');

    const identifier = isXuid ? `xuid(${userId})` : encodeURIComponent(userId);
    return this.get<UserProfile>(`/hi/users/${identifier}`);
  }

  /**
   * Get service awards for the current user.
   *
   * @returns Service award snapshot
   */
  getServiceAwards(): Promise<HaloApiResult<ServiceAwardSnapshot>> {
    return this.get<ServiceAwardSnapshot>('/hi/users/me/serviceawards');
  }

  /**
   * Update featured service awards for the current user.
   *
   * @param awards - Awards to feature
   * @returns Updated awards
   */
  putFeaturedServiceAwards(
    awards: ServiceAwardSnapshot
  ): Promise<HaloApiResult<ServiceAwardSnapshot>> {
    return this.putJson<ServiceAwardSnapshot, ServiceAwardSnapshot>(
      '/hi/users/me/serviceawards/featured',
      awards
    );
  }
}
