import type { AuthoringAsset } from './asset';
import type { AssetKind } from '../enums/asset-kind';
import type { ResultOrder } from '../enums/result-order';

/**
 * Search parameters for UGC discovery.
 */
export interface UgcSearchParams {
  /** Search term */
  term?: string;
  /** Asset kinds to include */
  assetKinds?: AssetKind[];
  /** Tags to filter by */
  tags?: string[];
  /** Author XUID */
  author?: string;
  /** Sort field */
  sort?: string;
  /** Sort order */
  order?: ResultOrder;
  /** Number of results to return */
  count?: number;
  /** Starting offset */
  start?: number;
}

/**
 * Search result container.
 */
export interface UgcSearchResult {
  /** Search results */
  results?: AuthoringAsset[];
  /** Total matching results */
  totalCount?: number;
  /** Returned count */
  count?: number;
  /** Starting offset */
  start?: number;
  /** Pagination links */
  links?: SearchLinks;
}

/**
 * Search pagination links.
 */
export interface SearchLinks {
  /** Current page */
  self?: string;
  /** Next page */
  next?: string;
  /** Previous page */
  prev?: string;
}

/**
 * Map variant asset.
 */
export interface MapAsset extends Omit<AuthoringAsset, 'customData'> {
  /** Map-specific custom data */
  customData?: MapCustomData;
}

/**
 * Map custom data.
 */
export interface MapCustomData {
  /** Number of supported players */
  supportedPlayerCount?: number;
  /** Recommended player count */
  recommendedPlayerCount?: number;
  /** Budget usage */
  budgetUsed?: number;
  /** Object count */
  objectCount?: number;
  /** Has lightmap */
  hasLightmap?: boolean;
}

/**
 * Game variant asset.
 */
export interface UgcGameVariantAsset extends Omit<AuthoringAsset, 'customData'> {
  /** Game variant custom data */
  customData?: GameVariantCustomData;
}

/**
 * Game variant custom data.
 */
export interface GameVariantCustomData {
  /** Game variant category */
  category?: number;
  /** Score to win */
  scoreToWin?: number;
  /** Time limit */
  timeLimit?: number;
  /** Rounds to win */
  roundsToWin?: number;
  /** Max players */
  maxPlayers?: number;
  /** Min players */
  minPlayers?: number;
}

/**
 * Film asset (theater recording).
 */
export interface FilmAsset extends Omit<AuthoringAsset, 'customData'> {
  /** Film custom data */
  customData?: FilmCustomData;
}

/**
 * Film custom data.
 */
export interface FilmCustomData {
  /** Match ID this film is from */
  matchId?: string;
  /** Duration of the film */
  duration?: string;
  /** Map asset ID */
  mapAssetId?: string;
  /** Game variant asset ID */
  gameVariantAssetId?: string;
}

/**
 * Prefab asset.
 */
export interface PrefabAsset extends Omit<AuthoringAsset, 'customData'> {
  /** Prefab custom data */
  customData?: PrefabCustomData;
}

/**
 * Prefab custom data.
 */
export interface PrefabCustomData {
  /** Object count */
  objectCount?: number;
  /** Budget usage */
  budgetUsed?: number;
}
