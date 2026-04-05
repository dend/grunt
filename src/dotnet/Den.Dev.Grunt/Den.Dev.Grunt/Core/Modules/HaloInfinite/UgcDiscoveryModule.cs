// <copyright file="UgcDiscoveryModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules.HaloInfinite
{
    /// <summary>
    /// Module for UGC Discovery API operations including searching, manifests, maps, playlists, and game variants.
    /// </summary>
    public sealed class UgcDiscoveryModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UgcDiscoveryModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal UgcDiscoveryModule(ClientBase client)
            : base(client, HaloCoreEndpoints.DiscoveryOrigin)
        {
        }

        /// <summary>
        /// Gets the game manifest based on a build GUID.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetManifestByBuildGuid.xml' path='example'/>
        /// <param name="buildGuid">Build GUID. Example value is "5df1784f-72a9-4207-a529-2f91eb37fc1f".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="Manifest"/>. Otherwise, returns a null object along with error details.</returns>
        public Task<HaloApiResultContainer<Manifest, RawResponseContainer>> GetManifestByBuildGuidAsync(string buildGuid, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(buildGuid);

            return this.GetAsync<Manifest>(
                $"/hi/manifests/guids/{buildGuid}/game",
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the collection of Forge templates (canvases) such as Arid, Seafloor, Mires, Void, Argyle, and more. These are suggested maps from which to start when making a new map in Forge.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetForgeTemplates.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="Project"/> containing the templates. Otherwise, returns a null object along with error details.</returns>
        public Task<HaloApiResultContainer<Project, RawResponseContainer>> GetForgeTemplatesAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Project>(
                "/hi/projects/bf0e9bab-6fed-47a4-8bf7-bfd4422ee552",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the Forge Mode Creator Variants, used for mode creator system inside Forge.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetForgeModeCategories.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="Project"/> containing the variants. Otherwise, returns a null object along with error details.</returns>
        public Task<HaloApiResultContainer<Project, RawResponseContainer>> GetForgeModeCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Project>(
                "/hi/projects/aff73c44-0771-468f-b9cf-5c52eee7ab4c",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the collection of community assets.
        /// </summary>
        /// <remarks>Important to note that the API currently does not return a viable result while being listed in the endpoint configuration.</remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetCommunityTab.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="Project"/> containing the list of assets in the community tab. Otherwise, returns a null object along with error details.</returns>
        public Task<HaloApiResultContainer<Project, RawResponseContainer>> GetCommunityTabAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Project>(
                "/hi/projects/90f9e508-99ce-411c-bf88-7bf12b5e9f52",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the details about a match film.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetFilm.xml' path='example'/>
        /// <param name="assetId">Film asset ID. This is not the same as the match ID, but can be retrieved from match details.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="Film"/> containing film metadata. Otherwise, returns a null object along with error details.</returns>
        public Task<HaloApiResultContainer<Film, RawResponseContainer>> GetFilmAsync(string assetId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);

            return this.GetAsync<Film>(
                $"/hi/films/{assetId}",
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the list of assets recommended by 343 Industries.
        /// </summary>
        /// <remarks>
        /// This endpoint is used within the content browser in Halo Infinite.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_Get343Recommended.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="Project"/> containing the list of recommended assets. Otherwise, returns a null object along with error details.</returns>
        public Task<HaloApiResultContainer<Project, RawResponseContainer>> Get343RecommendedAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<Project>(
                "/hi/projects/712add52-f989-48e1-b3bb-ac7cd8a1c17a",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns metadata about a given engine game variant version.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetEngineGameVariant.xml' path='example'/>
        /// <param name="assetId">Unique asset ID for the engine game variant.</param>
        /// <param name="versionId">Unique ID for the asset version for the engine game variant.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of EngineGameVariant containing appropriate metadata. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<EngineGameVariant, RawResponseContainer>> GetEngineGameVariantAsync(string assetId, string versionId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);
            ArgumentException.ThrowIfNullOrEmpty(versionId);

            return this.GetAsync<EngineGameVariant>(
                $"/hi/engineGameVariants/{assetId}/versions/{versionId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets an engine game variant without an associated version.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetEngineGameVariantWithoutVersion.xml' path='example'/>
        /// <param name="assetId">Unique asset ID for the engine game variant.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of EngineGameVariant containing appropriate metadata. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<EngineGameVariant, RawResponseContainer>> GetEngineGameVariantWithoutVersionAsync(string assetId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);

            return this.GetAsync<EngineGameVariant>(
                $"/hi/engineGameVariants/{assetId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a game manifest.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetManifest.xml' path='example'/>
        /// <param name="assetId">Unique asset ID for the manifest. Example value is "6369c3a6-390e-496c-ab71-93c326347327".</param>
        /// <param name="versionId">Unique version ID for the manifest. Example value is "9a348b5b-08aa-41c2-8b3a-681870c78a76".</param>
        /// <param name="clearanceId">ID of the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, an instance of <see cref="Manifest"/> representing the asset details. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Manifest, RawResponseContainer>> GetManifestAsync(string assetId, string versionId, string clearanceId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);
            ArgumentException.ThrowIfNullOrEmpty(versionId);
            ArgumentException.ThrowIfNullOrEmpty(clearanceId);

            return this.GetAsync<Manifest>(
                $"/hi/manifests/{assetId}/versions/{versionId}?clearanceId={clearanceId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the current game manifest.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetManifestByBuild.xml' path='example'/>
        /// <param name="buildNumber">Build for which the manifest needs to be obtained. Maps to official Halo builds, such as 6.10022.10499.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of Manifest containing game manifest information if request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Manifest, RawResponseContainer>> GetManifestByBuildAsync(string buildNumber, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(buildNumber);

            return this.GetAsync<Manifest>(
                $"/hi/manifests/builds/{buildNumber}/game",
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns information about a given map at a specific release version.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetMap.xml' path='example'/>
        /// <param name="assetId">Unique map ID. For example, the ID for the Recharge map is "8420410b-044d-44d7-80b6-98a766c8c39f".</param>
        /// <param name="versionId">Unique version ID for a map. For example, for the Recharge map a version is "068c0974-f748-41ba-b457-b8fed603576e".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of Map containing map metadata if request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Map, RawResponseContainer>> GetMapAsync(string assetId, string versionId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);
            ArgumentException.ThrowIfNullOrEmpty(versionId);

            return this.GetAsync<Map>(
                $"/hi/maps/{assetId}/versions/{versionId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns information about a given map and mode combination. For example, the Breaker map can be used in Big Team Battle (BTB).
        /// </summary>
        /// <remarks>
        /// An example fully constructed HTTP request to the API is: https://discovery-infiniteugc.svc.halowaypoint.com/hi/mapModePairs/9e056bcc-b9bc-4845-9fe3-6d667f018463/versions/37b8cd75-d1ce-4abf-9349-a76673503410.
        /// This request represents the BTB game mode on the Breaker map.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetMapModePair.xml' path='example'/>
        /// <param name="assetId">Unique ID for the map and mode combination.</param>
        /// <param name="versionId">Unique version ID for the map and mode combination.</param>
        /// <param name="clearanceId">ID of the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of MapModePair containing map metadata if request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<MapModePair, RawResponseContainer>> GetMapModePairAsync(string assetId, string versionId, string clearanceId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);
            ArgumentException.ThrowIfNullOrEmpty(versionId);
            ArgumentException.ThrowIfNullOrEmpty(clearanceId);

            return this.GetAsync<MapModePair>(
                $"/hi/mapModePairs/{assetId}/versions/{versionId}?clearanceId={clearanceId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets a map and mode combination without the version.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetMapModePairWithoutVersion.xml' path='example'/>
        /// <param name="assetId">Unique ID for the map and mode combination. Example value is "b6aca0c7-8ba7-4066-bf91-693571374c3c" for "sgh_interlock".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="MapModePair"/> representing the map and mode combination. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<MapModePair, RawResponseContainer>> GetMapModePairWithoutVersionAsync(string assetId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);

            return this.GetAsync<MapModePair>(
                $"/hi/mapModePairs/{assetId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about a given map.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetMapWithoutVersion.xml' path='example'/>
        /// <param name="assetId">Unique map ID. For example, the ID for the Recharge map is "8420410b-044d-44d7-80b6-98a766c8c39f".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of Map containing map metadata if request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Map, RawResponseContainer>> GetMapWithoutVersionAsync(string assetId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);

            return this.GetAsync<Map>(
                $"/hi/maps/{assetId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about a specific playlist.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetPlaylist.xml' path='example'/>
        /// <param name="assetId">Unique asset ID for the playlist.</param>
        /// <param name="versionId">Unique version ID for the playlist.</param>
        /// <param name="clearanceId">ID of the currently active flight.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of Playlist containing playlist information. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Playlist, RawResponseContainer>> GetPlaylistAsync(string assetId, string versionId, string clearanceId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);
            ArgumentException.ThrowIfNullOrEmpty(versionId);
            ArgumentException.ThrowIfNullOrEmpty(clearanceId);

            return this.GetAsync<Playlist>(
                $"/hi/playlists/{assetId}/versions/{versionId}?clearanceId={clearanceId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about a specific playlist without its version.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetPlaylistWithoutVersion.xml' path='example'/>
        /// <param name="assetId">Unique asset ID for the playlist.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="Playlist"/> representing the targeted playlist. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Playlist, RawResponseContainer>> GetPlaylistWithoutVersionAsync(string assetId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);

            return this.GetAsync<Playlist>(
                $"/hi/playlists/{assetId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about a specific prefab version.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetPrefab.xml' path='example'/>
        /// <param name="assetId">Unique asset ID for the prefab.</param>
        /// <param name="versionId">Unique version ID for the prefab.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns a <see cref="Prefab"/> instance representing the specific prefab. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Prefab, RawResponseContainer>> GetPrefabAsync(string assetId, string versionId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);
            ArgumentException.ThrowIfNullOrEmpty(versionId);

            return this.GetAsync<Prefab>(
                $"/hi/prefabs/{assetId}/versions/{versionId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets information about a specific prefab.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetPrefabWithoutVersion.xml' path='example'/>
        /// <param name="assetId">Unique asset ID for the prefab.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns a <see cref="Prefab"/> instance representing the specific prefab. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Prefab, RawResponseContainer>> GetPrefabWithoutVersionAsync(string assetId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);

            return this.GetAsync<Prefab>(
                $"/hi/prefabs/{assetId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns the project details that are associated with a given version of a manifest. This manifest contains all the maps and modes to show in the custom game menus.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetProject.xml' path='example'/>
        /// <param name="assetId">Unique asset ID representing the project. Example asset ID currently active is the custom game manifest ID: "a9dc0785-2a99-4fec-ba6e-0216feaaf041".</param>
        /// <param name="versionId">Version ID for the project. As an example, a version of a production manifest is "a4e68648-f994-44bb-853e-d09ee224d799".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of Project containing current game project information if request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Project, RawResponseContainer>> GetProjectAsync(string assetId, string versionId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);
            ArgumentException.ThrowIfNullOrEmpty(versionId);

            return this.GetAsync<Project>(
                $"/hi/projects/{assetId}/versions/{versionId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns information on a project (collection of game modes and maps). This manifest contains all the maps and modes to show in the custom game menus.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetProjectWithoutVersion.xml' path='example'/>
        /// <param name="assetId">Unique asset ID representing the project. Example asset ID currently active is the custom game manifest ID: "a9dc0785-2a99-4fec-ba6e-0216feaaf041".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of Project containing current game project information if request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Project, RawResponseContainer>> GetProjectWithoutVersionAsync(string assetId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);

            return this.GetAsync<Project>(
                $"/hi/projects/{assetId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns information about available tags that can be associated with game assets.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetTagsInfo.xml' path='example'/>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of TagInfo containing a list of tags if the request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<TagInfo, RawResponseContainer>> GetTagsInfoAsync(CancellationToken cancellationToken = default)
        {
            return this.GetAsync<TagInfo>(
                "/hi/info/tags",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns information about a game asset version. This information is specific only to the version specified and does not contain general asset metadata. To get general asset metadata, use GetUgcGameVariantWithoutVersion.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetUgcGameVariant.xml' path='example'/>
        /// <param name="assetId">Unique ID for the game asset. For example, for "Fiesta - Slayer" game mode, the asset ID is "aca7bbf8-7a18-4aae-8785-1bd3f58275fd".</param>
        /// <param name="versionId">Version for the asset to obtain. Example value is "3685f6b2-2860-4e98-9d13-513087edb465".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of UGCGameVariant containing game variant metadata if the request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<UGCGameVariant, RawResponseContainer>> GetUgcGameVariantAsync(string assetId, string versionId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);
            ArgumentException.ThrowIfNullOrEmpty(versionId);

            return this.GetAsync<UGCGameVariant>(
                $"/hi/ugcGameVariants/{assetId}/versions/{versionId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns general asset metadata related to a game asset.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_GetUgcGameVariantWithoutVersion.xml' path='example'/>
        /// <param name="assetId">Unique ID for the game asset. For example, for "Fiesta - Slayer" game mode, the asset ID is "aca7bbf8-7a18-4aae-8785-1bd3f58275fd".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of UGCGameVariant containing asset metadata if the request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<UGCGameVariant, RawResponseContainer>> GetUgcGameVariantWithoutVersionAsync(string assetId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(assetId);

            return this.GetAsync<UGCGameVariant>(
                $"/hi/ugcGameVariants/{assetId}",
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Searches for assets in the user generated content directory.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_Search.xml' path='example'/>
        /// <param name="start">Number of results from which to start the iteration.</param>
        /// <param name="count">Count of results to return.</param>
        /// <param name="includeTimes">Include creation, modification, and deletion times in results.</param>
        /// <param name="sort">Property by which to sort the results. Example is "PlaysRecent".</param>
        /// <param name="order">Determines whether results are ordered in descending or ascending order.</param>
        /// <param name="assetKinds">List of asset kinds to be included in the search.</param>
        /// <param name="author">The author's numeric XUID.</param>
        /// <param name="term">Search term. Optional.</param>
        /// <param name="tags">List of tags. Multiple tags are applied with an OR operator. Optional.</param>
        /// <param name="averageRatingMin">Minimum average rating between 0 and 5. Optional.</param>
        /// <param name="fromDateCreatedUtc">Minimum date created. Optional.</param>
        /// <param name="toDateCreatedUtc">Maximum date created. Optional.</param>
        /// <param name="fromDateModifiedUtc">Minimum date modified. Optional.</param>
        /// <param name="toDateModifiedUtc">Maximum date modified. Optional.</param>
        /// <param name="fromDatePublishedUtc">Minimum date published. Optional.</param>
        /// <param name="toDatePublishedUtc">Maximum date published. Optional.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of SearchResultsContainer containing assets. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<SearchResultsContainer, RawResponseContainer>> SearchAsync(int start = 0, int count = 12, bool includeTimes = true, string sort = "DatePublishedUtc", ResultOrder order = ResultOrder.Desc, List<AssetKind>? assetKinds = null, string? author = null, string? term = null, List<string>? tags = null, decimal? averageRatingMin = null, DateTime? fromDateCreatedUtc = null, DateTime? toDateCreatedUtc = null, DateTime? fromDateModifiedUtc = null, DateTime? toDateModifiedUtc = null, DateTime? fromDatePublishedUtc = null, DateTime? toDatePublishedUtc = null, CancellationToken cancellationToken = default)
        {
            var baseSearchString = $"/hi/search?start={start}&count={count}&include-times={includeTimes}&sort={sort}&order={order}";

            if (!string.IsNullOrEmpty(author))
            {
                baseSearchString += $"&author=xuid({author})";
            }

            if (assetKinds != null && assetKinds.Any())
            {
                baseSearchString += "&assetKind=";
                baseSearchString += string.Join("&assetKind=", assetKinds.ToArray());
            }

            if (!string.IsNullOrEmpty(term))
            {
                baseSearchString += $"&term={Uri.EscapeDataString(term)}";
            }

            if (tags != null && tags.Any())
            {
                foreach (var tag in tags)
                {
                    baseSearchString += $"&tags={Uri.EscapeDataString(tag)}";
                }
            }

            if (averageRatingMin.HasValue)
            {
                baseSearchString += $"&averageRatingMin={averageRatingMin.Value}";
            }

            if (fromDateCreatedUtc.HasValue)
            {
                baseSearchString += $"&fromDateCreatedUtc={Uri.EscapeDataString(fromDateCreatedUtc.Value.ToString("o"))}";
            }

            if (toDateCreatedUtc.HasValue)
            {
                baseSearchString += $"&toDateCreatedUtc={Uri.EscapeDataString(toDateCreatedUtc.Value.ToString("o"))}";
            }

            if (fromDateModifiedUtc.HasValue)
            {
                baseSearchString += $"&fromDateModifiedUtc={Uri.EscapeDataString(fromDateModifiedUtc.Value.ToString("o"))}";
            }

            if (toDateModifiedUtc.HasValue)
            {
                baseSearchString += $"&toDateModifiedUtc={Uri.EscapeDataString(toDateModifiedUtc.Value.ToString("o"))}";
            }

            if (fromDatePublishedUtc.HasValue)
            {
                baseSearchString += $"&fromDatePublishedUtc={Uri.EscapeDataString(fromDatePublishedUtc.Value.ToString("o"))}";
            }

            if (toDatePublishedUtc.HasValue)
            {
                baseSearchString += $"&toDatePublishedUtc={Uri.EscapeDataString(toDatePublishedUtc.Value.ToString("o"))}";
            }

            return this.GetAsync<SearchResultsContainer>(
                baseSearchString,
                useClearance: true,
                cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns information about available film chunks that are used to reconstruct the entire match.
        /// </summary>
        /// <remarks>Despite the name of this request, the data captured here is not actually a movie but rather a full re-creation of the match, using in-game assets and player positions.</remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_Discovery_SpectateByMatchId.xml' path='example'/>
        /// <param name="matchId">Unique ID for the match.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of Film containing film metadata if the request is successful. Otherwise, returns null.</returns>
        public Task<HaloApiResultContainer<Film, RawResponseContainer>> SpectateByMatchIdAsync(string matchId, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(matchId);

            return this.GetAsync<Film>(
                $"/hi/films/matches/{matchId}/spectate",
                cancellationToken: cancellationToken);
        }
    }
}
