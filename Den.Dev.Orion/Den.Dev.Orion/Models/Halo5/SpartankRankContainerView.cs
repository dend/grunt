// <copyright file="SpartankRankContainerView.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using Den.Dev.Orion.Models.Halo5.Foundation;

namespace Den.Dev.Orion.Models.Halo5
{
    /// <summary>
    /// Container for the Spartan rank configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SpartankRankContainerView : AssetBase
    {
        /// <summary>
        /// Gets or sets the spartan rank manifest.
        /// </summary>
        public SpartanRankContainer? SpartanRankManifest { get; set; }
    }
}
