// <copyright file="CurrencySnapshot.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Snapshot for an in-game currency.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CurrencySnapshot
    {
        /// <summary>
        /// Gets or sets the list of currency amounts.
        /// </summary>
        public List<CurrencyAmount>? Currencies { get; set; }
    }
}
