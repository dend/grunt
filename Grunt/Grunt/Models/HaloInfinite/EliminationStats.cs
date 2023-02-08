// <copyright file="EliminationStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Player statistics associated with the Elimination game mode.
    /// </summary>
    [IsAutomaticallySerializable]
    public class EliminationStats
    {
        /// <summary>
        /// Gets or sets the number of allies revived.
        /// </summary>
        public int AlliesRevived { get; set; }

        /// <summary>
        /// Gets or sets the number of assists.
        /// </summary>
        public int EliminationAssists { get; set; }

        /// <summary>
        /// Gets or sets the number of eliminations.
        /// </summary>
        public int Eliminations { get; set; }

        /// <summary>
        /// Gets or sets the number of enemy revives that were denied.
        /// </summary>
        public int EnemyRevivesDenied { get; set; }

        /// <summary>
        /// Gets or sets the number of executions.
        /// </summary>
        public int Executions { get; set; }

        /// <summary>
        /// Gets or sets the number of kills as last player standing.
        /// </summary>
        public int KillsAsLastPlayerStanding { get; set; }

        /// <summary>
        /// Gets or sets the number of last players standing killed.
        /// </summary>
        public int LastPlayersStandingKilled { get; set; }

        /// <summary>
        /// Gets or sets the number of rounds survived.
        /// </summary>
        public int RoundsSurvived { get; set; }

        /// <summary>
        /// Gets or sets the number of times being revived by an ally.
        /// </summary>
        public int TimesRevivedByAlly { get; set; }

        /// <summary>
        /// Gets or sets the number of lives remaining.
        /// </summary>
        public int? LivesRemaining { get; set; }

        /// <summary>
        /// Gets or sets the elimination order.
        /// </summary>
        public int EliminationOrder { get; set; }
    }
}
