// <copyright file="LobbyError.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Enum representing potential lobby join errors.
    /// </summary>
    public enum LobbyError
    {
        /// <summary>
        /// No errors occurred during the lobby bootstrap process.
        /// </summary>
        None = 0,

        /// <summary>
        /// Lobby is recovering and is not bootstrapped. Use provided nonce the bootstrap the lobby.
        /// </summary>
        LobbyRecovering = 12,
    }
}
