// <copyright file="InfectionStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Statistics related to the Infection matchmade games.
    /// </summary>
    [IsAutomaticallySerializable]
    public class InfectionStats
    {
        /// <summary>
        /// Gets or sets the number of humans infected.
        /// </summary>
        public int HumansInfected { get; set; }

        /// <summary>
        /// Gets or sets the number of humans infected as the alpha zombie.
        /// </summary>
        public int HumansInfectedAsAlpha { get; set; }

        /// <summary>
        /// Gets or sets the number of last humans standing infected.
        /// </summary>
        public int LastHumansStandingInfected { get; set; }

        /// <summary>
        /// Gets or sets the number of zombies killed.
        /// </summary>
        public int ZombiesKilled { get; set; }

        /// <summary>
        /// Gets or sets the number of alpha zombies killed.
        /// </summary>
        public int AlphaZombiesKilled { get; set; }

        /// <summary>
        /// Gets or sets the number of kills as the last human standing.
        /// </summary>
        public int KillsAsLastHumanStanding { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds survived as a human.
        /// </summary>
        public int RoundsSurvivedAsHuman { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds survived as the last human standing.
        /// </summary>
        public int RoundsSurvivedAsLastHumanStanding { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds played as the last human standing.
        /// </summary>
        public int RoundsAsLastHumanStanding { get; set; }

        /// <summary>
        /// Gets or sets the time spent as the last human standing.
        /// </summary>
        public TimeSpan? TimeAsLastHumanStanding { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds played as the alpha zombie.
        /// </summary>
        public int RoundsAsAlphaZombie { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds finished as a zombie.
        /// </summary>
        public int RoundsFinishedAsZombie { get; set; }
    }
}
