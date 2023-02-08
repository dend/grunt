// <copyright file="TagInfo.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Information about tags associated with in-game assets.
    /// </summary>
    [IsAutomaticallySerializable]
    public class TagInfo
    {
        /// <summary>
        /// Gets or sets a list of pre-canned tags.
        /// </summary>
        public List<AssetTag>? CannedTags { get; set; }
    }
}
