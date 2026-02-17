import type { DisplayString } from '../economy/inventory';

/**
 * Career rank information.
 */
export interface CareerRank {
  /** Rank number */
  Rank?: number;
  /** Title of the rank */
  Title?: DisplayString;
  /** Subtitle */
  Subtitle?: DisplayString;
  /** Large icon path */
  LargeIconPath?: string;
  /** Small icon path */
  SmallIconPath?: string;
  /** Adornment icon path */
  AdornmentIconPath?: string;
  /** XP required to reach this rank */
  XpRequired?: number;
  /** Cumulative XP at this rank */
  CumulativeXp?: number;
  /** Grade within the rank */
  Grade?: number;
  /** Tier within the rank */
  Tier?: number;
}

/**
 * Container for career ranks.
 */
export interface CareerTrackContainer {
  /** Career path identifier */
  CareerPathId?: string;
  /** List of career ranks */
  Ranks?: CareerRank[];
  /** Maximum rank */
  MaxRank?: number;
}

/**
 * Player's career rank result.
 */
export interface PlayerCareerRankResult {
  /** Player identifier */
  PlayerId?: string;
  /** Current career rank */
  CurrentRank?: number;
  /** Current XP */
  CurrentXp?: number;
  /** XP to next rank */
  XpToNextRank?: number;
  /** Result code */
  ResultCode?: number;
}

/**
 * Container for player career rank results.
 */
export interface RewardTrackResultContainer {
  /** Career path identifier */
  CareerPathId?: string;
  /** Results for each player */
  Value?: PlayerCareerRankResult[];
}

/**
 * Match progression (post-game rewards).
 */
export interface MatchProgression {
  /** Player identifier */
  PlayerId?: string;
  /** Match identifier */
  MatchId?: string;
  /** Challenges progressed */
  ChallengeProgress?: ChallengeProgress[];
  /** XP earned breakdown */
  XpBreakdown?: XpBreakdown;
  /** Career rank progression */
  CareerRankProgress?: CareerRankProgress;
}

/**
 * Challenge progress from a match.
 */
export interface ChallengeProgress {
  /** Challenge identifier */
  ChallengeId?: string;
  /** Progress made */
  Progress?: number;
  /** Whether challenge was completed */
  Completed?: boolean;
}

/**
 * XP breakdown from a match.
 */
export interface XpBreakdown {
  /** Base XP from match */
  MatchXp?: number;
  /** XP from medals */
  MedalXp?: number;
  /** XP from challenges */
  ChallengeXp?: number;
  /** Boost XP */
  BoostXp?: number;
  /** Total XP */
  TotalXp?: number;
}

/**
 * Career rank progress from a match.
 */
export interface CareerRankProgress {
  /** Rank before match */
  PreviousRank?: number;
  /** Rank after match */
  CurrentRank?: number;
  /** XP earned */
  XpEarned?: number;
  /** Whether player ranked up */
  RankedUp?: boolean;
}
