import type { DisplayString } from '../economy/inventory';

/**
 * Career rank information.
 */
export interface CareerRank {
  /** Rank number */
  rank?: number;
  /** Title of the rank */
  title?: DisplayString;
  /** Subtitle */
  subtitle?: DisplayString;
  /** Large icon path */
  largeIconPath?: string;
  /** Small icon path */
  smallIconPath?: string;
  /** Adornment icon path */
  adornmentIconPath?: string;
  /** XP required to reach this rank */
  xpRequired?: number;
  /** Cumulative XP at this rank */
  cumulativeXp?: number;
  /** Grade within the rank */
  grade?: number;
  /** Tier within the rank */
  tier?: number;
}

/**
 * Container for career ranks.
 */
export interface CareerTrackContainer {
  /** Career path identifier */
  careerPathId?: string;
  /** List of career ranks */
  ranks?: CareerRank[];
  /** Maximum rank */
  maxRank?: number;
}

/**
 * Player's career rank result.
 */
export interface PlayerCareerRankResult {
  /** Player identifier */
  playerId?: string;
  /** Current career rank */
  currentRank?: number;
  /** Current XP */
  currentXp?: number;
  /** XP to next rank */
  xpToNextRank?: number;
  /** Result code */
  resultCode?: number;
}

/**
 * Container for player career rank results.
 */
export interface RewardTrackResultContainer {
  /** Career path identifier */
  careerPathId?: string;
  /** Results for each player */
  value?: PlayerCareerRankResult[];
}

/**
 * Match progression (post-game rewards).
 */
export interface MatchProgression {
  /** Player identifier */
  playerId?: string;
  /** Match identifier */
  matchId?: string;
  /** Challenges progressed */
  challengeProgress?: ChallengeProgress[];
  /** XP earned breakdown */
  xpBreakdown?: XpBreakdown;
  /** Career rank progression */
  careerRankProgress?: CareerRankProgress;
}

/**
 * Challenge progress from a match.
 */
export interface ChallengeProgress {
  /** Challenge identifier */
  challengeId?: string;
  /** Progress made */
  progress?: number;
  /** Whether challenge was completed */
  completed?: boolean;
}

/**
 * XP breakdown from a match.
 */
export interface XpBreakdown {
  /** Base XP from match */
  matchXp?: number;
  /** XP from medals */
  medalXp?: number;
  /** XP from challenges */
  challengeXp?: number;
  /** Boost XP */
  boostXp?: number;
  /** Total XP */
  totalXp?: number;
}

/**
 * Career rank progress from a match.
 */
export interface CareerRankProgress {
  /** Rank before match */
  previousRank?: number;
  /** Rank after match */
  currentRank?: number;
  /** XP earned */
  xpEarned?: number;
  /** Whether player ranked up */
  rankedUp?: boolean;
}
