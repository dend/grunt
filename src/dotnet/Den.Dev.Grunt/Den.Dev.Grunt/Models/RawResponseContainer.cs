// <copyright file="RawResponseContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models
{
    /// <summary>
    /// Container class used to encapsulate any API errors.
    /// </summary>
    public sealed class RawResponseContainer
    {
        /// <summary>
        /// Gets or sets the HTTP error code produced by the API.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the message returned by the API. If the request is successful, the message is the response in raw format.
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the full URL of the HTTP request.
        /// </summary>
        public string? RequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the HTTP method used for the request (GET, POST, etc.).
        /// </summary>
        public string? RequestMethod { get; set; }

        /// <summary>
        /// Gets or sets the HTTP headers sent with the request.
        /// </summary>
        public Dictionary<string, string>? RequestHeaders { get; set; }

        /// <summary>
        /// Gets or sets the body content sent with the request.
        /// </summary>
        public string? RequestBody { get; set; }

        /// <summary>
        /// Gets or sets the HTTP headers received in the response.
        /// </summary>
        public Dictionary<string, string>? ResponseHeaders { get; set; }

        /// <summary>
        /// Gets a value indicating whether the HTTP response indicates success (2xx status code).
        /// </summary>
        public bool IsSuccess => this.Code >= 200 && this.Code < 300;
    }
}
