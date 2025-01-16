// <copyright file="AcademyTutorial.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Class containing configuration information for the academy tutorial.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AcademyTutorial
    {
        /// <summary>
        /// Gets or sets the academy tutorial title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the list of series available in an academy tutorial.
        /// </summary>
        public List<AcademySeries>? Series { get; set; }

        /// <summary>
        /// Gets or sets the sprite frame index for an academy tutorial.
        /// </summary>
        public int SpriteFrameIndex { get; set; }
    }
}
