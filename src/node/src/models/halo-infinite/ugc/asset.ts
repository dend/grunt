import type { AssetKind } from '../enums/asset-kind';

/**
 * Asset file information.
 */
export interface AssetVersionFile {
  /** Prefix for file URLs */
  Prefix?: string;
  /** List of file paths */
  FileRelativePaths?: string[];
  /** Prefix endpoint reference */
  PrefixEndpoint?: Record<string, unknown>;
}

/**
 * Asset play statistics.
 */
export interface PlayAssetStats {
  /** Recent plays */
  PlaysRecent?: number;
  /** All-time plays */
  PlaysAllTime?: number;
  /** Favorites count */
  Favorites?: number;
  /** Likes count */
  Likes?: number;
  /** Bookmarks count */
  Bookmarks?: number;
  /** Parent asset count */
  ParentAssetCount?: number;
  /** Average rating */
  AverageRating?: number;
  /** Number of ratings */
  NumberOfRatings?: number;
}

/**
 * Base asset properties.
 */
export interface AssetBase {
  /** Asset unique identifier */
  AssetId?: string;
  /** Version identifier */
  VersionId?: string;
  /** Combined asset version ID */
  AssetVersionId?: string;
  /** Public display name */
  PublicName?: string;
  /** Internal name */
  Name?: string;
  /** Description */
  Description?: string;
  /** Inspection result code */
  InspectionResult?: number;
  /** Clone behavior setting */
  CloneBehavior?: number;
  /** Asset home (where it's stored) */
  AssetHome?: number;
  /** Descriptive tags */
  Tags?: string[];
  /** Contributors (XUIDs) */
  Contributors?: string[];
  /** File information */
  Files?: AssetVersionFile;
  /** Type of asset */
  AssetKind?: AssetKind;
  /** Display order */
  Order?: number;
  /** Play statistics */
  AssetStats?: PlayAssetStats;
  /** When published (ISO 8601) */
  PublishedDate?: string;
  /** Version number */
  VersionNumber?: number;
  /** Admin XUID */
  Admin?: string;
  /** Display owner override */
  DisplayOwnerOverride?: string;
}

/**
 * UGC authoring asset.
 */
export interface AuthoringAsset {
  /** Asset identifier */
  AssetId?: string;
  /** Asset kind */
  Kind?: number;
  /** Original owner XUID */
  OriginalOwner?: string;
  /** Admin XUID */
  Admin?: string;
  /** Last modified date (ISO 8601) */
  LastModifiedDateUtc?: string;
  /** Created date (ISO 8601) */
  CreatedDateUtc?: string;
  /** Internal name */
  InternalName?: string;
  /** Description */
  Description?: string;
  /** Hard delete time (ISO 8601) */
  HardDeleteTimeUtc?: string;
  /** Permissions */
  Permissions?: Permission[];
  /** Asset statistics */
  AssetStats?: Record<string, unknown>;
  /** Asset home */
  AssetHome?: number;
  /** Whether currently being edited */
  IsCurrentlyBeingEdited?: boolean;
}

/**
 * Authoring asset version.
 */
export interface AuthoringAssetVersion {
  /** Readonly status */
  Readonly?: boolean;
  /** Custom data */
  CustomData?: Record<string, unknown>;
}

/**
 * Container for multiple authoring assets.
 */
export interface AuthoringAssetContainer {
  /** List of assets */
  Results?: AuthoringAsset[];
}

/**
 * Container for asset versions.
 */
export interface AuthoringAssetVersionContainer {
  /** List of versions */
  Results?: AuthoringAssetVersion[];
}

/**
 * Pagination links.
 */
export interface AssetLinks {
  /** Link to current page */
  Self?: string;
  /** Link to next page */
  Next?: string;
  /** Link to previous page */
  Prev?: string;
}

/**
 * Asset rating.
 */
export interface AuthoringAssetRating {
  /** Links */
  Links?: unknown;
  /** Custom data */
  CustomData?: Record<string, unknown>;
  /** Version ratings */
  VersionRatings?: unknown[];
}

/**
 * Favorite asset reference.
 */
export interface FavoriteAsset {
  /** Asset ID */
  AssetId?: string;
  /** Asset kind */
  AssetKind?: AssetKind;
  /** When favorited (ISO 8601) */
  FavoritedAt?: string;
}

/**
 * Container for favorite assets.
 */
export interface AuthoringFavoritesContainer {
  /** List of favorites */
  Results?: FavoriteAsset[];
}

/**
 * Permission for an asset.
 */
export interface Permission {
  /** Player XUID */
  Player?: string;
  /** Permission type */
  PermissionType?: string;
  /** Whether permission is granted */
  Granted?: boolean;
}

/**
 * Asset report for moderation.
 */
export interface AssetReport {
  /** Report reason */
  Reason?: string;
  /** Additional details */
  Details?: string;
  /** When reported (ISO 8601) */
  Timestamp?: string;
}

/**
 * Authoring session for asset editing.
 */
export interface AssetAuthoringSession {
  /** Session identifier */
  SessionId?: string;
  /** Session expiration (ISO 8601) */
  ExpiresAt?: string;
  /** Container SAS URL for uploads */
  ContainerSas?: string;
}

/**
 * Session starter for creating new versions.
 */
export interface AuthoringSessionSourceStarter {
  /** Source asset ID (to clone from) */
  SourceAssetId?: string;
  /** Source version ID */
  SourceVersionId?: string;
}
