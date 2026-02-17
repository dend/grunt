import type { LifecycleMode } from '../enums/lifecycle-mode';

/**
 * Generic asset reference used across multiple API responses.
 */
export interface GenericAsset {
  /** Unique asset identifier */
  AssetId?: string;
  /** Version identifier */
  VersionId?: string;
  /** Combined asset version identifier */
  AssetVersionId?: string;
  /** Display name */
  PublicName?: string;
}

/**
 * UGC game variant information in match context.
 */
export interface UgcGameVariant {
  /** Asset identifier */
  AssetId?: string;
  /** Version identifier */
  VersionId?: string;
  /** Display name */
  PublicName?: string;
}

/**
 * Playlist experience tracking.
 */
export interface PlaylistExperience {
  /** Experience value */
  Value?: number;
}

/**
 * Gameplay interaction type.
 */
export interface GameplayInteraction {
  /** Interaction type identifier */
  Value?: number;
}

/**
 * General information about a match.
 *
 * Contains metadata like timing, game mode, map, and playlist info.
 */
export interface MatchInfo {
  /** Match start time (ISO 8601) */
  StartTime?: string;
  /** Match end time (ISO 8601) */
  EndTime?: string;
  /** Match duration as ISO 8601 duration string (e.g., "PT10M30S") */
  Duration?: string;
  /** Lifecycle mode (matchmade, custom, local) */
  LifecycleMode?: LifecycleMode;
  /** Game variant category */
  GameVariantCategory?: number;
  /** Map/level identifier */
  LevelId?: string;
  /** Map variant information */
  MapVariant?: GenericAsset;
  /** UGC game variant (for custom games) */
  UgcGameVariant?: UgcGameVariant;
  /** Clearance ID used for the match */
  ClearanceId?: string;
  /** Playlist information */
  Playlist?: GenericAsset;
  /** Playlist experience info */
  PlaylistExperience?: PlaylistExperience;
  /** Map-mode pair info */
  PlaylistMapModePair?: GenericAsset;
  /** Season identifier */
  SeasonId?: string;
  /** Playable duration */
  PlayableDuration?: string;
  /** Whether teams were enabled */
  TeamsEnabled?: boolean;
  /** Whether team scoring was enabled */
  TeamScoringEnabled?: boolean;
  /** Gameplay interaction type */
  GameplayInteraction?: GameplayInteraction;
}
