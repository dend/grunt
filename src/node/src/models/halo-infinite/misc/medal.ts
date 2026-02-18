import type { DisplayString } from '../economy/inventory';

/**
 * Medal definition.
 */
export interface Medal {
  /** Medal name identifier */
  NameId?: number;
  /** Number of times earned */
  Count?: number;
  /** Total personal score awarded */
  TotalPersonalScoreAwarded?: number;
  /** Display name */
  Name?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Sprite index on the sprite sheet (zero-based) */
  SpriteIndex?: number;
  /** Sorting weight */
  SortingWeight?: number;
  /** Difficulty index, mapped to the difficulties property in medal metadata */
  DifficultyIndex?: number;
  /** Type index, mapped to the types property in medal metadata */
  TypeIndex?: number;
  /** Personal score */
  PersonalScore?: number;
}

/**
 * Medal metadata collection.
 */
export interface MedalMetadata {
  /** List of medals */
  Medals?: Medal[];
  /** Sprite sheet information */
  SpriteSheet?: Sprite;
}

/**
 * Sprite sheet information.
 */
export interface Sprite {
  /** Path to sprite sheet image */
  Path?: string;
  /** Number of columns */
  Columns?: number;
  /** Sprite size */
  Size?: number;
}
