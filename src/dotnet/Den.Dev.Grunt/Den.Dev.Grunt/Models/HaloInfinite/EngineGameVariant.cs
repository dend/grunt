// <copyright file="EngineGameVariant.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Grunt.Models.HaloInfinite.Foundation;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Engine game variant.
    /// </summary>
    [IsAutomaticallySerializable]
    public class EngineGameVariant : AssetBase
    {
        /// <summary>
        /// Gets or sets custom data associated with an engine game variant.
        /// </summary>
        public EngineGameVariantCustomData? CustomData { get; set; }
    }
}
