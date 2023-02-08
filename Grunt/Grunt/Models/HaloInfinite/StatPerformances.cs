// <copyright file="StatPerformances.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for player-specific stats.
    /// </summary>
    [IsAutomaticallySerializable]
    public class StatPerformances
    {
        /// <summary>
        /// Gets or sets metadata for player kills.
        /// </summary>
        public PerformanceValue? Kills { get; set; }

        /// <summary>
        /// Gets or sets metadata for player deaths.
        /// </summary>
        public PerformanceValue? Deaths { get; set; }
    }
}
