// <copyright file="ReadNotificationsResult.cs" company="Den Delimarsky">
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
    /// Entity representing the result of marking notifications as read.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ReadNotificationsResult
    {
        /// <summary>
        /// Gets or sets the Xbox User ID (XUID) of the user.
        /// </summary>
        [JsonPropertyName("xuid")]
        public string? Xuid { get; set; }

        /// <summary>
        /// Gets or sets the date and time when notifications were marked as read.
        /// </summary>
        [JsonPropertyName("notificationsReadDate")]
        public DateTimeOffset? NotificationsReadDate { get; set; }
    }
}
