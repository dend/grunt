import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { FlightedFeatureFlags, PlayerClearance } from '../../models/halo-infinite/misc';

/**
 * Settings module for clearance and flight configuration.
 *
 * @example
 * ```typescript
 * // Get flighted feature flags
 * const flags = await client.settings.getFlightedFeatureFlags('flight-id');
 *
 * // Get active clearance
 * const active = await client.settings.getActiveClearance('1.6');
 * ```
 */
export class SettingsModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.SETTINGS_ORIGIN);
  }

  /**
   * Get a list of features enabled for a given flight.
   *
   * @param flightId - Clearance ID/flight that is being used
   * @returns Flighted feature flags
   */
  getFlightedFeatureFlags(flightId: string): Promise<HaloApiResult<FlightedFeatureFlags>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<FlightedFeatureFlags>(`/featureflags/hi?flight=${flightId}`, {
      useClearance: true,
    });
  }

  /**
   * Get the currently active clearance.
   *
   * @param release - Release identifier (e.g., "1.4", "1.5", "1.6")
   * @returns Player clearance data
   */
  getActiveClearance(release: string): Promise<HaloApiResult<PlayerClearance>> {
    this.assertNotEmpty(release, 'release');
    return this.get<PlayerClearance>(`/hi/clearances/active?release=${release}`, {
      useSpartanToken: false,
    });
  }

  /**
   * Get the currently active flight.
   *
   * @param sandbox - Sandbox identifier (typically "UNUSED")
   * @param buildNumber - Game build number (e.g., "211755.22.01.23.0549-0")
   * @param release - Release identifier (e.g., "1.4", "1.5")
   * @returns Player clearance data
   */
  getActiveFlight(
    sandbox: string,
    buildNumber: string,
    release: string
  ): Promise<HaloApiResult<PlayerClearance>> {
    this.assertNotEmpty(sandbox, 'sandbox');
    this.assertNotEmpty(buildNumber, 'buildNumber');
    this.assertNotEmpty(release, 'release');
    return this.get<PlayerClearance>(
      `/oban/flight-configurations/titles/hi/audiences/RETAIL/active?sandbox=${sandbox}&build=${buildNumber}&release=${release}`
    );
  }

  /**
   * Get the clearance/flight ID for a specific audience.
   *
   * @param audience - Audience targeting (e.g., "RETAIL")
   * @param sandbox - Sandbox identifier (typically "UNUSED")
   * @param buildNumber - Game build number
   * @param release - Release identifier
   * @returns Player clearance data
   */
  getClearance(
    audience: string,
    sandbox: string,
    buildNumber: string,
    release: string
  ): Promise<HaloApiResult<PlayerClearance>> {
    this.assertNotEmpty(audience, 'audience');
    this.assertNotEmpty(sandbox, 'sandbox');
    this.assertNotEmpty(buildNumber, 'buildNumber');
    this.assertNotEmpty(release, 'release');
    return this.get<PlayerClearance>(
      `/oban/flight-configurations/titles/hi/audiences/${audience}/active?sandbox=${sandbox}&build=${buildNumber}&release=${release}`
    );
  }

  /**
   * Get the player clearance/flight ID for a specific audience.
   *
   * @param audience - Audience targeting (e.g., "RETAIL")
   * @param player - Player XUID
   * @param sandbox - Sandbox identifier (typically "UNUSED")
   * @param buildNumber - Game build number
   * @param release - Release identifier
   * @returns Player clearance data
   */
  getPlayerClearance(
    audience: string,
    player: string,
    sandbox: string,
    buildNumber: string,
    release: string
  ): Promise<HaloApiResult<PlayerClearance>> {
    this.assertNotEmpty(audience, 'audience');
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(sandbox, 'sandbox');
    this.assertNotEmpty(buildNumber, 'buildNumber');
    this.assertNotEmpty(release, 'release');
    return this.get<PlayerClearance>(
      `/oban/flight-configurations/titles/hi/audiences/${audience}/players/xuid(${player})/active?sandbox=${sandbox}&build=${buildNumber}&release=${release}`,
      { useClearance: true }
    );
  }

  /**
   * Get the player clearance/flight ID for RETAIL audience.
   *
   * @param player - Player XUID
   * @param sandbox - Sandbox identifier (typically "UNUSED")
   * @param buildNumber - Game build number
   * @param release - Release identifier
   * @returns Player clearance data
   */
  getPlayerClearanceRetail(
    player: string,
    sandbox: string,
    buildNumber: string,
    release: string
  ): Promise<HaloApiResult<PlayerClearance>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(sandbox, 'sandbox');
    this.assertNotEmpty(buildNumber, 'buildNumber');
    this.assertNotEmpty(release, 'release');
    return this.get<PlayerClearance>(
      `/oban/flight-configurations/titles/hi/audiences/RETAIL/players/xuid(${player})/active?sandbox=${sandbox}&build=${buildNumber}&release=${release}`
    );
  }
}
