import type { AssetKind } from '../enums/asset-kind';

/**
 * Asset file information.
 */
export interface AssetVersionFile {
  /** Prefix for file URLs */
  prefix?: string;
  /** List of file paths */
  fileRelativePaths?: string[];
}

/**
 * Asset play statistics.
 */
export interface PlayAssetStats {
  /** Number of plays */
  playsRecent?: number;
  /** All-time plays */
  playsAllTime?: number;
  /** Favorites count */
  favorites?: number;
  /** Average rating */
  averageRating?: number;
  /** Number of ratings */
  numberOfRatings?: number;
  /** Parent asset ID (if this is a variant) */
  parentAssetId?: string;
}

/**
 * Base asset properties.
 */
export interface AssetBase {
  /** Asset unique identifier */
  assetId?: string;
  /** Version identifier */
  versionId?: string;
  /** Combined asset version ID */
  assetVersionId?: string;
  /** Public display name */
  publicName?: string;
  /** Internal name */
  name?: string;
  /** Description */
  description?: string;
  /** Inspection result code */
  inspectionResult?: number;
  /** Clone behavior setting */
  cloneBehavior?: number;
  /** Asset home (where it's stored) */
  assetHome?: number;
  /** Descriptive tags */
  tags?: string[];
  /** Contributors (XUIDs) */
  contributors?: string[];
  /** File information */
  files?: AssetVersionFile;
  /** Type of asset */
  assetKind?: AssetKind;
  /** Display order */
  order?: number;
  /** Play statistics */
  assetStats?: PlayAssetStats;
  /** When published (ISO 8601) */
  publishedDate?: string;
  /** Version number */
  versionNumber?: number;
  /** Admin XUID */
  admin?: string;
  /** Display owner override */
  displayOwnerOverride?: string;
}

/**
 * UGC authoring asset.
 */
export interface AuthoringAsset extends AssetBase {
  /** Original author XUID */
  originalAuthor?: string;
  /** Whether it's readonly */
  readonly?: boolean;
  /** Custom data for the asset type */
  customData?: Record<string, unknown>;
  /** Creation time (ISO 8601) */
  createdAt?: string;
  /** Last modification time (ISO 8601) */
  updatedAt?: string;
}

/**
 * Authoring asset version.
 */
export interface AuthoringAssetVersion extends AssetBase {
  /** Readonly status */
  readonly?: boolean;
  /** Custom data */
  customData?: Record<string, unknown>;
}

/**
 * Container for multiple authoring assets.
 */
export interface AuthoringAssetContainer {
  /** Asset count */
  count?: number;
  /** Total available */
  total?: number;
  /** List of assets */
  results?: AuthoringAsset[];
  /** Pagination links */
  links?: AssetLinks;
}

/**
 * Container for asset versions.
 */
export interface AuthoringAssetVersionContainer {
  /** List of versions */
  results?: AuthoringAssetVersion[];
}

/**
 * Pagination links.
 */
export interface AssetLinks {
  /** Link to current page */
  self?: string;
  /** Link to next page */
  next?: string;
  /** Link to previous page */
  prev?: string;
}

/**
 * Asset rating.
 */
export interface AuthoringAssetRating {
  /** User's rating value (1-5) */
  rating?: number;
  /** When rated (ISO 8601) */
  timestamp?: string;
}

/**
 * Favorite asset reference.
 */
export interface FavoriteAsset {
  /** Asset ID */
  assetId?: string;
  /** Asset kind */
  assetKind?: AssetKind;
  /** When favorited (ISO 8601) */
  favoritedAt?: string;
}

/**
 * Container for favorite assets.
 */
export interface AuthoringFavoritesContainer {
  /** List of favorites */
  results?: FavoriteAsset[];
  /** Total count */
  total?: number;
}

/**
 * Permission for an asset.
 */
export interface Permission {
  /** Player XUID */
  player?: string;
  /** Permission type */
  permissionType?: string;
  /** Whether permission is granted */
  granted?: boolean;
}

/**
 * Asset report for moderation.
 */
export interface AssetReport {
  /** Report reason */
  reason?: string;
  /** Additional details */
  details?: string;
  /** When reported (ISO 8601) */
  timestamp?: string;
}

/**
 * Authoring session for asset editing.
 */
export interface AssetAuthoringSession {
  /** Session identifier */
  sessionId?: string;
  /** Session expiration (ISO 8601) */
  expiresAt?: string;
  /** Container SAS URL for uploads */
  containerSas?: string;
}

/**
 * Session starter for creating new versions.
 */
export interface AuthoringSessionSourceStarter {
  /** Source asset ID (to clone from) */
  sourceAssetId?: string;
  /** Source version ID */
  sourceVersionId?: string;
}
