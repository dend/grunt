// <copyright file="Price.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Definition for an item cost.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Price
    {
        /// <summary>
        /// Gets or sets the item cost.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the path to the currency associated with the price.
        /// </summary>
        public string? CurrencyPath { get; set; }
    }
}
