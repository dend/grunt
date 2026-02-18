/**
 * Spartan body configuration.
 */
export interface SpartanBody {
  /** When last modified (ISO 8601) */
  LastModifiedDateUtc?: string;
  /** Left arm prosthetic path */
  LeftArm?: string;
  /** Right arm prosthetic path */
  RightArm?: string;
  /** Left leg prosthetic path */
  LeftLeg?: string;
  /** Right leg prosthetic path */
  RightLeg?: string;
  /** Body type identifier */
  BodyType?: string;
  /** Voice number */
  Voice?: number;
  /** Voice path */
  VoicePath?: string;
}

/**
 * Player appearance configuration.
 */
export interface Appearance {
  /** When last modified (ISO 8601) */
  LastModifiedDateUtc?: string;
  /** Service tag (4 characters) */
  ServiceTag?: string;
  /** Intro emote path */
  IntroEmotePath?: string;
  /** Stance path */
  StancePath?: string;
  /** Emblem configuration */
  Emblem?: Emblem;
  /** Backdrop image path */
  BackdropImagePath?: string;
  /** Action pose path */
  ActionPosePath?: string;
  /** Player title path */
  PlayerTitlePath?: string;
}

/**
 * Emblem configuration.
 */
export interface Emblem {
  /** Emblem path */
  EmblemPath?: string;
  /** Emblem configuration ID */
  ConfigurationId?: number;
  /** Alternative path reference */
  Path?: string;
  /** Location identifier */
  LocationId?: number;
}

/**
 * Complete player customization data.
 */
export interface CustomizationData {
  /** Spartan body configuration */
  SpartanBody?: SpartanBody;
  /** Appearance settings */
  Appearance?: Appearance;
  /** Armor cores */
  ArmorCores?: import('./cores').ArmorCoreCollection;
  /** Weapon cores */
  WeaponCores?: import('./cores').WeaponCoreCollection;
  /** Vehicle cores */
  VehicleCores?: import('./cores').VehicleCoreCollection;
  /** AI cores */
  AiCores?: import('./cores').AiCoreContainer;
}

/**
 * Appearance customization container.
 */
export interface AppearanceCustomization {
  /** Service tag */
  ServiceTag?: string;
  /** Appearance configuration */
  Appearance?: Appearance;
}
