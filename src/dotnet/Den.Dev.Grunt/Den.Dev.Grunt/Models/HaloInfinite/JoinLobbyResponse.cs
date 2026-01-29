// <copyright file="JoinLobbyResponse.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for the response obtained when bootstrapping a new lobby.
    /// </summary>
    [IsAutomaticallySerializable]
    public class JoinLobbyResponse
    {
        /// <summary>
        /// Gets or sets the error triggered when joining the lobby. Can represent success.
        /// </summary>
        [JsonPropertyName("joinLobbyError")]
        public LobbyError? Error { get; set; }

        /// <summary>
        /// Gets or sets the lobby activation nonce used for the bootstrap request.
        /// </summary>
        [JsonPropertyName("lobbyActivationNonce")]
        public int? ActivationNonce { get; set; }
    }
}
