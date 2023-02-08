// <copyright file="SpartanTokenProof.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models
{
    /// <summary>
    /// Container class for Spartan token information.
    /// </summary>
    public class SpartanTokenProof
    {
        /// <summary>
        /// Gets or sets the Spartan token content.
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// Gets or sets the token type.
        /// </summary>
        public string? TokenType { get; set; }
    }
}
