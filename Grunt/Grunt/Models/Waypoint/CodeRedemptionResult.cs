// <copyright file="CodeRedemptionResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;
using OpenSpartan.Grunt.Models.Waypoint.Foundation;

namespace OpenSpartan.Grunt.Models.Waypoint
{
    /// <summary>
    /// Class containing information about a redeemable Halo Waypoint code.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CodeRedemptionResult : CodeBase
    {
        /// <summary>
        /// Gets or sets the offer name for which the code was provided.
        /// </summary>
        [JsonPropertyName("offerName")]
        public string? OfferName { get; set; }
    }
}
