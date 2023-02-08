// <copyright file="ParticipationInfo.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Game participation info for a player in Halo Infinite.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ParticipationInfo
    {
        /// <summary>
        /// Gets or sets the first joined time.
        /// </summary>
        public DateTime? FirstJoinedTime { get; set; }

        /// <summary>
        /// Gets or sets the last leave time. If the player never left the game, this value will be null.
        /// </summary>
        public DateTime? LastLeaveTime { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the player was present from the beginning of the match.
        /// </summary>
        public bool? PresentAtBeginning { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the player joined a game that was already in progress.
        /// </summary>
        public bool? JoinedInProgress { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the player left a game that was in progress.
        /// </summary>
        public bool? LeftInProgress { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the player was present when the game ended.
        /// </summary>
        public bool? PresentAtCompletion { get; set; }

        /// <summary>
        /// Gets or sets the total time played.
        /// </summary>
        public TimeSpan TimePlayed { get; set; }

        /// <summary>
        /// Gets or sets whether player participation is confirmed.
        /// </summary>
        /// <remarks>
        /// Additional research is needed to determine this value.
        /// </remarks>
        public dynamic? ConfirmedParticipation { get; set; }
    }
}
