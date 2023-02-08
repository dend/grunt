// <copyright file="SpartanRankDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Halo5
{
    /// <summary>
    /// Definition of a Spartan rank.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SpartanRankDefinition
    {
        /// <summary>
        /// Gets or sets the rank starting experience (XP).
        /// </summary>
        public int StartXP { get; set; }

        /// <summary>
        /// Gets or sets the rank reward set.
        /// </summary>
        public RewardSet? RewardSet { get; set; }

        /// <summary>
        /// Gets or sets the scalar for awarded match experience at a given rank.
        /// </summary>
        public float SpartanRankMatchXPScalar { get; set; }

        /// <summary>
        /// Gets or sets the credit multiplied.
        /// </summary>
        public float CreditMultiplier { get; set; }
    }
}
