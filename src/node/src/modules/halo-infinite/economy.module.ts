import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type {
  PlayerInventory,
  CurrencySnapshot,
  StoreItem,
  ActiveBoostsContainer,
  RewardSnapshot,
  TransactionSnapshot,
} from '../../models/halo-infinite/economy';
import type {
  CustomizationData,
  ArmorCore,
  WeaponCore,
  VehicleCore,
  AiCore,
  ArmorCoreCollection,
  WeaponCoreCollection,
  VehicleCoreCollection,
  AiCoreContainer,
  SpartanBody,
  AppearanceCustomization,
} from '../../models/halo-infinite/customization';
import type { RewardTrack, OperationRewardTrackSnapshot } from '../../models/halo-infinite/progression';
import type { PlayerGiveaways } from '../../models/halo-infinite/misc';
import type { RewardTrackResultContainer } from '../../models/halo-infinite/progression/career';

/**
 * Economy module for player customization, inventory, and store access.
 *
 * Provides access to:
 * - Player inventory and currency balances
 * - Customization data (armor, weapons, vehicles, AI)
 * - In-game stores and offerings
 * - Active boosts and rewards
 * - Operation/battle pass progress
 *
 * @example
 * ```typescript
 * // Get player inventory
 * const inventory = await client.economy.getInventoryItems('xuid');
 *
 * // Get armor customization
 * const armor = await client.economy.armorCoresCustomization('xuid');
 *
 * // Get currency balances
 * const currencies = await client.economy.getVirtualCurrencyBalances('xuid');
 * ```
 */
export class EconomyModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.ECONOMY_ORIGIN);
  }

  // ─────────────────────────────────────────────────────────────────
  // Inventory & Currency
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get all inventory items for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Player inventory
   */
  getInventoryItems(player: string): Promise<HaloApiResult<PlayerInventory>> {
    this.assertNotEmpty(player, 'player');
    return this.get<PlayerInventory>(`/hi/players/xuid(${player})/inventory`);
  }

  /**
   * Get virtual currency balances for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Currency balances
   */
  getVirtualCurrencyBalances(player: string): Promise<HaloApiResult<CurrencySnapshot>> {
    this.assertNotEmpty(player, 'player');
    return this.get<CurrencySnapshot>(`/hi/players/xuid(${player})/currencies`);
  }

  /**
   * Post a currency transaction.
   *
   * @param player - Player's numeric XUID
   * @param currencyId - Currency identifier
   * @returns Transaction result
   */
  postCurrencyTransaction(
    player: string,
    currencyId: string
  ): Promise<HaloApiResult<TransactionSnapshot>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(currencyId, 'currencyId');
    return this.post<TransactionSnapshot>(
      `/hi/players/xuid(${player})/currencies/${currencyId}/transactions`
    );
  }

  // ─────────────────────────────────────────────────────────────────
  // Customization
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get full player customization data.
   *
   * @param player - Player's numeric XUID
   * @param viewType - View type (e.g., 'public', 'private')
   * @returns Complete customization data
   */
  getPlayerCustomization(
    player: string,
    viewType: string = 'public'
  ): Promise<HaloApiResult<CustomizationData>> {
    this.assertNotEmpty(player, 'player');
    return this.get<CustomizationData>(
      `/hi/players/xuid(${player})/customization?view=${viewType}`
    );
  }

  /**
   * Get all armor cores for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Armor core collection
   */
  armorCoresCustomization(player: string): Promise<HaloApiResult<ArmorCoreCollection>> {
    this.assertNotEmpty(player, 'player');
    return this.get<ArmorCoreCollection>(`/hi/players/xuid(${player})/customization/armors`);
  }

  /**
   * Get a specific armor core for a player.
   *
   * @param player - Player's numeric XUID
   * @param coreId - Core identifier
   * @returns Armor core details
   */
  armorCoreCustomization(
    player: string,
    coreId: string
  ): Promise<HaloApiResult<ArmorCore>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(coreId, 'coreId');
    return this.get<ArmorCore>(`/hi/players/xuid(${player})/customization/armors/${coreId}`);
  }

  /**
   * Get all weapon cores for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Weapon core collection
   */
  weaponCoresCustomization(player: string): Promise<HaloApiResult<WeaponCoreCollection>> {
    this.assertNotEmpty(player, 'player');
    return this.get<WeaponCoreCollection>(`/hi/players/xuid(${player})/customization/weapons`);
  }

  /**
   * Get a specific weapon core for a player.
   *
   * @param player - Player's numeric XUID
   * @param coreId - Core identifier
   * @returns Weapon core details
   */
  weaponCoreCustomization(
    player: string,
    coreId: string
  ): Promise<HaloApiResult<WeaponCore>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(coreId, 'coreId');
    return this.get<WeaponCore>(`/hi/players/xuid(${player})/customization/weapons/${coreId}`);
  }

  /**
   * Get all vehicle cores for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Vehicle core collection
   */
  vehicleCoresCustomization(player: string): Promise<HaloApiResult<VehicleCoreCollection>> {
    this.assertNotEmpty(player, 'player');
    return this.get<VehicleCoreCollection>(`/hi/players/xuid(${player})/customization/vehicles`);
  }

  /**
   * Get a specific vehicle core for a player.
   *
   * @param player - Player's numeric XUID
   * @param coreId - Core identifier
   * @returns Vehicle core details
   */
  vehicleCoreCustomization(
    player: string,
    coreId: string
  ): Promise<HaloApiResult<VehicleCore>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(coreId, 'coreId');
    return this.get<VehicleCore>(`/hi/players/xuid(${player})/customization/vehicles/${coreId}`);
  }

  /**
   * Get all AI cores for a player.
   *
   * @param player - Player's numeric XUID
   * @returns AI core container
   */
  aiCoresCustomization(player: string): Promise<HaloApiResult<AiCoreContainer>> {
    this.assertNotEmpty(player, 'player');
    return this.get<AiCoreContainer>(`/hi/players/xuid(${player})/customization/ais`);
  }

  /**
   * Get a specific AI core for a player.
   *
   * @param player - Player's numeric XUID
   * @param coreId - Core identifier
   * @returns AI core details
   */
  aiCoreCustomization(
    player: string,
    coreId: string
  ): Promise<HaloApiResult<AiCore>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(coreId, 'coreId');
    return this.get<AiCore>(`/hi/players/xuid(${player})/customization/ais/${coreId}`);
  }

  /**
   * Get Spartan body customization for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Spartan body configuration
   */
  spartanBodyCustomization(player: string): Promise<HaloApiResult<SpartanBody>> {
    this.assertNotEmpty(player, 'player');
    return this.get<SpartanBody>(`/hi/players/xuid(${player})/customization/spartanbody`);
  }

  /**
   * Get appearance customization for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Appearance configuration
   */
  playerAppearanceCustomization(
    player: string
  ): Promise<HaloApiResult<AppearanceCustomization>> {
    this.assertNotEmpty(player, 'player');
    return this.get<AppearanceCustomization>(
      `/hi/players/xuid(${player})/customization/appearance`
    );
  }

  /**
   * Get customization for multiple players.
   *
   * @param playerIds - List of player XUIDs
   * @returns Player customization collection
   */
  getMultiplePlayersCustomization(
    playerIds: string[]
  ): Promise<HaloApiResult<CustomizationData>> {
    if (!playerIds.length) {
      throw new Error('playerIds cannot be empty');
    }
    const formattedPlayers = playerIds.map((id) => `xuid(${id})`).join(',');
    return this.get<CustomizationData>(`/hi/customization?players=${formattedPlayers}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Cores
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get details about all owned cores for a player.
   *
   * @param player - Player's numeric XUID
   * @returns All player cores
   */
  getAllOwnedCoresDetails(
    player: string
  ): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(player, 'player');
    return this.get<Record<string, unknown>>(`/hi/players/xuid(${player})/cores`);
  }

  /**
   * Get details about a specific owned core.
   *
   * @param player - Player's numeric XUID
   * @param coreId - Core identifier
   * @returns Core details
   */
  getOwnedCoreDetails(
    player: string,
    coreId: string
  ): Promise<HaloApiResult<Record<string, unknown>>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(coreId, 'coreId');
    return this.get<Record<string, unknown>>(`/hi/players/xuid(${player})/cores/${coreId}`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Stores
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get the main store offerings.
   *
   * @param player - Player's numeric XUID
   * @returns Store items
   */
  getMainStore(player: string): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    return this.get<StoreItem>(`/hi/players/xuid(${player})/stores/main`);
  }

  /**
   * Get the HCS (esports) store offerings.
   *
   * @param player - Player's numeric XUID
   * @returns Store items
   */
  getHcsStore(player: string): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    return this.get<StoreItem>(`/hi/players/xuid(${player})/stores/hcs`);
  }

  /**
   * Get the boosts store offerings.
   *
   * @param player - Player's numeric XUID
   * @returns Store items
   */
  getBoostsStore(player: string): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    return this.get<StoreItem>(`/hi/players/xuid(${player})/stores/boosts`);
  }

  /**
   * Get the soft currency (Spartan Points) store.
   *
   * @param player - Player's numeric XUID
   * @returns Store items
   */
  getSoftCurrencyStore(player: string): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    return this.get<StoreItem>(`/hi/players/xuid(${player})/stores/softcurrency`);
  }

  /**
   * Get the customization store offerings.
   *
   * @param player - Player's numeric XUID
   * @returns Store items
   */
  getCustomizationStore(player: string): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    return this.get<StoreItem>(`/hi/players/xuid(${player})/stores/customization`);
  }

  /**
   * Get the operations store offerings.
   *
   * @param player - Player's numeric XUID
   * @returns Store items
   */
  getOperationsStore(player: string): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    return this.get<StoreItem>(`/hi/players/xuid(${player})/stores/operations`);
  }

  /**
   * Get the operation reward levels store.
   *
   * @param player - Player's numeric XUID
   * @returns Store items
   */
  getOperationRewardLevelsStore(player: string): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    return this.get<StoreItem>(`/hi/players/xuid(${player})/stores/operationrewardlevels`);
  }

  /**
   * Get the XP grants store.
   *
   * @param player - Player's numeric XUID
   * @returns Store items
   */
  getXpGrantsStore(player: string): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    return this.get<StoreItem>(`/hi/players/xuid(${player})/stores/xpgrants`);
  }

  /**
   * Get items from a credit sub-store.
   *
   * Credit sub-stores (0-5) contain different categories of items
   * purchasable with credits.
   *
   * @param player - Player's numeric XUID
   * @param storeIndex - Sub-store index (0-5)
   * @returns Store items
   */
  getCreditSubStore(player: string, storeIndex: number): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    if (storeIndex < 0 || storeIndex > 5) {
      throw new Error('storeIndex must be between 0 and 5');
    }
    const paddedIndex = storeIndex.toString().padStart(2, '0');
    return this.get<StoreItem>(
      `/hi/players/xuid(${player})/stores/creditsubstorefront${paddedIndex}`
    );
  }

  /**
   * Get items from a soft currency (Spartan Points) sub-store.
   *
   * Soft currency sub-stores (0-15) contain different categories of items
   * purchasable with Spartan Points on The Exchange.
   *
   * @param player - Player's numeric XUID
   * @param storeIndex - Sub-store index (0-15)
   * @returns Store items
   */
  getSoftCurrencySubStore(
    player: string,
    storeIndex: number
  ): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    if (storeIndex < 0 || storeIndex > 15) {
      throw new Error('storeIndex must be between 0 and 15');
    }
    const paddedIndex = storeIndex.toString().padStart(2, '0');
    return this.get<StoreItem>(
      `/hi/players/xuid(${player})/stores/softcurrencysubstorefront${paddedIndex}`
    );
  }

  /**
   * Get scheduled storefront offerings.
   *
   * @param player - Player's numeric XUID
   * @param storeId - Store identifier
   * @returns Scheduled store items
   */
  getScheduledStorefrontOfferings(
    player: string,
    storeId: string
  ): Promise<HaloApiResult<StoreItem>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(storeId, 'storeId');
    return this.get<StoreItem>(
      `/hi/players/xuid(${player})/stores/${storeId}/scheduled`
    );
  }

  // ─────────────────────────────────────────────────────────────────
  // Boosts & Rewards
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get active boosts for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Active boosts container
   */
  getActiveBoosts(player: string): Promise<HaloApiResult<ActiveBoostsContainer>> {
    this.assertNotEmpty(player, 'player');
    return this.get<ActiveBoostsContainer>(`/hi/players/xuid(${player})/boosts`);
  }

  /**
   * Get awarded rewards for a player.
   *
   * @param player - Player's numeric XUID
   * @param rewardId - Reward identifier
   * @returns Reward snapshot
   */
  getAwardedRewards(
    player: string,
    rewardId: string
  ): Promise<HaloApiResult<RewardSnapshot>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(rewardId, 'rewardId');
    return this.get<RewardSnapshot>(
      `/hi/players/xuid(${player})/rewards/${rewardId}`
    );
  }

  /**
   * Get giveaway rewards for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Available giveaways
   */
  getGiveawayRewards(player: string): Promise<HaloApiResult<PlayerGiveaways>> {
    this.assertNotEmpty(player, 'player');
    return this.get<PlayerGiveaways>(`/hi/players/xuid(${player})/giveaways`);
  }

  // ─────────────────────────────────────────────────────────────────
  // Reward Tracks / Operations
  // ─────────────────────────────────────────────────────────────────

  /**
   * Get reward track progress for a player.
   *
   * @param player - Player's numeric XUID
   * @param rewardTrackType - Type of reward track
   * @param trackId - Track identifier
   * @returns Reward track details
   */
  getRewardTrack(
    player: string,
    rewardTrackType: string,
    trackId: string
  ): Promise<HaloApiResult<RewardTrack>> {
    this.assertNotEmpty(player, 'player');
    this.assertNotEmpty(rewardTrackType, 'rewardTrackType');
    this.assertNotEmpty(trackId, 'trackId');
    return this.get<RewardTrack>(
      `/hi/players/xuid(${player})/rewardtracks/${rewardTrackType}/${trackId}`
    );
  }

  /**
   * Get operation progress for a player.
   *
   * @param player - Player's numeric XUID
   * @returns Operation reward track snapshot
   */
  getPlayerOperations(
    player: string
  ): Promise<HaloApiResult<OperationRewardTrackSnapshot>> {
    this.assertNotEmpty(player, 'player');
    return this.get<OperationRewardTrackSnapshot>(
      `/hi/players/xuid(${player})/operations`,
      { useClearance: true }
    );
  }

  /**
   * Get career rank for players.
   *
   * @param playerIds - List of player XUIDs
   * @param careerPathId - Career path identifier
   * @returns Career rank results
   */
  getPlayerCareerRank(
    playerIds: string[],
    careerPathId: string
  ): Promise<HaloApiResult<RewardTrackResultContainer>> {
    if (!playerIds.length) {
      throw new Error('playerIds cannot be empty');
    }
    this.assertNotEmpty(careerPathId, 'careerPathId');

    const formattedPlayerList = playerIds.map((id) => `xuid(${id})`).join(',');
    return this.get<RewardTrackResultContainer>(
      `/hi/careerranks/${careerPathId}?players=${formattedPlayerList}`,
      { useClearance: true }
    );
  }
}
