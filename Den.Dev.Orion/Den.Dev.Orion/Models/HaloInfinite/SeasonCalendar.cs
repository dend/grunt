// <copyright file="SeasonCalendar.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container class for season metadata.
    /// </summary>
    public class SeasonCalendar
    {
        /// <summary>
        /// Gets or sets the collection of defined seasons.
        /// </summary>
        public List<SeasonCalendarEntry>? Seasons { get; set; }

        /// <summary>
        /// Gets or sets the collection of defined events.
        /// </summary>
        /// <remarks>
        /// Since the shift to operations, events are not really a thing in Halo Infinite anymore and are used for reference purposes only.
        /// </remarks>
        public List<SeasonCalendarEntry>? Events { get; set; }

        /// <summary>
        /// Gets or sets the details about the career rank definitions.
        /// </summary>
        public List<SeasonCalendarEntry>? CareerRank { get; set; }
    }
}
