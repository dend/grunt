// <copyright file="PerformanceValue.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Player performance container.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PerformanceValue
    {
        /// <summary>
        /// Gets or sets the true value.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the expected value.
        /// </summary>
        public double? Expected { get; set; }

        /// <summary>
        /// Gets or sets the standard deviation.
        /// </summary>
        public double? StdDev { get; set; }
    }
}
