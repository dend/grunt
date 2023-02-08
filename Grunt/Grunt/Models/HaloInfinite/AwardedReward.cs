// <copyright file="AwardedReward.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Information about an awarded reward.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AwardedReward
    {
        /// <summary>
        /// Gets or sets the awarded reward.
        /// </summary>
        public Reward? Reward { get; set; }

        /// <summary>
        /// Gets or sets the rewarded award status.
        /// </summary>
        public string? Status { get; set; }
    }
}
