import type { DisplayString } from '../economy/inventory';

/**
 * Medal definition.
 */
export interface Medal {
  /** Medal name identifier */
  NameId?: number;
  /** Display name */
  Name?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Sprite index */
  SpriteIndex?: number;
  /** Medal type */
  Type?: string;
  /** Difficulty level */
  Difficulty?: string;
  /** Personal score awarded */
  PersonalScore?: number;
  /** Sorting weight */
  SortingWeight?: number;
}

/**
 * Medal metadata collection.
 */
export interface MedalMetadata {
  /** List of medals */
  Medals?: Medal[];
  /** Sprite sheet information */
  SpriteSheet?: SpriteSheet;
}

/**
 * Sprite sheet information.
 */
export interface SpriteSheet {
  /** Path to sprite sheet image */
  Path?: string;
  /** Sprite width */
  SpriteWidth?: number;
  /** Sprite height */
  SpriteHeight?: number;
  /** Number of columns */
  Columns?: number;
  /** Number of rows */
  Rows?: number;
}
