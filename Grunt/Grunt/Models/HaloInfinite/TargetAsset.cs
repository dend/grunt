// <copyright file="TargetAsset.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for an in-game target asset.
    /// </summary>
    [IsAutomaticallySerializable]
    public class TargetAsset
    {
        /// <summary>
        /// Gets or sets the target asset ID.
        /// </summary>
        public string? TargetAssetId { get; set; }

        /// <summary>
        /// Gets or sets the target asset version ID.
        /// </summary>
        public string? TargetAssetVersionId { get; set; }

        /// <summary>
        /// Gets or sets the asset order.
        /// </summary>
        public int Order { get; set; }
    }
}
