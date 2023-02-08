// <copyright file="Csr.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// A record of a match Competitive Skill Rank (CSR).
    /// </summary>
    [IsAutomaticallySerializable]
    public class Csr
    {
        /// <summary>
        /// Gets or sets the CSR value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the number of matches remaining to measure the CSR.
        /// </summary>
        public int MeasurementMatchesRemaining { get; set; }

        /// <summary>
        /// Gets or sets the tier.
        /// </summary>
        public string? Tier { get; set; }

        /// <summary>
        /// Gets or sets the tier start.
        /// </summary>
        public int TierStart { get; set; }

        /// <summary>
        /// Gets or sets the subtier.
        /// </summary>
        public int SubTier { get; set; }

        /// <summary>
        /// Gets or sets the next tier.
        /// </summary>
        public string? NextTier { get; set; }

        /// <summary>
        /// Gets or sets the start of the next tier.
        /// </summary>
        public int NextTierStart { get; set; }

        /// <summary>
        /// Gets or sets the next subtier.
        /// </summary>
        public int NextSubTier { get; set; }

        /// <summary>
        /// Gets or sets the number of initial measurement matches.
        /// </summary>
        public int InitialMeasurementMatches { get; set; }
    }
}
