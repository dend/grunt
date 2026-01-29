/**
 * Generic result container for all Halo API responses.
 * Mirrors C# HaloApiResultContainer<T, TRawResponseContainer>.
 *
 * All API methods return this type, providing both the deserialized
 * result data and raw response information for debugging.
 *
 * @template T - The type of the expected response data
 *
 * @example
 * ```typescript
 * const result = await client.stats.getMatchStats('match-id');
 * if (isSuccess(result)) {
 *   console.log(result.result.matchInfo);
 * } else {
 *   console.error(`Error ${result.response.code}: ${result.response.message}`);
 * }
 * ```
 */
export interface HaloApiResult<T> {
  /** The deserialized response data, or null if request failed */
  result: T | null;
  /** Raw response information including headers, status, and body */
  response: RawResponse;
}

/**
 * Raw response container with request/response diagnostics.
 * Equivalent to C# RawResponseContainer.
 *
 * Contains HTTP-level details useful for debugging API issues.
 * Request details are only populated when `includeRawResponses` is enabled.
 */
export interface RawResponse {
  /** HTTP status code (e.g., 200, 404, 500) */
  code: number;
  /** Response body text or error message */
  message?: string;
  /** Full request URL including query parameters */
  requestUrl?: string;
  /** HTTP method used (GET, POST, PUT, DELETE, PATCH) */
  requestMethod?: string;
  /** Request headers that were sent */
  requestHeaders?: Record<string, string>;
  /** Request body that was sent */
  requestBody?: string;
  /** Response headers received from the server */
  responseHeaders?: Record<string, string>;
}

/**
 * Type guard to check if an API result was successful.
 * A successful result has a non-null result and a 2xx status code.
 *
 * @param result - The API result to check
 * @returns true if the result was successful with valid data
 *
 * @example
 * ```typescript
 * const result = await client.stats.getMatchStats('match-id');
 * if (isSuccess(result)) {
 *   // TypeScript now knows result.result is non-null
 *   console.log(result.result.matchId);
 * }
 * ```
 */
export function isSuccess<T>(
  result: HaloApiResult<T>
): result is HaloApiResult<T> & { result: T } {
  return (
    result.result !== null &&
    result.response.code >= 200 &&
    result.response.code < 300
  );
}

/**
 * Type guard to check if an API result represents a "not modified" response.
 * This occurs when using ETag-based caching and the server returns 304.
 *
 * @param result - The API result to check
 * @returns true if the response was a 304 Not Modified
 */
export function isNotModified<T>(result: HaloApiResult<T>): boolean {
  return result.response.code === 304;
}

/**
 * Type guard to check if an API result represents a client error (4xx).
 *
 * @param result - The API result to check
 * @returns true if the response was a 4xx client error
 */
export function isClientError<T>(result: HaloApiResult<T>): boolean {
  return result.response.code >= 400 && result.response.code < 500;
}

/**
 * Type guard to check if an API result represents a server error (5xx).
 *
 * @param result - The API result to check
 * @returns true if the response was a 5xx server error
 */
export function isServerError<T>(result: HaloApiResult<T>): boolean {
  return result.response.code >= 500;
}
