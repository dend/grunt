// <copyright file="GuideContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container for in-game guide configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class GuideContainer
    {
        /// <summary>
        /// Gets or sets the value of the Accept header.
        /// </summary>
        public string? Accept { get; set; }

        /// <summary>
        /// Gets or sets the value for the Accept-Encoding header.
        /// </summary>
        public string? AcceptEncoding { get; set; }

        /// <summary>
        /// Gets or sets the value for the Accept-Language header.
        /// </summary>
        public string? AcceptLanguage { get; set; }

        /// <summary>
        /// Gets or sets a list of files returned by the guide request.
        /// </summary>
        public List<File>? Files { get; set; }
    }
}
