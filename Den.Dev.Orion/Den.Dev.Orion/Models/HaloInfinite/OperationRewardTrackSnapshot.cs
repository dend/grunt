// <copyright file="OperationRewardTrackSnapshot.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Reward track snapshot representing data from an operation.
    /// </summary>
    [IsAutomaticallySerializable]
    public class OperationRewardTrackSnapshot
    {
        /// <summary>
        /// Gets or sets the path to the currently active operation reward track.
        /// </summary>
        public string? ActiveOperationRewardTrackPath { get; set; }

        /// <summary>
        /// Gets or sets the list of operation reward tracks.
        /// </summary>
        public List<RewardTrack>? OperationRewardTracks { get; set; }

        /// <summary>
        /// Gets or sets the path for a scheduled operation reward track.
        /// </summary>
        public string? ScheduledOperationRewardTrackPath { get; set; }
    }
}
