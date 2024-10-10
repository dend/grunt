// <copyright file="AiCore.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// AI Core available in Halo Infinite.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AiCore : Foundation.CoreBase
    {
        /// <summary>
        /// Gets or sets a list of themes associated with an AI Core.
        /// </summary>
        public List<AiCoreTheme>? Themes { get; set; }
    }
}
