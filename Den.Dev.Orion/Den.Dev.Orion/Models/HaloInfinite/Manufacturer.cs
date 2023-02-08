// <copyright file="Manufacturer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Gear manufacturer record.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Manufacturer
    {
        /// <summary>
        /// Gets or sets the manufacturer name. Includes translated strings.
        /// </summary>
        public DisplayString? ManufacturerName { get; set; }

        /// <summary>
        /// Gets or sets the path to the manufacturer logo.
        /// </summary>
        public string? ManufacturerLogoImage { get; set; }
    }
}
