import type { Emblem } from './appearance';

/**
 * Base properties shared by all core types.
 */
export interface CoreBase {
  /** Path to the core definition */
  CorePath?: string;
  /** Whether this core is currently equipped */
  IsEquipped?: boolean;
  /** Unique core identifier */
  CoreId?: string;
  /** Type of core */
  CoreType?: string;
  /** When the core was first acquired (ISO 8601) */
  FirstAcquiredDate?: string;
}

/**
 * Base properties shared by all themes.
 */
export interface ThemeBase {
  /** When first modified (ISO 8601) */
  FirstModifiedDateUtc?: string;
  /** When last modified (ISO 8601) */
  LastModifiedDateUtc?: string;
  /** Whether this theme is currently equipped */
  IsEquipped?: boolean;
  /** Whether this is the default theme */
  IsDefault?: boolean;
  /** Path to the theme definition */
  ThemePath?: string;
}

/**
 * Armor core with themes.
 */
export interface ArmorCore extends CoreBase {
  /** Available themes for this core */
  Themes?: ArmorCoreTheme[];
}

/**
 * Armor core theme configuration.
 */
export interface ArmorCoreTheme extends ThemeBase {
  /** Coating item path */
  CoatingPath?: string;
  /** Glove path */
  GlovePath?: string;
  /** Helmet item path */
  HelmetPath?: string;
  /** Helmet attachment path */
  HelmetAttachmentPath?: string;
  /** Chest attachment path */
  ChestAttachmentPath?: string;
  /** Knee pad path */
  KneePadPath?: string;
  /** Left shoulder pad path */
  LeftShoulderPadPath?: string;
  /** Right shoulder pad path */
  RightShoulderPadPath?: string;
  /** Emblems */
  Emblems?: Emblem[];
  /** Armor FX path */
  ArmorFxPath?: string;
  /** Armor FX paths */
  ArmorFxPaths?: string[];
  /** Mythic FX path */
  MythicFxPath?: string;
  /** Visor item path */
  VisorPath?: string;
  /** Hip attachment path */
  HipAttachmentPath?: string;
  /** Wrist attachment path */
  WristAttachmentPath?: string;
  /** Big emblem */
  BigEmblem?: Emblem;
}

/**
 * Weapon core with themes.
 */
export interface WeaponCore extends CoreBase {
  /** Available themes for this core */
  Themes?: WeaponCoreTheme[];
}

/**
 * Weapon core theme configuration.
 */
export interface WeaponCoreTheme extends ThemeBase {
  /** Coating item path */
  CoatingPath?: string;
  /** Emblems */
  Emblems?: Emblem[];
  /** Big emblem */
  BigEmblem?: Emblem;
  /** Death FX path */
  DeathFxPath?: string;
  /** Ammo counter color path */
  AmmoCounterColorPath?: string;
  /** Stat tracker path */
  StatTrackerPath?: string;
  /** Weapon charm path */
  WeaponCharmPath?: string;
  /** Alternate geometry region path */
  AlternateGeometryRegionPath?: string;
  /** Ammo counter path */
  AmmoCounterPath?: string;
}

/**
 * Vehicle core with themes.
 */
export interface VehicleCore extends CoreBase {
  /** Available themes for this core */
  Themes?: VehicleCoreTheme[];
}

/**
 * Vehicle core theme configuration.
 */
export interface VehicleCoreTheme extends ThemeBase {
  /** Coating item path */
  CoatingPath?: string;
  /** Horn path */
  HornPath?: string;
  /** Vehicle FX path */
  VehicleFxPath?: string;
  /** Vehicle charm path */
  VehicleCharmPath?: string;
  /** Emblems */
  Emblems?: Emblem[];
  /** Alternate geometry region path */
  AlternateGeometryRegionPath?: string;
}

/**
 * AI core with themes.
 */
export interface AiCore extends CoreBase {
  /** Available themes for this core */
  Themes?: AiCoreTheme[];
}

/**
 * AI core theme configuration.
 */
export interface AiCoreTheme extends ThemeBase {
  /** AI model path */
  ModelPath?: string;
  /** Color path */
  ColorPath?: string;
}

/**
 * Collection of armor cores.
 */
export interface ArmorCoreCollection {
  /** List of armor cores */
  ArmorCores?: ArmorCore[];
}

/**
 * Collection of weapon cores.
 */
export interface WeaponCoreCollection {
  /** List of weapon cores */
  WeaponCores?: WeaponCore[];
}

/**
 * Collection of vehicle cores.
 */
export interface VehicleCoreCollection {
  /** List of vehicle cores */
  VehicleCores?: VehicleCore[];
}

/**
 * Collection of AI cores.
 */
export interface AiCoreContainer {
  /** List of AI cores */
  AiCores?: AiCore[];
}
