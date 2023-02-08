// <copyright file="CPUPreset.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// CPU preset configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CPUPreset
    {
        /// <summary>
        /// Gets or sets the number of physical CPU cores.
        /// </summary>
        public int PhysicalCores { get; set; }

        /// <summary>
        /// Gets or sets the number of logical CPU cores.
        /// </summary>
        public int LogicalCores { get; set; }

        /// <summary>
        /// Gets or sets the L3 cache size in MB.
        /// </summary>
        public int L3CacheSizeMB { get; set; }

        /// <summary>
        /// Gets or sets the RAM size in GB.
        /// </summary>
        public int RAMSizeGB { get; set; }

        /// <summary>
        /// Gets or sets the base frequency in MHz.
        /// </summary>
        public int BaseFreqMHz { get; set; }
    }
}
