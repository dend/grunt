// <copyright file="MatchInfo.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for general match information.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MatchInfo
    {
        /// <summary>
        /// Gets or sets the match start time.
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the match end time.
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the match duration.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the match lifecycle mode.
        /// </summary>
        public LifecycleMode LifecycleMode { get; set; }

        /// <summary>
        /// Gets or sets the match game variant category.
        /// </summary>
        public GameVariantCategory? GameVariantCategory { get; set; }

        /// <summary>
        /// Gets or sets the match level ID.
        /// </summary>
        public Guid LevelId { get; set; }

        /// <summary>
        /// Gets or sets the map variant for the match.
        /// </summary>
        public GenericAsset? MapVariant { get; set; }

        /// <summary>
        /// Gets or sets the game variant for the match.
        /// </summary>
        public UGCGameVariant? UgcGameVariant { get; set; }

        /// <summary>
        /// Gets or sets the clearance ID.
        /// </summary>
        public Guid ClearanceId { get; set; }

        /// <summary>
        /// Gets or sets the associated playlist.
        /// </summary>
        public GenericAsset? Playlist { get; set; }

        /// <summary>
        /// Gets or sets the associated playlist experience.
        /// </summary>
        public PlaylistExperience? PlaylistExperience { get; set; }

        /// <summary>
        /// Gets or sets the playlist map-mode pair.
        /// </summary>
        public GenericAsset? PlaylistMapModePair { get; set; }

        /// <summary>
        /// Gets or sets the season ID.
        /// </summary>
        public string? SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the playable duration.
        /// </summary>
        public TimeSpan PlayableDuration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether teams are enabled.
        /// </summary>
        public bool? TeamsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether team scoring is enabled.
        /// </summary>
        public bool? TeamScoringEnabled { get; set; }
    }
}
