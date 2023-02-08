// <copyright file="VehicleCoreTheme.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Vehicle core theme definition.
    /// </summary>
    [IsAutomaticallySerializable]
    public class VehicleCoreTheme : Foundation.ThemeBase
    {
        /// <summary>
        /// Gets or sets the coating path.
        /// </summary>
        public string? CoatingPath { get; set; }

        /// <summary>
        /// Gets or sets the horn path.
        /// </summary>
        public string? HornPath { get; set; }

        /// <summary>
        /// Gets or sets the vehicle effects path.
        /// </summary>
        public string? VehicleFxPath { get; set; }

        /// <summary>
        /// Gets or sets the vehicle charm path.
        /// </summary>
        public string? VehicleCharmPath { get; set; }

        /// <summary>
        /// Gets or sets the list of associated emblems.
        /// </summary>
        public List<Emblem>? Emblems { get; set; }

        /// <summary>
        /// Gets or sets the alternate geometry region path.
        /// </summary>
        public string? AlternateGeometryRegionPath { get; set; }
    }
}
