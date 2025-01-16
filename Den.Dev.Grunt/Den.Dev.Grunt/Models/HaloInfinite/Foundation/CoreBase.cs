// <copyright file="CoreBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite.Foundation
{
    /// <summary>
    /// Base class for core entities.
    /// </summary>
    public abstract class CoreBase
    {
        /// <summary>
        /// Gets or sets the path to the core.
        /// </summary>
        public string? CorePath { get; set; }

        /// <summary>
        /// Gets or sets whether the core is equipped by the requesting player.
        /// </summary>
        public bool? IsEquipped { get; set; }

        /// <summary>
        /// Gets or sets the core ID.
        /// </summary>
        public string? CoreId { get; set; }

        /// <summary>
        /// Gets or sets the core type.
        /// </summary>
        public string? CoreType { get; set; }

        /// <summary>
        /// Gets or sets the date of the core acquisition.
        /// </summary>
        public APIFormattedDate? FirstAcquiredDate { get; set; }
    }
}
