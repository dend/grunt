// <copyright file="DevicePresetOverrides.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Overrides for device presets.
    /// </summary>
    [IsAutomaticallySerializable]
    public class DevicePresetOverrides
    {
        /// <summary>
        /// Gets or sets the override version.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the NVidia preset.
        /// </summary>
        public NvidiaPreset? Nvidia { get; set; }

        /// <summary>
        /// Gets or sets the AMD preset.
        /// </summary>
        public AMDPreset? AMD { get; set; }

        /// <summary>
        /// Gets or sets the Intel preset.
        /// </summary>
        public IntelPreset? Intel { get; set; }
    }
}
