// <copyright file="GameCmsModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules.HaloInfinite
{
    /// <summary>
    /// Module for Game CMS related API operations including achievements, metadata, and content files.
    /// </summary>
    public sealed class GameCmsModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameCmsModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal GameCmsModule(ClientBase client)
            : base(client, HaloCoreEndpoints.GameCmsOrigin)
        {
        }

        /// <summary>
        /// Gets the contents of a store offering based on a given path.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetStoreOffering.xml' path='example'/>
        /// <param name="offeringPath">Path to a store offering, for example 'StoreContent/Display/Offerings/20240410-01.json'.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="StoreOffering"/> containing offering details. Otherwise, returns null with a description of the error.</returns>
        public Task<HaloApiResultContainer<StoreOffering, RawResponseContainer>> GetStoreOfferingAsync(string offeringPath, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(offeringPath);

            return this.GetAsync<StoreOffering>(
                $"/hi/Progression/file/{offeringPath}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the fallback playlist for the Play Now button.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetPlayNowButtonSettings.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="FallbackPlaylist"/>. Otherwise, returns null with a description of the error.</returns>
        public Task<HaloApiResultContainer<FallbackPlaylist, RawResponseContainer>> GetPlayNowButtonSettingsAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<FallbackPlaylist>(
                "/hi/Multiplayer/file/playlists/playNowButton/settings.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns the collection of available achievements to unlock in the game.
        /// </summary>
        /// <remarks>
        /// Keep in mind that this is not a list of achievements that the player has unlocked - it's just an aggregation of all available achievements in Halo Infinite.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetAchievements.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of AchievementCollection that contains the list of available achievements. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<AchievementCollection, RawResponseContainer>> GetAchievementsAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<AchievementCollection>(
                "/hi/Multiplayer/file/Live/Achievements.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about active async compute overrides.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetAsyncComputeOverrides.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of AsyncComputeOverrides containing override metadata. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<AsyncComputeOverrides, RawResponseContainer>> GetAsyncComputeOverridesAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<AsyncComputeOverrides>(
                "/hi/Specs/file/graphics/AsyncComputeOverrides.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns information about an existing challenge.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetChallenge.xml' path='example'/>
        /// <param name="challengePath">Path to the challenge file. Example is "ChallengeContent/ClientChallengeDefinitions/S1RotationalSet1Challenges/Normal/NTeamSlayerPlay.json".</param>
        /// <param name="flightId">The unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of Challenge containing challenge information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Challenge, RawResponseContainer>> GetChallengeAsync(string challengePath, string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(challengePath);
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<Challenge>(
                $"/hi/Progression/file/{challengePath}?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the information about a specific challenge deck.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetChallengeDeck.xml' path='example'/>
        /// <param name="challengeDeckPath">Path to the challenge deck. An example value is "ChallengeContent/ClientChallengeDeckDefinitions/S2EntrenchedWeeklyDeck2.json".</param>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of ChallengeDeckDefinition containing challenge deck metadata. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<ChallengeDeckDefinition, RawResponseContainer>> GetChallengeDeckAsync(string challengeDeckPath, string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(challengeDeckPath);
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<ChallengeDeckDefinition>(
                $"/hi/Progression/file/{challengeDeckPath}?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the information about a specific currency type.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetCurrency.xml' path='example'/>
        /// <param name="currencyPath">Path to the currency. An example is "currency/currencies/cr.json".</param>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of CurrencyDefinition containing information about the specified currency. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<CurrencyDefinition, RawResponseContainer>> GetCurrencyAsync(string currencyPath, string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(currencyPath);
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<CurrencyDefinition>(
                $"/hi/Progression/file/{currencyPath}?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns XUIDs with special access.
        /// </summary>
        /// <remarks>
        /// Based on the "claw" terminology, these are likely accounts with access to clawback services (for transaction refunds).
        /// At least one of the accounts returned for this API call is flagged as a member of the Xbox Scarlett team, so it's likely these are accounts that have a more direct access to Halo services.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetClawAccess.xml' path='example'/>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of ClawAccessSnapshot containing relevant XUID lists. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<ClawAccessSnapshot, RawResponseContainer>> GetClawAccessAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<ClawAccessSnapshot>(
                $"/hi/TitleAuthorization/file/claw/access.json?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the pre-defined CPU presets for different game performance configurations.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetCpuPresets.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of CPUPresetSnapshot containing preset information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<CPUPresetSnapshot, RawResponseContainer>> GetCpuPresetsAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<CPUPresetSnapshot>(
                "/hi/Specs/file/cpu/presets.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns the parameters for new custom games started in Halo Infinite.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetCustomGameDefaults.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of CustomGameDefinition containing game parameters. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<CustomGameDefinition, RawResponseContainer>> GetCustomGameDefaultsAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<CustomGameDefinition>(
                "/hi/Multiplayer/file/NonMatchmaking/customgame.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the full list of existing in-game items.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetCustomizationCatalog.xml' path='example'/>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of InventoryDefinition containing the full list of available items. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<InventoryDefinition, RawResponseContainer>> GetCustomizationCatalogAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<InventoryDefinition>(
                $"/hi/Progression/file/inventory/catalog/inventory_catalog.json?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about graphic device preset overrides.
        /// </summary>
        /// <remarks>
        /// The exact purpose of this function is unknown at this time, and requires additional investigation.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetDevicePresetOverrides.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of DevicePresetOverrides. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<DevicePresetOverrides, RawResponseContainer>> GetDevicePresetOverridesAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<DevicePresetOverrides>(
                "/hi/Specs/file/graphics/DevicePresetOverrides.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about an in-game event.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetEvent.xml' path='example'/>
        /// <param name="eventPath">The path to the event file. An example value is "RewardTracks/Events/Rituals/ritualEagleStrike.json".</param>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of RewardTrackMetadata is returned. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<RewardTrackMetadata, RawResponseContainer>> GetEventAsync(string eventPath, string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(eventPath);
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<RewardTrackMetadata>(
                $"/hi/Progression/file/{eventPath}?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the queries used to obtain override values for graphic device specifications.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetGraphicsSpecControlOverrides.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of OverrideQueryDefinition containing query definitions. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<OverrideQueryDefinition, RawResponseContainer>> GetGraphicsSpecControlOverridesAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<OverrideQueryDefinition>(
                "/hi/Specs/file/graphics/GraphicsSpecControlOverrides.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets configuration for graphic setting overrides. Returns a raw response string.
        /// </summary>
        /// <remarks>
        /// The exact structure of the API response has not been fully mapped. The API call returns a raw response for the time being.
        /// </remarks>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>Returns a string containing the response.</returns>
        public Task<HaloApiResultContainer<string, RawResponseContainer>> GetGraphicSpecsAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<string>(
                "/hi/Specs/file/graphics/overrides.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets an image for an associated game CMS asset. Example path is "progression/inventory/armor/gloves/003-001-olympus-8e7c9dff-sm.png".
        /// </summary>
        /// <param name="filePath">Path to the CMS image.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns the byte array for the requested image. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<byte[], RawResponseContainer>> GetImageAsync(string filePath, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(filePath);

            return this.GetAsync<byte[]>(
                $"/hi/images/file/{filePath}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a specific item from the Game CMS, such as armor emblems, weapon cores, vehicle cores, and others.
        /// </summary>
        /// <remarks>
        /// For example, you may find that you can get the data about an armor emblem with the path "/inventory/armor/emblems/013-001-363f4a25.json".
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetItem.xml' path='example'/>
        /// <param name="itemPath">Path to the item to be obtained. Example is "/inventory/armor/emblems/013-001-363f4a25.json".</param>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of InGameItem. Otherwise, null.</returns>
        public Task<HaloApiResultContainer<InGameItem, RawResponseContainer>> GetItemAsync(string itemPath, string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(itemPath);
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<InGameItem>(
                $"/hi/Progression/file/{itemPath}?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the list of possible error messages that a player can get when attempting to join multiplayer games.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetLobbyErrorMessages.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of LobbyHopperErrorMessageList that contains possible errors. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<LobbyHopperErrorMessageList, RawResponseContainer>> GetLobbyErrorMessagesAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<LobbyHopperErrorMessageList>(
                $"/hi/Multiplayer/file/gameStartErrorMessages/LobbyHoppperErrorMessageList.json?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns metadata on currently available in-game manufacturers and currencies.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetMetadata.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of Metadata containing the information about in-game manufacturers and currencies. Otherwise, null.</returns>
        public Task<HaloApiResultContainer<Metadata, RawResponseContainer>> GetMetadataAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<Metadata>(
                $"/hi/Progression/file/metadata/metadata.json?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns the network configuration for the current flight.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetNetworkConfiguration.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of NetworkConfiguration. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<NetworkConfiguration, RawResponseContainer>> GetNetworkConfigurationAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<NetworkConfiguration>(
                $"/hi/Multiplayer/file/network/config.json?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns the currently relevant news.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetNews.xml' path='example'/>
        /// <param name="filePath">Path to the news collection. Example is "/articles/articles.json".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns a News instance containing the currently active news. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<News, RawResponseContainer>> GetNewsAsync(string filePath, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(filePath);

            return this.GetAsync<News>(
                $"/hi/news/file/{filePath}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns information about a message that is displayed when authentication fails.
        /// </summary>
        /// <remarks>It's unclear where this is actually used because the sample response is a test one, without any relevant context.</remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetNotAllowedInTitleMessage.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of OEConfiguration containing the message. Otherwise, null.</returns>
        public Task<HaloApiResultContainer<OEConfiguration, RawResponseContainer>> GetNotAllowedInTitleMessageAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsyncFullUrl<OEConfiguration>(
                $"https://{HaloCoreEndpoints.GameCmsOrigin}.{HaloCoreEndpoints.ServiceDomain}/branches/hi/OEConfiguration/data/authfail/Default.json",
                useSpartanToken: false,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns a progression file. This method is using a generic parameter due to the fact that there are multiple progression file variants.
        /// </summary>
        /// <typeparam name="T">Type of progression file to be obtained.</typeparam>
        /// <param name="filePath">Path to the progression file.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of T, where T is the type of the progression file. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<T, RawResponseContainer>> GetProgressionFileAsync<T>(string filePath, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(filePath);

            return this.GetAsync<T>(
                $"/hi/Progression/file/{filePath}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets recommended drivers for the current version of Halo Infinite.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetRecommendedDrivers.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of DriverManifest that contains details on supported drivers. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<DriverManifest, RawResponseContainer>> GetRecommendedDriversAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<DriverManifest>(
                "/hi/Specs/file/graphics/RecommendedDrivers.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about a given Halo Infinite season.
        /// </summary>
        /// <remarks>
        /// Keep in mind that the season numbers do not align cleanly with the public season numbers. For example, public Season 2 is Season 7 in this API. That is caused by a number of test season that 343 added to the product ahead of release.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetSeasonRewardTrack.xml' path='example'/>
        /// <param name="seasonPath">The path to the season. Typical example is "Seasons/Season7.json" for the Lone Wolves season.</param>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of SeasonRewardTrack containing season information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<SeasonRewardTrack, RawResponseContainer>> GetSeasonRewardTrackAsync(string seasonPath, string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(seasonPath);
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<SeasonRewardTrack>(
                $"/hi/Progression/file/{seasonPath}?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the list of available career ranks for a given career path ID.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetCareerRanks.xml' path='example'/>
        /// <param name="careerPathId">Unique identifier for the career path. Example value is "careerRank1".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="CareerTrackContainer"/>. Otherwise, returns null with associated error details in <see cref="RawResponseContainer"/> within the result.</returns>
        public Task<HaloApiResultContainer<CareerTrackContainer, RawResponseContainer>> GetCareerRanksAsync(string careerPathId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(careerPathId);

            return this.GetAsync<CareerTrackContainer>(
                $"/hi/Progression/file/RewardTracks/CareerRanks/{careerPathId}.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the currently available season calendar.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetSeasonCalendar.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="SeasonCalendar"/> that contains pointers to season details. Otherwise, returns null with associated error details in <see cref="RawResponseContainer"/> within the result.</returns>
        public Task<HaloApiResultContainer<SeasonCalendar, RawResponseContainer>> GetSeasonCalendarAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<SeasonCalendar>(
                "/hi/Progression/file/Calendars/Seasons/SeasonCalendar.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the currently available CSR season calendar. This is applicable for ranked games and usually delineates when the rank reset will happen.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetCSRCalendar.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="SeasonCalendar"/> that contains pointers to season details. Otherwise, returns null with associated error details in <see cref="RawResponseContainer"/> within the result.</returns>
        public Task<HaloApiResultContainer<SeasonCalendar, RawResponseContainer>> GetCSRCalendarAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<SeasonCalendar>(
                "/hi/Progression/file/Csr/Calendars/CsrSeasonCalendar.json",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a list of all available image files currently used by the multiplayer service.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetGuide_Images.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideImagesAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<GuideContainer>(
                $"/hi/images/guide/xo?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a list of all available multiplayer files currently used by the multiplayer service.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetGuide_Multiplayer.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideMultiplayerAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<GuideContainer>(
                $"/hi/Multiplayer/guide/xo?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a list of all available news files currently used by the multiplayer service.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetGuide_News.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideNewsAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<GuideContainer>(
                $"/hi/News/guide/xo?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a list of all available progression files currently used by the multiplayer service.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetGuide_Progression.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideProgressionAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<GuideContainer>(
                $"/hi/Progression/guide/xo?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a list of all available spec files currently used by the multiplayer service.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetGuide_Specs.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideSpecsAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<GuideContainer>(
                $"/hi/Specs/guide/xo?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a list of all available title authorization files currently used by the multiplayer service.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetGuide_TitleAuthorization.xml' path='example'/>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideTitleAuthorizationAsync(string flightId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(flightId);

            return this.GetAsync<GuideContainer>(
                $"/hi/TitleAuthorization/guide/xo?flight={flightId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a list of all available medals and their metadata.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetMedalMetadata.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of <see cref="MedalMetadata"/> containing medal information. Otherwise, returns null and error details.</returns>
        public Task<HaloApiResultContainer<MedalMetadata, RawResponseContainer>> GetMedalMetadataAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<MedalMetadata>("/hi/Waypoint/file/medals/metadata.json", cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the detailed configuration for a Halo Infinite playlist.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetMultiplayerPlaylistConfiguration.xml' path='example'/>
        /// <param name="playlistFile">JSON file associated with a playlist. Example is "a446725e-b281-414c-a21e-31b8700e95a1.json".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of <see cref="PlaylistConfiguration"/> containing playlist configuration. Otherwise, returns null and error details.</returns>
        public Task<HaloApiResultContainer<PlaylistConfiguration, RawResponseContainer>> GetMultiplayerPlaylistConfigurationAsync(string playlistFile, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(playlistFile);

            return this.GetAsync<PlaylistConfiguration>(
                $"/hi/Multiplayer/file/playlists/assets/{playlistFile}",
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets emblem mapping configuration.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/GameCms_GetEmblemMapping.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of <see cref="Dictionary{String, Dictionary}"/> with emblem mapping. Otherwise, returns null and error details.</returns>
        public Task<HaloApiResultContainer<Dictionary<string, Dictionary<string, EmblemMapping>>, RawResponseContainer>> GetEmblemMappingAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Dictionary<string, Dictionary<string, EmblemMapping>>>(
                "/hi/Waypoint/file/images/emblems/mapping.json",
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a file from the Halo Waypoint service.
        /// </summary>
        /// <param name="filePath">Path to the file to be retrieved.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, a byte array containing the file contents. Otherwise, returns null and error details.</returns>
        public Task<HaloApiResultContainer<byte[], RawResponseContainer>> GetGenericWaypointFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(filePath);

            return this.GetAsync<byte[]>($"/hi/Waypoint/file/{filePath}", cancellationToken: cancellationToken);
        }
    }
}
