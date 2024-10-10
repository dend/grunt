// <copyright file="AcademyStarDefinitions.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Container class for academy star definitions.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AcademyStarDefinitions
    {
        /// <summary>
        /// Gets or sets the list of supported academy star definitions.
        /// </summary>
        public List<AcademyStarDefinition>? Values { get; set; }
    }
}
