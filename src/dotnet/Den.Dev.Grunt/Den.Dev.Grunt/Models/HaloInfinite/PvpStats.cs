// <copyright file="PvpStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for Player-vs-Player (PvP) stats.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PvpStats
    {
        /// <summary>
        /// Gets or sets the number of PvP assists.
        /// </summary>
        public int? Assists { get; set; }

        /// <summary>
        /// Gets or sets the number of PvP deaths.
        /// </summary>
        public int? Deaths { get; set; }

        /// <summary>
        /// Gets or sets the PvP KDA.
        /// </summary>
        public float? KDA { get; set; }
        
        /// <summary>
        /// Gets or sets the number of PvP kills.
        /// </summary>
        public int? Kills { get; set; }
    }
}
