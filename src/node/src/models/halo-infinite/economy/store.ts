import type { DisplayString, InventoryAmount } from './inventory';

/**
 * Store offering entry.
 */
export interface StoreOffering {
  /** Offering identifier */
  OfferingId?: string;
  /** Display title */
  Title?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Image path */
  ImagePath?: string;
  /** Price in each currency */
  Prices?: StorePrice[];
  /** Items included */
  IncludedItems?: InventoryAmount[];
  /** Offering type */
  OfferingType?: string;
  /** Start time (ISO 8601) */
  StartDate?: string;
  /** End time (ISO 8601) */
  EndDate?: string;
  /** Whether this is a bundle */
  IsBundle?: boolean;
  /** Whether player owns this */
  IsOwned?: boolean;
  /** Quality tier */
  Quality?: string;
}

/**
 * Price in a specific currency.
 */
export interface StorePrice {
  /** Currency identifier */
  CurrencyId?: string;
  /** Cost amount */
  Cost?: number;
  /** Original cost (before discount) */
  OriginalCost?: number;
  /** Discount percentage */
  DiscountPercent?: number;
}

/**
 * Store item container (multiple offerings).
 */
export interface StoreItem {
  /** Store identifier */
  StoreId?: string;
  /** Display name */
  StoreName?: string;
  /** List of offerings */
  Offerings?: StoreOffering[];
  /** Store refresh time (ISO 8601) */
  RefreshTime?: string;
  /** Store expiration time (ISO 8601) */
  ExpirationTime?: string;
}

/**
 * Active boost information.
 */
export interface ActiveBoost {
  /** Boost identifier */
  BoostId?: string;
  /** Boost type */
  BoostType?: string;
  /** Multiplier value */
  Multiplier?: number;
  /** Remaining uses */
  RemainingUses?: number;
  /** Expiration time (ISO 8601) */
  ExpirationTime?: string;
}

/**
 * Container for active boosts.
 */
export interface ActiveBoostsContainer {
  /** List of active boosts */
  Boosts?: ActiveBoost[];
}

/**
 * Reward snapshot.
 */
export interface RewardSnapshot {
  /** Reward identifier */
  RewardId?: string;
  /** Items awarded */
  Items?: InventoryAmount[];
  /** Currency awarded */
  Currencies?: CurrencyAmount[];
  /** XP awarded */
  XpAwarded?: number;
  /** Claimed status */
  Claimed?: boolean;
}

import type { CurrencyAmount } from './inventory';
