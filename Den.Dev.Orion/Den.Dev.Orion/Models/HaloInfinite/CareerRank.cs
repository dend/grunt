// <copyright file="CareerRank.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Player career rank in Halo Infinite.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CareerRank
    {
        /// <summary>
        /// Gets or sets the rank ID.
        /// </summary>
        public int? Rank { get; set; }

        /// <summary>
        /// Gets or sets the free rewards granted for rank.
        /// </summary>
        public RewardContainer? FreeRewards { get; set; }

        /// <summary>
        /// Gets or sets the paid rewards granted for rank.
        /// </summary>
        public RewardContainer? PaidRewards { get; set; }

        /// <summary>
        /// Gets or sets the experience required for rank.
        /// </summary>
        public int? XpRequiredForRank { get; set; }

        /// <summary>
        /// Gets or sets the rank title.
        /// </summary>
        public DisplayString? RankTitle { get; set; }

        /// <summary>
        /// Gets or sets the rank subtitle.
        /// </summary>
        public DisplayString? RankSubTitle { get; set; }

        /// <summary>
        /// Gets or sets the rank tier.
        /// </summary>
        public DisplayString? RankTier { get; set; }

        /// <summary>
        /// Gets or sets the path to the rank icon.
        /// </summary>
        public string? RankIcon { get; set; }

        /// <summary>
        /// Gets or sets the path to the large rank icon.
        /// </summary>
        public string? RankLargeIcon { get; set; }

        /// <summary>
        /// Gets or sets the rank adornment.
        /// </summary>
        public string? RankAdornmentIcon { get; set; }

        /// <summary>
        /// Gets or sets the tier type.
        /// </summary>
        public string? TierType { get; set; }

        /// <summary>
        /// Gets or sets the rank grade.
        /// </summary>
        public int? RankGrade { get; set; }
    }
}
