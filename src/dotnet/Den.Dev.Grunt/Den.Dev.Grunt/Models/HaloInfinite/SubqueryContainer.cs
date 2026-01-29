// <copyright file="SubqueryContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for service record subqueries.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SubqueryContainer
    {
        /// <summary>
        /// Gets or sets the list of season IDs.
        /// </summary>
        public List<string>? SeasonIds { get; set; }

        /// <summary>
        /// Gets or sets the list of game variant categories.
        /// </summary>
        public List<GameVariantCategory>? GameVariantCategories { get; set; }

        /// <summary>
        /// Gets or sets whether the player is ranked.
        /// </summary>
        public List<bool>? IsRanked { get; set; }

        /// <summary>
        /// Gets or sets the list of playlist IDs.
        /// </summary>
        public List<Guid>? PlaylistAssetIds { get; set; }
    }
}
