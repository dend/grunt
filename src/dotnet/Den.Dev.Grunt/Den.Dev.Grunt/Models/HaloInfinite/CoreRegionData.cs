// <copyright file="CoreRegionData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Core region data configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CoreRegionData
    {
        /// <summary>
        /// Gets or sets the base region data.
        /// </summary>
        public List<RegionMetadata>? BaseRegionData { get; set; }

        /// <summary>
        /// Gets or sets the body small overrides.
        /// </summary>
        public List<RegionMetadata>? BodyTypeSmallOverrides { get; set; }

        /// <summary>
        /// Gets or sets the body large overrides.
        /// </summary>
        public List<RegionMetadata>? BodyTypeLargeOverrides { get; set; }

        /// <summary>
        /// Gets or sets the prosthetic left arm overrides.
        /// </summary>
        public RegionOverrides? ProstheticLeftArmOverrides { get; set; }

        /// <summary>
        /// Gets or sets the prosthetic right arm overrides.
        /// </summary>
        public RegionOverrides? ProstheticRightArmOverrides { get; set; }

        /// <summary>
        /// Gets or sets the prosthetic left leg overrides.
        /// </summary>
        public RegionOverrides? ProstheticLeftLegOverrides { get; set; }

        /// <summary>
        /// Gets or sets the prosthetic right leg overrides.
        /// </summary>
        public RegionOverrides? ProstheticRightLegOverrides { get; set; }
    }
}
