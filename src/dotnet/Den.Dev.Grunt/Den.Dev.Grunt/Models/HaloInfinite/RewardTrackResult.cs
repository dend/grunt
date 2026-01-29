// <copyright file="RewardTrackResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Class containing the reward track result.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardTrackResult
    {
        /// <summary>
        /// Gets or sets the reward track ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the reward track query result code.
        /// </summary>
        public string? ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the reward track.
        /// </summary>
        public RewardTrack? Result { get; set; }
    }
}
