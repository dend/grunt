// <copyright file="RewardTrack.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Information related to a Halo Infinite reward track.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardTrack
    {
        /// <summary>
        /// Gets or sets the path to the reward track.
        /// </summary>
        public string? RewardTrackPath { get; set; }

        /// <summary>
        /// Gets or sets the reward track type.
        /// </summary>
        public string? TrackType { get; set; }

        /// <summary>
        /// Gets or sets the current progress for the reward track.
        /// </summary>
        public RewardTrackProgress? CurrentProgress { get; set; }

        /// <summary>
        /// Gets or sets the previous progress for the reward track.
        /// </summary>
        public RewardTrackProgress? PreviousProgress { get; set; }

        /// <summary>
        /// Gets or sets whether the player owns the reward track.
        /// </summary>
        public bool? IsOwned { get; set; }

        /// <summary>
        /// Gets or sets the base amount of experience (XP) awarded with the reward track.
        /// </summary>
        public int? BaseXp { get; set; }

        /// <summary>
        /// Gets or sets the boost experience (XP) amount awarded with the reward track.
        /// </summary>
        public int? BoostXp { get; set; }
    }
}
