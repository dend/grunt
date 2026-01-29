/**
 * Player inventory item.
 */
export interface PlayerItem {
  /** Item path identifier */
  itemPath?: string;
  /** Item type */
  itemType?: string;
  /** Quantity owned */
  amount?: number;
  /** When first acquired (ISO 8601) */
  firstAcquiredDate?: string;
  /** Source of acquisition */
  source?: string;
}

/**
 * Player inventory response.
 */
export interface PlayerInventory {
  /** List of inventory items */
  items?: PlayerItem[];
}

/**
 * Currency balance.
 */
export interface CurrencyAmount {
  /** Currency identifier */
  currencyId?: string;
  /** Current balance */
  amount?: number;
}

/**
 * Currency snapshot containing all balances.
 */
export interface CurrencySnapshot {
  /** List of currency balances */
  currencies?: CurrencyAmount[];
}

/**
 * Currency definition from CMS.
 */
export interface CurrencyDefinition {
  /** Currency identifier */
  currencyId?: string;
  /** Display title */
  title?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Image path */
  image?: string;
  /** Icon type */
  iconType?: string;
}

/**
 * Localized display string.
 */
export interface DisplayString {
  /** Status of the localization */
  status?: string;
  /** Default/fallback value */
  value?: string;
  /** Translations by locale code */
  translations?: Record<string, string>;
}

/**
 * Inventory amount with details.
 */
export interface InventoryAmount {
  /** Item path */
  itemPath?: string;
  /** Quantity */
  amount?: number;
  /** Item type */
  itemType?: string;
}

/**
 * Transaction result after currency operation.
 */
export interface TransactionSnapshot {
  /** Transaction identifier */
  transactionId?: string;
  /** New balance after transaction */
  newBalance?: CurrencyAmount[];
  /** Transaction timestamp (ISO 8601) */
  timestamp?: string;
}
