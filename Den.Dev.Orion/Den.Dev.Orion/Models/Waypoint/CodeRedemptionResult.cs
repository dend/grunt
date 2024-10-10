// <copyright file="CodeRedemptionResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Text.Json.Serialization;
using Den.Dev.Orion.Models.Waypoint.Foundation;

namespace Den.Dev.Orion.Models.Waypoint
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
