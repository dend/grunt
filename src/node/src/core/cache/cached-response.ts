/**
 * Represents a cached API response with ETag for conditional requests.
 *
 * The cache stores the raw response content along with the ETag value,
 * enabling HTTP 304 Not Modified responses when the content hasn't changed.
 */
export interface CachedResponse {
  /**
   * ETag value from the original response.
   * Used in If-None-Match header for subsequent requests.
   */
  etag?: string;

  /**
   * Raw response content as bytes.
   * Stored as Uint8Array to support both JSON and binary responses.
   */
  content: Uint8Array;
}
