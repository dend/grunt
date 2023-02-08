// <copyright file="WaypointEndpoints.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using static System.Net.WebRequestMethods;

namespace OpenSpartan.Grunt.Endpoints
{
    /// <summary>
    /// Container class for Halo Waypoint service API endpoints. This is distinct from the Halo APIs, since
    /// it works through the web interface or the app and is often not exposed through the game.
    /// </summary>
    public class WaypointEndpoints
    {
        // ====================
        // Origins
        // ====================

        /// <summary>
        /// Origin responsible for APIs for code redemption.
        /// </summary>
        internal static readonly string VoucherEndpoint = "voucher";

        /// <summary>
        /// Origin responsible for APIs for profile information.
        /// </summary>
        internal static readonly string ProfileEndpoint = "profile";

        /// <summary>
        /// Origin responsible for content published on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        internal static readonly string WPContentEndpoint = "wpcontent";

        // ====================
        // Service domains
        // ====================

        /// <summary>
        /// Halo Waypoint service domain used for all Halo API calls.
        /// </summary>
        internal static readonly string ServiceDomain = "svc.halowaypoint.com";
    }
}
