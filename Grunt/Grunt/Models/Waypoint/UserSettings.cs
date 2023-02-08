// <copyright file="UserSettings.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Waypoint
{
    /// <summary>
    /// Halo Waypoint user settings.
    /// </summary>
    [IsAutomaticallySerializable]
    public class UserSettings
    {
        /// <summary>
        /// Gets or sets the user Xbox Live ID (XUID).
        /// </summary>
        public string? Xuid { get; set; }

        /// <summary>
        /// Gets or sets the user email address.
        /// </summary>
        public UserEmail? Email { get; set; }

        /// <summary>
        /// Gets or sets the user's notitfication preferences.
        /// </summary>
        public UserNotificationsSettings? Notifications { get; set; }
    }
}
