// <copyright file="SearchResultsContainer.cs" company="Den Delimarsky">
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
    /// Container for search results obtained through the Discovery endpoint.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SearchResultsContainer
    {
        /// <summary>
        /// Gets or sets the list of tags associated with an asset.
        /// </summary>
        public List<AssetTag>? Tags { get; set; }

        /// <summary>
        /// Gets or sets the estimated total number of results.
        /// </summary>
        public int EstimatedTotal { get; set; }

        /// <summary>
        /// Gets or sets the start of the result counter.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the count of results on the current page.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the count of results on the current page. Seems to be duplicating <see cref="Count"/>.
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// Gets or sets the list of search results for the asset query.
        /// </summary>
        public List<AssetSearchResult>? Results { get; set; }

        /// <summary>
        /// Gets or sets the list of additional links related to the search results.
        /// </summary>
        public Dictionary<string, OnlineUriReference>? Links { get; set; }
    }
}
