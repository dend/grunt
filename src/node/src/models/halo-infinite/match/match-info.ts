import type { LifecycleMode } from '../enums/lifecycle-mode';

/**
 * Generic asset reference used across multiple API responses.
 */
export interface GenericAsset {
  /** Unique asset identifier */
  assetId?: string;
  /** Version identifier */
  versionId?: string;
  /** Combined asset version identifier */
  assetVersionId?: string;
  /** Display name */
  publicName?: string;
}

/**
 * UGC game variant information in match context.
 */
export interface UgcGameVariant {
  /** Asset identifier */
  assetId?: string;
  /** Version identifier */
  versionId?: string;
  /** Display name */
  publicName?: string;
}

/**
 * Playlist experience tracking.
 */
export interface PlaylistExperience {
  /** Experience value */
  value?: number;
}

/**
 * Gameplay interaction type.
 */
export interface GameplayInteraction {
  /** Interaction type identifier */
  value?: number;
}

/**
 * General information about a match.
 *
 * Contains metadata like timing, game mode, map, and playlist info.
 */
export interface MatchInfo {
  /** Match start time (ISO 8601) */
  startTime?: string;
  /** Match end time (ISO 8601) */
  endTime?: string;
  /** Match duration as ISO 8601 duration string (e.g., "PT10M30S") */
  duration?: string;
  /** Lifecycle mode (matchmade, custom, local) */
  lifecycleMode?: LifecycleMode;
  /** Game variant category */
  gameVariantCategory?: number;
  /** Map/level identifier */
  levelId?: string;
  /** Map variant information */
  mapVariant?: GenericAsset;
  /** UGC game variant (for custom games) */
  ugcGameVariant?: UgcGameVariant;
  /** Clearance ID used for the match */
  clearanceId?: string;
  /** Playlist information */
  playlist?: GenericAsset;
  /** Playlist experience info */
  playlistExperience?: PlaylistExperience;
  /** Map-mode pair info */
  playlistMapModePair?: GenericAsset;
  /** Season identifier */
  seasonId?: string;
  /** Playable duration */
  playableDuration?: string;
  /** Whether teams were enabled */
  teamsEnabled?: boolean;
  /** Whether team scoring was enabled */
  teamScoringEnabled?: boolean;
  /** Gameplay interaction type */
  gameplayInteraction?: GameplayInteraction;
}
