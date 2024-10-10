// <copyright file="StoreProduct.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// In-game store product.
    /// </summary>
    [IsAutomaticallySerializable]
    public class StoreProduct
    {
        /// <summary>
        /// Gets or sets the item definition.
        /// </summary>
        public int ItemDef { get; set; }

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public string? ProductId { get; set; }

        /// <summary>
        /// Gets or sets the path to the product.
        /// </summary>
        public string? Path { get; set; }
    }
}
