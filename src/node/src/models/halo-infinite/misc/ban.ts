/**
 * Ban result for a player.
 */
export interface BanResult {
  /** Bans currently in effect */
  BansInEffect?: unknown[];
}

/**
 * Target ban summary for a single player.
 */
export interface TargetBanSummary {
  /** Target player information */
  [key: string]: unknown;
}

/**
 * Container for ban query results.
 */
export interface BansSummaryQueryResult {
  /** List of ban summaries */
  Results?: TargetBanSummary[];
  /** Pagination/reference links */
  Links?: Record<string, unknown>;
}
