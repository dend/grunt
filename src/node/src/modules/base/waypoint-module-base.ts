import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { WAYPOINT_ENDPOINTS } from '../../endpoints/halo-core-endpoints';

/**
 * Abstract base class for Waypoint API modules.
 *
 * Similar to ModuleBase but uses Waypoint-specific endpoints
 * instead of the Halo Infinite service domain.
 */
export abstract class WaypointModuleBase {
  /**
   * Reference to the parent client for making HTTP requests.
   */
  protected readonly client: ClientBase;

  /**
   * Creates a new Waypoint module instance.
   *
   * @param client - Parent client instance
   */
  constructor(client: ClientBase) {
    this.client = client;
  }

  /**
   * Build a Waypoint API URL.
   *
   * @param path - API path starting with /
   * @returns Full HTTPS URL
   */
  protected buildUrl(path: string): string {
    return `https://${WAYPOINT_ENDPOINTS.API_DOMAIN}${path}`;
  }

  /**
   * Build a Waypoint web URL.
   *
   * @param path - Path starting with /
   * @returns Full HTTPS URL
   */
  protected buildWebUrl(path: string): string {
    return `https://${WAYPOINT_ENDPOINTS.WEB_DOMAIN}${path}`;
  }

  /**
   * Execute a GET request to the Waypoint API.
   */
  protected get<T>(
    path: string,
    options: {
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'GET', {
      useSpartanToken: options.useSpartanToken ?? true,
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Execute a POST request to the Waypoint API.
   */
  protected post<T>(
    path: string,
    body?: string,
    options: {
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'POST', {
      useSpartanToken: options.useSpartanToken ?? true,
      body,
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Execute a POST request with a JSON body.
   */
  protected postJson<T, TBody>(
    path: string,
    body: TBody,
    options: {
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'POST', {
      useSpartanToken: options.useSpartanToken ?? true,
      body: JSON.stringify(body),
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Execute a PUT request with a JSON body.
   */
  protected putJson<T, TBody>(
    path: string,
    body: TBody,
    options: {
      useSpartanToken?: boolean;
      customHeaders?: Record<string, string>;
    } = {}
  ): Promise<HaloApiResult<T>> {
    return this.client.executeRequest<T>(this.buildUrl(path), 'PUT', {
      useSpartanToken: options.useSpartanToken ?? true,
      body: JSON.stringify(body),
      customHeaders: options.customHeaders,
    });
  }

  /**
   * Validate that a parameter is not null or undefined.
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
   * Validate that a string is not empty.
   */
  protected assertNotEmpty(value: string, paramName: string): void {
    if (!value || value.trim().length === 0) {
      throw new Error(`${paramName} cannot be empty`);
    }
  }
}
