import type { MatchInfo } from './match-info';
import type { Player } from './player';

/**
 * Links for pagination in match history.
 */
export interface MatchLinks {
  /** Link to next page of results */
  next?: string;
  /** Link to previous page of results */
  prev?: string;
}

/**
 * Individual match record in match history.
 */
export interface PlayerMatchHistoryRecord {
  /** Unique match identifier */
  matchId?: string;
  /** Match information */
  matchInfo?: MatchInfo;
  /** Last team the player was on */
  lastTeamId?: number;
  /** Match outcome for this player */
  outcome?: number;
  /** Final rank/placement */
  rank?: number;
  /** Whether player was present at end */
  presentAtEnd?: boolean;
  /** Player-specific data for this match */
  player?: Player;
}

/**
 * Response container for match history queries.
 */
export interface MatchHistoryResponse {
  /** Starting index of results */
  start?: number;
  /** Number of results requested */
  count?: number;
  /** Number of results returned */
  resultCount?: number;
  /** List of match records */
  results?: PlayerMatchHistoryRecord[];
  /** Pagination links */
  links?: MatchLinks;
}

/**
 * Player match count summary.
 */
export interface PlayerMatchCount {
  /** Player identifier */
  playerId?: string;
  /** Total custom game count */
  customMatchesPlayedCount?: number;
  /** Total matchmade games count */
  matchmadeMatchesPlayedCount?: number;
  /** Total local games count */
  localMatchesPlayedCount?: number;
}
