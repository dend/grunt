// <copyright file="PlayerState.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Snapshot of a player state.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerState
    {
        /// <summary>
        /// Gets or sets the list of assigned reward tracks.
        /// </summary>
        public List<RewardTrack>? RewardTracks { get; set; }

        /// <summary>
        /// Gets or sets the list of item balances.
        /// </summary>
        public List<dynamic>? ItemBalances { get; set; }

        /// <summary>
        /// Gets or sets the list of currency balances.
        /// </summary>
        public List<CurrencyAmount>? CurrencyBalances { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a state refresh is needed.
        /// </summary>
        public bool? RefreshNeeded { get; set; }

        /// <summary>
        /// Gets or sets the list of available boosts.
        /// </summary>
        public List<dynamic>? Boosts { get; set; }
    }
}
