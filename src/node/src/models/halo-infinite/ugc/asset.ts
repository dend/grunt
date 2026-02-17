import type { AssetKind } from '../enums/asset-kind';

/**
 * Asset file information.
 */
export interface AssetVersionFile {
  /** Prefix for file URLs */
  Prefix?: string;
  /** List of file paths */
  FileRelativePaths?: string[];
}

/**
 * Asset play statistics.
 */
export interface PlayAssetStats {
  /** Number of plays */
  PlaysRecent?: number;
  /** All-time plays */
  PlaysAllTime?: number;
  /** Favorites count */
  Favorites?: number;
  /** Average rating */
  AverageRating?: number;
  /** Number of ratings */
  NumberOfRatings?: number;
  /** Parent asset ID (if this is a variant) */
  ParentAssetId?: string;
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
export interface AuthoringAsset extends AssetBase {
  /** Original author XUID */
  OriginalAuthor?: string;
  /** Whether it's readonly */
  Readonly?: boolean;
  /** Custom data for the asset type */
  CustomData?: Record<string, unknown>;
  /** Creation time (ISO 8601) */
  CreatedAt?: string;
  /** Last modification time (ISO 8601) */
  UpdatedAt?: string;
}

/**
 * Authoring asset version.
 */
export interface AuthoringAssetVersion extends AssetBase {
  /** Readonly status */
  Readonly?: boolean;
  /** Custom data */
  CustomData?: Record<string, unknown>;
}

/**
 * Container for multiple authoring assets.
 */
export interface AuthoringAssetContainer {
  /** Asset count */
  Count?: number;
  /** Total available */
  Total?: number;
  /** List of assets */
  Results?: AuthoringAsset[];
  /** Pagination links */
  Links?: AssetLinks;
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
  /** User's rating value (1-5) */
  Rating?: number;
  /** When rated (ISO 8601) */
  Timestamp?: string;
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
  /** Total count */
  Total?: number;
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
