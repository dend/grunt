/**
 * Configuration options for the retry policy.
 */
export interface RetryOptions {
  /**
   * Maximum number of retry attempts after the initial request.
   *
   * @default 3
   */
  maxRetries: number;

  /**
   * Delay between retries in milliseconds.
   * Array index corresponds to retry attempt (0-indexed).
   *
   * @default [200, 500, 1000]
   */
  retryDelays: number[];
}

/**
 * Default retry configuration matching the C# implementation.
 */
const DEFAULT_RETRY_OPTIONS: RetryOptions = {
  maxRetries: 3,
  retryDelays: [200, 500, 1000],
};

/**
 * HTTP status codes that indicate transient errors worth retrying.
 */
const TRANSIENT_STATUS_CODES = new Set([
  408, // Request Timeout
  429, // Too Many Requests
  500, // Internal Server Error
  502, // Bad Gateway
  503, // Service Unavailable
  504, // Gateway Timeout
]);

/**
 * Retry policy with exponential backoff for transient failures.
 *
 * Implements the same retry strategy as the C# ClientBase:
 * - Retries on transient HTTP errors (5xx, 408, 429)
 * - Retries on network failures (fetch TypeError)
 * - Uses configurable delays between attempts
 *
 * @example
 * ```typescript
 * const policy = new RetryPolicy({ maxRetries: 3 });
 *
 * const response = await policy.execute(async () => {
 *   return fetch('https://api.example.com/data');
 * });
 * ```
 */
export class RetryPolicy {
  private readonly options: RetryOptions;

  /**
   * Creates a new retry policy.
   *
   * @param options - Configuration options
   */
  constructor(options: Partial<RetryOptions> = {}) {
    this.options = {
      maxRetries: options.maxRetries ?? DEFAULT_RETRY_OPTIONS.maxRetries,
      retryDelays: options.retryDelays ?? DEFAULT_RETRY_OPTIONS.retryDelays,
    };
  }

  /**
   * Execute a function with retry logic.
   *
   * The function is called immediately. If it fails with a retryable error,
   * it will be retried up to maxRetries times with delays between attempts.
   *
   * @param fn - Async function that returns a Response
   * @returns The successful Response
   * @throws The last error if all retries are exhausted
   */
  async execute(fn: () => Promise<Response>): Promise<Response> {
    let lastError: Error | null = null;

    for (let attempt = 0; attempt <= this.options.maxRetries; attempt++) {
      try {
        const response = await fn();

        // Check if response indicates a transient error
        if (this.isTransientError(response)) {
          lastError = new Error(
            `HTTP ${response.status}: ${response.statusText}`
          );

          if (attempt < this.options.maxRetries) {
            await this.delay(attempt);
            continue;
          }
        }

        return response;
      } catch (error) {
        // Network errors are retryable
        lastError = error instanceof Error ? error : new Error(String(error));

        if (!this.isRetryableError(error) || attempt >= this.options.maxRetries) {
          throw lastError;
        }

        await this.delay(attempt);
      }
    }

    throw lastError ?? new Error('Retry exhausted');
  }

  /**
   * Check if a response indicates a transient server error.
   */
  private isTransientError(response: Response): boolean {
    return TRANSIENT_STATUS_CODES.has(response.status);
  }

  /**
   * Check if an error is worth retrying.
   * Network errors (TypeError from fetch) are retryable.
   */
  private isRetryableError(error: unknown): boolean {
    // Fetch throws TypeError for network failures
    if (error instanceof TypeError) {
      return true;
    }
    return false;
  }

  /**
   * Wait for the appropriate delay before the next retry.
   */
  private delay(attempt: number): Promise<void> {
    const delays = this.options.retryDelays;
    const delayMs = delays[attempt] ?? delays[delays.length - 1] ?? 1000;
    return new Promise((resolve) => setTimeout(resolve, delayMs));
  }
}
