// <copyright file="PlaylistCsrContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container for the playlist Competitive Skill Rank (CSR).
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlaylistCsrContainer
    {
        /// <summary>
        /// Gets or sets the current playlist Competitive Skill Rank (CSR).
        /// </summary>
        public Csr? Current { get; set; }

        /// <summary>
        /// Gets or sets the maximum season Competitive Skill Rank (CSR).
        /// </summary>
        public Csr? SeasonMax { get; set; }

        /// <summary>
        /// Gets or sets the maximum all-time Competitive Skill Rank (CSR).
        /// </summary>
        public Csr? AllTimeMax { get; set; }
    }
}
