/**
 * Ban result for a player.
 */
export interface BanResult {
  /** Player identifier */
  playerId?: string;
  /** Whether player is banned */
  isBanned?: boolean;
  /** Ban reason */
  reason?: string;
  /** Ban expiration (ISO 8601) */
  expiresAt?: string;
  /** Severity level */
  severity?: string;
}

/**
 * Container for ban query results.
 */
export interface BansSummaryQueryResult {
  /** List of ban results */
  results?: BanResult[];
}
