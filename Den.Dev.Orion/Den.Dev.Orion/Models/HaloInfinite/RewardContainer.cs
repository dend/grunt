// <copyright file="RewardContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container for reward items awarded to the player.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardContainer
    {
        /// <summary>
        /// Gets or sets the list of inventory rewards associated with an action and/or unlock.
        /// </summary>
        public List<InventoryAmount>? InventoryRewards { get; set; }

        /// <summary>
        /// Gets or sets the list of currency rewards associated with an action and/or unlock.
        /// </summary>
        public List<CurrencyAmount>? CurrencyRewards { get; set; }
    }
}
