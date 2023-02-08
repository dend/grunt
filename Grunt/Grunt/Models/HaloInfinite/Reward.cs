// <copyright file="Reward.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Reward given to a player.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Reward
    {
        /// <summary>
        /// Gets or sets event-related experience awarded.
        /// </summary>
        public int EventXp { get; set; }

        /// <summary>
        /// Gets or sets operation experience awarded.
        /// </summary>
        public int OperationXp { get; set; }

        /// <summary>
        /// Gets or sets operation experience awarded.
        /// </summary>
        /// <remarks>
        /// There is a difference in property used between calls to get awarded rewards and calls to get challenge information. The latter uses this property.
        /// </remarks>
        public int OperationExperience { get; set; }

        /// <summary>
        /// Gets or sets the list of awarded inventory rewards.
        /// </summary>
        public List<InventoryAmount>? InventoryRewards { get; set; }

        /// <summary>
        /// Gets or sets the list of inventory items.
        /// </summary>
        /// <remarks>
        /// Additional research is required to figure out what this data model encompasses. Used while getting list of awarded rewards.
        /// </remarks>
        public List<dynamic>? InventoryItems { get; set; }

        /// <summary>
        /// Gets or sets the tracking ID.
        /// </summary>
        public string? TrackingId { get; set; }

        /// <summary>
        /// Gets or sets the list of currencies awarded.
        /// </summary>
        /// <remarks>
        /// Additional research is required to figure out what this data model encompasses. Used while getting list of awarded rewards.
        /// </remarks>
        public List<dynamic>? Currencies { get; set; }

        /// <summary>
        /// Gets or sets information regarding reward track progression.
        /// </summary>
        /// <remarks>
        /// I am making an assumption here that still needs to be validated that this is truly RewardTrack and not
        /// something completely different.
        /// </remarks>
        public List<RewardTrack>? RewardTrackProgression { get; set; }
    }
}
