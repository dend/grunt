// <copyright file="ArmorConfigurationOptionsBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite.Foundation
{
    /// <summary>
    /// Container class for coating options.
    /// </summary>
    [IsAutomaticallySerializable]
    public abstract class ArmorConfigurationOptionsBase
    {
        /// <summary>
        /// Gets or sets whether the coating is required.
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// Gets or sets the default coating option path.
        /// </summary>
        public string? DefaultOptionPath { get; set; }

        /// <summary>
        /// Gets or sets all option paths.
        /// </summary>
        public List<string>? OptionPaths { get; set; }

        /// <summary>
        /// Gets or sets the region metadata.
        /// </summary>
        public List<RegionMetadata>? OffRegionInfo { get; set; }
    }
}
