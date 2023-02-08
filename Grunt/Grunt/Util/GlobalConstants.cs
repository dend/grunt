// <copyright file="GlobalConstants.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Util
{
    /// <summary>
    /// Constants that do not change within the application.
    /// </summary>
    internal class GlobalConstants
    {
        /// <summary>
        /// User agent for the web-based requests to the Halo Waypoint API.
        /// </summary>
        internal static readonly string WEB_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 Edg/105.0.1343.27";

        /// <summary>
        /// Halo Waypoint user agent used for outbound HTTP API requests.
        /// </summary>
        internal static readonly string HALO_WAYPOINT_USER_AGENT = "HaloWaypoint/2021112313511900 CFNetwork/1327.0.4 Darwin/21.2.0";

        /// <summary>
        /// Halo Infinite PC game user agent used for outbound HTTP API requests.
        /// </summary>
        internal static readonly string HALO_PC_USER_AGENT = "SHIVA-2043073184/6.10021.18539.0 (release; PC)";

        /// <summary>
        /// Default scopes used for the Xbox Live authentication.
        /// </summary>
        internal static readonly string[] DEFAULT_AUTH_SCOPES = new string[] { "Xboxlive.signin", "Xboxlive.offline_access" };
    }
}
