// <copyright file="MatchHistoryResponse.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for data on player match history.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MatchHistoryResponse
    {
        /// <summary>
        /// Gets or sets the starting index for the results.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the count of results on the page. This matches the requested number from the query.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the count of results returned. These are actual query results.
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// Gets or sets the list of match history records.
        /// </summary>
        public List<PlayerMatchHistoryRecord>? Results { get; set; }

        /// <summary>
        /// Gets or sets additional match links.
        /// </summary>
        public MatchLinks? Links { get; set; }
    }
}
