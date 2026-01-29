/**
 * Global constants and default values used throughout the library.
 */

/**
 * Default User-Agent strings for different contexts.
 * Matching the C# implementation's global constants.
 */
export const USER_AGENTS = {
  /**
   * User-Agent for Halo PC client requests.
   */
  HALO_PC:
    'SHIVA-2043073184/6.10025.12948.0 (release; PC)',

  /**
   * User-Agent for Halo Waypoint app requests.
   */
  HALO_WAYPOINT:
    'HaloWaypoint/2021112313511900 CFNetwork/1327.0.4 Darwin/21.2.0',

  /**
   * Standard web browser User-Agent.
   */
  WEB: 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36',
} as const;

/**
 * Default authentication scopes for Xbox Live.
 */
export const DEFAULT_AUTH_SCOPES = [
  'Xboxlive.signin',
  'Xboxlive.offline_access',
] as const;

/**
 * HTTP header names used in API requests.
 */
export const HEADERS = {
  /** Spartan token authentication header */
  SPARTAN_AUTH: 'x-343-authorization-spartan',

  /** Clearance/flight token header */
  CLEARANCE: '343-clearance',

  /** Standard Content-Type header */
  CONTENT_TYPE: 'Content-Type',

  /** Standard Accept header */
  ACCEPT: 'Accept',

  /** Standard User-Agent header */
  USER_AGENT: 'User-Agent',

  /** ETag header for caching */
  ETAG: 'ETag',

  /** If-None-Match header for conditional requests */
  IF_NONE_MATCH: 'If-None-Match',
} as const;

/**
 * Default timeout for HTTP requests in milliseconds.
 */
export const DEFAULT_TIMEOUT_MS = 30_000;

/**
 * Default cache TTL in milliseconds (60 minutes).
 */
export const DEFAULT_CACHE_TTL_MS = 60 * 60 * 1000;

/**
 * Default maximum retry attempts.
 */
export const DEFAULT_MAX_RETRIES = 3;
