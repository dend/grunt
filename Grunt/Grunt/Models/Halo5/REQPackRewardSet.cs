// <copyright file="REQPackRewardSet.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.Halo5
{
    /// <summary>
    /// Halo 5 <see href="https://www.halopedia.org/List_of_REQ_cards/REQ_Packs">REQ Pack</see> reward set definition.
    /// </summary>
    [IsAutomaticallySerializable]
    public class REQPackRewardSet
    {
        /// <summary>
        /// Gets or sets the list of REQ packs.
        /// </summary>
        public List<GenericAsset>? REQPacks { get; set; }

        /// <summary>
        /// Gets or sets the awarded XP.
        /// </summary>
        public int XP { get; set; }
    }
}
