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
  MatchId?: string;
  /** General match information */
  MatchInfo?: MatchInfo;
  /** List of teams in the match */
  Teams?: Team[];
  /** List of players in the match */
  Players?: Player[];
}
