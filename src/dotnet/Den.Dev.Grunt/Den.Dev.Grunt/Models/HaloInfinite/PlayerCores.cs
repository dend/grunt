// <copyright file="PlayerCores.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for player cores.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerCores
    {
        /// <summary>
        /// Gets or sets a list of cores.
        /// </summary>
        public List<GenericCore>? Cores { get; set; }
    }
}
