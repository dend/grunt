// <copyright file="BanResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Information about bans on a player account.
    /// </summary>
    [IsAutomaticallySerializable]
    public class BanResult
    {
        /// <summary>
        /// Gets or sets the list of bans in effect for a player account.
        /// </summary>
        /// <remarks>
        /// Additional research is needed to understand the data model here.
        /// </remarks>
        public List<dynamic>? BansInEffect { get; set; }
    }
}
