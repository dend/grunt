import type { DisplayString, InventoryAmount, CurrencyAmount } from '../economy/inventory';
import type { RewardTrack } from './challenges';

/**
 * Container for reward items awarded to the player.
 */
export interface RewardContainer {
  /** Inventory rewards associated with an action and/or unlock */
  InventoryRewards?: InventoryAmount[];
  /** Currency rewards associated with an action and/or unlock */
  CurrencyRewards?: CurrencyAmount[];
}

/**
 * Career rank information.
 */
export interface CareerRank {
  /** Rank ID */
  Rank?: number;
  /** Free rewards granted for rank */
  FreeRewards?: RewardContainer;
  /** Paid rewards granted for rank */
  PaidRewards?: RewardContainer;
  /** Experience required for rank */
  XpRequiredForRank?: number;
  /** Rank title */
  RankTitle?: DisplayString;
  /** Rank subtitle */
  RankSubTitle?: DisplayString;
  /** Rank tier */
  RankTier?: DisplayString;
  /** Path to the rank icon */
  RankIcon?: string;
  /** Path to the large rank icon */
  RankLargeIcon?: string;
  /** Rank adornment icon */
  RankAdornmentIcon?: string;
  /** Tier type */
  TierType?: string;
  /** Rank grade */
  RankGrade?: number;
}

/**
 * Container for career track information.
 */
export interface CareerTrackContainer {
  /** Career track ID */
  TrackId?: string;
  /** List of career ranks */
  Ranks?: CareerRank[];
  /** Name of the track */
  Name?: DisplayString;
  /** Description for the track */
  Description?: DisplayString;
  /** Operation number */
  OperationNumber?: number;
  /** Date range for the track */
  DateRange?: DisplayString;
  /** Whether the career track is a ritual */
  IsRitual?: boolean;
  /** Summary image path */
  SummaryImagePath?: string;
  /** Track week number */
  WeekNumber?: number;
  /** Volume of XP granted per rank in the track */
  XpPerRank?: number;
  /** Background image path */
  BackgroundImagePath?: string;
}

/**
 * Individual reward track result for a player.
 */
export interface RewardTrackResult {
  /** Reward track ID */
  Id?: string;
  /** Reward track query result code */
  ResultCode?: string;
  /** Reward track */
  Result?: RewardTrack;
}

/**
 * Container for reward track query results.
 */
export interface RewardTrackResultContainer {
  /** List of reward tracks */
  RewardTracks?: RewardTrackResult[];
}

/**
 * State tracker for challenge progress.
 */
export interface ChallengeProgressState {
  /** Path to the challenge */
  Path?: string;
  /** Challenge ID */
  Id?: string;
  /** Previous progress for the challenge */
  PreviousProgress?: number;
  /** Current progress for the challenge */
  Progress?: number;
}

/**
 * Container for match progression information.
 */
export interface MatchProgression {
  /** Clearance ID */
  ClearanceId?: string;
  /** Reward ID */
  RewardId?: string;
  /** Challenge progress state after the match */
  ChallengeProgressState?: ChallengeProgressState[];
  /** List of reward IDs */
  RewardIds?: string[];
  /** Custom XP eligibility reason */
  CustomXpEligibilityReason?: string;
}
