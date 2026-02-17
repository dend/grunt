/**
 * Ban result for a player.
 */
export interface BanResult {
  /** Player identifier */
  PlayerId?: string;
  /** Whether player is banned */
  IsBanned?: boolean;
  /** Ban reason */
  Reason?: string;
  /** Ban expiration (ISO 8601) */
  ExpiresAt?: string;
  /** Severity level */
  Severity?: string;
}

/**
 * Container for ban query results.
 */
export interface BansSummaryQueryResult {
  /** List of ban results */
  Results?: BanResult[];
}
