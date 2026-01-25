// <copyright file="NotificationData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace Den.Dev.Grunt.Models.Waypoint
{
    /// <summary>
    /// Entity representing the data payload of a Halo Waypoint notification.
    /// </summary>
    [IsAutomaticallySerializable]
    public class NotificationData
    {
        /// <summary>
        /// Gets or sets the type of the notification.
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the name of the item associated with the notification.
        /// </summary>
        [JsonPropertyName("itemName")]
        public string? ItemName { get; set; }

        /// <summary>
        /// Gets or sets the template ID for the notification.
        /// </summary>
        [JsonPropertyName("templateId")]
        public string? TemplateId { get; set; }

        /// <summary>
        /// Gets or sets the coupon code associated with the notification.
        /// </summary>
        [JsonPropertyName("couponCode")]
        public string? CouponCode { get; set; }

        /// <summary>
        /// Gets or sets the URL for more details about the notification.
        /// </summary>
        [JsonPropertyName("detailsUrl")]
        public string? DetailsUrl { get; set; }
    }
}
