// <copyright file="StoreProductMapping.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Definition of an in-store game item.
    /// </summary>
    [IsAutomaticallySerializable]
    public class StoreProductMapping
    {
        /// <summary>
        /// Gets or sets the item definition.
        /// </summary>
        public string? ItemDef { get; set; }

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public string? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product stock-keeping unit (SKU).
        /// </summary>
        public string? Sku { get; set; }

        /// <summary>
        /// Gets or sets the available product quantity.
        /// </summary>
        public int VCQuantity { get; set; }
    }
}
