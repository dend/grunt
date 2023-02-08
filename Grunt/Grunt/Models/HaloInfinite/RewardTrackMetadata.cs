// <copyright file="RewardTrackMetadata.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for metadata associated with a reward track.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RewardTrackMetadata
    {
        /// <summary>
        /// Gets or sets the reward track ID.
        /// </summary>
        public string? TrackId { get; set; }

        /// <summary>
        /// Gets or sets the amount of experience (XP) awarded with each progressed rank within the reward track.
        /// </summary>
        public int XpPerRank { get; set; }

        /// <summary>
        /// Gets or sets the list of available ranks.
        /// </summary>
        public List<RankSnapshot>? Ranks { get; set; }

        /// <summary>
        /// Gets or sets the name for the reward track. Includes translated strings.
        /// </summary>
        public DisplayString? Name { get; set; }

        /// <summary>
        /// Gets or sets the description for the reward track. Includes translated strings.
        /// </summary>
        public DisplayString? Description { get; set; }

        /// <summary>
        /// Gets or sets the operation number.
        /// </summary>
        public int OperationNumber { get; set; }

        /// <summary>
        /// Gets or sets the date range during which the reward track is active. Includes translated strings.
        /// </summary>
        public DisplayString? DateRange { get; set; }

        /// <summary>
        /// Gets or sets whether the reward track is associated with a ritual.
        /// </summary>
        public bool? IsRitual { get; set; }

        /// <summary>
        /// Gets or sets the path to the image used in the summary.
        /// </summary>
        public string? SummaryImagePath { get; set; }

        /// <summary>
        /// Gets or sets the week number.
        /// </summary>
        public int WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the path to the background image for the reward track.
        /// </summary>
        public string? BackgroundImagePath { get; set; }
    }
}
