// <copyright file="PlayerMatchCount.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Snapshot of played matches for a player.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerMatchCount
    {
        /// <summary>
        /// Gets or sets the number of custom matches played.
        /// </summary>
        public int CustomMatchesPlayedCount { get; set; }

        /// <summary>
        /// Gets or sets the number of matches played.
        /// </summary>
        public int MatchesPlayedCount { get; set; }

        /// <summary>
        /// Gets or sets the number of matchmade matches played.
        /// </summary>
        public int MatchmadeMatchesPlayedCount { get; set; }

        /// <summary>
        /// Gets or sets the number of local matches played.
        /// </summary>
        public int LocalMatchesPlayedCount { get; set; }
    }
}
