/**
 * Halo API endpoint configuration constants.
 *
 * These define the service origins for different API modules.
 * The Halo Infinite API is distributed across multiple services,
 * each handling a specific domain of functionality.
 */
export const HALO_CORE_ENDPOINTS = {
  /**
   * Base service domain for all Halo Waypoint services.
   */
  SERVICE_DOMAIN: 'svc.halowaypoint.com',

  // ─────────────────────────────────────────────────────────────────
  // Service Origins (prepended to SERVICE_DOMAIN)
  // ─────────────────────────────────────────────────────────────────

  /** Game CMS for static content (challenges, items, etc.) */
  GAME_CMS_ORIGIN: 'gamecms-hacs',

  /** Economy service for stores, inventory, customization */
  ECONOMY_ORIGIN: 'economy',

  /** UGC authoring service for creating/editing user content */
  AUTHORING_ORIGIN: 'authoring-infiniteugc',

  /** UGC discovery service for searching user content */
  DISCOVERY_ORIGIN: 'discovery-infiniteugc',

  /** Lobby service for multiplayer lobbies and presence */
  LOBBY_ORIGIN: 'lobby-hi',

  /** Settings and configuration service */
  SETTINGS_ORIGIN: 'settings',

  /** Skill and CSR rating service */
  SKILL_ORIGIN: 'skill',

  /** Ban processor service */
  BAN_PROCESSOR_ORIGIN: 'banprocessor',

  /** Stats service for match history and service records */
  STATS_ORIGIN: 'halostats',

  /** Text moderation service */
  TEXT_ORIGIN: 'text',

  /** Content service for articles and news */
  CONTENT_ORIGIN: 'content-hacs',

  // ─────────────────────────────────────────────────────────────────
  // Authentication Endpoints
  // ─────────────────────────────────────────────────────────────────

  /**
   * Endpoint for obtaining a Spartan token from an XSTS token.
   */
  SPARTAN_TOKEN_ENDPOINT: 'https://settings.svc.halowaypoint.com/spartan-token',

  /**
   * Endpoint for discovering available Halo Infinite API endpoints.
   * Returns configuration for all available services.
   */
  HALO_INFINITE_SETTINGS:
    'https://settings.svc.halowaypoint.com/settings/hipc/e2a0a7c6-6efe-42af-9283-c2ab73250c48',

  // ─────────────────────────────────────────────────────────────────
  // Xbox Live / XSTS Configuration
  // ─────────────────────────────────────────────────────────────────

  /**
   * Relying party URL for XSTS token exchange.
   * Use this when requesting an XSTS token for Halo Waypoint.
   */
  HALO_WAYPOINT_XSTS_RELYING_PARTY: 'https://prod.xsts.halowaypoint.com/',

  // ─────────────────────────────────────────────────────────────────
  // Blob Storage
  // ─────────────────────────────────────────────────────────────────

  /**
   * Base URL for UGC blob storage (maps, game variants, etc.)
   */
  BLOBS_ORIGIN: 'blobs-infiniteugc',
} as const;

/**
 * Waypoint-specific endpoints for non-game services.
 */
export const WAYPOINT_ENDPOINTS = {
  /**
   * Base domain for Halo Waypoint web services.
   */
  WEB_DOMAIN: 'www.halowaypoint.com',

  /**
   * API subdomain for Waypoint services.
   */
  API_DOMAIN: 'api.halowaypoint.com',

  /**
   * Profile API origin.
   */
  PROFILE_ORIGIN: 'profile',

  /**
   * Redemption API origin for code redemption.
   */
  REDEMPTION_ORIGIN: 'redemption',
} as const;

/**
 * Build a full URL from an origin and path.
 *
 * @param origin - The service origin (e.g., 'halostats')
 * @param path - The API path (e.g., '/hi/players/xuid(...)/matches')
 * @returns Full HTTPS URL
 *
 * @example
 * ```typescript
 * const url = buildServiceUrl(HALO_CORE_ENDPOINTS.STATS_ORIGIN, '/hi/players/xuid(123)/matches');
 * // Returns: 'https://halostats.svc.halowaypoint.com/hi/players/xuid(123)/matches'
 * ```
 */
export function buildServiceUrl(origin: string, path: string): string {
  return `https://${origin}.${HALO_CORE_ENDPOINTS.SERVICE_DOMAIN}${path}`;
}
