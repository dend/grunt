// <copyright file="ActiveBoost.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Active boost for the player.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ActiveBoost
    {
        /// <summary>
        /// Gets or sets the time of boost expiration.
        /// </summary>
        public APIFormattedDate? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the boost state.
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the path to the boost.
        /// </summary>
        public string? BoostPath { get; set; }

        /// <summary>
        /// Gets or sets the effective multiplier for the boost.
        /// </summary>
        public float EffectiveMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the number of matches for the boost.
        /// </summary>
        public int Matches { get; set; }
    }
}
