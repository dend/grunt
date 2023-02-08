// <copyright file="ExtractionStats.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Player statistics for the Extraction game mode.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ExtractionStats
    {
        /// <summary>
        /// Gets or sets the number of completed extraction conversions.
        /// </summary>
        public int ExtractionConversionsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the number of denied extraction conversions.
        /// </summary>
        public int ExtractionConversionsDenied { get; set; }

        /// <summary>
        /// Gets or sets the number of completed extraction initiations.
        /// </summary>
        public int ExtractionInitiationsCompleted { get; set; }

        /// <summary>
        /// Gets or sets the number of denied extraction initiations.
        /// </summary>
        public int ExtractionInitiationsDenied { get; set; }

        /// <summary>
        /// Gets or sets the number of successful extractions.
        /// </summary>
        public int SuccessfulExtractions { get; set; }
    }
}
