import type { DisplayString } from '../economy/inventory';

/**
 * Medal definition.
 */
export interface Medal {
  /** Medal name identifier */
  nameId?: number;
  /** Display name */
  name?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Sprite index */
  spriteIndex?: number;
  /** Medal type */
  type?: string;
  /** Difficulty level */
  difficulty?: string;
  /** Personal score awarded */
  personalScore?: number;
  /** Sorting weight */
  sortingWeight?: number;
}

/**
 * Medal metadata collection.
 */
export interface MedalMetadata {
  /** List of medals */
  medals?: Medal[];
  /** Sprite sheet information */
  spriteSheet?: SpriteSheet;
}

/**
 * Sprite sheet information.
 */
export interface SpriteSheet {
  /** Path to sprite sheet image */
  path?: string;
  /** Sprite width */
  spriteWidth?: number;
  /** Sprite height */
  spriteHeight?: number;
  /** Number of columns */
  columns?: number;
  /** Number of rows */
  rows?: number;
}
