// <copyright file="FireTeamDetails.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Details associated with a fireteam.
    /// </summary>
    [IsAutomaticallySerializable]
    public class FireTeamDetails
    {
        /// <summary>
        /// Gets or sets the fireteam ID.
        /// </summary>
        public string? FireteamId { get; set; }

        /// <summary>
        /// Gets or sets the player count in the fireteam.
        /// </summary>
        public int PlayerCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of players in a fireteam.
        /// </summary>
        public int MaxPlayers { get; set; }

        /// <summary>
        /// Gets or sets the joinability status within a fireteam.
        /// </summary>
        public int JoinabilityStatus { get; set; }

        /// <summary>
        /// Gets or sets the playlist reference.
        /// </summary>
        public dynamic? PlaylistRef { get; set; }

        /// <summary>
        /// Gets or sets the activity ID for the fireteam.
        /// </summary>
        public int Activity { get; set; }

        /// <summary>
        /// Gets or sets the fireteam leader menu activity ID.
        /// </summary>
        public int FireteamLeaderMenuActivity { get; set; }
    }
}
