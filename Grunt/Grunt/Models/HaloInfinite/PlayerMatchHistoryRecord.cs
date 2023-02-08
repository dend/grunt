// <copyright file="PlayerMatchHistoryRecord.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Match history record for a Halo Infinite player.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerMatchHistoryRecord
    {
        /// <summary>
        /// Gets or sets the match ID.
        /// </summary>
        public Guid MatchId { get; set; }

        /// <summary>
        /// Gets or sets match metadata.
        /// </summary>
        public MatchInfo? MatchInfo { get; set; }

        /// <summary>
        /// Gets or sets the last team ID.
        /// </summary>
        public int LastTeamId { get; set; }

        /// <summary>
        /// Gets or sets the match outcome.
        /// </summary>
        public Outcome Outcome { get; set; }

        /// <summary>
        /// Gets or sets the player rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the player was present at the end of the match.
        /// </summary>
        public bool? PresentAtEndOfMatch { get; set; }
    }
}
