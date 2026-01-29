/**
 * Spartan token response from the authentication service.
 */
export interface SpartanToken {
  /** The Spartan token string */
  token?: string;
  /** Expiration time (ISO 8601) */
  expiresUtc?: string;
  /** Token duration */
  tokenDuration?: string;
}

/**
 * Spartan token proof for authentication requests.
 */
export interface SpartanTokenProof {
  /** The token value */
  token?: string;
  /** Token type (e.g., 'Xbox_XSTSv3') */
  tokenType?: string;
}

/**
 * Request body for obtaining a Spartan token.
 */
export interface SpartanTokenRequest {
  /** Target audience */
  audience?: string;
  /** Minimum version */
  minVersion?: string;
  /** Token proofs */
  proof?: SpartanTokenProof[];
}
