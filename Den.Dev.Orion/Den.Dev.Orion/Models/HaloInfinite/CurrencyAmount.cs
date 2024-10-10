// <copyright file="CurrencyAmount.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Snapshot for currency amounts.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CurrencyAmount
    {
        /// <summary>
        /// Gets or sets the currency amount.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the path to the currency.
        /// </summary>
        public string? CurrencyPath { get; set; }

        /// <summary>
        /// Gets or sets the currency source.
        /// </summary>
        public string? Source { get; set; }
    }
}
