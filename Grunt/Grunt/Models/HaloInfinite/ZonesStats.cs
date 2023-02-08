// <copyright file="ZonesStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Stats related to Land Grab matches.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ZonesStats
    {
        /// <summary>
        /// Gets or sets the number of zones captured during a match.
        /// </summary>
        public int? ZoneCaptures { get; set; }

        /// <summary>
        /// Gets or sets the number of defensive kills from a captured zone during a match.
        /// </summary>
        public int? ZoneDefensiveKills { get; set; }

        /// <summary>
        /// Gets or sets number of offensive kills into a non-captured zone during a match.
        /// </summary>
        public int? ZoneOffensiveKills { get; set; }

        /// <summary>
        /// Gets or sets number of secured zones during a match.
        /// </summary>
        public int? ZoneSecures { get; set; }

        /// <summary>
        /// Gets or sets the duration of zone occupation during a match.
        /// </summary>
        public TimeSpan TotalZoneOccupationTime { get; set; }

        /// <summary>
        /// Gets or sets number of ticks during which the player was occupying a zone.
        /// </summary>
        public int? ZoneScoringTicks { get; set; }
    }
}
