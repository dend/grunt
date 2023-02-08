// <copyright file="DriverDetails.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Driver detail configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class DriverDetails
    {
        /// <summary>
        /// Gets or sets the minimum driver version.
        /// </summary>
        public string? Minimum { get; set; }

        /// <summary>
        /// Gets or sets the driver download link.
        /// </summary>
        public string? DownloadLink { get; set; }

        /// <summary>
        /// Gets or sets the list of blocklisted drivers.
        /// </summary>
        public List<dynamic>? Blocklist { get; set; }
    }
}
