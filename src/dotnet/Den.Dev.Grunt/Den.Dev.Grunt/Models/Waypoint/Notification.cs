// <copyright file="Notification.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Text.Json.Serialization;

namespace Den.Dev.Grunt.Models.Waypoint
{
    /// <summary>
    /// Entity representing a Halo Waypoint notification.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Notification
    {
        /// <summary>
        /// Gets or sets the Xbox User ID (XUID) of the user.
        /// </summary>
        [JsonPropertyName("xuid")]
        public string? Xuid { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the notification.
        /// </summary>
        [JsonPropertyName("notificationId")]
        public string? NotificationId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the notification was created.
        /// </summary>
        [JsonPropertyName("createdDate")]
        public DateTimeOffset? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the data payload of the notification.
        /// </summary>
        [JsonPropertyName("data")]
        public NotificationData? Data { get; set; }
    }
}
