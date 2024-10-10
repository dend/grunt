// <copyright file="RankRecap.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
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
