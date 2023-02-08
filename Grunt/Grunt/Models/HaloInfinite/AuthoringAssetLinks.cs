// <copyright file="AuthoringAssetLinks.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for authoring asset links.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringAssetLinks
    {
        /// <summary>
        /// Gets or sets the list of associated engine game variants.
        /// </summary>
        public List<EngineGameVariant>? EngineGameVariant { get; set; }
    }
}
