// <copyright file="BotCustomizationData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Information related to bot customizations.
    /// </summary>
    [IsAutomaticallySerializable]
    public class BotCustomizationData
    {
        /// <summary>
        /// Gets or sets the list of outfits associated with a bot.
        /// </summary>
        public List<Outfit>? Outfits { get; set; }
    }
}
