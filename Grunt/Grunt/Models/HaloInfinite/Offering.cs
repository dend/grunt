// <copyright file="Offering.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Data related to an in-game offering.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Offering
    {
        /// <summary>
        /// Gets or sets the offering ID.
        /// </summary>
        public string? OfferingId { get; set; }

        /// <summary>
        /// Gets or sets the offering display path.
        /// </summary>
        public string? OfferingDisplayPath { get; set; }

        /// <summary>
        /// Gets or sets the offering expiration date.
        /// </summary>
        public APIFormattedDate? OfferingExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the list of items included in the offering.
        /// </summary>
        public List<PlayerItem>? IncludedItems { get; set; }

        /// <summary>
        /// Gets or sets the list of prices associated with the offering.
        /// </summary>
        public List<Price>? Prices { get; set; }

        /// <summary>
        /// Gets or sets the list of currencies included in the offering.
        /// </summary>
        public List<CurrencyAmount>? IncludedCurrencies { get; set; }

        /// <summary>
        /// Gets or sets the list of reward tracks included in the offering.
        /// </summary>
        public List<string>? IncludedRewardTracks { get; set; }

        /// <summary>
        /// Gets or sets the path to the boost.
        /// </summary>
        public string? BoostPath { get; set; }

        /// <summary>
        /// Gets or sets the value of experience earned through the operation.
        /// </summary>
        public int OperationXp { get; set; }

        /// <summary>
        /// Gets or sets the value of experience earned through the event.
        /// </summary>
        public int EventXp { get; set; }

        /// <summary>
        /// Gets or sets match boosts.
        /// </summary>
        public dynamic? MatchBoosts { get; set; }
    }
}
