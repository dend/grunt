// <copyright file="ClientConfiguration.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models
{
    /// <summary>
    /// Configuration for the Azure Active Directory client.
    /// </summary>
    public class ClientConfiguration
    {
        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL for the client.
        /// </summary>
        [JsonPropertyName("redirect_url")]
        public string? RedirectUrl { get; set; }
    }
}
