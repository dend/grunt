// <copyright file="AsyncComputeOverrides.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for async compute overrides.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AsyncComputeOverrides
    {
        /// <summary>
        /// Gets or sets overrides for Nvidia.
        /// </summary>
        public Dictionary<string, bool>? Nvidia { get; set; }

        /// <summary>
        /// Gets or sets overrides for AMD.
        /// </summary>
        public Dictionary<string, bool>? AMD { get; set; }

        /// <summary>
        /// Gets or sets overrides for Intel.
        /// </summary>
        public Dictionary<string, bool>? Intel { get; set; }
    }
}
