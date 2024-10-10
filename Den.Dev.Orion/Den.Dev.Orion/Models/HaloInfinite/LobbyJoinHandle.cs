// <copyright file="LobbyJoinHandle.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Join handle for Halo Infinite lobbies.
    /// </summary>
    [IsAutomaticallySerializable]
    public class LobbyJoinHandle
    {
        /// <summary>
        /// Gets or sets the join handle string.
        /// </summary>
        public string? JoinHandle { get; set; }
    }
}
