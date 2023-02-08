// <copyright file="BombStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Statistics related to Bomb matchmade games.
    /// </summary>
    [IsAutomaticallySerializable]
    public class BombStats
    {
        /// <summary>
        /// Gets or sets the number of bomb carriers killed.
        /// </summary>
        public int BombCarriersKilled { get; set; }

        /// <summary>
        /// Gets or sets the number of bomb defusals.
        /// </summary>
        public int BombDefusals { get; set; }

        /// <summary>
        /// Gets or sets the number of bomb defusers killed.
        /// </summary>
        public int BombDefusersKilled { get; set; }

        /// <summary>
        /// Gets or sets the number of bomb detonations.
        /// </summary>
        public int BombDetonations { get; set; }

        /// <summary>
        /// Gets or sets the number of bomb pick-ups.
        /// </summary>
        public int BombPickUps { get; set; }

        /// <summary>
        /// Gets or sets the number of bomb plants.
        /// </summary>
        public int BombPlants { get; set; }

        /// <summary>
        /// Gets or sets the number of bomb returns.
        /// </summary>
        public int BombReturns { get; set; }

        /// <summary>
        /// Gets or sets the number of kills as a bomb carrier.
        /// </summary>
        public int KillsAsBombCarrier { get; set; }

        /// <summary>
        /// Gets or sets the time spent in game as a bomb carrier.
        /// </summary>
        public TimeSpan TimeAsBombCarrier { get; set; }
    }
}
