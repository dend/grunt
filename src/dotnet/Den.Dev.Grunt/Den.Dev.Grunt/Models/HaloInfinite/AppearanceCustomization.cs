// <copyright file="AppearanceCustomization.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Appearance customization options for a player.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AppearanceCustomization
    {
        /// <summary>
        /// Gets or sets the appearance status.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the appearance configuration.
        /// </summary>
        public Appearance? Appearance { get; set; }
    }
}
