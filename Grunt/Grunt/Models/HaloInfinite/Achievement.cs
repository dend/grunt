// <copyright file="Achievement.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Halo Infinite achievement.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Achievement
    {
        /// <summary>
        /// Gets or sets the achievement ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the achievement ID on Xbox Live.
        /// </summary>
        public string? XboxLiveId { get; set; }

        /// <summary>
        /// Gets or sets the achievement ID on Steam.
        /// </summary>
        public string? SteamId { get; set; }
    }
}
