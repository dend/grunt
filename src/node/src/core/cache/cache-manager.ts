import { LRUCache } from 'lru-cache';
import type { CachedResponse } from './cached-response';

/**
 * Default cache TTL: 60 minutes.
 */
const DEFAULT_TTL_MS = 60 * 60 * 1000;

/**
 * Maximum number of cached entries.
 */
const DEFAULT_MAX_SIZE = 1000;

/**
 * ETag-based cache manager with TTL expiration.
 *
 * Implements the same caching strategy as the C# ClientBase:
 * - Stores responses with their ETag values
 * - Enables 304 Not Modified responses
 * - Automatically expires entries after TTL
 *
 * Uses LRU (Least Recently Used) eviction when the cache is full.
 *
 * @example
 * ```typescript
 * const cache = new CacheManager(60 * 60 * 1000); // 1 hour TTL
 *
 * // Store a response
 * cache.set('endpoint-key', {
 *   etag: '"abc123"',
 *   content: new TextEncoder().encode('{"data": "value"}'),
 * });
 *
 * // Retrieve later
 * const cached = cache.get('endpoint-key');
 * if (cached?.etag) {
 *   // Use etag for If-None-Match header
 * }
 * ```
 */
export class CacheManager {
  private readonly cache: LRUCache<string, CachedResponse>;

  /**
   * Creates a new cache manager.
   *
   * @param ttlMs - Time-to-live for cached entries in milliseconds
   * @param maxSize - Maximum number of entries to cache
   */
  constructor(
    ttlMs: number = DEFAULT_TTL_MS,
    maxSize: number = DEFAULT_MAX_SIZE
  ) {
    this.cache = new LRUCache<string, CachedResponse>({
      max: maxSize,
      ttl: ttlMs,
      // Calculate size based on content length for memory management
      sizeCalculation: (value) => {
        return value.content.length + (value.etag?.length ?? 0);
      },
      // 50MB max memory for cache
      maxSize: 50 * 1024 * 1024,
    });
  }

  /**
   * Get a cached response if it exists and hasn't expired.
   *
   * @param key - Cache key (typically the request URL)
   * @returns Cached response or null if not found/expired
   */
  get(key: string): CachedResponse | null {
    return this.cache.get(key) ?? null;
  }

  /**
   * Store a response in the cache.
   *
   * @param key - Cache key (typically the request URL)
   * @param response - Response to cache
   */
  set(key: string, response: CachedResponse): void {
    this.cache.set(key, response);
  }

  /**
   * Check if a key exists in the cache without updating its recency.
   *
   * @param key - Cache key to check
   * @returns true if the key exists and hasn't expired
   */
  has(key: string): boolean {
    return this.cache.has(key);
  }

  /**
   * Remove a specific entry from the cache.
   *
   * @param key - Cache key to remove
   * @returns true if an entry was removed
   */
  delete(key: string): boolean {
    return this.cache.delete(key);
  }

  /**
   * Clear all cached entries.
   */
  clear(): void {
    this.cache.clear();
  }

  /**
   * Get the current number of cached entries.
   */
  get size(): number {
    return this.cache.size;
  }

  /**
   * Manually trigger cleanup of expired entries.
   * This is called automatically by the LRU cache, but can be
   * invoked manually if needed.
   */
  prune(): void {
    this.cache.purgeStale();
  }
}
