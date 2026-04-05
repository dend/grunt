// <copyright file="IWaypointClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Grunt.Core.Modules.Waypoint;

namespace Den.Dev.Grunt.Core
{
    /// <summary>
    /// Interface for the Halo Waypoint API client.
    /// </summary>
    public interface IWaypointClient
    {
        /// <summary>
        /// Gets the Profile module for user settings and profile APIs.
        /// </summary>
        ProfileModule Profile { get; }

        /// <summary>
        /// Gets the Redemption module for code redemption APIs.
        /// </summary>
        RedemptionModule Redemption { get; }

        /// <summary>
        /// Gets the Content module for article and content APIs.
        /// </summary>
        ContentModule Content { get; }

        /// <summary>
        /// Gets the Comms module for communication and notification APIs.
        /// </summary>
        CommsModule Comms { get; }
    }
}
