// <copyright file="Halo5Client.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Net.Http;
using System.Threading.Tasks;
using OpenSpartan.Grunt.Core.Foundation;
using OpenSpartan.Grunt.Endpoints;
using OpenSpartan.Grunt.Models;
using OpenSpartan.Grunt.Models.ApiIngress;
using OpenSpartan.Grunt.Models.Halo5;
using OpenSpartan.Grunt.Util;

namespace OpenSpartan.Grunt.Core
{
    /// <summary>
    /// Client used to access the Halo 5 API surface.
    /// </summary>
    public class Halo5Client : ClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Halo5Client"/> class, used to access the Halo 5 API.
        /// </summary>
        /// <param name="spartanToken">The Spartan token used to authenticate against the Halo 5 API.</param>
        /// <param name="xuid">The player identifier in the format "xuid(XUID_VALUE)".</param>
        /// <param name="clearanceToken">ID of the flight/clearance currently active for the player. Optional when first instantiating the client.</param>
        public Halo5Client(string spartanToken, string xuid = "", string clearanceToken = "")
        {
            this.SpartanToken = spartanToken;
            this.Xuid = xuid;
            this.ClearanceToken = clearanceToken;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Halo5Client"/> class, used to access the Halo 5 API.
        /// </summary>
        public Halo5Client()
        {
        }

        /// <summary>
        /// Gets the list of API settings as provided by the official Halo 5 API. This is the latest version of all available endpoints.
        /// </summary>
        /// <remarks>
        /// Method supports returning results in XML behind the scenes. Class names map to XML data model.
        /// </remarks>
        /// <include file='../APIDocsExamples/Halo5/GetApiSettingsContainer.xml' path='//example'/>
        /// <returns>An instance of <see cref="Configuration"/> if the call is successful. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<Configuration, HaloApiErrorContainer>> GetApiSettingsContainer()
        {
            return await this.ExecuteAPIRequest<Configuration>(
                HaloCoreEndpoints.Halo5EndpointsEndpoint,
                HttpMethod.Get,
                false,
                false,
                GlobalConstants.HALO_WAYPOINT_USER_AGENT);
        }

        // ================================================
        // ContentHacs
        // ================================================

        /// <summary>
        /// Gets information about the active season pass.
        /// </summary>
        /// <include file='../APIDocsExamples/Halo5/ContentHacs_GetActiveSeasonPass.xml' path='//example'/>
        /// <returns>If successful, returns an instance of <see cref="SeasonPassGameContent"/> containing season pass information. Otherwise, returns a null object with an error container attached.</returns>
        public async Task<HaloApiResultContainer<SeasonPassGameContent, HaloApiErrorContainer>> ContentHacsGetActiveSeasonPass()
        {
            return await this.ExecuteAPIRequest<SeasonPassGameContent>(
                $"https://{HaloCoreEndpoints.ContentHacsOrigin}.{HaloCoreEndpoints.ServiceDomain}/content/guid-3bb4a66ff4514d5b8466a3f82918720a",
                HttpMethod.Get,
                true,
                false,
                GlobalConstants.HALO_WAYPOINT_USER_AGENT);
        }

        /// <summary>
        /// Gets information about the Spartan rank breakdown in-game.
        /// </summary>
        /// <include file='../APIDocsExamples/Halo5/ContentHacs_GetActiveSpartanRankManifest.xml' path='//example'/>
        /// <returns>If successful, returns an instance of <see cref="SpartanRankManifest"/> containing spartan rank information. Otherwise, returns a null object with an error container attached.</returns>
        public async Task<HaloApiResultContainer<SpartanRankManifest, HaloApiErrorContainer>> ContentHacsGetActiveSpartanRankManifest()
        {
            return await this.ExecuteAPIRequest<SpartanRankManifest>(
                $"https://{HaloCoreEndpoints.ContentHacsOrigin}.{HaloCoreEndpoints.ServiceDomain}/content/guid-9d9fdd5cbd5e4540a24fa2caa59ebd8c",
                HttpMethod.Get,
                true,
                false,
                GlobalConstants.HALO_WAYPOINT_USER_AGENT);
        }

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetArmorModContent(string startAt, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/armormod?startat={startAt}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetArmorSuitContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/armorsuit?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetAssassinationContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/assassination?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetCommendationLevelContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/commendationlevel?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetContentByIdentity(string identity)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/content/guid-{identity}",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetDeathFxContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/deathfx?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetEmblemContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/emblem?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetForgeLabelContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/forgelabel?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetForgeMapTutorialMapVariant()
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/content/guid-d7315b8903454c7a9d2421e4f23f1c1f",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetGameBaseVariantContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/gamebasevariant?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetHelmetContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/helmet?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetMetaCommendationContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/metacommendation?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetMinimumRequiredVersion()
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/content/guid-44424028cfeb4b178c9ac1fdde08e4e3",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetNetworkConfiguration()
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/content/guid-9e663a5fbc3b4d9f92ceab2bcb210909",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetProgressiveCommendationContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/progressivecommendation?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetReqCategoryContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/reqcategory?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetRewardSetContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/rewardset?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetSeasonContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/season?startat={startat}&count={count}&orderby=publisheddate&client=h5pc",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetStanceContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/stance?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetStatsGlobalSettingsContent()
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/statsglobalsettings?startat=0&count=1&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetUGCCannedTags(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/UGCCannedTag?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetVisorContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/visor?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetVoiceOverContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/voiceover?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetWeaponSkinContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/weaponskin?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsGetWeaponSkinWeaponContent(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/weaponskinweapon?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsLobbyHopperErrorMessages()
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/content/guid-4ddc5d424888497c972f7caa478e9750",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsMessageOfTheDayCarousel()
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/content/guid-400513ab41344903a651774e470372ed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsNotAllowedInTitleMessage()
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/content/guid-d62eeddd9d3b4347b3ab37bc66881bbd",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsReqDeployables(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/REQDeployable?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsReqDisplayProperties(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/REQDisplayProperties?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsReqPacks(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/REQPack?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ContentHacsReqs(string startat, string count)
        //{
        //    var response = await ExecuteAPIRequest($"https://content-hacs.svc.halowaypoint.com:443/contents/REQ?startat={startat}&count={count}&orderby=fixed",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////================================================
        //// Lobby
        ////================================================
        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> LobbyGameConnection()
        //{
        //    var response = await ExecuteAPIRequest($"https://lobby.svc.halowaypoint.com:443/",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> LobbyJoinLobby(string lobbyId, string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://lobby.svc.halowaypoint.com:443/h5pc/lobbies/{lobbyId}/players/{player}?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> LobbyLobbyConnection()
        //{
        //    var response = await ExecuteAPIRequest($"https://lobby.svc.halowaypoint.com:443/",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> LobbyPlayerRosterUpdate(string lobbyId)
        //{
        //    var response = await ExecuteAPIRequest($"https://lobby.svc.halowaypoint.com:443/h5pc/lobbies/{lobbyId}",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> LobbyRegisterJoinLobbyHandle(string handleId, string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://lobby.svc.halowaypoint.com:443/h5pc/handles/{handleId}/players/{player}?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////================================================
        //// Profile
        ////================================================
        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ProfileBatchProfile(string players)
        //{
        //    var response = await ExecuteAPIRequest($"https://haloplayer.svc.halowaypoint.com:443/h5/profiles?players={players}",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ProfilePlayerProfileAppearance(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://haloplayer.svc.halowaypoint.com:443/h5/profiles/{player}/appearance",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ProfilePlayerProfileCampaign(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://haloplayer.svc.halowaypoint.com:443/h5/profiles/{player}/campaign",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ProfilePlayerProfileControls(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://haloplayer.svc.halowaypoint.com:443/h5/profiles/{player}/controls",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ProfilePlayerProfileInventory(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://haloplayer.svc.halowaypoint.com:443/h5/profiles/{player}/inventory",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> ProfilePlayerProfilePreferences(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://haloplayer.svc.halowaypoint.com:443/h5/profiles/{player}/preferences",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////================================================
        //// Stats
        ////================================================
        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> StatsBanSummary(string targetlist)
        //{
        //    var response = await ExecuteAPIRequest($"https://banprocessor.svc.halowaypoint.com:443/h5/bansummary?auth=st&targets={targetlist}",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> StatsExitExperience(string mode, string matchId)
        //{
        //    var response = await ExecuteAPIRequest($"https://spartanstats.svc.halowaypoint.com:443/h5pc/{mode}/matches/{matchId}?include-xuids=true&auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> StatsGetCredits(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://spartanstats.svc.halowaypoint.com:443/h5/players/{player}/credits?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> StatsGetSpartanCompany(string companyid)
        //{
        //    var response = await ExecuteAPIRequest($"https://spartanstats.svc.halowaypoint.com:443/oban/companies/{companyid}?auth=st&include-xuids=true",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> StatsPlayerCommendations(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://spartanstats.svc.halowaypoint.com:443/h5/players/{player}/commendations?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> StatsPlayerMatchHistory(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://spartanstats.svc.halowaypoint.com:443/h5pc/players/{player}/matches?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> StatsPlayerPresence(string players)
        //{
        //    var response = await ExecuteAPIRequest($"https://spartanstats.svc.halowaypoint.com:443/h5/presence?players={players}",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> StatsUserServiceRecord(string mode, string players)
        //{
        //    var response = await ExecuteAPIRequest($"https://spartanstats.svc.halowaypoint.com:443/h5/servicerecords/{mode}?players={players}&auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////================================================
        //// Ugc
        ////================================================
        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcFilmChunkDownload(string FilmGUID, string ChunkIndex)
        //{
        //    var response = await ExecuteAPIRequest($"https://s3ugc.blob.core.windows.net:443{FilmGUID}/filmChunk{ChunkIndex}.chunk",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcFilmItem(string film)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/films/{film}?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcFilmManifest(string film)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/films/{film}?view=film-manifest&auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerBookmarkItem(string player, string bookmark)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/bookmarks/{bookmark}?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerBookmarksCollection(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/bookmarks?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerFilesBatch()
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/playerfiles?auth=st&include-xuids=true",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerForgeGroupItem(string player, string group)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/forgegroups/{group}?auth=st&include-xuids=true",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerForgeGroupsCollection(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/forgegroups?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerGameVariantItem(string player, string variant)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/gamevariants/{variant}?auth=st&include-xuids=true",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerGameVariantsCollection(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/gamevariants?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerItemCopy(string player, string collection, string item)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/{collection}/{item}/actions/copy?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerItemLikes(string player, string collection, string item)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/{collection}/{item}/actions/like?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerItemOffensiveReports(string player, string collection, string item)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/{collection}/{item}/actions/report?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerMapVariantItem(string player, string variant)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/mapvariants/{variant}?auth=st&include-xuids=true",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerMapVariantsCollection(string player)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/mapvariants?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerScreenshotAdd(string player, string collection, string variant)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/{collection}/{variant}/screenshots?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPlayerScreenshotChange(string player, string collection, string variant, string id)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/{collection}/{variant}/screenshots/{id}?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcPostPlayerCopyAction(string player, string collection)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/players/{player}/{collection}/actions/copy?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcSearchForgeGroups()
        //{
        //    var response = await ExecuteAPIRequest($"https://search.svc.halowaypoint.com:443/h5/search/forgegroup?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcSearchGameVariants()
        //{
        //    var response = await ExecuteAPIRequest($"https://search.svc.halowaypoint.com:443/h5/search/gamevariants?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcSearchMapVariants()
        //{
        //    var response = await ExecuteAPIRequest($"https://search.svc.halowaypoint.com:443/h5/search/mapvariants?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcSearchPlayerFiles()
        //{
        //    var response = await ExecuteAPIRequest($"https://search.svc.halowaypoint.com:443/h5/search/playerfiles?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcSearchScreenshots()
        //{
        //    var response = await ExecuteAPIRequest($"https://search.svc.halowaypoint.com:443/h5/search/screenshots?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcServiceItemOffensiveReports(string collection, string item)
        //{
        //    var response = await ExecuteAPIRequest($"https://ugc.svc.halowaypoint.com:443/h5/{collection}/{item}/actions/report?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        ////TODO: This function requires manual invtervention/checks.
        //public async Task<string> UgcSuggestTags()
        //{
        //    var response = await ExecuteAPIRequest($"https://search.svc.halowaypoint.com:443/h5/suggest/ugctags?auth=st",
        //                           HttpMethod.Get,
        //                           false,
        //                           false,
        //                           GlobalConstants.HALO_WAYPOINT_USER_AGENT);

        //    if (!string.IsNullOrEmpty(response))
        //    {
        //        return response;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}
    }
}
