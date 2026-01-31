/**
 * Configuration options for creating a HaloInfiniteClient.
 *
 * Uses an options object pattern for flexible configuration.
 * Only the spartanToken is required; all other options have sensible defaults.
 *
 * @example
 * ```typescript
 * const client = new HaloInfiniteClient({
 *   spartanToken: 'your-spartan-token',
 *   xuid: 'xuid',
 *   clearanceToken: 'flight-clearance-id',
 *   includeRawResponses: true, // Enable for debugging
 * });
 * ```
 */
export interface HaloInfiniteClientOptions {
  /**
   * Spartan token for authentication.
   * Obtain this through Xbox Live authentication flow.
   */
  spartanToken: string;

  /**
   * Xbox User ID (XUID) in numeric format.
   * Used for player-specific API calls.
   *
   * @example 'xuid'
   */
  xuid?: string;

  /**
   * Clearance/flight token for accessing flighted content.
   * Required for some preview or test endpoints.
   */
  clearanceToken?: string;

  /**
   * Whether to include raw request/response data in results.
   * Useful for debugging API issues.
   *
   * @default false
   */
  includeRawResponses?: boolean;

  /**
   * Custom User-Agent header value.
   * If not provided, a default browser-like agent is used.
   */
  userAgent?: string;

  /**
   * Custom fetch implementation.
   * Useful for testing or Node.js environments with custom fetch.
   *
   * @default globalThis.fetch
   */
  fetchFn?: typeof fetch;

  /**
   * Cache time-to-live in milliseconds.
   * Cached responses are revalidated using ETags.
   *
   * @default 3600000 (60 minutes)
   */
  cacheTtlMs?: number;

  /**
   * Maximum number of retry attempts for transient failures.
   * Retries use exponential backoff: 200ms, 500ms, 1000ms.
   *
   * @default 3
   */
  maxRetries?: number;
}

/**
 * Configuration options for creating a WaypointClient.
 *
 * Similar to HaloInfiniteClientOptions but with optional spartanToken
 * since some Waypoint endpoints don't require authentication.
 */
export interface WaypointClientOptions {
  /**
   * Spartan token for authentication.
   * Optional for some public endpoints.
   */
  spartanToken?: string;

  /**
   * Xbox User ID (XUID) in numeric format.
   */
  xuid?: string;

  /**
   * Clearance token for flighted content.
   */
  clearanceToken?: string;

  /**
   * Custom User-Agent header value.
   */
  userAgent?: string;

  /**
   * Custom fetch implementation.
   *
   * @default globalThis.fetch
   */
  fetchFn?: typeof fetch;

  /**
   * Cache time-to-live in milliseconds.
   *
   * @default 3600000 (60 minutes)
   */
  cacheTtlMs?: number;

  /**
   * Maximum retry attempts for transient failures.
   *
   * @default 3
   */
  maxRetries?: number;
}

/**
 * Internal request options passed to executeRequest.
 * Used by module methods to configure individual API calls.
 */
export interface RequestOptions {
  /**
   * Whether to include the Spartan token header.
   *
   * @default true
   */
  useSpartanToken?: boolean;

  /**
   * Whether to include the clearance token header.
   *
   * @default false
   */
  useClearance?: boolean;

  /**
   * Request body content as string or binary.
   */
  body?: string | Uint8Array;

  /**
   * Content type for the request body.
   *
   * @default ApiContentType.Json
   */
  contentType?: import('./api-content-type').ApiContentType;

  /**
   * Additional headers to include in the request.
   */
  customHeaders?: Record<string, string>;

  /**
   * Whether to attempt deserialization even on error status codes.
   * Some API errors include useful JSON error bodies.
   *
   * @default true
   */
  enforceSuccess?: boolean;

  /**
   * Return raw bytes instead of deserializing the response.
   * Use this for binary data like images, blobs, and film chunks.
   *
   * @default false
   */
  returnRaw?: boolean;
}
