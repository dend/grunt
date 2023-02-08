// <copyright file="XboxDisplayClaims.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models
{
    /// <summary>
    /// Container class for Xbox Live API display claims.
    /// </summary>
    public class XboxDisplayClaims
    {
        /// <summary>
        /// Gets or sets Xbox user-related information.
        /// </summary>
        [JsonPropertyName("xui")]
        public XboxXui[]? Xui { get; set; }
    }
}
