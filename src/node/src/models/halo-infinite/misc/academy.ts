import type { DisplayString } from '../economy/inventory';

/**
 * Academy client manifest.
 */
export interface AcademyClientManifest {
  /** List of categories */
  categories?: AcademyCategory[];
  /** Version */
  version?: string;
}

/**
 * Academy category.
 */
export interface AcademyCategory {
  /** Category identifier */
  id?: string;
  /** Category title */
  title?: DisplayString;
  /** Category description */
  description?: DisplayString;
  /** Image path */
  imagePath?: string;
  /** Series in this category */
  series?: AcademySeries[];
}

/**
 * Academy series (collection of drills).
 */
export interface AcademySeries {
  /** Series identifier */
  id?: string;
  /** Series title */
  title?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Image path */
  imagePath?: string;
  /** Drills in this series */
  drills?: AcademyDrill[];
}

/**
 * Academy drill.
 */
export interface AcademyDrill {
  /** Drill identifier */
  id?: string;
  /** Drill title */
  title?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Image path */
  imagePath?: string;
  /** Difficulty level */
  difficulty?: string;
  /** Weapon path */
  weaponPath?: string;
  /** Map asset ID */
  mapAssetId?: string;
  /** Game variant asset ID */
  gameVariantAssetId?: string;
}

/**
 * Academy star definitions.
 */
export interface AcademyStarDefinitions {
  /** Star definitions by drill */
  definitions?: Record<string, AcademyStarDefinition>;
}

/**
 * Star thresholds for a drill.
 */
export interface AcademyStarDefinition {
  /** One star threshold */
  oneStar?: number;
  /** Two stars threshold */
  twoStars?: number;
  /** Three stars threshold */
  threeStars?: number;
}

/**
 * Bot customization data.
 */
export interface BotCustomizationData {
  /** Available bot difficulty levels */
  difficulties?: BotDifficulty[];
  /** Bot appearance options */
  appearances?: BotAppearance[];
}

/**
 * Bot difficulty level.
 */
export interface BotDifficulty {
  /** Difficulty identifier */
  id?: string;
  /** Display name */
  name?: DisplayString;
  /** Description */
  description?: DisplayString;
}

/**
 * Bot appearance option.
 */
export interface BotAppearance {
  /** Appearance identifier */
  id?: string;
  /** Display name */
  name?: DisplayString;
  /** Appearance path */
  path?: string;
}

/**
 * Test academy client manifest (for flighted content).
 */
export interface TestAcademyClientManifest extends AcademyClientManifest {
  /** Flight identifier */
  flightId?: string;
}
