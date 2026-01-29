// <copyright file="PlayerTeamStat.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Team performance statistics.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerTeamStat
    {
        /// <summary>
        /// Gets or sets the team ID.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the team statistics.
        /// </summary>
        public Stats? Stats { get; set; }
    }
}
