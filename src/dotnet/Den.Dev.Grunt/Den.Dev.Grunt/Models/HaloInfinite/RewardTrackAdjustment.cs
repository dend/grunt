// <copyright file="RewardTrackAdjustment.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Represents a reward track adjustment for an offering.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardTrackAdjustment
    {
        /// <summary>
        /// Gets or sets the amount of XP granted.
        /// </summary>
        public int GrantedXp { get; set; }

        /// <summary>
        /// Gets or sets the path to the reward track.
        /// </summary>
        public string? RewardTrackPath { get; set; }
    }
}
