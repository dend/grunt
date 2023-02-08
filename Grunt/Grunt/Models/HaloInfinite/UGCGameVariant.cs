// <copyright file="UGCGameVariant.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for User-Generated Content (UGC) game variant.
    /// </summary>
    [IsAutomaticallySerializable]
    public class UGCGameVariant : AssetBase
    {
        /// <summary>
        /// Gets or sets custom data associated with a game variant.
        /// </summary>
        /// <remarks>
        /// Additional research is needed to understand what metadata can be injected here.
        /// </remarks>
        public dynamic? CustomData { get; set; }
    }
}
