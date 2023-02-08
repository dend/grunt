// <copyright file="RankRecap.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Post-match player rank recap.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RankRecap
    {
        /// <summary>
        /// Gets or sets the Competitive Skill Rank (CSR) before the match.
        /// </summary>
        public Csr? PreMatchCsr { get; set; }

        /// <summary>
        /// Gets or sets the Competitive Skill Rank (CSR) after the match.
        /// </summary>
        public Csr? PostMatchCsr { get; set; }
    }
}
