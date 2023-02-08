// <copyright file="CodeBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.Waypoint.Foundation
{
    /// <summary>
    /// Class containing data about redeemable codes.
    /// </summary>
    public abstract class CodeBase
    {
        /// <summary>
        /// Gets or sets the status for the code redemption.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }
    }
}
