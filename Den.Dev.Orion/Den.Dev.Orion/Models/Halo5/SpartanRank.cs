// <copyright file="SpartanRank.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using Den.Dev.Orion.Models.Halo5.Foundation;

namespace Den.Dev.Orion.Models.Halo5
{
    /// <summary>
    /// Definition for a Spartan rank.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SpartanRank : AssetBase
    {
        /// <summary>
        /// Gets or sets the view for the Spartan rank definition.
        /// </summary>
        public SpartanRankView? View { get; set; }
    }
}
