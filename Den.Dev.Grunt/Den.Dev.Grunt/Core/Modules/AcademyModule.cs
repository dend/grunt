// <copyright file="AcademyModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules
{
    /// <summary>
    /// Module for Academy-related API operations including bot customization and drills.
    /// </summary>
    public class AcademyModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AcademyModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal AcademyModule(ClientBase client)
            : base(client, HaloCoreEndpoints.GameCmsOrigin)
        {
        }

        /// <summary>
        /// Get bot customization information.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Academy_GetBotCustomization.xml' path='example'/>
        /// <param name="flightId">ID of the flight/clearance associated with the request.</param>
        /// <returns>If successful, returns an instance of BotCustomizationData that contains bot customization information. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<BotCustomizationData, RawResponseContainer>> GetBotCustomization(string flightId)
        {
            return await this.GetAsync<BotCustomizationData>(
                $"/hi/multiplayer/file/Academy/BotCustomizationData.json?flight={flightId}",
                useClearance: true);
        }

        /// <summary>
        /// Gets the client manifest for the Academy.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Academy_GetContent.xml' path='example'/>
        /// <returns>If successful, returns an instance of AcademyClientManifest that contains the definition of drills available in the Academy. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AcademyClientManifest, RawResponseContainer>> GetContent()
        {
            return await this.GetAsync<AcademyClientManifest>(
                "/hi/multiplayer/file/Academy/AcademyClientManifest.json",
                useClearance: true);
        }

        /// <summary>
        /// Gets the client manifest for the Academy. From the endpoint name we can infer that this is test data.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Academy_GetContentTest.xml' path='example'/>
        /// <param name="clearanceId">ID of the flight/clearance associated with the request.</param>
        /// <returns>If successful, returns an instance of TestAcademyClientManifest that contains the definition of drills available in the Academy. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<TestAcademyClientManifest, RawResponseContainer>> GetContentTest(string clearanceId)
        {
            return await this.GetAsync<TestAcademyClientManifest>(
                $"/hi/multiplayer/file/Academy/AcademyClientManifest_Test.json?flight={clearanceId}");
        }

        /// <summary>
        /// Gets definitions for stars awarded in the Academy. This call breaks if a user agent is specified.
        /// </summary>
        /// <include file='../../APIDocsExamples/HaloInfinite/Academy_GetStarDefinitions.xml' path='example'/>
        /// <returns>If successful, returns an instance of AcademyStarDefinitions that contains definitions for stars awarded in the Academy. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<AcademyStarDefinitions, RawResponseContainer>> GetStarDefinitions()
        {
            return await this.GetAsync<AcademyStarDefinitions>(
                "/hi/multiplayer/file/Academy/AcademyStarGUIDDefinitions.json",
                useClearance: true);
        }
    }
}
