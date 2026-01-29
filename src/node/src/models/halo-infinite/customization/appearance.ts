/**
 * Spartan body configuration.
 */
export interface SpartanBody {
  /** When last modified (ISO 8601) */
  lastModifiedDateUtc?: string;
  /** Left arm prosthetic path */
  leftArm?: string;
  /** Right arm prosthetic path */
  rightArm?: string;
  /** Left leg prosthetic path */
  leftLeg?: string;
  /** Right leg prosthetic path */
  rightLeg?: string;
  /** Body type identifier */
  bodyType?: string;
  /** Voice number */
  voice?: number;
  /** Voice path */
  voicePath?: string;
}

/**
 * Player appearance configuration.
 */
export interface Appearance {
  /** When last modified (ISO 8601) */
  lastModifiedDateUtc?: string;
  /** Service tag (4 characters) */
  serviceTag?: string;
  /** Intro gesture path */
  introGesturePath?: string;
  /** Outro gesture path */
  outroGesturePath?: string;
  /** Stance path */
  stancePath?: string;
  /** Emblem configuration */
  emblem?: EmblemConfiguration;
  /** Backdrop path */
  backdropPath?: string;
  /** Action pose path */
  actionPosePath?: string;
}

/**
 * Emblem configuration.
 */
export interface EmblemConfiguration {
  /** Emblem path */
  emblemPath?: string;
  /** Emblem configuration ID */
  configurationId?: number;
}

/**
 * Complete player customization data.
 */
export interface CustomizationData {
  /** Spartan body configuration */
  spartanBody?: SpartanBody;
  /** Appearance settings */
  appearance?: Appearance;
  /** Armor cores */
  armorCores?: import('./cores').ArmorCoreCollection;
  /** Weapon cores */
  weaponCores?: import('./cores').WeaponCoreCollection;
  /** Vehicle cores */
  vehicleCores?: import('./cores').VehicleCoreCollection;
  /** AI cores */
  aiCores?: import('./cores').AiCoreContainer;
}

/**
 * Appearance customization container.
 */
export interface AppearanceCustomization {
  /** Service tag */
  serviceTag?: string;
  /** Appearance configuration */
  appearance?: Appearance;
}
