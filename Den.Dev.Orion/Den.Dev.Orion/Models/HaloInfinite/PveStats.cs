// <copyright file="PveStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container class for Player-versus-Environment (i.e., Firefight) game mode stats.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PveStats
    {
        /// <summary>
        /// Gets or sets the number of kills.
        /// </summary>
        public int? Kills { get; set; }

        /// <summary>
        /// Gets or sets the number of deaths.
        /// </summary>
        public int? Deaths { get; set; }

        /// <summary>
        /// Gets or sets the number of assists.
        /// </summary>
        public int? Assists { get; set; }

        /// <summary>
        /// Gets or sets the KDA.
        /// </summary>
        public float? KDA { get; set; }

        /// <summary>
        /// Gets or sets the number of marine kills.
        /// </summary>
        public int? MarineKills { get; set; }

        /// <summary>
        /// Gets or sets the number of grunt kills.
        /// </summary>
        public int? GruntKills { get; set; }

        /// <summary>
        /// Gets or sets the number of jackal kills.
        /// </summary>
        public int? JackalKills { get; set; }

        /// <summary>
        /// Gets or sets the number of elite kills.
        /// </summary>
        public int? EliteKills { get; set; }
        
        /// <summary>
        /// Gets or sets the number of brute kills.
        /// </summary>
        public int? BruteKills { get; set; }

        /// <summary>
        /// Gets or sets the number of hunter kills.
        /// </summary>
        public int? HunterKills { get; set; }

        /// <summary>
        /// Gets or sets the number of skimmer kills.
        /// </summary>
        public int? SkimmerKills { get; set; }

        /// <summary>
        /// Gets or sets the number of sentinel kills.
        /// </summary>
        public int? SentinelKills { get; set; }

        /// <summary>
        /// Gets or sets the number of boss kills.
        /// </summary>
        public int? BossKills { get; set; }
    }
}
