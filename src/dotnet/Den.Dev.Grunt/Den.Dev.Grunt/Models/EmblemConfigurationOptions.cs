// <copyright file="EmblemConfigurationOptions.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using Den.Dev.Grunt.Models.HaloInfinite.Foundation;

namespace Den.Dev.Grunt.Models
{
    /// <summary>
    /// Class for emblem configuration options.
    /// </summary>
    [IsAutomaticallySerializable]
    public class EmblemConfigurationOptions : ArmorConfigurationOptionsBase
    {
        /// <summary>
        /// Gets or sets the list of available emblem locations.
        /// </summary>
        public List<EmblemLocation>? AvailableLocations { get; set; }
    }
}
