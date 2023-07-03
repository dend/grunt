// <copyright file="RawResponseContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models
{
    /// <summary>
    /// Container class used to encapsulate any API errors.
    /// </summary>
    public class RawResponseContainer
    {
        /// <summary>
        /// Gets or sets the HTTP error code produced by the API.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the message returned by the API. If the request is successful, the message is the response in raw format.
        /// </summary>
        public string? Message { get; set; }
    }
}
