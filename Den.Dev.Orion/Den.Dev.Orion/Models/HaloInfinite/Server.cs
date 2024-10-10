// <copyright file="Server.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for multiplayer servers in Halo Infinite.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Server
    {
        /// <summary>
        /// Gets or sets the server region.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Gets or sets the server URL.
        /// </summary>
        public string? ServerUrl { get; set; }
    }
}
