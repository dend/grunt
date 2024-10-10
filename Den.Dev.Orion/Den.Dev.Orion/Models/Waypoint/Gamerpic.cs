// <copyright file="Gamerpic.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.Waypoint
{
    /// <summary>
    /// Configuration for a user's Xbox Live gamerpic.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Gamerpic
    {
        /// <summary>
        /// Gets or sets the URL to the small gamerpic.
        /// </summary>
        public string? Small { get; set; }

        /// <summary>
        /// Gets or sets the URL to the medium gamerpic.
        /// </summary>
        public string? Medium { get; set; }

        /// <summary>
        /// Gets or sets the URL to the large gamerpic.
        /// </summary>
        public string? Large { get; set; }

        /// <summary>
        /// Gets or sets the URL to the extra large gamerpic.
        /// </summary>
        public string? XLarge { get; set; }
    }
}
