import type { SpartanToken, SpartanTokenRequest, SpartanTokenProof } from './models';
import { HALO_CORE_ENDPOINTS } from '../endpoints/halo-core-endpoints';
import { HEADERS } from '../utils/constants';

/**
 * Client for Halo authentication operations.
 *
 * Handles the exchange of Xbox Live XSTS tokens for Halo Spartan tokens,
 * which are required for authenticated API calls.
 *
 * @example
 * ```typescript
 * import { HaloAuthenticationClient, HaloInfiniteClient } from '@dendev/grunt';
 *
 * // Create auth client
 * const authClient = new HaloAuthenticationClient();
 *
 * // Exchange XSTS token for Spartan token
 * // (You need to obtain the XSTS token through Xbox Live authentication first)
 * const spartanToken = await authClient.getSpartanToken(xstsToken);
 *
 * if (spartanToken) {
 *   // Use the Spartan token with HaloInfiniteClient
 *   const client = new HaloInfiniteClient({
 *     spartanToken: spartanToken.token!,
 *   });
 * }
 * ```
 */
export class HaloAuthenticationClient {
  private readonly fetchFn: typeof fetch;

  /**
   * Creates a new HaloAuthenticationClient instance.
   *
   * @param fetchFn - Custom fetch implementation (optional)
   */
  constructor(fetchFn?: typeof fetch) {
    this.fetchFn = fetchFn ?? globalThis.fetch.bind(globalThis);
  }

  /**
   * Exchange an XSTS token for a Halo Spartan token.
   *
   * The XSTS token must be obtained through Xbox Live authentication
   * using the Halo Waypoint relying party.
   *
   * @param xstsToken - Xbox Live XSTS token
   * @param version - Spartan token version (4 for Halo Infinite, 3 for Halo 5)
   * @returns Spartan token or null if exchange failed
   *
   * @example
   * ```typescript
   * // Version 4 is for Halo Infinite (default)
   * const spartanToken = await authClient.getSpartanToken(xstsToken);
   *
   * // Version 3 is for Halo 5
   * const halo5Token = await authClient.getSpartanToken(xstsToken, 3);
   * ```
   */
  async getSpartanToken(
    xstsToken: string,
    version: number = 4
  ): Promise<SpartanToken | null> {
    if (!xstsToken) {
      throw new Error('xstsToken is required');
    }

    const tokenProof: SpartanTokenProof = {
      token: xstsToken,
      tokenType: 'Xbox_XSTSv3',
    };

    const requestBody: SpartanTokenRequest = {
      audience: 'urn:343:s3:services',
      minVersion: version.toString(),
      proof: [tokenProof],
    };

    try {
      const response = await this.fetchFn(HALO_CORE_ENDPOINTS.SPARTAN_TOKEN_ENDPOINT, {
        method: 'POST',
        headers: {
          [HEADERS.CONTENT_TYPE]: 'application/json',
          [HEADERS.ACCEPT]: 'application/json',
        },
        body: JSON.stringify(requestBody),
      });

      if (!response.ok) {
        console.error(
          `Failed to get Spartan token: ${response.status} ${response.statusText}`
        );
        return null;
      }

      const data = (await response.json()) as Record<string, unknown>;

      // The API returns the token with a capitalized property name
      return {
        token: (data.SpartanToken ?? data.spartanToken ?? data.token) as string | undefined,
        expiresUtc: (data.ExpiresUtc ?? data.expiresUtc) as string | undefined,
        tokenDuration: (data.TokenDuration ?? data.tokenDuration) as string | undefined,
      };
    } catch (error) {
      console.error('Error getting Spartan token:', error);
      return null;
    }
  }

  /**
   * Check if a Spartan token is expired or about to expire.
   *
   * @param token - The Spartan token to check
   * @param bufferMinutes - Minutes before expiration to consider "about to expire"
   * @returns true if the token is expired or will expire within the buffer time
   */
  isTokenExpired(token: SpartanToken, bufferMinutes: number = 5): boolean {
    if (!token.expiresUtc) {
      return true;
    }

    const expiresAt = new Date(token.expiresUtc);
    const now = new Date();
    const bufferMs = bufferMinutes * 60 * 1000;

    return now.getTime() + bufferMs >= expiresAt.getTime();
  }

  /**
   * Get the Halo Waypoint XSTS relying party URL.
   *
   * Use this when requesting an XSTS token from Xbox Live.
   *
   * @returns The relying party URL
   */
  static getRelyingParty(): string {
    return HALO_CORE_ENDPOINTS.HALO_WAYPOINT_XSTS_RELYING_PARTY;
  }
}
