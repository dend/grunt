// <copyright file="RewardSummary.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Information about reward summaries. Usually used for player rewards on completion of challenges or specific event tracks.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardSummary
    {
        /// <summary>
        /// Gets or sets the list of updated reward tracks.
        /// </summary>
        public List<RewardTrack>? UpdatedRewardTracks { get; set; }

        /// <summary>
        /// Gets or sets the list of awarded rewards.
        /// </summary>
        public List<AwardedReward>? AwardedRewards { get; set; }

        /// <summary>
        /// Gets or sets the list of granted currencies.
        /// </summary>
        public List<CurrencyAmount>? GrantedCurrencies { get; set; }

        /// <summary>
        /// Gets or sets the list of granted in-game items.
        /// </summary>
        public List<PlayerItem>? GrantedItems { get; set; }
    }
}
