// <copyright file="Achievement.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
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
