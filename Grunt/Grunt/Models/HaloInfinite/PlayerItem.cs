// <copyright file="PlayerItem.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// In-game item associated with a player.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerItem
    {
        /// <summary>
        /// Gets or sets the amount of items available.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the item ID.
        /// </summary>
        public string? ItemId { get; set; }

        /// <summary>
        /// Gets or sets the item path.
        /// </summary>
        public string? ItemPath { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public string? ItemType { get; set; }

        /// <summary>
        /// Gets or sets the core type.
        /// </summary>
        public string? CoreType { get; set; }

        /// <summary>
        /// Gets or sets the item source.
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// Gets or sets the date of the first item acquisition.
        /// </summary>
        public APIFormattedDate? FirstAcquiredDate { get; set; }
    }
}
