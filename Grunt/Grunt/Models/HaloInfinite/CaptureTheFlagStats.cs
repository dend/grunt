// <copyright file="CaptureTheFlagStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Statistics related to the Capture The Flag (CTF) matchmade games.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CaptureTheFlagStats
    {
        /// <summary>
        /// Gets or sets the number of flag capture assists.
        /// </summary>
        public int FlagCaptureAssists { get; set; }

        /// <summary>
        /// Gets or sets the number of flag captures.
        /// </summary>
        public int FlagCaptures { get; set; }

        /// <summary>
        /// Gets or sets the number of flag carriers killed.
        /// </summary>
        public int FlagCarriersKilled { get; set; }

        /// <summary>
        /// Gets or sets the number of flag grabs.
        /// </summary>
        public int FlagGrabs { get; set; }

        /// <summary>
        /// Gets or sets the number of flag returnes killed.
        /// </summary>
        public int FlagReturnersKilled { get; set; }

        /// <summary>
        /// Gets or sets the number of flag returns.
        /// </summary>
        public int FlagReturns { get; set; }

        /// <summary>
        /// Gets or sets the number of flag secures.
        /// </summary>
        public int FlagSecures { get; set; }

        /// <summary>
        /// Gets or sets the number of flag steals.
        /// </summary>
        public int FlagSteals { get; set; }

        /// <summary>
        /// Gets or sets the number of kills as a flag carrier.
        /// </summary>
        public int KillsAsFlagCarrier { get; set; }

        /// <summary>
        /// Gets or sets the number of kills as a flag returner.
        /// </summary>
        public int KillsAsFlagReturner { get; set; }

        /// <summary>
        /// Gets or sets the time spent as a flag carrier.
        /// </summary>
        public TimeSpan TimeAsFlagCarrier { get; set; }
    }
}
