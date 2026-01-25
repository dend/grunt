// <copyright file="GameCmsModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules
{
    /// <summary>
    /// Module for Game CMS related API operations including achievements, metadata, and content files.
    /// </summary>
    public class GameCmsModule : ModuleBase
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
        /// <param name="offeringPath">Path to a store offering, for example 'StoreContent/Display/Offerings/20240410-01.json'.</param>
        /// <returns>If successful, returns an instance of <see cref="StoreOffering"/> containing offering details. Otherwise, returns null with a description of the error.</returns>
        public async Task<HaloApiResultContainer<StoreOffering, RawResponseContainer>> GetStoreOffering(string offeringPath)
        {
            return await this.GetAsync<StoreOffering>(
                $"/hi/Progression/file/{offeringPath}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the fallback playlist for the Play Now button.
        /// </summary>
        /// <returns>If successful, returns an instance of <see cref="FallbackPlaylist"/>. Otherwise, returns null with a description of the error.</returns>
        public async Task<HaloApiResultContainer<FallbackPlaylist, RawResponseContainer>> GetPlayNowButtonSettings()
        {
            return await this.GetAsync<FallbackPlaylist>(
                "/hi/Multiplayer/file/playlists/playNowButton/settings.json",
                useClearance: true);
        }

        /// <summary>
        /// Returns the collection of available achievements to unlock in the game.
        /// </summary>
        /// <remarks>
        /// Keep in mind that this is not a list of achievements that the player has unlocked - it's just an aggregation of all available achievements in Halo Infinite.
        /// </remarks>
        /// <returns>If successful, returns an instance of AchievementCollection that contains the list of available achievements. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AchievementCollection, RawResponseContainer>> GetAchievements()
        {
            return await this.GetAsync<AchievementCollection>(
                "/hi/Multiplayer/file/Live/Achievements.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about active async compute overrides. Unknown what the concrete purpose of this API is yet.
        /// </summary>
        /// <returns>If successful, returns an instance of AsyncComputeOverrides containing override metadata. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AsyncComputeOverrides, RawResponseContainer>> GetAsyncComputeOverrides()
        {
            return await this.GetAsync<AsyncComputeOverrides>(
                "/hi/Specs/file/graphics/AsyncComputeOverrides.json",
                useClearance: true);
        }

        /// <summary>
        /// Returns information about an existing challenge.
        /// </summary>
        /// <param name="challengePath">Path to the challenge file. Example is "ChallengeContent/ClientChallengeDefinitions/S1RotationalSet1Challenges/Normal/NTeamSlayerPlay.json".</param>
        /// <param name="flightId">The unique ID for the currently active flight.</param>
        /// <returns>If successful, returns an instance of Challenge containing challenge information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<Challenge, RawResponseContainer>> GetChallenge(string challengePath, string flightId)
        {
            return await this.GetAsync<Challenge>(
                $"/hi/Progression/file/{challengePath}?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the information about a specific challenge deck.
        /// </summary>
        /// <param name="challengeDeckPath">Path to the challenge deck. An example value is "ChallengeContent/ClientChallengeDeckDefinitions/S2EntrenchedWeeklyDeck2.json".</param>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <returns>If successful, returns an instance of ChallengeDeckDefinition containing challenge deck metadata. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<ChallengeDeckDefinition, RawResponseContainer>> GetChallengeDeck(string challengeDeckPath, string flightId)
        {
            return await this.GetAsync<ChallengeDeckDefinition>(
                $"/hi/Progression/file/{challengeDeckPath}?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the information about a specific currency type.
        /// </summary>
        /// <param name="currencyPath">Path to the currency. An example is "currency/currencies/cr.json".</param>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <returns>If successful, returns an instance of CurrencyDefinition containing information about the specified currency. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<CurrencyDefinition, RawResponseContainer>> GetCurrency(string currencyPath, string flightId)
        {
            return await this.GetAsync<CurrencyDefinition>(
                $"/hi/Progression/file/{currencyPath}?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Returns XUIDs with special access.
        /// </summary>
        /// <remarks>
        /// Based on the "claw" terminology, these are likely accounts with access to clawback services (for transaction refunds).
        /// At least one of the accounts returned for this API call is flagged as a member of the Xbox Scarlett team, so it's likely these are accounts that have a more direct access to Halo services.
        /// </remarks>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <returns>If successful, returns an instance of ClawAccessSnapshot containing relevant XUID lists. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<ClawAccessSnapshot, RawResponseContainer>> GetClawAccess(string flightId)
        {
            return await this.GetAsync<ClawAccessSnapshot>(
                $"/hi/TitleAuthorization/file/claw/access.json?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the pre-defined CPU presets for different game performance configurations.
        /// </summary>
        /// <returns>If successful, returns an instance of CPUPresetSnapshot containing preset information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<CPUPresetSnapshot, RawResponseContainer>> GetCpuPresets()
        {
            return await this.GetAsync<CPUPresetSnapshot>(
                "/hi/Specs/file/cpu/presets.json",
                useClearance: true);
        }

        /// <summary>
        /// Returns the parameters for new custom games started in Halo Infinite.
        /// </summary>
        /// <returns>If successful, returns an instance of CustomGameDefinition containing game parameters. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<CustomGameDefinition, RawResponseContainer>> GetCustomGameDefaults()
        {
            return await this.GetAsync<CustomGameDefinition>(
                "/hi/Multiplayer/file/NonMatchmaking/customgame.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets the full list of existing in-game items.
        /// </summary>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <returns>If successful, returns an instance of InventoryDefinition containing the full list of available items. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<InventoryDefinition, RawResponseContainer>> GetCustomizationCatalog(string flightId)
        {
            return await this.GetAsync<InventoryDefinition>(
                $"/hi/Progression/file/inventory/catalog/inventory_catalog.json?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about graphic device preset overrides.
        /// </summary>
        /// <remarks>
        /// The exact purpose of this function is unknown at this time, and requires additional investigation.
        /// </remarks>
        /// <returns>If successful, an instance of DevicePresetOverrides. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<DevicePresetOverrides, RawResponseContainer>> GetDevicePresetOverrides()
        {
            return await this.GetAsync<DevicePresetOverrides>(
                "/hi/Specs/file/graphics/DevicePresetOverrides.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about an in-game event.
        /// </summary>
        /// <param name="eventPath">The path to the event file. An example value is "RewardTracks/Events/Rituals/ritualEagleStrike.json".</param>
        /// <param name="flightId">Unique identifier for the currently active flight.</param>
        /// <returns>If successful, an instance of RewardTrackMetadata is returned. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<RewardTrackMetadata, RawResponseContainer>> GetEvent(string eventPath, string flightId)
        {
            return await this.GetAsync<RewardTrackMetadata>(
                $"/hi/Progression/file/{eventPath}?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the queries used to obtain override values for graphic device specifications.
        /// </summary>
        /// <returns>If successful, returns an instance of OverrideQueryDefinition containing query definitions. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<OverrideQueryDefinition, RawResponseContainer>> GetGraphicsSpecControlOverrides()
        {
            return await this.GetAsync<OverrideQueryDefinition>(
                "/hi/Specs/file/graphics/GraphicsSpecControlOverrides.json",
                useClearance: true);
        }

        /// <summary>
        /// Unknown what this API specifically returns, but the assumption is that it's configuration for graphic setting overrides.
        /// </summary>
        /// <remarks>
        /// TODO: Need to figure out what the API response here is. Haven't seen this actually activated in-game. For the time being, the API call will return a raw response.
        /// </remarks>
        /// <returns>Returns a string containing the response.</returns>
        public async Task<HaloApiResultContainer<string, RawResponseContainer>> GetGraphicSpecs()
        {
            return await this.GetAsync<string>(
                "/hi/Specs/file/graphics/overrides.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets an image for an associated game CMS asset. Example path is "progression/inventory/armor/gloves/003-001-olympus-8e7c9dff-sm.png".
        /// </summary>
        /// <param name="filePath">Path to the CMS image.</param>
        /// <returns>If successful, returns the byte array for the requested image. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<byte[], RawResponseContainer>> GetImage(string filePath)
        {
            return await this.GetAsync<byte[]>(
                $"/hi/images/file/{filePath}",
                useClearance: true);
        }

        /// <summary>
        /// Gets a specific item from the Game CMS, such as armor emblems, weapon cores, vehicle cores, and others.
        /// </summary>
        /// <remarks>
        /// For example, you may find that you can get the data about an armor emblem with the path "/inventory/armor/emblems/013-001-363f4a25.json".
        /// </remarks>
        /// <param name="itemPath">Path to the item to be obtained. Example is "/inventory/armor/emblems/013-001-363f4a25.json".</param>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of InGameItem. Otherwise, null.</returns>
        public async Task<HaloApiResultContainer<InGameItem, RawResponseContainer>> GetItem(string itemPath, string flightId)
        {
            return await this.GetAsync<InGameItem>(
                $"/hi/Progression/file/{itemPath}?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the list of possible error messages that a player can get when attempting to join multiplayer games.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, returns an instance of LobbyHopperErrorMessageList that contains possible errors. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<LobbyHopperErrorMessageList, RawResponseContainer>> GetLobbyErrorMessages(string flightId)
        {
            return await this.GetAsync<LobbyHopperErrorMessageList>(
                $"/hi/Multiplayer/file/gameStartErrorMessages/LobbyHoppperErrorMessageList.json?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Returns metadata on currently available in-game manufacturers and currencies.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of Metadata containing the information about in-game manufacturers and currencies. Otherwise, null.</returns>
        public async Task<HaloApiResultContainer<Metadata, RawResponseContainer>> GetMetadata(string flightId)
        {
            return await this.GetAsync<Metadata>(
                $"/hi/Progression/file/metadata/metadata.json?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Returns the network configuration for the current flight.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, returns an instance of NetworkConfiguration. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<NetworkConfiguration, RawResponseContainer>> GetNetworkConfiguration(string flightId)
        {
            return await this.GetAsync<NetworkConfiguration>(
                $"/hi/Multiplayer/file/network/config.json?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Returns the currently relevant news.
        /// </summary>
        /// <param name="filePath">Path to the news collection. Example is "/articles/articles.json".</param>
        /// <returns>If successful, returns a News instance containing the currently active news. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<News, RawResponseContainer>> GetNews(string filePath)
        {
            return await this.GetAsync<News>(
                $"/hi/news/file/{filePath}",
                useClearance: true);
        }

        /// <summary>
        /// Returns information about a message that is displayed when, I assume, authentication fails.
        /// </summary>
        /// <remarks>It's unclear where this is actually used because the sample response is a test one, without any relevant context.</remarks>
        /// <returns>If successful, an instance of OEConfiguration containing the message. Otherwise, null.</returns>
        public async Task<HaloApiResultContainer<OEConfiguration, RawResponseContainer>> GetNotAllowedInTitleMessage()
        {
            return await this.GetAsyncFullUrl<OEConfiguration>(
                $"https://{HaloCoreEndpoints.GameCmsOrigin}.{HaloCoreEndpoints.ServiceDomain}/branches/hi/OEConfiguration/data/authfail/Default.json",
                useSpartanToken: false);
        }

        /// <summary>
        /// Returns a progression file. This method is using a generic parameter due to the fact that there are multiple progression file variants.
        /// </summary>
        /// <typeparam name="T">Type of progression file to be obtained.</typeparam>
        /// <param name="filePath">Path to the progression file.</param>
        /// <returns>If successful, returns an instance of T, where T is the type of the progression file. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<T, RawResponseContainer>> GetProgressionFile<T>(string filePath)
        {
            return await this.GetAsync<T>(
                $"/hi/Progression/file/{filePath}",
                useClearance: true);
        }

        /// <summary>
        /// Get recommended drivers for the current version of Halo Infinite.
        /// </summary>
        /// <returns>If successful, returns an instance of DriverManifest that contains details on supported drivers. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<DriverManifest, RawResponseContainer>> GetRecommendedDrivers()
        {
            return await this.GetAsync<DriverManifest>(
                "/hi/Specs/file/graphics/RecommendedDrivers.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about a given Halo Infinite season.
        /// </summary>
        /// <remarks>
        /// Keep in mind that the season numbers do not align cleanly with the public season numbers. For example, public Season 2 is Season 7 in this API. That is caused by a number of test season that 343 added to the product ahead of release.
        /// </remarks>
        /// <param name="seasonPath">The path to the season. Typical example is "Seasons/Season7.json" for the Lone Wolves season.</param>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of SeasonRewardTrack containing season information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<SeasonRewardTrack, RawResponseContainer>> GetSeasonRewardTrack(string seasonPath, string flightId)
        {
            return await this.GetAsync<SeasonRewardTrack>(
                $"/hi/Progression/file/{seasonPath}?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the list of available career ranks for a given career path ID.
        /// </summary>
        /// <param name="careerPathId">Unique identifier for the career path. Example value is "careerRank1".</param>
        /// <returns>If successful, returns an instance of <see cref="CareerTrackContainer"/>. Otherwise, returns null with associated error details in <see cref="RawResponseContainer"/> within the result.</returns>
        public async Task<HaloApiResultContainer<CareerTrackContainer, RawResponseContainer>> GetCareerRanks(string careerPathId)
        {
            return await this.GetAsync<CareerTrackContainer>(
                $"/hi/Progression/file/RewardTracks/CareerRanks/{careerPathId}.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets the currently available season calendar.
        /// </summary>
        /// <returns>If successful, returns an instance of <see cref="SeasonCalendar"/> that contains pointers to season details. Otherwise, returns null with associated error details in <see cref="RawResponseContainer"/> within the result.</returns>
        public async Task<HaloApiResultContainer<SeasonCalendar, RawResponseContainer>> GetSeasonCalendar()
        {
            return await this.GetAsync<SeasonCalendar>(
                "/hi/Progression/file/Calendars/Seasons/SeasonCalendar.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets the currently available CSR season calendar. This is applicable for ranked games and usually delineates when the rank reset will happen.
        /// </summary>
        /// <returns>If successful, returns an instance of <see cref="SeasonCalendar"/> that contains pointers to season details. Otherwise, returns null with associated error details in <see cref="RawResponseContainer"/> within the result.</returns>
        public async Task<HaloApiResultContainer<SeasonCalendar, RawResponseContainer>> GetCSRCalendar()
        {
            return await this.GetAsync<SeasonCalendar>(
                "/hi/Progression/file/Csr/Calendars/CsrSeasonCalendar.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets a list of all available image files currently used by the multiplayer service.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideImages(string flightId)
        {
            return await this.GetAsync<GuideContainer>(
                $"/hi/images/guide/xo?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets a list of all available multiplayer files currently used by the multiplayer service.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideMultiplayer(string flightId)
        {
            return await this.GetAsync<GuideContainer>(
                $"/hi/Multiplayer/guide/xo?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets a list of all available news files currently used by the multiplayer service.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideNews(string flightId)
        {
            return await this.GetAsync<GuideContainer>(
                $"/hi/News/guide/xo?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets a list of all available progression files currently used by the multiplayer service.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideProgression(string flightId)
        {
            return await this.GetAsync<GuideContainer>(
                $"/hi/Progression/guide/xo?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets a list of all available spec files currently used by the multiplayer service.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideSpecs(string flightId)
        {
            return await this.GetAsync<GuideContainer>(
                $"/hi/Specs/guide/xo?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets a list of all available title authorization files currently used by the multiplayer service.
        /// </summary>
        /// <param name="flightId">Unique ID for the currently active flight.</param>
        /// <returns>If successful, an instance of GuideContainer containing file information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<GuideContainer, RawResponseContainer>> GetGuideTitleAuthorization(string flightId)
        {
            return await this.GetAsync<GuideContainer>(
                $"/hi/TitleAuthorization/guide/xo?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets a list of all available medals and their metadata.
        /// </summary>
        /// <returns>If successful, an instance of <see cref="MedalMetadata"/> containing medal information. Otherwise, returns null and error details.</returns>
        public async Task<HaloApiResultContainer<MedalMetadata, RawResponseContainer>> GetMedalMetadata()
        {
            return await this.GetAsync<MedalMetadata>("/hi/Waypoint/file/medals/metadata.json");
        }

        /// <summary>
        /// Gets the detailed configuration for a Halo Infinite playlist.
        /// </summary>
        /// <param name="playlistFile">JSON file associated with a playlist. Example is "a446725e-b281-414c-a21e-31b8700e95a1.json".</param>
        /// <returns>If successful, an instance of <see cref="PlaylistConfiguration"/> containing playlist configuration. Otherwise, returns null and error details.</returns>
        public async Task<HaloApiResultContainer<PlaylistConfiguration, RawResponseContainer>> GetMultiplayerPlaylistConfiguration(string playlistFile)
        {
            return await this.GetAsync<PlaylistConfiguration>(
                $"/hi/Multiplayer/file/playlists/assets/{playlistFile}");
        }

        /// <summary>
        /// Gets emblem mapping configuration.
        /// </summary>
        /// <returns>If successful, an instance of <see cref="Dictionary{String, Dictionary}"/> with emblem mapping. Otherwise, returns null and error details.</returns>
        public async Task<HaloApiResultContainer<Dictionary<string, Dictionary<string, EmblemMapping>>, RawResponseContainer>> GetEmblemMapping()
        {
            return await this.GetAsync<Dictionary<string, Dictionary<string, EmblemMapping>>>(
                "/hi/Waypoint/file/images/emblems/mapping.json");
        }

        /// <summary>
        /// Gets a file from the Halo Waypoint service.
        /// </summary>
        /// <param name="filePath">Path to the file to be retrieved.</param>
        /// <returns>If successful, a byte array containing the file contents. Otherwise, returns null and error details.</returns>
        public async Task<HaloApiResultContainer<byte[], RawResponseContainer>> GetGenericWaypointFile(string filePath)
        {
            return await this.GetAsync<byte[]>($"/hi/Waypoint/file/{filePath}");
        }
    }
}
