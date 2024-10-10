// <copyright file="StockpileStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Stats associated with a Stockpile match.
    /// </summary>
    [IsAutomaticallySerializable]
    public class StockpileStats
    {
        /// <summary>
        /// Gets or sets the number of kills while carrying a power seed.
        /// </summary>
        public int? KillsAsPowerSeedCarrier { get; set; }

        /// <summary>
        /// Gets or sets the number of power seeds deposited at base.
        /// </summary>
        public int? PowerSeedsDeposited { get; set; }

        /// <summary>
        /// Gets or sets the number of power seeds stolen from the opposing team's base.
        /// </summary>
        public int? PowerSeedsStolen { get; set; }

        /// <summary>
        /// Gets or sets the number of power seed carrier kills.
        /// </summary>
        public int? PowerSeedCarriersKilled { get; set; }

        /// <summary>
        /// Gets or sets the amount of time spent carrying a power seed during a match.
        /// </summary>
        public TimeSpan? TimeAsPowerSeedCarrier { get; set; }

        /// <summary>
        /// Gets or sets the amount of time spent driving while carrying a power seed during a match.
        /// </summary>
        public TimeSpan? TimeAsPowerSeedDriver { get; set; }
    }
}
