// <copyright file="ConfigurationModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.ApiIngress;

namespace Den.Dev.Grunt.Core.Modules.HaloInfinite
{
    /// <summary>
    /// Module for configuration and endpoint discovery APIs.
    /// </summary>
    public class ConfigurationModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal ConfigurationModule(ClientBase client)
            : base(client, HaloCoreEndpoints.SettingsOrigin)
        {
        }

        /// <summary>
        /// Gets the API settings container, which has the full list of available endpoints.
        /// </summary>
        /// <returns>If successful, returns an instance of Configuration that contains the full list of available endpoints. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<Configuration, RawResponseContainer>> GetApiSettingsContainer()
        {
            return await this.GetAsyncFullUrl<Configuration>(
                HaloCoreEndpoints.HaloInfiniteEndpointsEndpoint,
                useClearance: false,
                useSpartanToken: true);
        }
    }
}
