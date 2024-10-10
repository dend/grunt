// <copyright file="OddballStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Statistics for the Oddball game mode.
    /// </summary>
    [IsAutomaticallySerializable]
    public class OddballStats
    {
        /// <summary>
        /// Gets or sets the number of kills as a skull carrier.
        /// </summary>
        public int? KillsAsSkullCarrier { get; set; }

        /// <summary>
        /// Gets or sets the longest time spent as a skull carrier.
        /// </summary>
        public TimeSpan? LongestTimeAsSkullCarrier { get; set; }

        /// <summary>
        /// Gets or sets the number of skull carriers killed.
        /// </summary>
        public int? SkullCarriersKilled { get; set; }

        /// <summary>
        /// Gets or sets the number of skull grabs.
        /// </summary>
        public int? SkullGrabs { get; set; }

        /// <summary>
        /// Gets or sets the time spent as a skull carrier.
        /// </summary>
        public TimeSpan? TimeAsSkullCarrier { get; set; }

        /// <summary>
        /// Gets or sets the number of skull scoring ticks.
        /// </summary>
        public int? SkullScoringTicks { get; set; }
    }
}
