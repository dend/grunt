import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { ApiContentType } from '../../models/common/api-content-type';
import { buildServiceUrl } from '../../endpoints/halo-core-endpoints';

/**
 * Abstract base class for all Halo Infinite API modules.
 *
 * Provides shared functionality for building URLs and making HTTP requests.
 * Each module (Stats, Economy, GameCms, etc.) extends this class and uses
 * the helper methods to interact with its specific API endpoints.
 *
 * @example
 * ```typescript
 * class StatsModule extends ModuleBase {
 *   constructor(client: ClientBase) {
 *     super(client, HALO_CORE_ENDPOINTS.STATS_ORIGIN);
 *   }
 *
 *   async getMatchStats(matchId: string) {
 *     return this.get<MatchStats>(`/hi/matches/${matchId}/stats`);
 *   }
 * }
 * ```
 */
export abstract class ModuleBase {
  /**
   * Reference to the parent client for making HTTP requests.
   */
  protected readonly client: ClientBase;

  /**
   * Service origin for this module (e.g., 'halostats', 'economy').
   */
  protected readonly origin: string;

  /**
   * Creates a new module instance.
   *
   * @param client - Parent client instance
   * @param origin - Service origin for URL building
   */
  constructor(client: ClientBase, origin: string) {
    this.client = client;
    this.origin = origin;
  }

  /**
   * Build a full URL from a relative path using this module's origin.
   *
   * @param path - API path starting with /
   * @returns Full HTTPS URL
   */
  protected buildUrl(path: string): string {
    return buildServiceUrl(this.origin, path);
  }

  /**
   * Execute a GET request to this module's service.
   *
   * @template T - Expected response type
   * @param path - API path (e.g., '/hi/players/xuid(...)/matches')
   * @param options - Request options
   * @returns Promise with the API result
   */
  protected get<T>(
    path: string,
    options: {
      useClearance?: boolean;
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'GET', {
      useSpartanToken: options.useSpartanToken ?? true,
      useClearance: options.useClearance ?? false,
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Execute a GET request to an absolute URL.
   *
   * Used when the URL doesn't follow the standard origin pattern
   * (e.g., blob storage URLs).
   *
   * @template T - Expected response type
   * @param fullUrl - Complete URL to request
   * @param options - Request options
   * @returns Promise with the API result
   */
  protected getFullUrl<T>(
    fullUrl: string,
    options: {
      useClearance?: boolean;
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
      enforceSuccess?: boolean;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(fullUrl, 'GET', {
      useSpartanToken: options.useSpartanToken ?? true,
      useClearance: options.useClearance ?? false,
      customHeaders: options.customHeaders,
      enforceSuccess: options.enforceSuccess ?? true,
    });
  }

  /**
   * Execute a POST request to this module's service.
   *
   * @template T - Expected response type
   * @param path - API path
   * @param body - Optional request body as string
   * @param options - Request options
   * @returns Promise with the API result
   */
  protected post<T>(
    path: string,
    body?: string,
    options: {
      useClearance?: boolean;
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'POST', {
      useSpartanToken: options.useSpartanToken ?? true,
      useClearance: options.useClearance ?? false,
      body,
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Execute a POST request with a JSON body.
   *
   * @template T - Expected response type
   * @template TBody - Request body type
   * @param path - API path
   * @param body - Request body (will be serialized to JSON)
   * @param options - Request options
   * @returns Promise with the API result
   */
  protected postJson<T, TBody>(
    path: string,
    body: TBody,
    options: {
      useClearance?: boolean;
      useSpartanToken?: boolean;
      contentType?: ApiContentType;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'POST', {
      useSpartanToken: options.useSpartanToken ?? true,
      useClearance: options.useClearance ?? false,
      body: JSON.stringify(body),
      contentType: options.contentType ?? ApiContentType.Json,
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Execute a PUT request with a JSON body.
   *
   * @template T - Expected response type
   * @template TBody - Request body type
   * @param path - API path
   * @param body - Request body (will be serialized to JSON)
   * @param options - Request options
   * @returns Promise with the API result
   */
  protected putJson<T, TBody>(
    path: string,
    body: TBody,
    options: {
      useClearance?: boolean;
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'PUT', {
      useSpartanToken: options.useSpartanToken ?? true,
      useClearance: options.useClearance ?? false,
      body: JSON.stringify(body),
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Execute a PATCH request with a JSON body.
   *
   * @template T - Expected response type
   * @template TBody - Request body type
   * @param path - API path
   * @param body - Request body (will be serialized to JSON)
   * @param options - Request options
   * @returns Promise with the API result
   */
  protected patchJson<T, TBody>(
    path: string,
    body: TBody,
    options: {
      useClearance?: boolean;
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'PATCH', {
      useSpartanToken: options.useSpartanToken ?? true,
      useClearance: options.useClearance ?? false,
      body: JSON.stringify(body),
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Execute a DELETE request.
   *
   * @template T - Expected response type
   * @param path - API path
   * @param options - Request options
   * @returns Promise with the API result
   */
  protected delete<T>(
    path: string,
    options: {
      useClearance?: boolean;
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'DELETE', {
      useSpartanToken: options.useSpartanToken ?? true,
      useClearance: options.useClearance ?? false,
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Validate that a parameter is not null or undefined.
   *
   * @param value - Value to check
   * @param paramName - Parameter name for error message
   * @throws Error if value is null or undefined
   */
  protected assertNotNull<T>(
    value: T | null | undefined,
    paramName: string
  ): asserts value is T {
    if (value == null) {
      throw new Error(`${paramName} cannot be null or undefined`);
    }
  }

  /**
   * Validate that a number is within a range.
   *
   * @param value - Value to check
   * @param min - Minimum allowed value (inclusive)
   * @param max - Maximum allowed value (inclusive)
   * @param paramName - Parameter name for error message
   * @throws RangeError if value is out of range
   */
  protected assertRange(
    value: number,
    min: number,
    max: number,
    paramName: string
  ): void {
    if (value < min || value > max) {
      throw new RangeError(`${paramName} must be between ${min} and ${max}`);
    }
  }

  /**
   * Validate that a string is not empty.
   *
   * @param value - Value to check
   * @param paramName - Parameter name for error message
   * @throws Error if value is empty
   */
  protected assertNotEmpty(value: string, paramName: string): void {
    if (!value || value.trim().length === 0) {
      throw new Error(`${paramName} cannot be empty`);
    }
  }
}
