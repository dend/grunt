// <copyright file="WaypointClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Core.Modules.Waypoint;

namespace Den.Dev.Grunt.Core
{
    /// <summary>
    /// Client for interacting with the Halo Waypoint APIs.
    /// </summary>
    public class WaypointClient : ClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointClient"/> class, used to access the Halo Waypoint API.
        /// </summary>
        /// <param name="spartanToken">The Spartan token used to authenticate against the Halo Infinite API.</param>
        /// <param name="xuid">The player identifier in the format "xuid(XUID_VALUE)".</param>
        /// <param name="clearanceToken">ID of the flight/clearance currently active for the player. Optional when first instantiating the client.</param>
        /// <param name="userAgent">Optional User-Agent header value for outbound requests.</param>
        public WaypointClient(string spartanToken, string xuid = "", string clearanceToken = "", string userAgent = "")
        {
            this.SpartanToken = spartanToken;
            this.Xuid = xuid;
            this.ClearanceToken = clearanceToken;
            this.UserAgent = userAgent;

            this.InitializeModules();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointClient"/> class, used to access the Halo Waypoint API.
        /// </summary>
        /// <param name="userAgent">Optional User-Agent header value for outbound requests.</param>
        public WaypointClient(string userAgent = "")
        {
            this.UserAgent = userAgent;

            this.InitializeModules();
        }

        /// <summary>
        /// Gets the Profile module for user settings and profile APIs.
        /// </summary>
        public ProfileModule Profile { get; private set; } = null!;

        /// <summary>
        /// Gets the Redemption module for code redemption APIs.
        /// </summary>
        public RedemptionModule Redemption { get; private set; } = null!;

        /// <summary>
        /// Gets the Content module for article and content APIs.
        /// </summary>
        public ContentModule Content { get; private set; } = null!;

        /// <summary>
        /// Gets the Comms module for communication and notification APIs.
        /// </summary>
        public CommsModule Comms { get; private set; } = null!;

        private void InitializeModules()
        {
            this.Profile = new ProfileModule(this);
            this.Redemption = new RedemptionModule(this);
            this.Content = new ContentModule(this);
            this.Comms = new CommsModule(this);
        }
    }
}
