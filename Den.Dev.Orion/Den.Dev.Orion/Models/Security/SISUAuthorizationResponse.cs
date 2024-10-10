// <copyright file="SISUAuthorizationResponse.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.Security
{
    /// <summary>
    /// Container class for SISU authorization responses.
    /// </summary>
    public class SISUAuthorizationResponse
    {
        /// <summary>
        /// Gets or sets the device token.
        /// </summary>
        public string? DeviceToken { get; set; }

        /// <summary>
        /// Gets or sets the title token.
        /// </summary>
        public XboxTicket? TitleToken { get; set; }

        /// <summary>
        /// Gets or sets the user token.
        /// </summary>
        public XboxTicket? UserToken { get; set; }

        /// <summary>
        /// Gets or sets the authorization token.
        /// </summary>
        public XboxTicket? AuthorizationToken { get; set; }

        /// <summary>
        /// Gets or sets the web page.
        /// </summary>
        public string? WebPage { get; set; }

        /// <summary>
        /// Gets or sets the sandbox.
        /// </summary>
        public string? Sandbox { get; set; }

        /// <summary>
        /// Gets or sets whether the modern gamertag is used.
        /// </summary>
        public bool? UseModernGamertag { get; set; }

        /// <summary>
        /// Gets or sets the flow.
        /// </summary>
        public string? Flow { get; set; }
    }
}
