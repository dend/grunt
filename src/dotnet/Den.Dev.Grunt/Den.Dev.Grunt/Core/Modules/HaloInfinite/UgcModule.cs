// <copyright file="UgcModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules.HaloInfinite
{
    /// <summary>
    /// Module for UGC (User Generated Content) authoring API operations including asset management, favorites, and ratings.
    /// </summary>
    public class UgcModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UgcModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal UgcModule(ClientBase client)
            : base(client, HaloCoreEndpoints.AuthoringOrigin)
        {
        }

        /// <summary>
        /// Grants or revokes permissions for a player in relation to an in-game asset.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_GrantOrRevokePermissions.xml' path='example'/>
        /// <param name="title">Title associated with an asset. Example value is "hi" for Halo Infinite.</param>
        /// <param name="assetType">Type of asset to modify permissions for. Example value is "ugcGameVariants".</param>
        /// <param name="assetId">Unique asset ID. Example value is "3895f3d4-2493-4b84-ae18-876ad3ab344d" for a UGC game variant.</param>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="permission">A <see cref="Permission"/> object with the AuthoringRole set to the desired permission level.</param>
        /// <returns>If successful, returns an instance of <see cref="Permission"/> with permission details. Otherwise, returns a null result object with attached error details.</returns>
        public async Task<HaloApiResultContainer<Permission, RawResponseContainer>> GrantOrRevokePermissions(string title, string assetType, string assetId, string player, Permission permission)
        {
            return await this.PatchJsonAsync<Permission, Permission>(
                $"/{title}/{assetType}/{assetId}/permissions/xuid({player})",
                permission,
                useClearance: true);
        }

        /// <summary>
        /// Checks whether the player has favorited a specific asset.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_CheckAssetPlayerBookmark.xml' path='example'/>
        /// <param name="title">Title for which the asset should be obtained. An example value is "hi".</param>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "373f3d27-cb4c-4d7b-b6c9-7757de3c1133" for "Arena:King of the Hill".</param>
        /// <returns>If successful, returns an instance of FavoriteAsset containing asset information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<FavoriteAsset, RawResponseContainer>> CheckAssetPlayerBookmark(string title, string player, string assetType, string assetId)
        {
            return await this.GetAsync<FavoriteAsset>(
                $"/{title}/players/xuid({player})/favorites/{assetType}/{assetId}");
        }

        /// <summary>
        /// Creates a new version of an asset as part of a working editing session.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_CreateAssetVersionAgnostic.xml' path='example'/>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <param name="starter">Container for the session descriptor that starts the new version.</param>
        /// <returns>If version creation is successful, returns an instance of AuthoringAssetVersion. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetVersion, RawResponseContainer>> CreateAssetVersionAgnostic(string title, string assetType, string assetId, AuthoringSessionSourceStarter starter)
        {
            return await this.PostJsonAsync<AuthoringAssetVersion, AuthoringSessionSourceStarter>(
                $"/{title}/{assetType}/{assetId}/versions",
                starter,
                useClearance: true);
        }

        /// <summary>
        /// Deletes all versions of an asset.
        /// </summary>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If deletion is successful, returns true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> DeleteAllVersions(string title, string assetType, string assetId)
        {
            return await this.DeleteAsync<bool>(
                $"/{title}/{assetType}/{assetId}/versions",
                useClearance: true);
        }

        /// <summary>
        /// Deletes an asset.
        /// </summary>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If deletion is successful, returns true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> DeleteAsset(string title, string assetType, string assetId)
        {
            return await this.DeleteAsync<bool>(
                $"/{title}/{assetType}/{assetId}",
                useClearance: true);
        }

        /// <summary>
        /// Deletes a specific version of an asset.
        /// </summary>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <param name="versionId">Unique ID for the version of the asset.</param>
        /// <returns>If deletion is successful, returns true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> DeleteVersion(string title, string assetType, string assetId, string versionId)
        {
            return await this.DeleteAsync<bool>(
                $"/{title}/{assetType}/{assetId}/versions/{versionId}");
        }

        /// <summary>
        /// End all active asset authoring sessions for a given asset.
        /// </summary>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If session termination is successful, return true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> EndSession(string title, string assetType, string assetId)
        {
            return await this.DeleteAsync<bool>(
                $"/{title}/{assetType}/{assetId}/sessions/active");
        }

        /// <summary>
        /// Favorites an asset for the player.
        /// </summary>
        /// <remarks>
        /// This method expects a JSON body, but I don't yet know what the underlying data structure is.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_FavoriteAnAsset.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If successful, returns an instance of FavoriteAsset confirming the addition of the asset to favorites. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<FavoriteAsset, RawResponseContainer>> FavoriteAnAsset(string player, string assetType, string assetId)
        {
            return await this.PutAsync<FavoriteAsset>(
                $"/hi/players/xuid({player})/favorites/{assetType}/{assetId}",
                "{}");
        }

        /// <summary>
        /// Gets authoring metadata about a specific asset.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_GetAsset.xml' path='example'/>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If successful, returns an instance of AuthoringAsset containing authoring metadata. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAsset, RawResponseContainer>> GetAsset(string title, string assetType, string assetId)
        {
            return await this.GetAsync<AuthoringAsset>(
                $"/{title}/{assetType}/{assetId}");
        }

        /// <summary>
        /// Returns a binary blob using its path as a reference.
        /// </summary>
        /// <param name="blobPath">Path to the blob to be obtained.</param>
        /// <returns>If successful, returns a binary blob containing file data. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<byte[], RawResponseContainer>> GetBlob(string blobPath)
        {
            return await this.GetAsyncFullUrl<byte[]>(
                $"https://blobs-infiniteugc.{HaloCoreEndpoints.ServiceDomain}/{blobPath}",
                useSpartanToken: false);
        }

        /// <summary>
        /// Gets the films for the latest asset version.
        /// </summary>
        /// <remarks>
        /// Interestingly enough, this API call did not contain the Film suffix in the name. I added it for explicit identification because otherwise it would be confusing.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_GetLatestAssetVersionFilm.xml' path='example'/>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If successful, returns an instance of AuthoringAssetVersion containing film data in the CustomData property. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetVersion, RawResponseContainer>> GetLatestAssetVersionFilm(string title, string assetId)
        {
            return await this.GetAsync<AuthoringAssetVersion>(
                $"/{title}/films/{assetId}/versions/latest",
                useClearance: true);
        }

        /// <summary>
        /// Gets metadata related to the latest version of a specified asset.
        /// </summary>
        /// <remarks>
        /// Certain asset types, such as engine game variants, might return a 403 response code for the API, therefore you will not get a real version here.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_GetLatestAssetVersionAgnostic.xml' path='example'/>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If successful, returns an instance of AuthoringAssetVersion containing version metadata for an asset. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetVersion, RawResponseContainer>> GetLatestAssetVersionAgnostic(string title, string assetType, string assetId)
        {
            return await this.GetAsync<AuthoringAssetVersion>(
                $"/{title}/{assetType}/{assetId}/versions/latest",
                useClearance: true);
        }

        /// <summary>
        /// Returns a published version of the asset.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_GetPublishedVersion.xml' path='example'/>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If successful, returns an instance of AuthoringAssetVersion containing version metadata for a published asset. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetVersion, RawResponseContainer>> GetPublishedVersion(string title, string assetType, string assetId)
        {
            return await this.GetAsync<AuthoringAssetVersion>(
                $"/{title}/{assetType}/{assetId}/versions/published",
                useClearance: true);
        }

        /// <summary>
        /// Gets metadata related to a concrete version of a specified asset.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_GetSpecificAssetVersion.xml' path='example'/>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <param name="versionId">Unique ID for the version of the asset.</param>
        /// <returns>If successful, returns an instance of AuthoringAssetVersion that contains asset version information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetVersion, RawResponseContainer>> GetSpecificAssetVersion(string title, string assetType, string assetId, string versionId)
        {
            return await this.GetAsync<AuthoringAssetVersion>(
                $"/{title}/{assetType}/{assetId}/versions/{versionId}");
        }

        /// <summary>
        /// Gets information about all versions for a specified asset.
        /// </summary>
        /// <remarks>
        /// The underlying request supports specifying parameters that limit the search, such as ?start=number, however that is not yet implemented in this version of the API wrapper.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_ListAllVersions.xml' path='example'/>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If successful, returns an instance of AuthoringAssetVersionContainer that contains information about all available versions for an asset. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetVersionContainer, RawResponseContainer>> ListAllVersions(string title, string assetType, string assetId)
        {
            return await this.GetAsync<AuthoringAssetVersionContainer>(
                $"/{title}/{assetType}/{assetId}/versions");
        }

        /// <summary>
        /// Gets information about all authored assets that a player owns.
        /// </summary>
        /// <remarks>
        /// The underlying request supports specifying parameters that limit the search, such as ?start=number, however that is not yet implemented in this version of the API wrapper.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_ListPlayerAssets.xml' path='example'/>
        /// <param name="title">Title which contains the asset. An example value here is "hi".</param>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="start">Number of results from which to start the iteration.</param>
        /// <param name="count">Number of assets to return. Maximum is 25. Going beyond 25 will result in only 25 values being returned.</param>
        /// <param name="includeTimes">Include times for asset modification.</param>
        /// <param name="sort">Property by which to sort the results. Example is "PlaysRecent".</param>
        /// <param name="order">Determines whether results are ordered in descending or ascending order.</param>
        /// <param name="keywords">List of keywords by which to filter.</param>
        /// <param name="kind">Type of asset to return.</param>
        /// <returns>If successful, returns an instance of AuthoringAssetContainer containing information about assets a player owns. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetContainer, RawResponseContainer>> ListPlayerAssets(string title, string player, int start, int count, bool includeTimes, string sort, ResultOrder order, List<string> keywords, AssetKind kind)
        {
            var formattedKeywordList = string.Empty;
            if (keywords != null && keywords.Count > 0)
            {
                formattedKeywordList = string.Join(",", keywords);
            }

            return await this.GetAsync<AuthoringAssetContainer>(
                $"/{title}/players/xuid({player})/assets?start={start}&count={count}&include-times={includeTimes}&sort={sort}&order={order}&keywords={formattedKeywordList}&kind={kind}",
                useClearance: true);
        }

        /// <summary>
        /// Gets information about favorite assets of a specific type a player has registered on their account.
        /// </summary>
        /// <remarks>
        /// The underlying request supports specifying parameters that limit the search, such as ?start=number, however that is not yet implemented in this version of the API wrapper.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_ListPlayerFavorites.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <returns>If successful, returns an instance of AuthoringFavoritesContainer containing the list of favorites. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringFavoritesContainer, RawResponseContainer>> ListPlayerFavorites(string player, string assetType)
        {
            return await this.GetAsync<AuthoringFavoritesContainer>(
                $"/hi/players/xuid({player})/favorites/{assetType}",
                useClearance: true);
        }

        /// <summary>
        /// Gets authored favorites a player has registered on their account.
        /// </summary>
        /// <remarks>
        /// The underlying request supports specifying parameters that limit the search, such as ?start=number, however that is not yet implemented in this version of the API wrapper.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_ListPlayerFavoritesAgnostic.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <returns>If successful, returns an instance of AuthoringFavoritesContainer containing the list of favorites. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringFavoritesContainer, RawResponseContainer>> ListPlayerFavoritesAgnostic(string player)
        {
            return await this.GetAsync<AuthoringFavoritesContainer>(
                $"/hi/players/xuid({player})/favorites",
                useClearance: true);
        }

        /// <summary>
        /// Update an existing asset version.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_PatchAssetVersion.xml' path='example'/>
        /// <param name="title">Title for the game for which the authoring session needs to be spawned. Example variant is "hi" for "Halo Infinite".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <param name="versionId">Unique ID for the asset version to be published.</param>
        /// <param name="patchedAsset">Updated asset version with custom configuration.</param>
        /// <returns>If successful, returns an instance of AuthoringAssetVersion containing the changes. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetVersion, RawResponseContainer>> PatchAssetVersion(string title, string assetType, string assetId, string versionId, AuthoringAssetVersion patchedAsset)
        {
            return await this.PatchJsonAsync<AuthoringAssetVersion, AuthoringAssetVersion>(
                $"/{title}/{assetType}/{assetId}/versions/{versionId}",
                patchedAsset,
                useClearance: true);
        }

        /// <summary>
        /// Publishes an asset version.
        /// </summary>
        /// <remarks>
        /// There is no content returned for the response other than a HTTP 200 OK if the operation is successful.
        /// </remarks>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <param name="versionId">Unique ID for the asset version to be published.</param>
        /// <param name="clearanceId">ID of the currently active flight.</param>
        /// <returns>If the publishing process is successful, returns true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> PublishAssetVersion(string assetType, string assetId, string versionId, string clearanceId)
        {
            return await this.PostAsync<bool>(
                $"/hi/{assetType}/{assetId}/publish/{versionId}?clearanceId={clearanceId}",
                "{}",
                useClearance: true);
        }

        /// <summary>
        /// Gets player-assigned ratings for an asset.
        /// </summary>
        /// <remarks>
        /// This API is actually not captured in the endpoint catalog, but it seems to return values anyway.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_GetAssetRatings.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <returns>If successful, returns an instance of AuthoringAssetRating containing rating information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetRating, RawResponseContainer>> GetAssetRatings(string player, string assetType, string assetId)
        {
            return await this.GetAsync<AuthoringAssetRating>(
                $"/hi/players/xuid({player})/ratings/{assetType}/{assetId}");
        }

        /// <summary>
        /// Rates an asset the player has access to.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_RateAnAsset.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <param name="rating">An object containing asset rating information. Rating should be set in CustomData.</param>
        /// <returns>If successful, returns an instance of AuthoringAssetRating containing the updated rating. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAssetRating, RawResponseContainer>> RateAnAsset(string player, string assetType, string assetId, AuthoringAssetRating rating)
        {
            return await this.PutJsonAsync<AuthoringAssetRating, AuthoringAssetRating>(
                $"/hi/players/xuid({player})/ratings/{assetType}/{assetId}",
                rating);
        }

        /// <summary>
        /// Reports an asset.
        /// </summary>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_ReportAnAsset.xml' path='example'/>
        /// <param name="player">The player's numeric XUID.</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique ID for the asset. Example value is "f96f57e2-9f15-45c5-83ac-5775a48d2ba8" for "Attrition-Default-UGC".</param>
        /// <param name="report">Instance of <see cref="AssetReport"/> containing the report for the asset.</param>
        /// <returns>If successful, returns an instance of AssetReport containing the report information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AssetReport, RawResponseContainer>> ReportAnAsset(string player, string assetType, string assetId, AssetReport report)
        {
            return await this.PutJsonAsync<AssetReport, AssetReport>(
                $"/hi/players/xuid({player})/reports/{assetType}/{assetId}",
                report);
        }

        /// <summary>
        /// API for creating new assets.
        /// </summary>
        /// <remarks>
        /// This API is used to create new assets in the user's file browser. The game generally uses Bond-encoded requests, so it's
        /// still up to discovery to figure out what the values for the POST request are.
        /// TODO: Need to figure out what the actual data model is for the POST request.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_SpawnAsset.xml' path='example'/>
        /// <param name="title">Title for the game for which the authoring session needs to be spawned. Example variant is "hi" for "Halo Infinite".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants", "Maps", or "Prefabs".</param>
        /// <param name="asset">Asset definition, containing information about the asset to be created.</param>
        /// <param name="contentType">Content type to be used for the request. Default value uses the Bond encoding.</param>
        /// <returns>If successful, returns an instance of AuthoringAsset containing asset information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AuthoringAsset, RawResponseContainer>> SpawnAsset(string title, string assetType, object? asset = null, APIContentType contentType = APIContentType.BondCompactBinary)
        {
            ArgumentNullException.ThrowIfNull(asset);

            return await this.PostJsonAsync<AuthoringAsset, object>(
                $"/{title}/{assetType}",
                asset!,
                useClearance: true,
                contentType: contentType);
        }

        /// <summary>
        /// Starts a new authoring session to edit an asset.
        /// </summary>
        /// <remarks>
        /// It also seems that using `includeContainerSas` results in a 403 response, but without it a session can be created.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_StartSessionAgnostic.xml' path='example'/>
        /// <param name="title">Title for the game for which the authoring session needs to be spawned. Example variant is "hi" for "Halo Infinite".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique asset ID for the asset type specified earlier.</param>
        /// <param name="includeContainerSas">Determines whether to include the container SAS in the response or not. Setting this value to "true" will result in a 403 Forbidden error.</param>
        /// <param name="starter">Starter object that describes who is starting the session and the previous version of the asset.</param>
        /// <returns>If successful, returns an instance of AssetAuthoringSession with details about the created session. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AssetAuthoringSession, RawResponseContainer>> StartSessionAgnostic(string title, string assetType, string assetId, bool includeContainerSas, AuthoringSessionStarter starter)
        {
            return await this.PostJsonAsync<AssetAuthoringSession, AuthoringSessionStarter>(
                $"/{title}/{assetType}/{assetId}/sessions?include-container-sas={includeContainerSas}",
                starter,
                useClearance: true);
        }

        /// <summary>
        /// Extends an existing authoring session.
        /// </summary>
        /// <remarks>
        /// For now, an empty JSON is passed to the PATCH request. In the future, analysis needs to be done to understand more about how the request actually
        /// can be used to modify the data, since that's what a PATCH is usually about.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/HIUGC_ExtendSessionAgnostic.xml' path='example'/>
        /// <param name="title">Title for the game for which the authoring session needs to be spawned. Example variant is "hi" for "Halo Infinite".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique asset ID for the asset type specified earlier.</param>
        /// <param name="includeContainerSas">Determines whether to include the container SAS in the response or not. Setting this value to "true" will result in a 403 Forbidden error.</param>
        /// <returns>If successful, returns an instance of AssetAuthoringSession with details about the created session. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AssetAuthoringSession, RawResponseContainer>> ExtendSessionAgnostic(string title, string assetType, string assetId, bool includeContainerSas)
        {
            return await this.PatchAsync<AssetAuthoringSession>(
                $"/{title}/{assetType}/{assetId}/sessions?include-container-sas={includeContainerSas}",
                "{}");
        }

        /// <summary>
        /// Deletes an existing authoring session.
        /// </summary>
        /// <param name="title">Title for the game for which the authoring session needs to be spawned. Example variant is "hi" for "Halo Infinite".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique asset ID for the asset type specified earlier.</param>
        /// <returns>If the request to delete the session is successful, returns true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> DeleteSessionAgnostic(string title, string assetType, string assetId)
        {
            return await this.DeleteAsync<bool>(
                $"/{title}/{assetType}/{assetId}/sessions");
        }

        /// <summary>
        /// Undeletes a previously deleted asset.
        /// </summary>
        /// <remarks>
        /// Interestingly enough, the API itself, as seen in the settings endpoint, does not contain the `/recover` suffix. I had to add it manually
        /// in this specific implementation.
        /// </remarks>
        /// <param name="title">Title for the game for which the authoring session needs to be spawned. Example variant is "hi" for "Halo Infinite".</param>
        /// <param name="assetType">Type of asset to check. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique asset ID for the asset type specified earlier.</param>
        /// <returns>If the request to undelete an asset was successful, returns true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> UndeleteAsset(string title, string assetType, string assetId)
        {
            return await this.PostAsync<bool>(
                $"/{title}/{assetType}/{assetId}/recover");
        }

        /// <summary>
        /// Undeletes a previously deleted asset version.
        /// </summary>
        /// <param name="title">Title for the game for which the authoring session needs to be spawned. Example variant is "hi" for "Halo Infinite".</param>
        /// <param name="assetType">Type of asset to unpublish. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique asset ID for the asset type specified earlier.</param>
        /// <param name="versionId">Unique ID for the asset version to be undeleted.</param>
        /// <returns>If successful, returns true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> UndeleteVersion(string title, string assetType, string assetId, string versionId)
        {
            return await this.PostAsync<bool>(
                $"/{title}/{assetType}/{assetId}/versions/{versionId}/recover");
        }

        /// <summary>
        /// Unpublishes a previously published asset.
        /// </summary>
        /// <param name="assetType">Type of asset to unpublish. Example value is "UgcGameVariants".</param>
        /// <param name="assetId">Unique asset ID for the asset type specified earlier.</param>
        /// <returns>If successful, returns true. Otherwise, returns false.</returns>
        public async Task<HaloApiResultContainer<bool, RawResponseContainer>> UnpublishAsset(string assetType, string assetId)
        {
            return await this.PostAsync<bool>(
                $"/hi/{assetType}/{assetId}/unpublish");
        }
    }
}
