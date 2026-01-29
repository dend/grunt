// <copyright file="RewardTrackProgress.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for reward track progress measurement.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardTrackProgress
    {
        /// <summary>
        /// Gets or sets the reward track rank.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the partial progress for the rank.
        /// </summary>
        public int PartialProgress { get; set; }

        /// <summary>
        /// Gets or sets whether the reward track is owned by the player.
        /// </summary>
        public bool? IsOwned { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player has reached the maximum rank.
        /// </summary>
        public bool? HasReachedMaxRank { get; set; }
    }
}
