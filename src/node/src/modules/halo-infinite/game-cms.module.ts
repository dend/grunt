import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type { DisplayString, CurrencyDefinition, StoreOffering } from '../../models/halo-infinite/economy';
import type { Challenge, ChallengeDeckDefinition, RewardTrackMetadata, CareerTrackContainer } from '../../models/halo-infinite/progression';
import type { MedalMetadata, News, SeasonCalendar, AcademyStarDefinitions } from '../../models/halo-infinite/misc';

/**
 * In-game item definition from CMS.
 */
export interface InGameItem {
  /** Tag identifier */
  tagId?: number;
  /** Common item data */
  commonData?: CommonItemData;
  /** Image path */
  imagePath?: string;
  /** Is kit */
  isKit?: boolean;
  /** Quality */
  quality?: string;
}

/**
 * Common data for in-game items.
 */
export interface CommonItemData {
  /** Item identifier */
  id?: string;
  /** Title */
  title?: DisplayString;
  /** Description */
  description?: DisplayString;
  /** Quality tier */
  quality?: string;
  /** Manufacturing season */
  manufacturingSeason?: string;
}

/**
 * Inventory definition from CMS.
 */
export interface InventoryDefinition {
  /** Item definitions */
  items?: Record<string, InGameItem>;
}

/**
 * Metadata container.
 */
export interface Metadata {
  /** Season info */
  season?: SeasonInfo;
  /** Active playlists */
  playlists?: PlaylistInfo[];
}

/**
 * Season info from metadata.
 */
export interface SeasonInfo {
  /** Season identifier */
  seasonId?: string;
  /** Season number */
  seasonNumber?: number;
}

/**
 * Playlist info.
 */
export interface PlaylistInfo {
  /** Playlist ID */
  playlistId?: string;
  /** Playlist name */
  name?: DisplayString;
  /** Description */
  description?: DisplayString;
}

/**
 * Game CMS module for static content and definitions.
 *
 * Provides access to:
 * - Item definitions
 * - Challenge definitions
 * - Season and career metadata
 * - Medal information
 * - News and guides
 *
 * @example
 * ```typescript
 * // Get medal metadata
 * const medals = await client.gameCms.getMedalMetadata();
 *
 * // Get career ranks
 * const careerRanks = await client.gameCms.getCareerRanks('career-path-id');
 *
 * // Get item definition
 * const item = await client.gameCms.getItem('item/path/here', 'flight-id');
 * ```
 */
export class GameCmsModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.GAME_CMS_ORIGIN);
  }

  // ─────────────────────────────────────────────────────────────────
  // Items & Inventory
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get an item definition by path.
   *
   * @param itemPath - Path to the item
   * @param flightId - Flight ID for clearance
   * @returns Item definition
   */
  getItem(itemPath: string, flightId?: string): Promise<HaloApiResult<InGameItem>> {
    this.assertNotEmpty(itemPath, 'itemPath');
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<InGameItem>(`/hi/Progression/file/${itemPath}${flightParam}`, {
      useClearance: !!flightId,
    });
  }

  /**
   * Get the customization catalog/inventory definitions.
   *
   * @param flightId - Optional flight ID for flighted content
   * @returns Inventory definition
   */
  getCustomizationCatalog(flightId?: string): Promise<HaloApiResult<InventoryDefinition>> {
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<InventoryDefinition>(`/hi/Progression/file/inventory/catalog${flightParam}`, {
      useClearance: !!flightId,
    });
  }

  /**
   * Get a store offering definition.
   *
   * @param offeringPath - Path to the offering
   * @returns Store offering
   */
  getStoreOffering(offeringPath: string): Promise<HaloApiResult<StoreOffering>> {
    this.assertNotEmpty(offeringPath, 'offeringPath');
    return this.get<StoreOffering>(`/hi/Progression/file/${offeringPath}`);
  }

  /**
   * Get a currency definition.
   *
   * @param currencyPath - Path to the currency
   * @param flightId - Optional flight ID
   * @returns Currency definition
   */
  getCurrency(
    currencyPath: string,
    flightId?: string
  ): Promise<HaloApiResult<CurrencyDefinition>> {
    this.assertNotEmpty(currencyPath, 'currencyPath');
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<CurrencyDefinition>(`/hi/Progression/file/${currencyPath}${flightParam}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Challenges & Events
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get a challenge definition.
   *
   * @param challengePath - Path to the challenge
   * @param flightId - Optional flight ID
   * @returns Challenge definition
   */
  getChallenge(
    challengePath: string,
    flightId?: string
  ): Promise<HaloApiResult<Challenge>> {
    this.assertNotEmpty(challengePath, 'challengePath');
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<Challenge>(`/hi/Progression/file/${challengePath}${flightParam}`, {
      useClearance: !!flightId,
    });
  }

  /**
   * Get a challenge deck definition.
   *
   * @param challengeDeckPath - Path to the challenge deck
   * @param flightId - Optional flight ID
   * @returns Challenge deck definition
   */
  getChallengeDeck(
    challengeDeckPath: string,
    flightId?: string
  ): Promise<HaloApiResult<ChallengeDeckDefinition>> {
    this.assertNotEmpty(challengeDeckPath, 'challengeDeckPath');
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<ChallengeDeckDefinition>(
      `/hi/Progression/file/${challengeDeckPath}${flightParam}`,
      { useClearance: !!flightId }
    );
  }

  /**
   * Get an event/reward track definition.
   *
   * @param eventPath - Path to the event
   * @param flightId - Optional flight ID
   * @returns Reward track metadata
   */
  getEvent(
    eventPath: string,
    flightId?: string
  ): Promise<HaloApiResult<RewardTrackMetadata>> {
    this.assertNotEmpty(eventPath, 'eventPath');
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<RewardTrackMetadata>(`/hi/Progression/file/${eventPath}${flightParam}`, {
      useClearance: !!flightId,
    });
  }

  // ─────────────────────────────────────────────────────────────────
  // Career & Seasons
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get career rank definitions.
   *
   * @param careerPathId - Career path identifier
   * @returns Career track container
   */
  getCareerRanks(careerPathId: string): Promise<HaloApiResult<CareerTrackContainer>> {
    this.assertNotEmpty(careerPathId, 'careerPathId');
    return this.get<CareerTrackContainer>(
      `/hi/Progression/file/RewardTracks/CareerRanks/${careerPathId}.json`,
      { useClearance: true }
    );
  }

  /**
   * Get the season calendar.
   *
   * @returns Season calendar
   */
  getSeasonCalendar(): Promise<HaloApiResult<SeasonCalendar>> {
    return this.get<SeasonCalendar>('/hi/Progression/file/calendars/seasons');
  }

  /**
   * Get the CSR/ranked season calendar.
   *
   * @returns CSR season calendar
   */
  getCsrCalendar(): Promise<HaloApiResult<SeasonCalendar>> {
    return this.get<SeasonCalendar>('/hi/Progression/file/calendars/csrseasons');
  }

  /**
   * Get a season reward track definition.
   *
   * @param seasonPath - Path to the season
   * @param flightId - Optional flight ID
   * @returns Season reward track
   */
  getSeasonRewardTrack(
    seasonPath: string,
    flightId?: string
  ): Promise<HaloApiResult<RewardTrackMetadata>> {
    this.assertNotEmpty(seasonPath, 'seasonPath');
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<RewardTrackMetadata>(`/hi/Progression/file/${seasonPath}${flightParam}`, {
      useClearance: !!flightId,
    });
  }

  // ─────────────────────────────────────────────────────────────────
  // Medals & Metadata
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get medal metadata and definitions.
   *
   * @returns Medal metadata
   */
  getMedalMetadata(): Promise<HaloApiResult<MedalMetadata>> {
    return this.get<MedalMetadata>('/hi/Waypoint/file/medals/metadata.json');
  }

  /**
   * Get general game metadata.
   *
   * @param flightId - Optional flight ID
   * @returns Metadata
   */
  getMetadata(flightId?: string): Promise<HaloApiResult<Metadata>> {
    const flightParam = flightId ? `?flight=${flightId}` : '';
    return this.get<Metadata>(`/hi/Progression/file/metadata${flightParam}`, {
      useClearance: !!flightId,
    });
  }

  // ─────────────────────────────────────────────────────────────────
  // News & Guides
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get news articles.
   *
   * @param filePath - Path to news file
   * @returns News collection
   */
  getNews(filePath: string): Promise<HaloApiResult<News>> {
    this.assertNotEmpty(filePath, 'filePath');
    return this.get<News>(`/hi/news/${filePath}`);
  }

  /**
   * Get academy star definitions.
   *
   * @returns Star definitions
   */
  getAcademyStarDefinitions(): Promise<HaloApiResult<AcademyStarDefinitions>> {
    return this.get<AcademyStarDefinitions>('/hi/Progression/file/academy/stars');
  }

  // ─────────────────────────────────────────────────────────────────
  // Raw Files & Images
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get an image file from the CMS.
   *
   * @param filePath - Path to the image
   * @returns Image data as bytes
   */
  getImage(filePath: string): Promise<HaloApiResult<Uint8Array>> {
    this.assertNotEmpty(filePath, 'filePath');
    return this.get<Uint8Array>(`/hi/images/file/${filePath}`, { returnRaw: true });
  }

  /**
   * Get a generic file from the CMS.
   *
   * @param filePath - Path to the file
   * @returns File data as bytes
   */
  getGenericFile(filePath: string): Promise<HaloApiResult<Uint8Array>> {
    this.assertNotEmpty(filePath, 'filePath');
    return this.get<Uint8Array>(`/hi/Progression/file/${filePath}`, { returnRaw: true });
  }

  /**
   * Get a raw progression file with custom type.
   *
   * @template T - Expected return type
   * @param filePath - Path to the file
   * @returns Typed file contents
   */
  getProgressionFile<T>(filePath: string): Promise<HaloApiResult<T>> {
    this.assertNotEmpty(filePath, 'filePath');
    return this.get<T>(`/hi/Progression/file/${filePath}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Achievements & Settings
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get all available achievements.
   *
   * @returns Achievement collection
   */
  getAchievements(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>('/hi/Multiplayer/file/Live/Achievements.json');
  }

  /**
   * Get the Play Now button settings (fallback playlist).
   *
   * @returns Fallback playlist settings
   */
  getPlayNowButtonSettings(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>(
      '/hi/Multiplayer/file/playlists/playNowButton/settings.json'
    );
  }

  /**
   * Get custom game default settings.
   *
   * @returns Custom game definition
   */
  getCustomGameDefaults(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>('/hi/Multiplayer/file/NonMatchmaking/customgame.json');
  }

  /**
   * Get lobby error messages.
   *
   * @param flightId - Flight ID
   * @returns Lobby error message list
   */
  getLobbyErrorMessages(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(
      `/hi/Multiplayer/file/gameStartErrorMessages/LobbyHoppperErrorMessageList.json?flight=${flightId}`
    );
  }

  /**
   * Get network configuration.
   *
   * @param flightId - Flight ID
   * @returns Network configuration
   */
  getNetworkConfiguration(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(
      `/hi/Multiplayer/file/network/config.json?flight=${flightId}`
    );
  }

  /**
   * Get a multiplayer playlist configuration.
   *
   * @param playlistFile - Playlist file name (e.g., "uuid.json")
   * @returns Playlist configuration
   */
  getMultiplayerPlaylistConfiguration(
    playlistFile: string
  ): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(playlistFile, 'playlistFile');
    return this.get<Record<string, unknown>>(
      `/hi/Multiplayer/file/playlists/assets/${playlistFile}`
    );
  }

  // ─────────────────────────────────────────────────────────────────
  // Graphics & Specs
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get async compute overrides.
   *
   * @returns Async compute override configuration
   */
  getAsyncComputeOverrides(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>('/hi/Specs/file/graphics/AsyncComputeOverrides.json');
  }

  /**
   * Get CPU presets.
   *
   * @returns CPU preset configuration
   */
  getCpuPresets(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>('/hi/Specs/file/cpu/presets.json');
  }

  /**
   * Get device preset overrides.
   *
   * @returns Device preset overrides
   */
  getDevicePresetOverrides(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>('/hi/Specs/file/graphics/DevicePresetOverrides.json');
  }

  /**
   * Get graphics spec control overrides.
   *
   * @returns Graphics spec control overrides
   */
  getGraphicsSpecControlOverrides(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>(
      '/hi/Specs/file/graphics/GraphicsSpecControlOverrides.json'
    );
  }

  /**
   * Get graphics specs/overrides.
   *
   * @returns Graphics overrides
   */
  getGraphicSpecs(): Promise<HaloApiResult<string>> {
    return this.get<string>('/hi/Specs/file/graphics/overrides.json');
  }

  /**
   * Get recommended drivers.
   *
   * @returns Driver manifest
   */
  getRecommendedDrivers(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>('/hi/Specs/file/graphics/RecommendedDrivers.json');
  }

  // ─────────────────────────────────────────────────────────────────
  // Title Authorization & Access
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get claw (clawback) access list.
   *
   * @param flightId - Flight ID
   * @returns Claw access snapshot
   */
  getClawAccess(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(
      `/hi/TitleAuthorization/file/claw/access.json?flight=${flightId}`
    );
  }

  /**
   * Get not allowed in title message.
   *
   * @returns OE configuration message
   */
  getNotAllowedInTitleMessage(): Promise<HaloApiResult<Record<string, unknown>>> {
    const url = `https://${HALO_CORE_ENDPOINTS.GAME_CMS_ORIGIN}.${HALO_CORE_ENDPOINTS.SERVICE_DOMAIN}/branches/hi/OEConfiguration/data/authfail/Default.json`;
    return this.getFullUrl<Record<string, unknown>>(url, { useSpartanToken: false });
  }

  // ─────────────────────────────────────────────────────────────────
  // Guides (File Listings)
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get guide for images files.
   *
   * @param flightId - Flight ID
   * @returns Guide container with file listings
   */
  getGuideImages(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(`/hi/images/guide/xo?flight=${flightId}`);
  }

  /**
   * Get guide for multiplayer files.
   *
   * @param flightId - Flight ID
   * @returns Guide container with file listings
   */
  getGuideMultiplayer(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(`/hi/Multiplayer/guide/xo?flight=${flightId}`);
  }

  /**
   * Get guide for news files.
   *
   * @param flightId - Flight ID
   * @returns Guide container with file listings
   */
  getGuideNews(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(`/hi/News/guide/xo?flight=${flightId}`);
  }

  /**
   * Get guide for progression files.
   *
   * @param flightId - Flight ID
   * @returns Guide container with file listings
   */
  getGuideProgression(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(`/hi/Progression/guide/xo?flight=${flightId}`);
  }

  /**
   * Get guide for spec files.
   *
   * @param flightId - Flight ID
   * @returns Guide container with file listings
   */
  getGuideSpecs(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(`/hi/Specs/guide/xo?flight=${flightId}`);
  }

  /**
   * Get guide for title authorization files.
   *
   * @param flightId - Flight ID
   * @returns Guide container with file listings
   */
  getGuideTitleAuthorization(flightId: string): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(flightId, 'flightId');
    return this.get<Record<string, unknown>>(`/hi/TitleAuthorization/guide/xo?flight=${flightId}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Waypoint Files
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get emblem mapping configuration.
   *
   * @returns Emblem mapping dictionary
   */
  getEmblemMapping(): Promise<HaloApiResult<Record<string, unknown>>> {
    return this.get<Record<string, unknown>>('/hi/Waypoint/file/images/emblems/mapping.json');
  }

  /**
   * Get a generic file from Waypoint service.
   *
   * @param filePath - Path to the file
   * @returns File data as bytes
   */
  getGenericWaypointFile(filePath: string): Promise<HaloApiResult<Uint8Array>> {
    this.assertNotEmpty(filePath, 'filePath');
    return this.get<Uint8Array>(`/hi/Waypoint/file/${filePath}`, { returnRaw: true });
  }
}
