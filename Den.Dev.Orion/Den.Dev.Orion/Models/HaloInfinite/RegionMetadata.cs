// <copyright file="RegionMetadata.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container class for region metadata, used with armors.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RegionMetadata
    {
        /// <summary>
        /// Gets or sets the region ID.
        /// </summary>
        public IdentifierName? RegionId { get; set; }

        /// <summary>
        /// Gets or sets the permutation ID.
        /// </summary>
        public IdentifierName? PermutationId { get; set; }

        /// <summary>
        /// Gets or sets the style ID override.
        /// </summary>
        public IdentifierName? StyleIdOverride { get; set; }
    }
}
