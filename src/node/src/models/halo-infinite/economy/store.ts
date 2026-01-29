import type { DisplayString, InventoryAmount } from './inventory';

/**
 * Store offering entry.
 */
export interface StoreOffering {
  /** Offering identifier */
  offeringId?: string;
  /** Display title */
  title?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Image path */
  imagePath?: string;
  /** Price in each currency */
  prices?: StorePrice[];
  /** Items included */
  includedItems?: InventoryAmount[];
  /** Offering type */
  offeringType?: string;
  /** Start time (ISO 8601) */
  startDate?: string;
  /** End time (ISO 8601) */
  endDate?: string;
  /** Whether this is a bundle */
  isBundle?: boolean;
  /** Whether player owns this */
  isOwned?: boolean;
  /** Quality tier */
  quality?: string;
}

/**
 * Price in a specific currency.
 */
export interface StorePrice {
  /** Currency identifier */
  currencyId?: string;
  /** Cost amount */
  cost?: number;
  /** Original cost (before discount) */
  originalCost?: number;
  /** Discount percentage */
  discountPercent?: number;
}

/**
 * Store item container (multiple offerings).
 */
export interface StoreItem {
  /** Store identifier */
  storeId?: string;
  /** Display name */
  storeName?: string;
  /** List of offerings */
  offerings?: StoreOffering[];
  /** Store refresh time (ISO 8601) */
  refreshTime?: string;
  /** Store expiration time (ISO 8601) */
  expirationTime?: string;
}

/**
 * Active boost information.
 */
export interface ActiveBoost {
  /** Boost identifier */
  boostId?: string;
  /** Boost type */
  boostType?: string;
  /** Multiplier value */
  multiplier?: number;
  /** Remaining uses */
  remainingUses?: number;
  /** Expiration time (ISO 8601) */
  expirationTime?: string;
}

/**
 * Container for active boosts.
 */
export interface ActiveBoostsContainer {
  /** List of active boosts */
  boosts?: ActiveBoost[];
}

/**
 * Reward snapshot.
 */
export interface RewardSnapshot {
  /** Reward identifier */
  rewardId?: string;
  /** Items awarded */
  items?: InventoryAmount[];
  /** Currency awarded */
  currencies?: CurrencyAmount[];
  /** XP awarded */
  xpAwarded?: number;
  /** Claimed status */
  claimed?: boolean;
}

import type { CurrencyAmount } from './inventory';
