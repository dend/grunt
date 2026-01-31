import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { AssetKind } from '../../models/halo-infinite/enums/asset-kind';
import type { ResultOrder } from '../../models/halo-infinite/enums/result-order';
import type { UgcSearchResult, AuthoringAsset } from '../../models/halo-infinite/ugc';

/**
 * UGC Discovery module for searching and browsing user content.
 *
 * Provides access to:
 * - Searching for maps, game variants, and other content
 * - Browsing featured and popular content
 * - Getting recommended content
 *
 * @example
 * ```typescript
 * // Search for maps
 * const maps = await client.ugcDiscovery.search({
 *   assetKinds: [AssetKind.Map],
 *   term: 'blood gulch',
 *   count: 10,
 * });
 *
 * // Get featured maps
 * const featured = await client.ugcDiscovery.getFeatured('Map');
 * ```
 */
export class UgcDiscoveryModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.DISCOVERY_ORIGIN);
  }

  /**
   * Search for user-generated content.
   *
   * @param params - Search parameters
   * @returns Search results
   */
  search(params: {
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
    /** Number of results */
    count?: number;
    /** Starting offset */
    start?: number;
  }): Promise<HaloApiResult<UgcSearchResult>> {
    const queryParts: string[] = [];

    if (params.term) {
      queryParts.push(`term=${encodeURIComponent(params.term)}`);
    }
    if (params.assetKinds?.length) {
      params.assetKinds.forEach((kind) => {
        queryParts.push(`assetKind=${kind}`);
      });
    }
    if (params.tags?.length) {
      params.tags.forEach((tag) => {
        queryParts.push(`tags=${encodeURIComponent(tag)}`);
      });
    }
    if (params.author) {
      queryParts.push(`author=xuid(${params.author})`);
    }
    if (params.sort) {
      queryParts.push(`sort=${params.sort}`);
    }
    if (params.order) {
      queryParts.push(`order=${params.order}`);
    }
    if (params.count !== undefined) {
      queryParts.push(`count=${params.count}`);
    }
    if (params.start !== undefined) {
      queryParts.push(`start=${params.start}`);
    }

    const queryString = queryParts.length > 0 ? `?${queryParts.join('&')}` : '';
    return this.get<UgcSearchResult>(`/hi/search${queryString}`);
  }

  /**
   * Get featured content of a specific type.
   *
   * @param assetKind - Type of asset
   * @returns Featured assets
   */
  getFeatured(assetKind: AssetKind): Promise<HaloApiResult<UgcSearchResult>> {
    return this.get<UgcSearchResult>(`/hi/featured/${assetKind}`);
  }

  /**
   * Get popular content of a specific type.
   *
   * @param assetKind - Type of asset
   * @param start - Starting offset
   * @param count - Number of results
   * @returns Popular assets
   */
  getPopular(
    assetKind: AssetKind,
    start: number = 0,
    count: number = 25
  ): Promise<HaloApiResult<UgcSearchResult>> {
    return this.get<UgcSearchResult>(
      `/hi/popular/${assetKind}?start=${start}&count=${count}`
    );
  }

  /**
   * Get recent content of a specific type.
   *
   * @param assetKind - Type of asset
   * @param start - Starting offset
   * @param count - Number of results
   * @returns Recent assets
   */
  getRecent(
    assetKind: AssetKind,
    start: number = 0,
    count: number = 25
  ): Promise<HaloApiResult<UgcSearchResult>> {
    return this.get<UgcSearchResult>(
      `/hi/recent/${assetKind}?start=${start}&count=${count}`
    );
  }

  /**
   * Get recommended content for a player.
   *
   * @param player - Player XUID
   * @param assetKind - Type of asset
   * @param count - Number of results
   * @returns Recommended assets
   */
  getRecommended(
    player: string,
    assetKind: AssetKind,
    count: number = 10
  ): Promise<HaloApiResult<UgcSearchResult>> {
    this.assertNotEmpty(player, 'player');
    return this.get<UgcSearchResult>(
      `/hi/players/xuid(${player})/recommendations/${assetKind}?count=${count}`
    );
  }

  /**
   * Browse content by tag.
   *
   * @param assetKind - Type of asset
   * @param tag - Tag to filter by
   * @param start - Starting offset
   * @param count - Number of results
   * @returns Tagged assets
   */
  browseByTag(
    assetKind: AssetKind,
    tag: string,
    start: number = 0,
    count: number = 25
  ): Promise<HaloApiResult<UgcSearchResult>> {
    this.assertNotEmpty(tag, 'tag');
    return this.get<UgcSearchResult>(
      `/hi/tags/${encodeURIComponent(tag)}/${assetKind}?start=${start}&count=${count}`
    );
  }

  /**
   * Get asset details for discovery purposes.
   *
   * @param assetKind - Type of asset
   * @param assetId - Asset GUID
   * @returns Asset details
   */
  getAssetDetails(
    assetKind: AssetKind,
    assetId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/${assetKind}/${assetId}`);
  }

  /**
   * Get film asset for a match.
   *
   * @param matchId - Match GUID
   * @returns Film asset if available
   */
  getFilmByMatchId(matchId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(matchId, 'matchId');
    return this.get<AuthoringAsset>(`/hi/films/matches/${matchId}/spectate`);
  }
}
