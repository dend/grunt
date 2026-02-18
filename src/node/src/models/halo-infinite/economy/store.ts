import type { DisplayString, CurrencyAmount, PlayerItem } from './inventory';
import type { RewardTrack } from '../progression/challenges';
import type { Reward } from '../progression/challenges';

/**
 * Store offering display metadata.
 */
export interface StoreOffering {
  /** Display title */
  Title?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Quality tier */
  Quality?: string;
  /** Width hint for UI display */
  WidthHint?: number;
  /** Height hint for UI display */
  HeightHint?: number;
  /** Flair text */
  FlairText?: DisplayString;
  /** Flair icon path */
  FlairIconPath?: string;
  /** Flair background path */
  FlairBackgroundPath?: string;
  /** Object image path */
  ObjectImagePath?: string;
  /** HCS team index */
  HCSTeamIndex?: number;
  /** Store tile type */
  StoreTileType?: string;
  /** Whether offering has gleam effect */
  HasGleam?: boolean;
  /** Whether offering is on sale */
  IsOnSale?: boolean;
  /** Sale percentage */
  SalePercentage?: number;
  /** Whether this is an event item */
  IsEventItem?: boolean;
  /** Whether this is new */
  IsNew?: boolean;
  /** Flair background color override (RGB) */
  FlairBackgroundColorOverrideRGB?: string;
  /** Flair text color override (RGB) */
  FlairTextColorOverrideRGB?: string;
  /** Title color override (RGB) */
  TitleColorOverrideRGB?: string;
  /** Price color override (RGB) */
  PriceColorOverrideRGB?: string;
  /** Price shadow color override (RGB) */
  PriceShadowColorOverrideRGB?: string;
  /** Whether offering has flair */
  HasFlair?: boolean;
  /** Season number */
  SeasonNumber?: number;
}

/**
 * Store offering with items and pricing.
 */
export interface Offering {
  /** Offering identifier */
  OfferingId?: string;
  /** Offering display path */
  OfferingDisplayPath?: string;
  /** Offering expiration date (ISO 8601) */
  OfferingExpirationDate?: string;
  /** Items included in the offering */
  IncludedItems?: PlayerItem[];
  /** Prices for the offering */
  Prices?: Price[];
  /** Included currencies */
  IncludedCurrencies?: CurrencyAmount[];
  /** Included reward tracks */
  IncludedRewardTracks?: string[];
  /** Boost path */
  BoostPath?: string;
  /** Operation XP */
  OperationXp?: number;
  /** Event XP */
  EventXp?: number;
  /** Match boosts */
  MatchBoosts?: unknown;
  /** Reward track adjustments */
  RewardTrackAdjustments?: RewardTrackAdjustment[];
}

/**
 * Price in a specific currency.
 */
export interface Price {
  /** Cost amount */
  Cost?: number;
  /** Currency path */
  CurrencyPath?: string;
}

/**
 * Reward track adjustment.
 */
export interface RewardTrackAdjustment {
  /** Granted XP */
  GrantedXp?: number;
  /** Reward track path */
  RewardTrackPath?: string;
}

/**
 * Store item container.
 */
export interface StoreItem {
  /** Store identifier */
  StoreId?: string;
  /** Storefront expiration date (ISO 8601) */
  StorefrontExpirationDate?: string;
  /** Storefront display path */
  StorefrontDisplayPath?: string;
  /** List of offerings */
  Offerings?: Offering[];
}

/**
 * Active boost information.
 */
export interface ActiveBoost {
  /** Expiration date (ISO 8601) */
  ExpirationDate?: string;
  /** Boost state */
  State?: string;
  /** Boost path */
  BoostPath?: string;
  /** Effective multiplier */
  EffectiveMultiplier?: number;
  /** Number of matches */
  Matches?: number;
}

/**
 * Container for active boosts.
 */
export interface ActiveBoostsContainer {
  /** List of active boosts */
  Boosts?: ActiveBoost[];
}

/**
 * Awarded reward entry.
 */
export interface AwardedReward {
  /** Reward details */
  Reward?: Reward;
  /** Award status */
  Status?: string;
}

/**
 * Reward summary information.
 */
export interface RewardSummary {
  /** Updated reward tracks */
  UpdatedRewardTracks?: RewardTrack[];
  /** Awarded rewards */
  AwardedRewards?: AwardedReward[];
  /** Granted currencies */
  GrantedCurrencies?: CurrencyAmount[];
  /** Granted items */
  GrantedItems?: PlayerItem[];
}

/**
 * Player state snapshot.
 */
export interface PlayerState {
  /** Reward tracks */
  RewardTracks?: RewardTrack[];
  /** Item balances */
  ItemBalances?: unknown[];
  /** Currency balances */
  CurrencyBalances?: CurrencyAmount[];
  /** Whether refresh is needed */
  RefreshNeeded?: boolean;
  /** Active boosts */
  Boosts?: unknown[];
}

/**
 * Reward snapshot.
 */
export interface RewardSnapshot {
  /** Rewards summary */
  RewardsSummary?: RewardSummary;
  /** Player state */
  PlayerState?: PlayerState;
}
