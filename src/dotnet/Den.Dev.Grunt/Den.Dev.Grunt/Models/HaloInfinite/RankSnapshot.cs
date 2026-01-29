// <copyright file="RankSnapshot.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for rank snapshots.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RankSnapshot
    {
        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets free rewards for the player.
        /// </summary>
        public RewardContainer? FreeRewards { get; set; }

        /// <summary>
        /// Gets or sets paid rewards for the player.
        /// </summary>
        public RewardContainer? PaidRewards { get; set; }
    }
}
