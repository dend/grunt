// <copyright file="PlayerServiceRecord.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Details about a player service record.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerServiceRecord
    {
        /// <summary>
        /// Gets or sets the container for all subqueries for the service record.
        /// </summary>
        public SubqueryContainer? Subqueries { get; set; }

        /// <summary>
        /// Gets or sets the total time played.
        /// </summary>
        public TimeSpan TimePlayed { get; set; }

        /// <summary>
        /// Gets or sets the total number of matches completed.
        /// </summary>
        public int MatchesCompleted { get; set; }

        /// <summary>
        /// Gets or sets the total number of wins.
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Gets or sets the total number of losses.
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Gets or sets the total number of ties.
        /// </summary>
        public int Ties { get; set; }

        /// <summary>
        /// Gets or sets core player stats.
        /// </summary>
        public CoreStats? CoreStats { get; set; }

        /// <summary>
        /// Gets or sets stats for the player in the Bomb game mode.
        /// </summary>
        public BombStats? BombStats { get; set; }

        /// <summary>
        /// Gets or sets stats for the player in the Capture The Flag (CTF) game mode.
        /// </summary>
        public CaptureTheFlagStats? CaptureTheFlagStats { get; set; }

        /// <summary>
        /// Gets or sets stats for the player in the Elimination game mode.
        /// </summary>
        public EliminationStats? EliminationStats { get; set; }

        /// <summary>
        /// Gets or sets stats for the player in the Extraction game mode.
        /// </summary>
        public ExtractionStats? ExtractionStats { get; set; }

        /// <summary>
        /// Gets or sets stats for the player in the Infection game mode.
        /// </summary>
        public InfectionStats? InfectionStats { get; set; }

        /// <summary>
        /// Gets or sets stats for the player in the Oddball game mode.
        /// </summary>
        public OddballStats? OddballStats { get; set; }

        /// <summary>
        /// Gets or sets stats for the player in the Land Grab game mode.
        /// </summary>
        public ZonesStats? ZonesStats { get; set; }

        /// <summary>
        /// Gets or sets stats for the player in the Stockpile game mode.
        /// </summary>
        public StockpileStats? StockpileStats { get; set; }
    }
}
