// <copyright file="MatchesPrivacy.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for match privacy.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MatchesPrivacy
    {
        /// <summary>
        /// Gets or sets the privacy setting for matchmade games.
        /// </summary>
        public int MatchmadeGames { get; set; }

        /// <summary>
        /// Gets or sets the privacy for other games.
        /// </summary>
        public int OtherGames { get; set; }
    }
}
