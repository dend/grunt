/**
 * API content types supported by the Halo API.
 *
 * Most endpoints use JSON, but some UGC-related endpoints
 * use Bond Compact Binary for binary asset data.
 */
export const ApiContentType = {
  /** Standard JSON content type */
  Json: 'json',
  /** Bond compact binary format for binary data */
  BondCompactBinary: 'bond',
} as const;

/**
 * Type representing valid API content types.
 */
export type ApiContentType = (typeof ApiContentType)[keyof typeof ApiContentType];

/**
 * Get the HTTP Content-Type header value for a content type.
 *
 * @param contentType - The content type enum value
 * @returns The corresponding Content-Type header value
 */
export function getContentTypeHeader(contentType: ApiContentType): string {
  switch (contentType) {
    case ApiContentType.Json:
      return 'application/json';
    case ApiContentType.BondCompactBinary:
      return 'application/x-bond-compact-binary';
    default:
      return 'application/json';
  }
}
