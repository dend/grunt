// <copyright file="AcademyCategory.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for academy drill categories.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AcademyCategory
    {
        /// <summary>
        /// Gets or sets the drill type.
        /// </summary>
        public string? DrillType { get; set; }

        /// <summary>
        /// Gets or sets supported academy drills.
        /// </summary>
        public List<AcademyDrill>? Drills { get; set; }
    }
}
