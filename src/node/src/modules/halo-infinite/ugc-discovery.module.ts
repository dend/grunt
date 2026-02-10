import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { ResultOrder } from '../../models/halo-infinite/enums/result-order';
import type { UgcSearchResult, AuthoringAsset } from '../../models/halo-infinite/ugc';

/**
 * UGC Discovery module for searching and browsing user content.
 *
 * Provides access to:
 * - Searching for maps, game variants, and other content
 * - Browsing featured and popular content
 * - Getting recommended content
 * - Getting specific asset types (maps, playlists, prefabs, etc.)
 *
 * @example
 * ```typescript
 * // Search for maps
 * const maps = await client.ugcDiscovery.search({
 *   assetKinds: ['Map'],
 *   term: 'blood gulch',
 *   count: 10,
 * });
 *
 * // Get a specific map
 * const map = await client.ugcDiscovery.getMap('asset-id', 'version-id');
 *
 * // Get featured maps
 * const featured = await client.ugcDiscovery.getFeatured('Map');
 * ```
 */
export class UgcDiscoveryModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.DISCOVERY_ORIGIN);
  }

  // ─────────────────────────────────────────────────────────────────
  // Search & Browse
  // ─────────────────────────────────────────────────────────────────

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
    assetKinds?: string[];
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
    /** Minimum average rating between 0 and 5 */
    averageRatingMin?: number;
    /** Minimum date created */
    fromDateCreatedUtc?: Date;
    /** Maximum date created */
    toDateCreatedUtc?: Date;
    /** Minimum date modified */
    fromDateModifiedUtc?: Date;
    /** Maximum date modified */
    toDateModifiedUtc?: Date;
    /** Minimum date published */
    fromDatePublishedUtc?: Date;
    /** Maximum date published */
    toDatePublishedUtc?: Date;
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
    if (params.averageRatingMin !== undefined) {
      queryParts.push(`averageRatingMin=${params.averageRatingMin}`);
    }
    if (params.fromDateCreatedUtc) {
      queryParts.push(`fromDateCreatedUtc=${encodeURIComponent(params.fromDateCreatedUtc.toISOString())}`);
    }
    if (params.toDateCreatedUtc) {
      queryParts.push(`toDateCreatedUtc=${encodeURIComponent(params.toDateCreatedUtc.toISOString())}`);
    }
    if (params.fromDateModifiedUtc) {
      queryParts.push(`fromDateModifiedUtc=${encodeURIComponent(params.fromDateModifiedUtc.toISOString())}`);
    }
    if (params.toDateModifiedUtc) {
      queryParts.push(`toDateModifiedUtc=${encodeURIComponent(params.toDateModifiedUtc.toISOString())}`);
    }
    if (params.fromDatePublishedUtc) {
      queryParts.push(`fromDatePublishedUtc=${encodeURIComponent(params.fromDatePublishedUtc.toISOString())}`);
    }
    if (params.toDatePublishedUtc) {
      queryParts.push(`toDatePublishedUtc=${encodeURIComponent(params.toDatePublishedUtc.toISOString())}`);
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
  getFeatured(assetKind: string): Promise<HaloApiResult<UgcSearchResult>> {
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
    assetKind: string,
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
    assetKind: string,
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
    assetKind: string,
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
    assetKind: string,
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
    assetKind: string,
    assetId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/${assetKind}/${assetId}`);
  }

  /**
   * Get tags information.
   *
   * @returns Available tags info
   */
  getTagsInfo(): Promise<HaloApiResult<AuthoringAsset>> {
    return this.get<AuthoringAsset>('/hi/info/tags');
  }

  // ─────────────────────────────────────────────────────────────────
  // Manifests
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get the game manifest by build GUID.
   *
   * @param buildGuid - Build GUID
   * @returns Manifest data
   */
  getManifestByBuildGuid(buildGuid: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(buildGuid, 'buildGuid');
    return this.get<AuthoringAsset>(`/hi/manifests/guids/${buildGuid}/game`);
  }

  /**
   * Get the game manifest by build number.
   *
   * @param buildNumber - Build number (e.g., "6.10022.10499")
   * @returns Manifest data
   */
  getManifestByBuild(buildNumber: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(buildNumber, 'buildNumber');
    return this.get<AuthoringAsset>(`/hi/manifests/builds/${buildNumber}/game`);
  }

  /**
   * Get a specific manifest version.
   *
   * @param assetId - Manifest asset ID
   * @param versionId - Manifest version ID
   * @param clearanceId - Active flight clearance ID
   * @returns Manifest data
   */
  getManifest(
    assetId: string,
    versionId: string,
    clearanceId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAsset>(
      `/hi/manifests/${assetId}/versions/${versionId}?clearanceId=${clearanceId}`
    );
  }

  // ─────────────────────────────────────────────────────────────────
  // Maps
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a specific map version.
   *
   * @param assetId - Map asset ID
   * @param versionId - Map version ID
   * @returns Map data
   */
  getMap(assetId: string, versionId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAsset>(`/hi/maps/${assetId}/versions/${versionId}`);
  }

  /**
   * Get a map without specifying version (returns latest).
   *
   * @param assetId - Map asset ID
   * @returns Map data
   */
  getMapWithoutVersion(assetId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/maps/${assetId}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Map Mode Pairs
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a specific map mode pair version.
   *
   * @param assetId - Map mode pair asset ID
   * @param versionId - Version ID
   * @param clearanceId - Active flight clearance ID
   * @returns Map mode pair data
   */
  getMapModePair(
    assetId: string,
    versionId: string,
    clearanceId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAsset>(
      `/hi/mapModePairs/${assetId}/versions/${versionId}?clearanceId=${clearanceId}`
    );
  }

  /**
   * Get a map mode pair without specifying version.
   *
   * @param assetId - Map mode pair asset ID
   * @returns Map mode pair data
   */
  getMapModePairWithoutVersion(assetId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/mapModePairs/${assetId}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Playlists
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a specific playlist version.
   *
   * @param assetId - Playlist asset ID
   * @param versionId - Version ID
   * @param clearanceId - Active flight clearance ID
   * @returns Playlist data
   */
  getPlaylist(
    assetId: string,
    versionId: string,
    clearanceId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAsset>(
      `/hi/playlists/${assetId}/versions/${versionId}?clearanceId=${clearanceId}`
    );
  }

  /**
   * Get a playlist without specifying version.
   *
   * @param assetId - Playlist asset ID
   * @returns Playlist data
   */
  getPlaylistWithoutVersion(assetId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/playlists/${assetId}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Prefabs
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a specific prefab version.
   *
   * @param assetId - Prefab asset ID
   * @param versionId - Version ID
   * @returns Prefab data
   */
  getPrefab(assetId: string, versionId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAsset>(`/hi/prefabs/${assetId}/versions/${versionId}`);
  }

  /**
   * Get a prefab without specifying version.
   *
   * @param assetId - Prefab asset ID
   * @returns Prefab data
   */
  getPrefabWithoutVersion(assetId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/prefabs/${assetId}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Projects
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a specific project version.
   *
   * @param assetId - Project asset ID
   * @param versionId - Version ID
   * @returns Project data
   */
  getProject(assetId: string, versionId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAsset>(`/hi/projects/${assetId}/versions/${versionId}`);
  }

  /**
   * Get a project without specifying version.
   *
   * @param assetId - Project asset ID
   * @returns Project data
   */
  getProjectWithoutVersion(assetId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/projects/${assetId}`);
  }

  /**
   * Get the Forge templates (canvases).
   *
   * @returns Forge templates project
   */
  getForgeTemplates(): Promise<HaloApiResult<AuthoringAsset>> {
    return this.get<AuthoringAsset>('/hi/projects/bf0e9bab-6fed-47a4-8bf7-bfd4422ee552');
  }

  /**
   * Get the Forge mode categories.
   *
   * @returns Forge mode categories project
   */
  getForgeModeCategories(): Promise<HaloApiResult<AuthoringAsset>> {
    return this.get<AuthoringAsset>('/hi/projects/aff73c44-0771-468f-b9cf-5c52eee7ab4c');
  }

  /**
   * Get the community tab assets.
   *
   * @returns Community tab project
   */
  getCommunityTab(): Promise<HaloApiResult<AuthoringAsset>> {
    return this.get<AuthoringAsset>('/hi/projects/90f9e508-99ce-411c-bf88-7bf12b5e9f52');
  }

  /**
   * Get 343 recommended assets.
   *
   * @returns 343 recommended project
   */
  get343Recommended(): Promise<HaloApiResult<AuthoringAsset>> {
    return this.get<AuthoringAsset>('/hi/projects/712add52-f989-48e1-b3bb-ac7cd8a1c17a');
  }

  // ─────────────────────────────────────────────────────────────────
  // Game Variants
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a specific engine game variant version.
   *
   * @param assetId - Engine game variant asset ID
   * @param versionId - Version ID
   * @returns Engine game variant data
   */
  getEngineGameVariant(
    assetId: string,
    versionId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAsset>(
      `/hi/engineGameVariants/${assetId}/versions/${versionId}`
    );
  }

  /**
   * Get an engine game variant without specifying version.
   *
   * @param assetId - Engine game variant asset ID
   * @returns Engine game variant data
   */
  getEngineGameVariantWithoutVersion(
    assetId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/engineGameVariants/${assetId}`);
  }

  /**
   * Get a specific UGC game variant version.
   *
   * @param assetId - UGC game variant asset ID
   * @param versionId - Version ID
   * @returns UGC game variant data
   */
  getUgcGameVariant(
    assetId: string,
    versionId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAsset>(
      `/hi/ugcGameVariants/${assetId}/versions/${versionId}`
    );
  }

  /**
   * Get a UGC game variant without specifying version.
   *
   * @param assetId - UGC game variant asset ID
   * @returns UGC game variant data
   */
  getUgcGameVariantWithoutVersion(
    assetId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/ugcGameVariants/${assetId}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Films
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a film by asset ID.
   *
   * @param assetId - Film asset ID
   * @returns Film data
   */
  getFilm(assetId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/hi/films/${assetId}`);
  }

  /**
   * Get film asset for a match (spectate).
   *
   * @param matchId - Match GUID
   * @returns Film asset if available
   */
  spectateByMatchId(matchId: string): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(matchId, 'matchId');
    return this.get<AuthoringAsset>(`/hi/films/matches/${matchId}/spectate`);
  }

  /**
   * Get film asset for a match.
   *
   * @param matchId - Match GUID
   * @returns Film asset if available
   * @deprecated Use spectateByMatchId instead
   */
  getFilmByMatchId(matchId: string): Promise<HaloApiResult<AuthoringAsset>> {
    return this.spectateByMatchId(matchId);
  }
}
