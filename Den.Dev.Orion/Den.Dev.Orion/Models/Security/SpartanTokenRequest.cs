// <copyright file="SpartanTokenRequest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.Security
{
    /// <summary>
    /// Container class containing information required for Spartan token acquisition.
    /// </summary>
    public class SpartanTokenRequest
    {
        /// <summary>
        /// Gets or sets the token audience.
        /// </summary>
        public string? Audience { get; set; }

        /// <summary>
        /// Gets or sets the target minimum version.
        /// </summary>
        public string? MinVersion { get; set; }

        /// <summary>
        /// Gets or sets token information.
        /// </summary>
        public SpartanTokenProof[]? Proof { get; set; }
    }
}
