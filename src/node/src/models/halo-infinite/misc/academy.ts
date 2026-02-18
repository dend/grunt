import type { DisplayString } from '../economy/inventory';

/**
 * Academy tutorial information.
 */
export interface AcademyTutorial {
  /** Tutorial content */
  [key: string]: unknown;
}

/**
 * Academy client manifest.
 */
export interface AcademyClientManifest {
  /** Tutorial information */
  Tutorial?: AcademyTutorial;
  /** List of categories */
  Categories?: AcademyCategory[];
}

/**
 * Academy category.
 */
export interface AcademyCategory {
  /** Drill type */
  DrillType?: string;
  /** Drills in this category */
  Drills?: AcademyDrill[];
}

/**
 * Academy series (collection within a drill).
 */
export interface AcademySeries {
  /** Game asset ID */
  GameAssetID?: string;
  /** Map asset ID */
  MapAssetID?: string;
  /** Whether the series is available */
  Available?: boolean;
  /** Series title */
  Title?: DisplayString;
  /** Description */
  Description?: DisplayString;
  /** Game variant */
  GameVariant?: string;
  /** Map variant */
  MapVariant?: string;
  /** Gameplay GUID */
  GameplayGUID?: string;
}

/**
 * Academy drill.
 */
export interface AcademyDrill {
  /** Title string ID */
  TitleStringID?: string;
  /** Series in this drill */
  Series?: AcademySeries[];
  /** Sprite frame index */
  SpriteFrameIndex?: number;
  /** Description string ID */
  DescriptionStringID?: string;
  /** Title */
  Title?: string;
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
