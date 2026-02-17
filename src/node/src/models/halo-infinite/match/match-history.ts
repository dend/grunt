import type { MatchInfo } from './match-info';
import type { Player } from './player';

/**
 * Links for pagination in match history.
 */
export interface MatchLinks {
  /** Link to next page of results */
  Next?: string;
  /** Link to previous page of results */
  Prev?: string;
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
  Outcome?: number;
  /** Final rank/placement */
  Rank?: number;
  /** Whether player was present at end */
  PresentAtEnd?: boolean;
  /** Player-specific data for this match */
  Player?: Player;
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
  /** Player identifier */
  PlayerId?: string;
  /** Total custom game count */
  CustomMatchesPlayedCount?: number;
  /** Total matchmade games count */
  MatchmadeMatchesPlayedCount?: number;
  /** Total local games count */
  LocalMatchesPlayedCount?: number;
}
