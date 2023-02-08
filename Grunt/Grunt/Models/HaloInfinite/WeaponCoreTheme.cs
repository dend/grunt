// <copyright file="WeaponCoreTheme.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Theme associated with a weapon core.
    /// </summary>
    [IsAutomaticallySerializable]
    public class WeaponCoreTheme : Foundation.ThemeBase
    {
        /// <summary>
        /// Gets or sets the path to the weapon coating file.
        /// </summary>
        public string? CoatingPath { get; set; }

        /// <summary>
        /// Gets or sets the list of associated emblems.
        /// </summary>
        public List<Emblem>? Emblems { get; set; }

        /// <summary>
        /// Gets or sets the big emblem shown on the weapon.
        /// </summary>
        public Emblem? BigEmblem { get; set; }

        /// <summary>
        /// Gets or sets the path to the special effects associated with a weapon kill.
        /// </summary>
        public string? DeathFxPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the ammo counter color configuration.
        /// </summary>
        public string? AmmoCounterColorPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the weapon stat tracker.
        /// </summary>
        public string? StatTrackerPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the charm attached to the weapon.
        /// </summary>
        public string? WeaponCharmPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the alternate geometry region configuration.
        /// </summary>
        public string? AlternateGeometryRegionPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the ammo counter.
        /// </summary>
        public string? AmmoCounterPath { get; set; }
    }
}
