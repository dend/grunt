import type { DisplayString, InventoryAmount } from '../economy/inventory';

/**
 * Reward structure for challenges and events.
 */
export interface Reward {
  /** Event XP awarded */
  eventXp?: number;
  /** Operation XP awarded */
  operationXp?: number;
  /** Operation experience */
  operationExperience?: number;
  /** Soft experience (Spartan Points) */
  softExperience?: number;
  /** Inventory items rewarded */
  inventoryRewards?: InventoryAmount[];
  /** Inventory items */
  inventoryItems?: unknown[];
  /** Tracking identifier */
  trackingId?: string;
  /** Currencies awarded */
  currencies?: unknown[];
  /** Reward track progression */
  rewardTrackProgression?: RewardTrack[];
}

/**
 * Challenge definition.
 */
export interface Challenge {
  /** Challenge description */
  description?: DisplayString;
  /** Difficulty level */
  difficulty?: string;
  /** Challenge category */
  category?: string;
  /** Primary reward */
  reward?: Reward;
  /** Secondary reward */
  secondaryReward?: Reward;
  /** Threshold for success */
  thresholdForSuccess?: number;
  /** Challenge title */
  title?: DisplayString;
  /** Type icon path */
  typeIconPath?: string;
  /** Whether this is a user event challenge */
  isUserEvent?: boolean;
  /** Challenge path */
  path?: string;
  /** Current progress */
  progress?: number;
  /** Challenge identifier */
  id?: string;
  /** Whether the challenge can be rerolled */
  canReroll?: boolean;
}

/**
 * Challenge deck (collection of challenges).
 */
export interface ChallengeDeck {
  /** Deck identifier */
  id?: string;
  /** Path to the deck */
  path?: string;
  /** Deck title */
  title?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Challenges in this deck */
  challenges?: Challenge[];
}

/**
 * Challenge deck definition from CMS.
 */
export interface ChallengeDeckDefinition {
  /** Deck identifier */
  id?: string;
  /** Deck path */
  path?: string;
  /** Deck title */
  title?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Image path */
  imagePath?: string;
  /** Whether deck is visible */
  isVisible?: boolean;
}

/**
 * Response for challenge decks query.
 */
export interface ChallengeDecksResponse {
  /** Active challenge decks */
  activeDecks?: ChallengeDeck[];
  /** Upcoming decks */
  upcomingDecks?: ChallengeDeck[];
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
  trackId?: string;
  /** Track path */
  path?: string;
  /** Display title */
  title?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Image path */
  imagePath?: string;
  /** Track type */
  type?: string;
  /** Start date (ISO 8601) */
  startDate?: string;
  /** End date (ISO 8601) */
  endDate?: string;
}

/**
 * Operation reward track snapshot.
 */
export interface OperationRewardTrackSnapshot {
  /** Track identifier */
  trackId?: string;
  /** Current rank */
  rank?: number;
  /** Current XP */
  xp?: number;
  /** Total XP earned */
  totalXpEarned?: number;
  /** Premium status */
  isPremium?: boolean;
}
