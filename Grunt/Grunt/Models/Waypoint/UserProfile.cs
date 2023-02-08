// <copyright file="UserProfile.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Waypoint
{
    /// <summary>
    /// Data related to a user's Halo Waypoint profile.
    /// </summary>
    [IsAutomaticallySerializable]
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets the user's Xbox Live ID (XUID).
        /// </summary>
        public string? Xuid { get; set; }

        /// <summary>
        /// Gets or sets the user's gamertag.
        /// </summary>
        public string? Gamertag { get; set; }

        /// <summary>
        /// Gets or sets the user's gamerpic.
        /// </summary>
        public Gamerpic? Gamerpic { get; set; }
    }
}
