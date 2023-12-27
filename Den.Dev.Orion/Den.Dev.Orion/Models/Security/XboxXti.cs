// <copyright file="XboxXti.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace Den.Dev.Orion.Models.Security
{
    /// <summary>
    /// Container class encapsulating the Xbox device information.
    /// </summary>
    public class XboxXti
    {
        /// <summary>
        /// Gets or sets the title ID.
        /// </summary>
        [JsonPropertyName("tid")]
        public string? TID { get; set; }
    }
}
