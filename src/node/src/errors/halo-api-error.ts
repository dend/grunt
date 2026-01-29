import type { RawResponse } from '../models/common/api-result';

/**
 * Custom error class for Halo API errors.
 *
 * Thrown when an API request fails and provides access to
 * the full response details for debugging.
 *
 * @example
 * ```typescript
 * try {
 *   const result = await client.stats.getMatchStats('invalid-id');
 *   if (!isSuccess(result)) {
 *     throw HaloApiError.fromResponse(result.response);
 *   }
 * } catch (error) {
 *   if (error instanceof HaloApiError) {
 *     console.error(`API Error ${error.statusCode}: ${error.message}`);
 *   }
 * }
 * ```
 */
export class HaloApiError extends Error {
  /**
   * HTTP status code from the response.
   */
  readonly statusCode: number;

  /**
   * Full raw response details.
   */
  readonly response: RawResponse;

  /**
   * Creates a new HaloApiError.
   *
   * @param message - Error message
   * @param statusCode - HTTP status code
   * @param response - Full raw response
   */
  constructor(message: string, statusCode: number, response: RawResponse) {
    super(message);
    this.name = 'HaloApiError';
    this.statusCode = statusCode;
    this.response = response;

    // Maintains proper stack trace in V8 environments
    if (Error.captureStackTrace) {
      Error.captureStackTrace(this, HaloApiError);
    }
  }

  /**
   * Creates a HaloApiError from a raw response.
   *
   * @param response - The raw response to convert to an error
   * @returns A new HaloApiError instance
   */
  static fromResponse(response: RawResponse): HaloApiError {
    const message =
      response.message ?? `HTTP Error ${response.code}`;
    return new HaloApiError(message, response.code, response);
  }

  /**
   * Check if this is a client error (4xx).
   */
  get isClientError(): boolean {
    return this.statusCode >= 400 && this.statusCode < 500;
  }

  /**
   * Check if this is a server error (5xx).
   */
  get isServerError(): boolean {
    return this.statusCode >= 500;
  }

  /**
   * Check if this is a "not found" error (404).
   */
  get isNotFound(): boolean {
    return this.statusCode === 404;
  }

  /**
   * Check if this is an "unauthorized" error (401).
   */
  get isUnauthorized(): boolean {
    return this.statusCode === 401;
  }

  /**
   * Check if this is a "forbidden" error (403).
   */
  get isForbidden(): boolean {
    return this.statusCode === 403;
  }

  /**
   * Check if this is a "rate limited" error (429).
   */
  get isRateLimited(): boolean {
    return this.statusCode === 429;
  }
}
