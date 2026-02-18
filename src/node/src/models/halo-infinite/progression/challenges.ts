import type { DisplayString, InventoryAmount } from '../economy/inventory';

/**
 * Reward structure for challenges and events.
 */
export interface Reward {
  /** Event XP awarded */
  EventXp?: number;
  /** Operation XP awarded */
  OperationXp?: number;
  /** Operation experience */
  OperationExperience?: number;
  /** Soft experience (Spartan Points) */
  SoftExperience?: number;
  /** Inventory items rewarded */
  InventoryRewards?: InventoryAmount[];
  /** Inventory items */
  InventoryItems?: unknown[];
  /** Tracking identifier */
  TrackingId?: string;
  /** Currencies awarded */
  Currencies?: unknown[];
  /** Reward track progression */
  RewardTrackProgression?: RewardTrack[];
}

/**
 * Challenge definition.
 */
export interface Challenge {
  /** Challenge description */
  Description?: DisplayString;
  /** Difficulty level */
  Difficulty?: string;
  /** Challenge category */
  Category?: string;
  /** Primary reward */
  Reward?: Reward;
  /** Secondary reward */
  SecondaryReward?: Reward;
  /** Threshold for success */
  ThresholdForSuccess?: number;
  /** Challenge title */
  Title?: DisplayString;
  /** Type icon path */
  TypeIconPath?: string;
  /** Whether this is a user event challenge */
  IsUserEvent?: boolean;
  /** Challenge path */
  Path?: string;
  /** Current progress */
  Progress?: number;
  /** Challenge identifier */
  Id?: string;
  /** Whether the challenge can be rerolled */
  CanReroll?: boolean;
}

/**
 * Challenge deck (collection of challenges).
 */
export interface ChallengeDeck {
  /** Deck identifier */
  Id?: string;
  /** Path to the deck */
  Path?: string;
  /** Active challenges */
  ActiveChallenges?: Challenge[];
  /** Upcoming challenges */
  UpcomingChallenges?: Challenge[];
  /** Deck expiration date (ISO 8601) */
  Expiration?: string;
  /** Completed challenges */
  CompletedChallenges?: Challenge[];
}

/**
 * Challenge deck definition from CMS.
 */
export interface ChallengeDeckDefinition {
  /** Capstone challenge path */
  CapstoneChallengePath?: string;
  /** Deck type */
  DeckType?: string;
  /** Type */
  Type?: string;
}

/**
 * Response for challenge decks query.
 */
export interface ChallengeDecksResponse {
  /** Active challenge decks */
  ActiveDecks?: ChallengeDeck[];
  /** Upcoming decks */
  UpcomingDecks?: ChallengeDeck[];
}

/**
 * Reward track progress measurement.
 */
export interface RewardTrackProgress {
  /** Reward track rank */
  Rank?: number;
  /** Partial progress within rank */
  PartialProgress?: number;
  /** Whether the reward track is owned */
  IsOwned?: boolean;
  /** Whether maximum rank has been reached */
  HasReachedMaxRank?: boolean;
}

/**
 * Reward track (battle pass / operation / career rank).
 */
export interface RewardTrack {
  /** Path to the reward track */
  RewardTrackPath?: string;
  /** Type of reward track */
  TrackType?: string;
  /** Current progress */
  CurrentProgress?: RewardTrackProgress;
  /** Previous progress */
  PreviousProgress?: RewardTrackProgress;
  /** Whether the player owns the reward track */
  IsOwned?: boolean;
  /** Base XP amount */
  BaseXp?: number;
  /** Boost XP amount */
  BoostXp?: number;
  /** Whether maximum rank has been reached */
  HasReachedMaxRank?: boolean;
}

/**
 * Reward track metadata.
 */
export interface RewardTrackMetadata {
  /** Track identifier */
  TrackId?: string;
  /** XP per rank */
  XpPerRank?: number;
  /** Rank snapshots */
  Ranks?: unknown[];
  /** Display name */
  Name?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Operation number */
  OperationNumber?: number;
  /** Date range for the track */
  DateRange?: DisplayString;
  /** Whether the track is a ritual */
  IsRitual?: boolean;
  /** Summary image path */
  SummaryImagePath?: string;
  /** Track week number */
  WeekNumber?: number;
  /** Background image path */
  BackgroundImagePath?: string;
  /** Whether to hide if not owned */
  HideIfNotOwned?: boolean;
}

/**
 * Operation reward track snapshot.
 */
export interface OperationRewardTrackSnapshot {
  /** Active operation reward track path */
  ActiveOperationRewardTrackPath?: string;
  /** Operation reward tracks */
  OperationRewardTracks?: RewardTrack[];
  /** Scheduled operation reward track path */
  ScheduledOperationRewardTrackPath?: string;
}
