// <copyright file="SeasonPassManifest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Halo5
{
    /// <summary>
    /// Manifest associated with a Halo 5 season pass.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SeasonPassManifest
    {
        /// <summary>
        /// Gets or sets the season pass image.
        /// </summary>
        public Image? Image { get; set; }
        
        /// <summary>
        /// Gets or sets the season pass entitlement.
        /// </summary>
        public GenericAsset? Entitlement { get; set; }

        /// <summary>
        /// Gets or sets the Xbox Live Marketplace ID for the season pass.
        /// </summary>
        public string? XboxLiveMarketplaceID { get; set; }

        /// <summary>
        /// Gets or sets the asset grant program.
        /// </summary>
        public GenericAsset? GrantProgram { get; set; }
    }
}
