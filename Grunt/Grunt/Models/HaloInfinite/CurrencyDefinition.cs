// <copyright file="CurrencyDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Definition of an in-game currency.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CurrencyDefinition
    {
        /// <summary>
        /// Gets or sets the currency ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the initial currency balance.
        /// </summary>
        public int InitialBalanceAmount { get; set; }

        /// <summary>
        /// Gets or sets the list of Microsoft Store products associated with the currency.
        /// </summary>
        public List<StoreProduct>? MSStoreProducts { get; set; }

        /// <summary>
        /// Gets or sets the list of Steam Store products associated with the currency.
        /// </summary>
        public List<StoreProduct>? SteamStoreProducts { get; set; }

        /// <summary>
        /// Gets or sets the Microsoft Store inventory for the currency.
        /// </summary>
        public MicrosoftStoreInventory? MicrosoftStore { get; set; }

        /// <summary>
        /// Gets or sets the Steam Store inventory for the currency.
        /// </summary>
        public SteamStoreInventory? SteamInventory { get; set; }
    }
}
