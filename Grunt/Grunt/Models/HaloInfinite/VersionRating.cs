// <copyright file="VersionRating.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Rating associated with a specific asset version.
    /// </summary>
    [IsAutomaticallySerializable]
    public class VersionRating
    {
        /// <summary>
        /// Gets or sets the unique asset version ID.
        /// </summary>
        public Guid AssetVersionId { get; set; }

        /// <summary>
        /// Gets or sets the asset rating score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the date when the rating was last modified.
        /// </summary>
        public APIFormattedDate? LastModified { get; set; }
    }
}
