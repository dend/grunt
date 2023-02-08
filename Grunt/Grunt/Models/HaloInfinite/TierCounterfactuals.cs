// <copyright file="TierCounterfactuals.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Counterfactuals associated with ranked matchmaking tiers.
    /// </summary>
    [IsAutomaticallySerializable]
    public class TierCounterfactuals
    {
        /// <summary>
        /// Gets or sets kill and death stats for the bronze tier.
        /// </summary>
        public KillDeathStats? Bronze { get; set; }

        /// <summary>
        /// Gets or sets kill and death stats for the silver tier.
        /// </summary>
        public KillDeathStats? Silver { get; set; }

        /// <summary>
        /// Gets or sets kill and death stats for the gold tier.
        /// </summary>
        public KillDeathStats? Gold { get; set; }

        /// <summary>
        /// Gets or sets kill and death stats for the platinum tier.
        /// </summary>
        public KillDeathStats? Platinum { get; set; }

        /// <summary>
        /// Gets or sets kill and death stats for the diamond tier.
        /// </summary>
        public KillDeathStats? Diamond { get; set; }

        /// <summary>
        /// Gets or sets kill and death stats for the onyx tier.
        /// </summary>
        public KillDeathStats? Onyx { get; set; }
    }
}
