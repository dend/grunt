// <copyright file="XboxTicketRequest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models
{
    /// <summary>
    /// Container for the Xbox Live authentication ticket request.
    /// </summary>
    public class XboxTicketRequest
    {
        /// <summary>
        /// Gets or sets the relying party for which tokens will be obtained.
        /// </summary>
        public string? RelyingParty { get; set; }

        /// <summary>
        /// Gets or sets the type of token to be obtained.
        /// </summary>
        public string? TokenType { get; set; }

        /// <summary>
        /// Gets or sets additional properties associated with the Xbox Live authentication ticket.
        /// </summary>
        public XboxTicketProperties? Properties { get; set; }
    }
}
