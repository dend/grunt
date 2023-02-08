// <copyright file="MatchStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for match statistics.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MatchStats
    {
        /// <summary>
        /// Gets or sets the match ID.
        /// </summary>
        public Guid MatchId { get; set; }

        /// <summary>
        /// Gets or sets complete match information.
        /// </summary>
        public MatchInfo? MatchInfo { get; set; }

        /// <summary>
        /// Gets or sets a list of teams participating in the match.
        /// </summary>
        public List<Team>? Teams { get; set; }

        /// <summary>
        /// Gets or sets a list of players participating in the match.
        /// </summary>
        public List<Player>? Players { get; set; }
    }
}
