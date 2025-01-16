// <copyright file="RewardSnapshot.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Snapshot of an in-game reward.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardSnapshot
    {
        /// <summary>
        /// Gets or sets the summary information for the reward snapshot.
        /// </summary>
        public RewardSummary? RewardsSummary { get; set; }

        /// <summary>
        /// Gets or sets the player state when the reward snapshot was activated.
        /// </summary>
        public PlayerState? PlayerState { get; set; }
    }
}
