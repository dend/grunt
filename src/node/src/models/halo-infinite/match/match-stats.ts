import type { MatchInfo } from './match-info';
import type { Player, Team } from './player';

/**
 * Complete match statistics.
 *
 * Contains all information about a completed match including
 * players, teams, and game mode details.
 */
export interface MatchStats {
  /** Unique match identifier */
  matchId?: string;
  /** General match information */
  matchInfo?: MatchInfo;
  /** List of teams in the match */
  teams?: Team[];
  /** List of players in the match */
  players?: Player[];
}
