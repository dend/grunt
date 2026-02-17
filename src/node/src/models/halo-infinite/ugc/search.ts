import type { AuthoringAsset } from './asset';
import type { AssetKind } from '../enums/asset-kind';
import type { ResultOrder } from '../enums/result-order';

/**
 * Search parameters for UGC discovery.
 */
export interface UgcSearchParams {
  /** Search term */
  Term?: string;
  /** Asset kinds to include */
  AssetKinds?: AssetKind[];
  /** Tags to filter by */
  Tags?: string[];
  /** Author XUID */
  Author?: string;
  /** Sort field */
  Sort?: string;
  /** Sort order */
  Order?: ResultOrder;
  /** Number of results to return */
  Count?: number;
  /** Starting offset */
  Start?: number;
}

/**
 * Search result container.
 */
export interface UgcSearchResult {
  /** Search results */
  Results?: AuthoringAsset[];
  /** Total matching results */
  TotalCount?: number;
  /** Returned count */
  Count?: number;
  /** Starting offset */
  Start?: number;
  /** Pagination links */
  Links?: SearchLinks;
}

/**
 * Search pagination links.
 */
export interface SearchLinks {
  /** Current page */
  Self?: string;
  /** Next page */
  Next?: string;
  /** Previous page */
  Prev?: string;
}

/**
 * Map variant asset.
 */
export interface MapAsset extends Omit<AuthoringAsset, 'CustomData'> {
  /** Map-specific custom data */
  CustomData?: MapCustomData;
}

/**
 * Map custom data.
 */
export interface MapCustomData {
  /** Number of supported players */
  SupportedPlayerCount?: number;
  /** Recommended player count */
  RecommendedPlayerCount?: number;
  /** Budget usage */
  BudgetUsed?: number;
  /** Object count */
  ObjectCount?: number;
  /** Has lightmap */
  HasLightmap?: boolean;
}

/**
 * Game variant asset.
 */
export interface UgcGameVariantAsset extends Omit<AuthoringAsset, 'CustomData'> {
  /** Game variant custom data */
  CustomData?: GameVariantCustomData;
}

/**
 * Game variant custom data.
 */
export interface GameVariantCustomData {
  /** Game variant category */
  Category?: number;
  /** Score to win */
  ScoreToWin?: number;
  /** Time limit */
  TimeLimit?: number;
  /** Rounds to win */
  RoundsToWin?: number;
  /** Max players */
  MaxPlayers?: number;
  /** Min players */
  MinPlayers?: number;
}

/**
 * Film asset (theater recording).
 */
export interface FilmAsset extends Omit<AuthoringAsset, 'CustomData'> {
  /** Film custom data */
  CustomData?: FilmCustomData;
}

/**
 * Film custom data.
 */
export interface FilmCustomData {
  /** Match ID this film is from */
  MatchId?: string;
  /** Duration of the film */
  Duration?: string;
  /** Map asset ID */
  MapAssetId?: string;
  /** Game variant asset ID */
  GameVariantAssetId?: string;
}

/**
 * Prefab asset.
 */
export interface PrefabAsset extends Omit<AuthoringAsset, 'CustomData'> {
  /** Prefab custom data */
  CustomData?: PrefabCustomData;
}

/**
 * Prefab custom data.
 */
export interface PrefabCustomData {
  /** Object count */
  ObjectCount?: number;
  /** Budget usage */
  BudgetUsed?: number;
}
