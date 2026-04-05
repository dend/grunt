// <copyright file="BanProcessorModule.cs" company="Den Delimarsky">
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
    /// Module for ban processor related API operations.
    /// </summary>
    public sealed class BanProcessorModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BanProcessorModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal BanProcessorModule(ClientBase client)
            : base(client, HaloCoreEndpoints.BanProcessorOrigin)
        {
        }

        /// <summary>
        /// Gets the summary information for applicable bans to players and devices.
        /// </summary>
        /// <remarks>
        /// In the query result the entity will include a link to self. The authority ID ("spartanstats") there is incorrect, as the ban summary needs to be obtained from the "banprocessor" authority.
        /// In some quick tests, it seems that including Authenticated(Device) in the request results in 401 Unauthorized if called outside the game. Additional work might be required to understand how to validate the device.
        /// </remarks>
        /// <include file='../../../APIDocsExamples/HaloInfinite/BanProcessor_BanSummary.xml' path='example'/>
        /// <param name="targetList">A list of targets that need to be checked. Authenticated devices can be included as "Authenticated(Device)". Individual players can be specified as "xuid(XUID_VALUE)".</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>An instance of BanSummary containing applicable ban information if request was successful. Return value is null otherwise.</returns>
        public Task<HaloApiResultContainer<BansSummaryQueryResult, RawResponseContainer>> GetBanSummaryAsync(List<string> targetList, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(targetList);

            var formattedTargetList = string.Join(",", targetList);
            return this.GetAsync<BansSummaryQueryResult>(
                $"/hi/bansummary?auth=st&targets={formattedTargetList}",
                cancellationToken: cancellationToken);
        }
    }
}
