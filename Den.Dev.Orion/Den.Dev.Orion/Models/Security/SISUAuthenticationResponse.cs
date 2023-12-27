// <copyright file="SISUAuthenticationResponse.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace Den.Dev.Orion.Models.Security
{
    /// <summary>
    /// Gets or sets the SISU authentication response.
    /// </summary>
    public class SISUAuthenticationResponse
    {
        /// <summary>
        /// Gets or sets the redirect URL where the user needs to go to receieve authentication code.
        /// </summary>
        [JsonPropertyName("MsaOauthRedirect")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? MSAOAuthRedirect { get; set; }

        /// <summary>
        /// Gets or sets additional MSA request parameters.
        /// </summary>
        [JsonPropertyName("MsaRequestParameters")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public MSARequestParameters? MSARequestParameters { get; set; }

        /// <summary>
        /// Gets or sets the session ID. Populated automatically within Orion when authenticating against SISU from the X-SessionId header.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? SessionId { get; set; }
    }
}
