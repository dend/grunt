import { CacheManager } from '../../core/cache';
import { RetryPolicy } from '../../core/http';
import type { HaloApiResult } from '../../models/common/api-result';
import {
  ApiContentType,
  getContentTypeHeader,
} from '../../models/common/api-content-type';
import type { RequestOptions } from '../../models/common/client-options';
import {
  HEADERS,
  DEFAULT_CACHE_TTL_MS,
  DEFAULT_MAX_RETRIES,
} from '../../utils/constants';

/**
 * Abstract base class for all API clients.
 *
 * Provides shared functionality for HTTP execution, ETag-based caching,
 * and retry logic. Both HaloInfiniteClient and WaypointClient extend this.
 *
 * Mirrors the C# ClientBase implementation with:
 * - Shared HttpClient-equivalent fetch function
 * - ETag-based caching with TTL expiration
 * - Automatic retry for transient failures
 * - Support for multiple content types
 * - Comprehensive error handling with raw response capture
 */
export abstract class ClientBase {
  /**
   * Fetch function used for HTTP requests.
   * Can be overridden for testing or custom environments.
   */
  protected readonly fetchFn: typeof fetch;

  /**
   * Cache manager for ETag-based response caching.
   */
  protected readonly cache: CacheManager;

  /**
   * Retry policy for handling transient failures.
   */
  protected readonly retryPolicy: RetryPolicy;

  /**
   * Spartan token for API authentication.
   * Obtained through Xbox Live XSTS token exchange.
   */
  spartanToken: string = '';

  /**
   * Xbox User ID in numeric format.
   * Used for player-specific API requests.
   */
  xuid: string = '';

  /**
   * Clearance/flight token for accessing flighted content.
   */
  clearanceToken: string = '';

  /**
   * Whether to include raw request/response data in results.
   * Useful for debugging but adds overhead.
   */
  includeRawResponses: boolean = false;

  /**
   * Custom User-Agent header value.
   */
  userAgent: string = '';

  /**
   * Creates a new ClientBase instance.
   *
   * @param options - Configuration options
   */
  constructor(options?: {
    fetchFn?: typeof fetch;
    cacheTtlMs?: number;
    maxRetries?: number;
  }) {
    this.fetchFn = options?.fetchFn ?? globalThis.fetch.bind(globalThis);
    this.cache = new CacheManager(options?.cacheTtlMs ?? DEFAULT_CACHE_TTL_MS);
    this.retryPolicy = new RetryPolicy({
      maxRetries: options?.maxRetries ?? DEFAULT_MAX_RETRIES,
    });
  }

  /**
   * Execute an API request with caching, retry, and response handling.
   *
   * This is the core method that all module methods call. It handles:
   * - Building the request with appropriate headers
   * - ETag-based caching and 304 Not Modified responses
   * - Retry logic for transient failures
   * - Response deserialization
   * - Raw response capture (when enabled)
   *
   * @template T - Expected response data type
   * @param endpoint - Full URL for the request
   * @param method - HTTP method to use
   * @param options - Request configuration options
   * @returns Promise resolving to the API result
   */
  async executeRequest<T>(
    endpoint: string,
    method: 'GET' | 'POST' | 'PUT' | 'PATCH' | 'DELETE',
    options: RequestOptions = {}
  ): Promise<HaloApiResult<T>> {
    const result: HaloApiResult<T> = {
      result: null,
      response: { code: 0 },
    };

    // Build the request
    const headers = this.buildHeaders(options);
    const requestInit: RequestInit = {
      method,
      headers,
    };

    // Add body for POST/PUT/PATCH
    if (options.body) {
      requestInit.body = options.body;
    }

    // Capture request details if enabled
    if (this.includeRawResponses) {
      result.response.requestUrl = endpoint;
      result.response.requestMethod = method;
      result.response.requestHeaders = Object.fromEntries(headers.entries());
      if (typeof options.body === 'string') {
        result.response.requestBody = options.body;
      }
    }

    try {
      // Check cache for GET requests
      const cacheKey = method === 'GET' ? endpoint : null;
      const cached = cacheKey ? this.cache.get(cacheKey) : null;

      // Add ETag header if we have a cached response
      if (cached?.etag) {
        headers.set(HEADERS.IF_NONE_MATCH, cached.etag);
      }

      // Execute request with retry
      const response = await this.retryPolicy.execute(async () => {
        return this.fetchFn(endpoint, requestInit);
      });

      result.response.code = response.status;

      // Capture response headers if enabled
      if (this.includeRawResponses) {
        result.response.responseHeaders = Object.fromEntries(
          response.headers.entries()
        );
      }

      // Handle 304 Not Modified - use cached content
      if (response.status === 304 && cached) {
        result.result = this.deserializeResponse<T>(cached.content);
        result.response.message = '304 Not Modified - using cached response';
        return result;
      }

      // Read response body
      const bodyBuffer = await response.arrayBuffer();
      const bodyBytes = new Uint8Array(bodyBuffer);

      // Cache successful GET responses
      if (cacheKey && response.ok) {
        const etag = response.headers.get(HEADERS.ETAG) ?? undefined;
        this.cache.set(cacheKey, { etag, content: bodyBytes });
      }

      // Capture raw response message
      const bodyText = new TextDecoder().decode(bodyBytes);
      result.response.message = bodyText;

      // Deserialize response
      if (response.ok || options.enforceSuccess !== false) {
        result.result = this.deserializeResponse<T>(bodyBytes);
      }
    } catch (error) {
      result.response.code = 0;
      result.response.message =
        error instanceof Error ? error.message : String(error);
    }

    return result;
  }

  /**
   * Build request headers based on options.
   */
  private buildHeaders(options: RequestOptions): Headers {
    const headers = new Headers();

    // Set default Accept header
    headers.set(HEADERS.ACCEPT, 'application/json');

    // Set Content-Type for requests with body
    if (options.body !== undefined) {
      const contentType = getContentTypeHeader(
        options.contentType ?? ApiContentType.Json
      );
      headers.set(HEADERS.CONTENT_TYPE, contentType);
    }

    // Add Spartan token if requested
    if (options.useSpartanToken !== false && this.spartanToken) {
      headers.set(HEADERS.SPARTAN_AUTH, this.spartanToken);
    }

    // Add clearance token if requested
    if (options.useClearance && this.clearanceToken) {
      headers.set(HEADERS.CLEARANCE, this.clearanceToken);
    }

    // Add User-Agent if set
    if (this.userAgent) {
      headers.set(HEADERS.USER_AGENT, this.userAgent);
    }

    // Add any custom headers
    if (options.customHeaders) {
      for (const [key, value] of Object.entries(options.customHeaders)) {
        headers.set(key, value);
      }
    }

    return headers;
  }

  /**
   * Deserialize response bytes to the expected type.
   */
  private deserializeResponse<T>(data: Uint8Array): T | null {
    if (data.length === 0) {
      return null;
    }

    const text = new TextDecoder().decode(data);

    // Handle boolean responses
    if (text === 'true') {
      return true as unknown as T;
    }
    if (text === 'false') {
      return false as unknown as T;
    }

    // Try to parse as JSON
    try {
      return JSON.parse(text) as T;
    } catch {
      // If not valid JSON, return as string
      return text as unknown as T;
    }
  }

  /**
   * Clear the response cache.
   * Useful when you know data has changed.
   */
  clearCache(): void {
    this.cache.clear();
  }
}
