// <copyright file="SeasonCalendarEntry.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for a season calendar metadata entry.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SeasonCalendarEntry
    {
        /// <summary>
        /// Gets or sets the CSR season file path that contains details about the ranked season.
        /// </summary>
        /// <remarks>
        /// Navigating to this URL returns a 403 Forbidden at this time.
        /// </remarks>
        public string? CsrSeasonFilePath { get; set; }

        /// <summary>
        /// Gets or sets the operation track path.
        /// </summary>
        public string? OperationTrackPath { get; set; }

        /// <summary>
        /// Gets or sets the path for the season metadata.
        /// </summary>
        public string? SeasonMetadata { get; set; }

        /// <summary>
        /// Gets or sets the reward track path.
        /// </summary>
        public string? RewardTrackPath { get; set; }

        /// <summary>
        /// Gets or sets the start date for the timed event.
        /// </summary>
        public APIFormattedDate? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date for the timed event.
        /// </summary>
        public APIFormattedDate? EndDate { get; set; }
    }
}
