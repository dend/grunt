// <copyright file="REQPackRewardSetView.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using OpenSpartan.Grunt.Models.Halo5.Foundation;

namespace OpenSpartan.Grunt.Models.Halo5
{
    /// <summary>
    /// Container for REQ Pack reward definitions.
    /// </summary>
    [IsAutomaticallySerializable]
    public class REQPackRewardSetView : AssetBase
    {
        /// <summary>
        /// Gets or sets the REQ Pack reward set.
        /// </summary>
        public REQPackRewardSet? RewardSet { get; set; }
    }
}
