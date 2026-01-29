// <copyright file="DriverManifest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Driver manifest configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class DriverManifest
    {
        /// <summary>
        /// Gets or sets the version of the manifest.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets NVidia driver details.
        /// </summary>
        public DriverDetails? Nvidia { get; set; }

        /// <summary>
        /// Gets or sets AMD driver details.
        /// </summary>
        public DriverDetails? AMD { get; set; }

        /// <summary>
        /// Gets or sets Intel driver details.
        /// </summary>
        public DriverDetails? Intel { get; set; }
    }
}
