// <copyright file="BansSummaryQueryResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using OpenSpartan.Grunt.Models.ApiIngress;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Information related to ban summaries for player accounts.
    /// </summary>
    [IsAutomaticallySerializable]
    public class BansSummaryQueryResult
    {
        /// <summary>
        /// Gets or sets the list of ban summaries.
        /// </summary>
        public List<TargetBanSummary>? Results { get; set; }

        /// <summary>
        /// Gets or sets the list of additional links related to ban summaries.
        /// </summary>
        public Dictionary<string, OnlineUriReference>? Links { get; set; }
    }
}
