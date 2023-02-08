// <copyright file="Emblem.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Player emblem.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Emblem
    {
        /// <summary>
        /// Gets or sets the emblem path.
        /// </summary>
        public string? EmblemPath { get; set; }

        /// <summary>
        /// Gets or sets the configuration ID.
        /// </summary>
        public int ConfigurationId { get; set; }
    }
}
