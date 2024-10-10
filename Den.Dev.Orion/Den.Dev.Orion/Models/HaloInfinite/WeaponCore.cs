// <copyright file="WeaponCore.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Weapon core.
    /// </summary>
    [IsAutomaticallySerializable]
    public class WeaponCore : Foundation.CoreBase
    {
        /// <summary>
        /// Gets or sets the list of associated weapon core themes.
        /// </summary>
        public List<WeaponCoreTheme>? Themes { get; set; }
    }
}
