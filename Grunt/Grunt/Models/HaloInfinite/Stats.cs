// <copyright file="Stats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Stats associated with a matchmade game.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Stats
    {
        /// <summary>
        /// Gets or sets core match stats.
        /// </summary>
        public CoreStats? CoreStats { get; set; }

        /// <summary>
        /// Gets or sets Bomb game mode stats.
        /// </summary>
        public BombStats? BombStats { get; set; }

        /// <summary>
        /// Gets or sets Capture The Flag (CTF) game mode stats.
        /// </summary>
        public CaptureTheFlagStats? CaptureTheFlagStats { get; set; }

        /// <summary>
        /// Gets or sets Elimination game mode stats.
        /// </summary>
        public EliminationStats? EliminationStats { get; set; }

        /// <summary>
        /// Gets or sets Extraction game mode stats.
        /// </summary>
        public ExtractionStats? ExtractionStats { get; set; }

        /// <summary>
        /// Gets or sets Infection game mode stats.
        /// </summary>
        public InfectionStats? InfectionStats { get; set; }

        /// <summary>
        /// Gets or sets Oddball game mode stats.
        /// </summary>
        public OddballStats? OddballStats { get; set; }

        /// <summary>
        /// Gets or sets Land Grab game mode stats.
        /// </summary>
        public ZonesStats? ZonesStats { get; set; }

        /// <summary>
        /// Gets or sets Stockpile game mode stats.
        /// </summary>
        public StockpileStats? StockpileStats { get; set; }

        /// <summary>
        /// Gets or sets VIP game mode stats.
        /// </summary>
        public VIPStats? VipStats { get; set; }
    }
}
