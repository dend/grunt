// <copyright file="Metadata.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Game CMS item metadata.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Metadata
    {
        /// <summary>
        /// Gets or sets a list of associated manufacturers.
        /// </summary>
        public List<Manufacturer>? Manufacturers { get; set; }

        /// <summary>
        /// Gets or sets a list of associated currencies.
        /// </summary>
        public List<Currency>? Currencies { get; set; }
    }
}
