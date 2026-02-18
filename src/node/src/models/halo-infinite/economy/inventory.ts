/**
 * Player inventory item.
 */
export interface PlayerItem {
  /** Item identifier */
  ItemId?: string;
  /** Item path identifier */
  ItemPath?: string;
  /** Item type */
  ItemType?: string;
  /** Core type */
  CoreType?: string;
  /** Quantity owned */
  Amount?: number;
  /** When first acquired (ISO 8601) */
  FirstAcquiredDate?: string;
  /** Source of acquisition */
  Source?: string;
}

/**
 * Player inventory response.
 */
export interface PlayerInventory {
  /** List of inventory items */
  Items?: PlayerItem[];
}

/**
 * Currency balance.
 */
export interface CurrencyAmount {
  /** Currency path */
  CurrencyPath?: string;
  /** Current balance */
  Amount?: number;
  /** Source of currency */
  Source?: string;
}

/**
 * Currency snapshot containing all balances.
 */
export interface CurrencySnapshot {
  /** List of currency balances */
  Currencies?: CurrencyAmount[];
}

/**
 * Store product reference.
 */
export interface StoreProduct {
  /** Item definition ID */
  ItemDef?: number;
  /** Product identifier */
  ProductId?: string;
  /** Product path */
  Path?: string;
}

/**
 * Currency definition from CMS.
 */
export interface CurrencyDefinition {
  /** Currency identifier */
  Id?: string;
  /** Initial balance amount */
  InitialBalanceAmount?: number;
  /** Microsoft Store products */
  MSStoreProducts?: StoreProduct[];
  /** Steam Store products */
  SteamStoreProducts?: StoreProduct[];
  /** Microsoft Store inventory */
  MicrosoftStore?: Record<string, unknown>;
  /** Steam inventory */
  SteamInventory?: Record<string, unknown>;
}

/**
 * Localized display string.
 */
export interface DisplayString {
  /** Status of the localization */
  Status?: string;
  /** Default/fallback value */
  Value?: string;
  /** Translations by locale code */
  Translations?: Record<string, string>;
}

/**
 * Inventory amount with details.
 */
export interface InventoryAmount {
  /** Inventory item path */
  InventoryItemPath?: string;
  /** Quantity */
  Amount?: number;
  /** Item type */
  Type?: string;
}

/**
 * Individual transaction record.
 */
export interface Transaction {
  /** Adjustment source */
  AdjustmentSource?: string;
  /** Balance adjustment amount */
  BalanceAdjustment?: number;
  /** Resulting balance after adjustment */
  ResultingBalance?: number;
  /** Whether the transaction is finalized */
  Finalized?: boolean;
  /** Transaction identifier */
  TransactionId?: string;
  /** Transaction date (ISO 8601) */
  TransactionDate?: string;
  /** Product reference */
  ProductReference?: string;
  /** Units consumed */
  UnitsConsumed?: number;
  /** Authenticated identities */
  AuthenticatedIdentities?: string[];
}

/**
 * Transaction snapshot containing transaction history.
 */
export interface TransactionSnapshot {
  /** List of transactions */
  Transactions?: Transaction[];
  /** Continuation token for pagination */
  ContinuationToken?: string;
  /** Marketplace last successful dates */
  MarketplaceLastSuccessfulDates?: Record<string, unknown>[];
}
