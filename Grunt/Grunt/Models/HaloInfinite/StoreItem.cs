// <copyright file="StoreItem.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Listed store item.
    /// </summary>
    [IsAutomaticallySerializable]
    public class StoreItem
    {
        /// <summary>
        /// Gets or sets the store ID.
        /// </summary>
        public string? StoreId { get; set; }

        /// <summary>
        /// Gets or sets the date when the storefront item will expire.
        /// </summary>
        public APIFormattedDate? StorefrontExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the display path for a storefront item.
        /// </summary>
        public string? StorefrontDisplayPath { get; set; }

        /// <summary>
        /// Gets or sets the list of offerings related to the item.
        /// </summary>
        public List<Offering>? Offerings { get; set; }
    }
}
