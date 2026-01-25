// <copyright file="EconomyModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules.HaloInfinite
{
    /// <summary>
    /// Module for economy-related API operations including player customization, stores, and inventory.
    /// </summary>
    public class EconomyModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EconomyModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal EconomyModule(ClientBase client)
            : base(client, HaloCoreEndpoints.EconomyOrigin)
        {
        }

        /// <summary>
        /// Gets information about an individual AI Core.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_AiCoreCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="coreId">Unique AI Core ID. Example ID is "304-100-ai-core-debb20e3".</param>
        /// <returns>If successful, returns an instance of Core containing AI core customization metadata if request was successful. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AiCore, RawResponseContainer>> AiCoreCustomization(string player, string coreId)
        {
            return await this.GetAsync<AiCore>(
                $"/hi/players/xuid({player})/customization/ais/{coreId}",
                useClearance: true);
        }

        /// <summary>
        /// Get AI core customization for a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_AiCoresCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>An instance of AiCores containing AI core customization metadata if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<AiCoreContainer, RawResponseContainer>> AiCoresCustomization(string player)
        {
            return await this.GetAsync<AiCoreContainer>(
                $"/hi/players/xuid({player})/customization/ais",
                useClearance: true);
        }

        /// <summary>
        /// Get details about all owned cores for a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_AllOwnedCoresDetails.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>An instance of PlayerCores containing player core customization metadata if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<PlayerCores, RawResponseContainer>> AllOwnedCoresDetails(string player)
        {
            return await this.GetAsync<PlayerCores>(
                $"/hi/players/xuid({player})/cores",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about a specific armor core a player owns.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_ArmorCoreCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="coreId">The unique identifier for an armor core. An example value is "017-001-eag-c13d0b38".</param>
        /// <returns>If successful, returns an instance of ArmorCore containing customization information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<ArmorCore, RawResponseContainer>> ArmorCoreCustomization(string player, string coreId)
        {
            return await this.GetAsync<ArmorCore>(
                $"/hi/players/xuid({player})/customization/armors/{coreId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about all armor cores a player owns.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_ArmorCoresCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of ArmorCoreCollection that contains the list of armor cores. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<ArmorCoreCollection, RawResponseContainer>> ArmorCoresCustomization(string player)
        {
            return await this.GetAsync<ArmorCoreCollection>(
                $"/hi/players/xuid({player})/customization/armors",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about currently active boosts for the player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetActiveBoosts.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of ActiveBoostsContainer that contains the list of active boosts. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<ActiveBoostsContainer, RawResponseContainer>> GetActiveBoosts(string player)
        {
            return await this.GetAsync<ActiveBoostsContainer>(
                $"/hi/players/xuid({player})/boosts",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about a reward given to a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetAwardedRewards.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="rewardId">The unique ID for the reward given to a player. Example value is "Challenges-35a86ae3-017c-4b5a-b633-b2802a770e0a".</param>
        /// <returns>If successful, returns an instance of RewardSnapshot that contains the list of awarded rewards. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<RewardSnapshot, RawResponseContainer>> GetAwardedRewards(string player, string rewardId)
        {
            return await this.GetAsync<RewardSnapshot>(
                $"/hi/players/xuid({player})/rewards/{rewardId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about boosts offering in the store for a given player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetBoostsStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of StoreItem containing boost information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetBoostsStore(string player)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/boosts",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about the items available on The Exchange (Soft Currency Store).
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetSoftCurrencyStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of StoreItem containing The Exchange information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetSoftCurrencyStore(string player)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/softcurrencyoffers",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about items available in a credit sub-store for a given player.
        /// </summary>
        /// <remarks>
        /// Credit sub-stores (creditsubstorefront00 through creditsubstorefront05) contain different
        /// categories of items purchasable with credits. The storeIndex parameter should be 0-5.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetCreditSubStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="storeIndex">The sub-store index (0-5). Maps to creditsubstorefront00 through creditsubstorefront05.</param>
        /// <returns>If successful, returns an instance of StoreItem containing store offerings. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetCreditSubStore(string player, int storeIndex)
        {
            ValidateRange(storeIndex, 0, 5, nameof(storeIndex));

            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/creditsubstorefront{storeIndex:D2}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about items available in a soft currency (Spartan Points) sub-store for a given player.
        /// </summary>
        /// <remarks>
        /// Soft currency sub-stores (softcurrencysubstorefront00 through softcurrencysubstorefront15) contain
        /// different categories of items purchasable with Spartan Points on The Exchange.
        /// The storeIndex parameter should be 0-15.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetSoftCurrencySubStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="storeIndex">The sub-store index (0-15). Maps to softcurrencysubstorefront00 through softcurrencysubstorefront15.</param>
        /// <returns>If successful, returns an instance of StoreItem containing store offerings. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetSoftCurrencySubStore(string player, int storeIndex)
        {
            ValidateRange(storeIndex, 0, 15, nameof(storeIndex));

            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/softcurrencysubstorefront{storeIndex:D2}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the information about giveaways available for a given player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetGiveawayRewards.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of PlayerGiveaways containing available giveaways. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlayerGiveaways, RawResponseContainer>> GetGiveawayRewards(string player)
        {
            return await this.GetAsync<PlayerGiveaways>(
                $"/hi/players/xuid({player})/giveaways",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about items available for sale in the Halo Championship Series (HCS) store.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetHCSStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, an instance of StoreItem containing store offerings. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetHCSStore(string player)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/hcs",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about items available in the current player's inventory.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetInventoryItems.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of PlayerInventory that contains a list of items in the player's inventory. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlayerInventory, RawResponseContainer>> GetInventoryItems(string player)
        {
            return await this.GetAsync<PlayerInventory>(
                $"/hi/players/xuid({player})/inventory",
                useClearance: true);
        }

        /// <summary>
        /// Gets the information about all available items in the main store.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetMainStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of StoreItem that contains information about items available in the main store. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetMainStore(string player)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/main",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about customizations for multiple players.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetMultiplePlayersCustomization.xml' path='example'/>
        /// <param name="playerIds">List of numeric XUIDs for the players.</param>
        /// <returns>If successful, returns an instance of PlayerCustomizationCollection that contains player customizations. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<PlayerCustomizationCollection, RawResponseContainer>> GetMultiplePlayersCustomization(List<string> playerIds)
        {
            var formattedPlayerList = string.Join(",", playerIds.Select(id => $"xuid({id})"));
            return await this.GetAsync<PlayerCustomizationCollection>(
                $"/hi/customization?players={formattedPlayerList}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about the operations reward levels store.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetOperationRewardLevelsStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of StoreItem that contains information about items available in the operations reward levels store. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetOperationRewardLevelsStore(string player)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/operationrewardlevels",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about the operations store.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetOperationsStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of StoreItem that contains information about items available in the operations store. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetOperationsStore(string player)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/operations",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about rewards associated with a given reward track, such as a season or special event.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetRewardTrack.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="rewardTrackType">Type of reward track. For seasons, this is usually "operation". This parameter is a singular noun, and is pluralized automatically in the function (the "s" character is appended).</param>
        /// <param name="trackId">Unique identifier for the reward track. An example value is "battlepass-noblesacrifice.json".</param>
        /// <returns>If successful, returns an instance of RewardTrack containing information for reward track tiers. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<RewardTrack, RawResponseContainer>> GetRewardTrack(string player, string rewardTrackType, string trackId)
        {
            return await this.GetAsync<RewardTrack>(
                $"/hi/players/xuid({player})/rewardtracks/{rewardTrackType}s/{trackId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the amount of currencies that the player has in their account.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetVirtualCurrencyBalances.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of CurrencySnapshot that contains the balances. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<CurrencySnapshot, RawResponseContainer>> GetVirtualCurrencyBalances(string player)
        {
            return await this.GetAsync<CurrencySnapshot>(
                $"/hi/players/xuid({player})/currencies",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about items on sale in the XP grants store.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetXpGrantsStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of StoreItem that contains information about items in the store. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetXpGrantsStore(string player)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/xpgrants",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about a specific owned core.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_OwnedCoreDetails.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="coreId">The unique core ID. An example is "017-001-eag-c13d0b38".</param>
        /// <returns>If successful, returns an instance of Core containing core information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<GenericCore, RawResponseContainer>> OwnedCoreDetails(string player, string coreId)
        {
            return await this.GetAsync<GenericCore>(
                $"/hi/players/xuid({player})/cores/{coreId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the current player appearance customization state.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_PlayerAppearanceCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of AppearanceCustomization containing customization information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AppearanceCustomization, RawResponseContainer>> PlayerAppearanceCustomization(string player)
        {
            return await this.GetAsync<AppearanceCustomization>(
                $"/hi/players/xuid({player})/customization/appearance",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about available player customizations.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_PlayerCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="viewType">Determines which view into customizations is shown. Available values are "public" and "private". The private view enables showing all available cores, while the public view only shows equipped cores.</param>
        /// <returns>If successful, returns an instance of CustomizationData containing player customizations. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<CustomizationData, RawResponseContainer>> PlayerCustomization(string player, string viewType)
        {
            return await this.GetAsync<CustomizationData>(
                $"/hi/players/xuid({player})/customization?view={viewType}",
                useClearance: true);
        }

        /// <summary>
        /// Gets available reward tracks for a player based on current and past battle passes.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_PlayerOperations.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="flightId">The unique ID for the currently active flight.</param>
        /// <returns>If successful, returns an instance of OperationRewardTrackSnapshot containing battle pass information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<OperationRewardTrackSnapshot, RawResponseContainer>> PlayerOperations(string player, string flightId)
        {
            return await this.GetAsync<OperationRewardTrackSnapshot>(
                $"/hi/players/xuid({player})/rewardtracks/operations?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about transactions that the player executed.
        /// </summary>
        /// <remarks>
        /// This function is likely used as a POST as well (hence the name - right now we're only using GET). Once we discover how this API works, we can extend the functionality further.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_PostCurrencyTransaction.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="currencyId">The unique identifier for the currency. Valid values include "cr", "rerollcurrency", "xpboost", and "xpgrant".</param>
        /// <returns>If successful, returns an instance of TransactionSnapshot listing all existing transactions. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<TransactionSnapshot, RawResponseContainer>> PostCurrencyTransaction(string player, string currencyId)
        {
            return await this.GetAsync<TransactionSnapshot>(
                $"/hi/players/xuid({player})/currencies/{currencyId}/transactions",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about offerings for a player in a given store.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_ScheduledStorefrontOfferings.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="storeId">The unique store identifier. An example value is "hcs".</param>
        /// <returns>If successful, returns an instance of StoreItem containing offerings. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> ScheduledStorefrontOfferings(string player, string storeId)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/{storeId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the currently active Spartan body customization.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_SpartanBodyCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of SpartanBody containing the customization information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<SpartanBody, RawResponseContainer>> SpartanBodyCustomization(string player)
        {
            return await this.GetAsync<SpartanBody>(
                $"/hi/players/xuid({player})/customization/spartanbody",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about a vehicle core.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_VehicleCoreCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="coreId">Unique vehicle core ID. Example value is "409-304-olympus-e8b8a8b3".</param>
        /// <returns>If successful, returns an instance of VehicleCore. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<VehicleCore, RawResponseContainer>> VehicleCoreCustomization(string player, string coreId)
        {
            return await this.GetAsync<VehicleCore>(
                $"/hi/players/xuid({player})/customization/vehicles/{coreId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about the vehicle core customizations available to a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_VehicleCoresCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of VehicleCoreCollection containing a list of available vehicle cores. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<VehicleCoreCollection, RawResponseContainer>> VehicleCoresCustomization(string player)
        {
            return await this.GetAsync<VehicleCoreCollection>(
                $"/hi/players/xuid({player})/customization/vehicles",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about a specific weapon core.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_WeaponCoreCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="coreId">The unique ID of the weapon core.</param>
        /// <returns>If successful, returns an instance of WeaponCore containing information about the weapon core. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<WeaponCore, RawResponseContainer>> WeaponCoreCustomization(string player, string coreId)
        {
            return await this.GetAsync<WeaponCore>(
                $"/hi/players/xuid({player})/customization/weapons/{coreId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about weapon cores equipped on a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_WeaponCoresCustomization.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of WeaponCoreCollection. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<WeaponCoreCollection, RawResponseContainer>> WeaponCoresCustomization(string player)
        {
            return await this.GetAsync<WeaponCoreCollection>(
                $"/hi/players/xuid({player})/customization/weapons",
                useClearance: true);
        }

        /// <summary>
        /// Gets or sets the store customization offers available for a player.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetCustomizationStore.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of <see cref="StoreItem"/>. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<StoreItem, RawResponseContainer>> GetCustomizationStore(string player)
        {
            return await this.GetAsync<StoreItem>(
                $"/hi/players/xuid({player})/stores/customizationoffers",
                useClearance: true);
        }

        /// <summary>
        /// Gets the player career progression status.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/Economy_GetPlayerCareerRank.xml' path='example'/>
        /// <param name="players">List of numeric XUIDs for the players.</param>
        /// <param name="careerPathId">Unique identifier for the career path. Example value is "careerRank1".</param>
        /// <returns>If successful, returns an instance of <see cref="RewardTrackResultContainer"/>. Otherwise, returns null with associated error details in <see cref="RawResponseContainer"/> within the result.</returns>
        public async Task<HaloApiResultContainer<RewardTrackResultContainer, RawResponseContainer>> GetPlayerCareerRank(List<string> players, string careerPathId)
        {
            var formattedPlayerList = string.Empty;
            if (players != null && players.Count > 0)
            {
                formattedPlayerList = string.Join(",", players.Select(id => $"xuid({id})"));
            }

            return await this.GetAsync<RewardTrackResultContainer>(
                $"/hi/careerranks/{careerPathId}?players={formattedPlayerList}",
                useClearance: true);
        }
    }
}
