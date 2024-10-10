// <copyright file="ThemeBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite.Foundation
{
    /// <summary>
    /// Base class for equipment themes.
    /// </summary>
    public abstract class ThemeBase
    {
        /// <summary>
        /// Gets or sets the first modified date.
        /// </summary>
        public APIFormattedDate? FirstModifiedDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        public APIFormattedDate? LastModifiedDateUtc { get; set; }

        /// <summary>
        /// Gets or sets whether the theme is equipped.
        /// </summary>
        public bool? IsEquipped { get; set; }

        /// <summary>
        /// Gets or sets whether the theme is the default.
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the path to the theme.
        /// </summary>
        public string? ThemePath { get; set; }
    }
}
