// <copyright file="RewardTrackResultContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Class containing the results of a reward track query.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardTrackResultContainer
    {
        /// <summary>
        /// Gets or sets the list of reward tracks.
        /// </summary>
        public List<RewardTrackResult>? RewardTracks { get; set; }
    }
}
