// <copyright file="AcademyClientManifest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for the academy client manifest.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AcademyClientManifest
    {
        /// <summary>
        /// Gets or sets the academy tutorial contents.
        /// </summary>
        public AcademyTutorial? Tutorial { get; set; }

        /// <summary>
        /// Gets or sets the academy drill categories.
        /// </summary>
        public List<AcademyCategory>? Categories { get; set; }
    }
}
