import type { DisplayString } from '../economy/inventory';

/**
 * Academy client manifest.
 */
export interface AcademyClientManifest {
  /** List of categories */
  Categories?: AcademyCategory[];
  /** Version */
  Version?: string;
}

/**
 * Academy category.
 */
export interface AcademyCategory {
  /** Category identifier */
  Id?: string;
  /** Category title */
  Title?: DisplayString;
  /** Category description */
  Description?: DisplayString;
  /** Image path */
  ImagePath?: string;
  /** Series in this category */
  Series?: AcademySeries[];
}

/**
 * Academy series (collection of drills).
 */
export interface AcademySeries {
  /** Series identifier */
  Id?: string;
  /** Series title */
  Title?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Image path */
  ImagePath?: string;
  /** Drills in this series */
  Drills?: AcademyDrill[];
}

/**
 * Academy drill.
 */
export interface AcademyDrill {
  /** Drill identifier */
  Id?: string;
  /** Drill title */
  Title?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Image path */
  ImagePath?: string;
  /** Difficulty level */
  Difficulty?: string;
  /** Weapon path */
  WeaponPath?: string;
  /** Map asset ID */
  MapAssetId?: string;
  /** Game variant asset ID */
  GameVariantAssetId?: string;
}

/**
 * Academy star definitions.
 */
export interface AcademyStarDefinitions {
  /** Star definitions by drill */
  Definitions?: Record<string, AcademyStarDefinition>;
}

/**
 * Star thresholds for a drill.
 */
export interface AcademyStarDefinition {
  /** One star threshold */
  OneStar?: number;
  /** Two stars threshold */
  TwoStars?: number;
  /** Three stars threshold */
  ThreeStars?: number;
}

/**
 * Bot customization data.
 */
export interface BotCustomizationData {
  /** Available bot difficulty levels */
  Difficulties?: BotDifficulty[];
  /** Bot appearance options */
  Appearances?: BotAppearance[];
}

/**
 * Bot difficulty level.
 */
export interface BotDifficulty {
  /** Difficulty identifier */
  Id?: string;
  /** Display name */
  Name?: DisplayString;
  /** Description */
  Description?: DisplayString;
}

/**
 * Bot appearance option.
 */
export interface BotAppearance {
  /** Appearance identifier */
  Id?: string;
  /** Display name */
  Name?: DisplayString;
  /** Appearance path */
  Path?: string;
}

/**
 * Test academy client manifest (for flighted content).
 */
export interface TestAcademyClientManifest extends AcademyClientManifest {
  /** Flight identifier */
  FlightId?: string;
}
