// <copyright file="KillDeathStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for kill and death stats in matchmaking.
    /// </summary>
    [IsAutomaticallySerializable]
    public class KillDeathStats
    {
        /// <summary>
        /// Gets or sets the number of kills.
        /// </summary>
        public double? Kills { get; set; }

        /// <summary>
        /// Gets or sets the number of deaths.
        /// </summary>
        public double? Deaths { get; set; }
    }
}
