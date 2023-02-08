// <copyright file="PlayerInventory.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for the player inventory.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerInventory
    {
        /// <summary>
        /// Gets or sets the list of items a player has in the inventory.
        /// </summary>
        public List<PlayerItem>? Items { get; set; }
    }
}
