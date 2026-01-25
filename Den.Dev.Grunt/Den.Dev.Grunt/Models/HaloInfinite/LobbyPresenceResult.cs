// <copyright file="LobbyPresenceResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Result information about a lobby presence query.
    /// </summary>
    [IsAutomaticallySerializable]
    public class LobbyPresenceResult
    {
        /// <summary>
        /// Gets or sets the fireteam details.
        /// </summary>
        public FireTeamDetails? FireteamDetails { get; set; }

        /// <summary>
        /// Gets or sets the match details.
        /// </summary>
        public dynamic? MatchDetails { get; set; }
    }
}
