// <copyright file="RegionOverrides.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Class for region overrides.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RegionOverrides
    {
        /// <summary>
        /// Gets or sets the full override.
        /// </summary>
        public List<RegionMetadata>? Full { get; set; }

        /// <summary>
        /// Gets or sets half overrides.
        /// </summary>
        public List<RegionMetadata>? Half { get; set; }

        /// <summary>
        /// Gets or sets extremity overrides.
        /// </summary>
        public List<RegionMetadata>? Extremity { get; set; }
    }
}
