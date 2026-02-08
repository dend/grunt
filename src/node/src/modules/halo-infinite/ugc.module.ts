import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type {
  AuthoringAsset,
  AuthoringAssetVersion,
  AuthoringAssetContainer,
  AuthoringAssetVersionContainer,
  AuthoringAssetRating,
  AuthoringFavoritesContainer,
  FavoriteAsset,
  Permission,
  AssetReport,
  AssetAuthoringSession,
  AuthoringSessionSourceStarter,
} from '../../models/halo-infinite/ugc';

/**
 * UGC (User Generated Content) module for authoring operations.
 *
 * Provides access to:
 * - Creating, editing, and deleting user content
 * - Managing asset permissions
 * - Rating and favoriting assets
 * - Publishing and unpublishing assets
 *
 * @example
 * ```typescript
 * // Get an asset
 * const asset = await client.ugc.getAsset('hi', 'Map', 'asset-guid');
 *
 * // Favorite an asset
 * await client.ugc.favoriteAnAsset('xuid', 'Map', 'asset-guid');
 *
 * // List player's assets
 * const assets = await client.ugc.listPlayerAssets('hi', 'xuid', 'Map', 0, 25);
 * ```
 */
export class UgcModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.AUTHORING_ORIGIN);
  }

  // ─────────────────────────────────────────────────────────────────
  // Asset Retrieval
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get an asset by ID.
   *
   * @param title - Game title (e.g., 'hi' for Halo Infinite)
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Asset details
   */
  getAsset(
    title: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAsset>(`/${title}/${assetType}/${assetId}`);
  }

  /**
   * Get the latest version of an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Latest asset version
   */
  getLatestAssetVersion(
    title: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<AuthoringAssetVersion>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAssetVersion>(`/${title}/${assetType}/${assetId}/versions/latest`);
  }

  /**
   * Get the latest version of a film asset.
   *
   * @param title - Game title
   * @param assetId - Film asset GUID
   * @returns Latest film asset version
   */
  getLatestAssetVersionFilm(
    title: string,
    assetId: string
  ): Promise<HaloApiResult<AuthoringAssetVersion>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAssetVersion>(`/${title}/films/${assetId}/versions/latest`);
  }

  /**
   * Get a specific version of an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param versionId - Version GUID
   * @returns Asset version
   */
  getSpecificAssetVersion(
    title: string,
    assetType: string,
    assetId: string,
    versionId: string
  ): Promise<HaloApiResult<AuthoringAssetVersion>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.get<AuthoringAssetVersion>(
      `/${title}/${assetType}/${assetId}/versions/${versionId}`
    );
  }

  /**
   * Get the published version of an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Published asset version
   */
  getPublishedVersion(
    title: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<AuthoringAssetVersion>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAssetVersion>(`/${title}/${assetType}/${assetId}/versions/published`);
  }

  /**
   * List all versions of an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns All asset versions
   */
  listAllVersions(
    title: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<AuthoringAssetVersionContainer>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAssetVersionContainer>(`/${title}/${assetType}/${assetId}/versions`);
  }

  /**
   * List assets created by a player.
   *
   * @param title - Game title
   * @param player - Player XUID
   * @param assetType - Type of asset
   * @param start - Starting offset
   * @param count - Number of results
   * @returns Player's assets
   */
  listPlayerAssets(
    title: string,
    player: string,
    assetType: string,
    start: number = 0,
    count: number = 25
  ): Promise<HaloApiResult<AuthoringAssetContainer>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(player, 'player');
    return this.get<AuthoringAssetContainer>(
      `/${title}/players/xuid(${player})/${assetType}?start=${start}&count=${count}`
    );
  }

  // ─────────────────────────────────────────────────────────────────
  // Favorites
  // ─────────────────────────────────────────────────────────────────

  /**
   * Favorite an asset.
   *
   * @param player - Player XUID
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Favorite result
   */
  favoriteAnAsset(
    player: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<FavoriteAsset>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(assetId, 'assetId');
    return this.postJson<FavoriteAsset, { assetId: string; assetKind: string }>(
      `/hi/players/xuid(${player})/favorites`,
      { assetId, assetKind: assetType }
    );
  }

  /**
   * Check if a player has bookmarked an asset.
   *
   * @param title - Game title
   * @param player - Player XUID
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Favorite status
   */
  checkAssetPlayerBookmark(
    title: string,
    player: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<FavoriteAsset>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<FavoriteAsset>(
      `/${title}/players/xuid(${player})/favorites/${assetType}/${assetId}`
    );
  }

  /**
   * List player's favorite assets of a specific type.
   *
   * @param player - Player XUID
   * @param assetType - Type of asset
   * @returns Favorites container
   */
  listPlayerFavorites(
    player: string,
    assetType: string
  ): Promise<HaloApiResult<AuthoringFavoritesContainer>> {
    this.assertNotEmpty(player, 'player');
    return this.get<AuthoringFavoritesContainer>(
      `/hi/players/xuid(${player})/favorites/${assetType}`
    );
  }

  /**
   * List all of a player's favorite assets.
   *
   * @param player - Player XUID
   * @returns All favorites
   */
  listPlayerFavoritesAgnostic(
    player: string
  ): Promise<HaloApiResult<AuthoringFavoritesContainer>> {
    this.assertNotEmpty(player, 'player');
    return this.get<AuthoringFavoritesContainer>(`/hi/players/xuid(${player})/favorites`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Ratings & Reports
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a player's rating for an asset.
   *
   * @param player - Player XUID
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Rating info
   */
  getAssetRatings(
    player: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<AuthoringAssetRating>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(assetId, 'assetId');
    return this.get<AuthoringAssetRating>(
      `/hi/players/xuid(${player})/ratings/${assetType}/${assetId}`
    );
  }

  /**
   * Rate an asset.
   *
   * @param player - Player XUID
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param rating - Rating to submit
   * @returns Updated rating
   */
  rateAnAsset(
    player: string,
    assetType: string,
    assetId: string,
    rating: AuthoringAssetRating
  ): Promise<HaloApiResult<AuthoringAssetRating>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(assetId, 'assetId');
    return this.putJson<AuthoringAssetRating, AuthoringAssetRating>(
      `/hi/players/xuid(${player})/ratings/${assetType}/${assetId}`,
      rating
    );
  }

  /**
   * Report an asset for moderation.
   *
   * @param player - Player XUID
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param report - Report details
   * @returns Report result
   */
  reportAnAsset(
    player: string,
    assetType: string,
    assetId: string,
    report: AssetReport
  ): Promise<HaloApiResult<AssetReport>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(assetId, 'assetId');
    return this.postJson<AssetReport, AssetReport>(
      `/hi/players/xuid(${player})/reports/${assetType}/${assetId}`,
      report
    );
  }

  // ─────────────────────────────────────────────────────────────────
  // Asset Management
  // ─────────────────────────────────────────────────────────────────

  /**
   * Delete an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Success status
   */
  deleteAsset(
    title: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.delete<boolean>(`/${title}/${assetType}/${assetId}`);
  }

  /**
   * Delete all versions of an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Success status
   */
  deleteAllVersions(
    title: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.delete<boolean>(`/${title}/${assetType}/${assetId}/versions`);
  }

  /**
   * Delete a specific version of an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param versionId - Version GUID
   * @returns Success status
   */
  deleteVersion(
    title: string,
    assetType: string,
    assetId: string,
    versionId: string
  ): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.delete<boolean>(`/${title}/${assetType}/${assetId}/versions/${versionId}`);
  }

  /**
   * Undelete a previously deleted asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Success status
   */
  undeleteAsset(
    title: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.post<boolean>(`/${title}/${assetType}/${assetId}/recover`);
  }

  /**
   * Undelete a previously deleted asset version.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param versionId - Version GUID
   * @returns Success status
   */
  undeleteVersion(
    title: string,
    assetType: string,
    assetId: string,
    versionId: string
  ): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.post<boolean>(`/${title}/${assetType}/${assetId}/versions/${versionId}/recover`);
  }

  /**
   * Publish an asset version.
   *
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param versionId - Version GUID
   * @param clearanceId - Clearance ID
   * @returns Success status
   */
  publishAssetVersion(
    assetType: string,
    assetId: string,
    versionId: string,
    clearanceId: string
  ): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.post<boolean>(
      `/hi/${assetType}/${assetId}/versions/${versionId}/publish?clearanceId=${clearanceId}`
    );
  }

  /**
   * Unpublish an asset.
   *
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Success status
   */
  unpublishAsset(
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(assetId, 'assetId');
    return this.post<boolean>(`/hi/${assetType}/${assetId}/unpublish`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Permissions
  // ─────────────────────────────────────────────────────────────────

  /**
   * Grant or revoke permissions for an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param player - Player XUID to grant/revoke
   * @param permission - Permission details
   * @returns Updated permission
   */
  grantOrRevokePermissions(
    title: string,
    assetType: string,
    assetId: string,
    player: string,
    permission: Permission
  ): Promise<HaloApiResult<Permission>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(player, 'player');
    return this.putJson<Permission, Permission>(
      `/${title}/${assetType}/${assetId}/permissions/xuid(${player})`,
      permission
    );
  }

  // ─────────────────────────────────────────────────────────────────
  // Sessions
  // ─────────────────────────────────────────────────────────────────

  /**
   * Start an authoring session for an asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param includeContainerSas - Include container SAS URL
   * @returns Session details
   */
  startSession(
    title: string,
    assetType: string,
    assetId: string,
    includeContainerSas: boolean = false
  ): Promise<HaloApiResult<AssetAuthoringSession>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.post<AssetAuthoringSession>(
      `/${title}/${assetType}/${assetId}/sessions?includeContainerSas=${includeContainerSas}`
    );
  }

  /**
   * Extend an authoring session.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param includeContainerSas - Include container SAS URL
   * @returns Extended session
   */
  extendSession(
    title: string,
    assetType: string,
    assetId: string,
    includeContainerSas: boolean = false
  ): Promise<HaloApiResult<AssetAuthoringSession>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.putJson<AssetAuthoringSession, Record<string, never>>(
      `/${title}/${assetType}/${assetId}/sessions?includeContainerSas=${includeContainerSas}`,
      {}
    );
  }

  /**
   * End an authoring session.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @returns Success status
   */
  endSession(
    title: string,
    assetType: string,
    assetId: string
  ): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.delete<boolean>(`/${title}/${assetType}/${assetId}/sessions`);
  }

  /**
   * Spawn (create) a new asset.
   *
   * @param title - Game title
   * @param assetType - Type of asset (e.g., "UgcGameVariants", "Maps", "Prefabs")
   * @param asset - Asset definition
   * @returns Created asset
   */
  spawnAsset(
    title: string,
    assetType: string,
    asset: Record<string, unknown>
  ): Promise<HaloApiResult<AuthoringAsset>> {
    this.assertNotEmpty(title, 'title');
    return this.postJson<AuthoringAsset, Record<string, unknown>>(
      `/${title}/${assetType}`,
      asset
    );
  }

  /**
   * Create a new asset version.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param starter - Source asset to clone from
   * @returns New asset version
   */
  createAssetVersion(
    title: string,
    assetType: string,
    assetId: string,
    starter: AuthoringSessionSourceStarter
  ): Promise<HaloApiResult<AuthoringAssetVersion>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    return this.postJson<AuthoringAssetVersion, AuthoringSessionSourceStarter>(
      `/${title}/${assetType}/${assetId}/versions`,
      starter
    );
  }

  /**
   * Patch an asset version.
   *
   * @param title - Game title
   * @param assetType - Type of asset
   * @param assetId - Asset GUID
   * @param versionId - Version GUID
   * @param patchedAsset - Updated asset data
   * @returns Updated asset version
   */
  patchAssetVersion(
    title: string,
    assetType: string,
    assetId: string,
    versionId: string,
    patchedAsset: Partial<AuthoringAssetVersion>
  ): Promise<HaloApiResult<AuthoringAssetVersion>> {
    this.assertNotEmpty(title, 'title');
    this.assertNotEmpty(assetId, 'assetId');
    this.assertNotEmpty(versionId, 'versionId');
    return this.patchJson<AuthoringAssetVersion, Partial<AuthoringAssetVersion>>(
      `/${title}/${assetType}/${assetId}/versions/${versionId}`,
      patchedAsset
    );
  }

  // ─────────────────────────────────────────────────────────────────
  // Blob Storage
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a blob from UGC storage.
   *
   * @param blobPath - Path to the blob
   * @returns Blob data as bytes
   */
  getBlob(blobPath: string): Promise<HaloApiResult<Uint8Array>> {
    this.assertNotEmpty(blobPath, 'blobPath');
    const blobUrl = `https://${HALO_CORE_ENDPOINTS.BLOBS_ORIGIN}.${HALO_CORE_ENDPOINTS.SERVICE_DOMAIN}${blobPath}`;
    return this.getFullUrl<Uint8Array>(blobUrl, { useSpartanToken: false, returnRaw: true });
  }
}
