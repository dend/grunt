/**
 * Player inventory item.
 */
export interface PlayerItem {
  /** Item path identifier */
  ItemPath?: string;
  /** Item type */
  ItemType?: string;
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
  /** Currency identifier */
  CurrencyId?: string;
  /** Current balance */
  Amount?: number;
}

/**
 * Currency snapshot containing all balances.
 */
export interface CurrencySnapshot {
  /** List of currency balances */
  Currencies?: CurrencyAmount[];
}

/**
 * Currency definition from CMS.
 */
export interface CurrencyDefinition {
  /** Currency identifier */
  CurrencyId?: string;
  /** Display title */
  Title?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Image path */
  Image?: string;
  /** Icon type */
  IconType?: string;
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
  /** Item path */
  ItemPath?: string;
  /** Quantity */
  Amount?: number;
  /** Item type */
  ItemType?: string;
}

/**
 * Transaction result after currency operation.
 */
export interface TransactionSnapshot {
  /** Transaction identifier */
  TransactionId?: string;
  /** New balance after transaction */
  NewBalance?: CurrencyAmount[];
  /** Transaction timestamp (ISO 8601) */
  Timestamp?: string;
}
