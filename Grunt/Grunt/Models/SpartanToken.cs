// <copyright file="SpartanToken.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models
{
    /// <summary>
    /// Model containing the information about an issued Spartan V4 token.
    /// </summary>
    public class SpartanToken
    {
        /// <summary>
        /// Gets or sets the Spartan token.
        /// </summary>
        [JsonPropertyName("SpartanToken")]
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the token expiration date.
        /// </summary>
        public APIFormattedDate? ExpiresUtc { get; set; }

        /// <summary>
        /// Gets or sets the token validity duration.
        /// </summary>
        public string? TokenDuration { get; set; }
    }
}
