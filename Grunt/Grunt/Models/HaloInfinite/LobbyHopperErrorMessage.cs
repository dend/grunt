// <copyright file="LobbyHopperErrorMessage.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Error messages associated with the lobby service.
    /// </summary>
    [IsAutomaticallySerializable]
    public class LobbyHopperErrorMessage
    {
        /// <summary>
        /// Gets or sets the game start error message.
        /// </summary>
        public string? GameStartError { get; set; }

        /// <summary>
        /// Gets or sets the game start error message ID.
        /// </summary>
        public int GameStartErrorId { get; set; }

        /// <summary>
        /// Gets or sets the error string. Includes translated strings.
        /// </summary>
        public DisplayString? DisplayString { get; set; }
    }
}
