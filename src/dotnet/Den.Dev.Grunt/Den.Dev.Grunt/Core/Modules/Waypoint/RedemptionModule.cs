// <copyright file="RedemptionModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.Waypoint;

namespace Den.Dev.Grunt.Core.Modules.Waypoint
{
    /// <summary>
    /// Module for Halo Waypoint code redemption APIs.
    /// </summary>
    public class RedemptionModule : WaypointModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedemptionModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal RedemptionModule(ClientBase client)
            : base(client, WaypointEndpoints.VoucherEndpoint)
        {
        }

        /// <summary>
        /// Redeems a Halo Waypoint code.
        /// </summary>
        /// <remarks>
        /// The codes redeemable here can be those that are obtained through Xbox Game Pass perks, but can also be outside the scope of that particular program.
        /// </remarks>
        /// <param name="code">Code to be redeemed.</param>
        /// <returns>If call is successful, returns an instance of <see cref="CodeRedemptionResult"/> that contains information about the redeemed code. Otherwise, returns a null object and error details.</returns>
        public async Task<HaloApiResultContainer<CodeRedemptionResult, RawResponseContainer>> RedeemCode(string code)
        {
            RedeemableCode container = new()
            {
                Code = code,
            };

            return await this.PostJsonAsync<CodeRedemptionResult, RedeemableCode>("/users/me/codes", container, useSpartanToken: true);
        }
    }
}
