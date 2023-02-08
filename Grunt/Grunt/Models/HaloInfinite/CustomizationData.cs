// <copyright file="CustomizationData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Player customization data.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CustomizationData
    {
        /// <summary>
        /// Gets or sets the Spartan body configuration.
        /// </summary>
        public SpartanBody? SpartanBody { get; set; }

        /// <summary>
        /// Gets or sets the appearance configuration.
        /// </summary>
        public Appearance? Appearance { get; set; }

        /// <summary>
        /// Gets or sets the armor core configuration.
        /// </summary>
        public ArmorCoreCollection? ArmorCores { get; set; }

        /// <summary>
        /// Gets or sets the weapon core configuration.
        /// </summary>
        public WeaponCoreCollection? WeaponCores { get; set; }

        /// <summary>
        /// Gets or sets the AI core configuration.
        /// </summary>
        public AiCoreCollection? AiCores { get; set; }

        /// <summary>
        /// Gets or sets the vehicle core configuration.
        /// </summary>
        public VehicleCoreCollection? VehicleCores { get; set; }
    }
}
