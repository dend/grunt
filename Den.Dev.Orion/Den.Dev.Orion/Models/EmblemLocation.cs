// <copyright file="EmblemLocation.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using Den.Dev.Orion.Models.HaloInfinite;

namespace Den.Dev.Orion.Models
{
    /// <summary>
    /// Class for emblem location configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class EmblemLocation
    {
        /// <summary>
        /// Gets or sets the location ID.
        /// </summary>
        public string? LocationId { get; set; }
        
        /// <summary>
        /// gets or sets the default emblem option.
        /// </summary>
        public Emblem? DefaultOption { get; set; }
    }
}
