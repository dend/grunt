// <copyright file="CareerTrackContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for career track information.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CareerTrackContainer
    {
        /// <summary>
        /// Gets or sets the career track ID.
        /// </summary>
        public string? TrackId { get; set; }

        /// <summary>
        /// Gets or sets the list of career ranks.
        /// </summary>
        public List<CareerRank>? Ranks { get; set; }

        /// <summary>
        /// Gets or sets the name of the track.
        /// </summary>
        public DisplayString? Name { get; set; }

        /// <summary>
        /// Gets or sets the description for the track.
        /// </summary>
        public DisplayString? Description { get; set; }

        /// <summary>
        /// Gets or sets the operation number.
        /// </summary>
        public int? OperationNumber { get; set; }

        /// <summary>
        /// Gets or sets the date range for the track.
        /// </summary>
        public DisplayString? DateRange { get; set; }

        /// <summary>
        /// Gets or sets whether the career track is a ritual.
        /// </summary>
        public bool? IsRitual { get; set; }

        /// <summary>
        /// Gets or sets the summary image path.
        /// </summary>
        public string? SummaryImagePath { get; set; }

        /// <summary>
        /// Gets or sets the track week number.
        /// </summary>
        public int? WeekNumber { get; set; }

        /// <summary>
        /// Gets or sets the volume of XP granted per rank in the track.
        /// </summary>
        public int? XpPerRank { get; set; }

        /// <summary>
        /// Gets or sets the background image path.
        /// </summary>
        public string? BackgroundImagePath { get; set; }
    }
}
