import type { MatchInfo } from './match-info';
import type { Outcome } from '../enums/outcome';

/**
 * Links for pagination in match history.
 */
export interface MatchLinks {
  // Empty in C# - properties populated dynamically
}

/**
 * Individual match record in match history.
 */
export interface PlayerMatchHistoryRecord {
  /** Unique match identifier */
  MatchId?: string;
  /** Match information */
  MatchInfo?: MatchInfo;
  /** Last team the player was on */
  LastTeamId?: number;
  /** Match outcome for this player */
  Outcome?: Outcome;
  /** Final rank/placement */
  Rank?: number;
  /** Whether player was present at end of match */
  PresentAtEndOfMatch?: boolean;
}

/**
 * Response container for match history queries.
 */
export interface MatchHistoryResponse {
  /** Starting index of results */
  Start?: number;
  /** Number of results requested */
  Count?: number;
  /** Number of results returned */
  ResultCount?: number;
  /** List of match records */
  Results?: PlayerMatchHistoryRecord[];
  /** Pagination links */
  Links?: MatchLinks;
}

/**
 * Player match count summary.
 */
export interface PlayerMatchCount {
  /** Total custom game count */
  CustomMatchesPlayedCount?: number;
  /** Total matches played count */
  MatchesPlayedCount?: number;
  /** Total matchmade games count */
  MatchmadeMatchesPlayedCount?: number;
  /** Total local games count */
  LocalMatchesPlayedCount?: number;
}
