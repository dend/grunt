// <copyright file="MatchProgression.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for match progression information.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MatchProgression
    {
        /// <summary>
        /// Gets or sets the clearance ID.
        /// </summary>
        public string? ClearanceId { get; set; }

        /// <summary>
        /// Gets or sets the reward ID.
        /// </summary>
        public string? RewardId { get; set; }

        /// <summary>
        /// Gets or sets the challenge progress state after the match.
        /// </summary>
        public List<ChallengeProgressState>? ChallengeProgressState { get; set; }
    }
}
