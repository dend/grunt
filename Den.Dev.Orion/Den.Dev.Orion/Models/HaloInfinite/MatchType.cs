// <copyright file="MatchType.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Types of matches that a user can query with <see cref="Den.Dev.Orion.Core.HaloInfiniteClient.StatsGetMatchHistory"/>.
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
