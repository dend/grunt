// <copyright file="MatchType.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Types of matches that a user can query with <see cref="Den.Dev.Grunt.Core.HaloInfiniteClient.StatsGetMatchHistory"/>.
    /// </summary>
    public enum MatchType
    {
        /// <summary>
        /// All match types.
        /// </summary>
        All,

        /// <summary>
        /// Matchmaking matches. These are standard multiplayer games.
        /// </summary>
        Matchmaking,

        /// <summary>
        /// Custom matches.
        /// </summary>
        Custom,

        /// <summary>
        /// Local (LAN) matches.
        /// </summary>
        Local,
    }
}
