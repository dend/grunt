// <copyright file="CPUPresetSnapshot.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Snapshots for pre-defined CPU presets.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CPUPresetSnapshot
    {
        /// <summary>
        /// Gets or sets the configuration for low game settings.
        /// </summary>
        public CPUPreset? Low { get; set; }

        /// <summary>
        /// Gets or sets the configuration for medium game settings.
        /// </summary>
        public CPUPreset? Medium { get; set; }

        /// <summary>
        /// Gets or sets the configuration for high game settings.
        /// </summary>
        public CPUPreset? High { get; set; }

        /// <summary>
        /// Gets or sets the configuration for ultra game settings.
        /// </summary>
        public CPUPreset? Ultra { get; set; }
    }
}
