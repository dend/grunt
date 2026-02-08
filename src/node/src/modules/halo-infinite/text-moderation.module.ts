import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';

/**
 * Text moderation key response.
 */
export interface ModerationKey {
  /** The moderation key */
  key?: string;
  /** Expiration time (ISO 8601) */
  expiresAt?: string;
}

/**
 * Signing key for moderation proofs.
 */
export interface SigningKey {
  /** Key type */
  kty?: string;
  /** Key use */
  use?: string;
  /** Key ID */
  kid?: string;
  /** Key modulus (RSA) */
  n?: string;
  /** Key exponent (RSA) */
  e?: string;
  /** X.509 certificate chain */
  x5c?: string[];
}

/**
 * Container for moderation proof signing keys.
 */
export interface ModerationProofKeys {
  /** List of signing keys */
  keys?: SigningKey[];
}

/**
 * Text Moderation module for moderation-related APIs.
 *
 * @example
 * ```typescript
 * // Get moderation key
 * const key = await client.textModeration.getModerationKey();
 *
 * // Get all signing keys
 * const keys = await client.textModeration.getSigningKeys();
 *
 * // Get a specific signing key
 * const key = await client.textModeration.getSigningKey('key-id');
 * ```
 */
export class TextModerationModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.TEXT_ORIGIN);
  }

  /**
   * Get a moderation key for text validation.
   *
   * @returns Moderation key
   */
  getModerationKey(): Promise<HaloApiResult<ModerationKey>> {
    return this.get<ModerationKey>('/hi/moderation/key');
  }

  /**
   * Get a specific moderation proof signing key.
   *
   * @param keyId - Key ID (can be obtained from getSigningKeys)
   * @returns Signing key data
   */
  getSigningKey(keyId: string): Promise<HaloApiResult<SigningKey>> {
    this.assertNotEmpty(keyId, 'keyId');
    return this.get<SigningKey>(`/hi/moderation-proof-keys/${keyId}`, {
      useSpartanToken: false,
    });
  }

  /**
   * Get all available moderation proof signing keys.
   *
   * @returns Container with all signing keys
   */
  getSigningKeys(): Promise<HaloApiResult<ModerationProofKeys>> {
    return this.get<ModerationProofKeys>('/hi/moderation-proof-keys', {
      useSpartanToken: false,
    });
  }
}
