// <copyright file="UserNotificationsSettings.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.Waypoint
{
    /// <summary>
    /// Settings associated with a user's notification preferences.
    /// </summary>
    [IsAutomaticallySerializable]
    public class UserNotificationsSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the user is enrolled into Halo Insider notifications.
        /// </summary>
        public bool? Insider { get; set; }
    }
}
