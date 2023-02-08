// <copyright file="InventoryAmount.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Representation of in-game consumable items within a player inventory.
    /// </summary>
    [IsAutomaticallySerializable]
    public class InventoryAmount
    {
        /// <summary>
        /// Gets or sets the inventory item path.
        /// </summary>
        public string? InventoryItemPath { get; set; }

        /// <summary>
        /// Gets or sets the amount of consumable items.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the consumable type.
        /// </summary>
        public string? Type { get; set; }
    }
}
