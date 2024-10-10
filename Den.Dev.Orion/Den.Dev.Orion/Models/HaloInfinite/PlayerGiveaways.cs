// <copyright file="PlayerGiveaways.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// In-game giveaways.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerGiveaways
    {
        /// <summary>
        /// Gets or sets the list of in-game giveaways.
        /// </summary>
        /// <remarks>
        /// Additional research is needed to understand the underlying data model.
        /// </remarks>
        public List<dynamic>? GiveawayResults { get; set; }
    }
}
