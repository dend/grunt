// <copyright file="LobbyPresenceRequestContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container for a request for lobby presence information.
    /// </summary>
    [IsAutomaticallySerializable]
    public class LobbyPresenceRequestContainer
    {
        /// <summary>
        /// Gets or sets a list of Xbox Live user IDs (XUIDs) to be verified.
        /// </summary>
        public List<long>? Xuids { get; set; }
    }
}
