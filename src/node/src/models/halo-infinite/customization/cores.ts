/**
 * Base properties shared by all core types.
 */
export interface CoreBase {
  /** Path to the core definition */
  corePath?: string;
  /** Whether this core is currently equipped */
  isEquipped?: boolean;
  /** Unique core identifier */
  coreId?: string;
  /** Type of core */
  coreType?: string;
  /** When the core was first acquired (ISO 8601) */
  firstAcquiredDate?: string;
}

/**
 * Base properties shared by all themes.
 */
export interface ThemeBase {
  /** When first modified (ISO 8601) */
  firstModifiedDateUtc?: string;
  /** When last modified (ISO 8601) */
  lastModifiedDateUtc?: string;
  /** Whether this theme is currently equipped */
  isEquipped?: boolean;
  /** Whether this is the default theme */
  isDefault?: boolean;
  /** Path to the theme definition */
  themePath?: string;
}

/**
 * Armor core with themes.
 */
export interface ArmorCore extends CoreBase {
  /** Available themes for this core */
  themes?: ArmorCoreTheme[];
}

/**
 * Armor core theme configuration.
 */
export interface ArmorCoreTheme extends ThemeBase {
  /** Helmet item path */
  helmetPath?: string;
  /** Visor item path */
  visorPath?: string;
  /** Coating item path */
  coatingPath?: string;
  /** Left shoulder pad path */
  leftShoulderPadPath?: string;
  /** Right shoulder pad path */
  rightShoulderPadPath?: string;
  /** Gloves path */
  glovesPath?: string;
  /** Chest attachment path */
  chestAttachmentPath?: string;
  /** Knee pads path */
  kneePadsPath?: string;
  /** Wrist attachment path */
  wristAttachmentPath?: string;
  /** Hip attachment path */
  hipAttachmentPath?: string;
  /** Armor effect path */
  armorEffectPath?: string;
  /** Mythic effect path */
  mythicEffectPath?: string;
}

/**
 * Weapon core with themes.
 */
export interface WeaponCore extends CoreBase {
  /** Available themes for this core */
  themes?: WeaponCoreTheme[];
}

/**
 * Weapon core theme configuration.
 */
export interface WeaponCoreTheme extends ThemeBase {
  /** Coating item path */
  coatingPath?: string;
  /** Charm item path */
  charmPath?: string;
  /** Death FX path */
  deathFxPath?: string;
  /** Emblem path */
  emblemPath?: string;
}

/**
 * Vehicle core with themes.
 */
export interface VehicleCore extends CoreBase {
  /** Available themes for this core */
  themes?: VehicleCoreTheme[];
}

/**
 * Vehicle core theme configuration.
 */
export interface VehicleCoreTheme extends ThemeBase {
  /** Coating item path */
  coatingPath?: string;
  /** Emblem path */
  emblemPath?: string;
  /** Vehicle effect path */
  vehicleEffectPath?: string;
}

/**
 * AI core with themes.
 */
export interface AiCore extends CoreBase {
  /** Available themes for this core */
  themes?: AiCoreTheme[];
}

/**
 * AI core theme configuration.
 */
export interface AiCoreTheme extends ThemeBase {
  /** AI model path */
  modelPath?: string;
  /** Color primary path */
  colorPrimaryPath?: string;
  /** Color secondary path */
  colorSecondaryPath?: string;
}

/**
 * Collection of armor cores.
 */
export interface ArmorCoreCollection {
  /** List of armor cores */
  armorCores?: ArmorCore[];
}

/**
 * Collection of weapon cores.
 */
export interface WeaponCoreCollection {
  /** List of weapon cores */
  weaponCores?: WeaponCore[];
}

/**
 * Collection of vehicle cores.
 */
export interface VehicleCoreCollection {
  /** List of vehicle cores */
  vehicleCores?: VehicleCore[];
}

/**
 * Collection of AI cores.
 */
export interface AiCoreContainer {
  /** List of AI cores */
  aiCores?: AiCore[];
}
