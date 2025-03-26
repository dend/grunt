// <copyright file="AcademyDrill.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Academy drill.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AcademyDrill
    {
        /// <summary>
        /// Gets or sets the title string ID.
        /// </summary>
        public string? TitleStringID { get; set; }

        /// <summary>
        /// Gets or sets the list of supported academy series.
        /// </summary>
        public List<AcademySeries>? Series { get; set; }

        /// <summary>
        /// Gets or sets the sprite frame index.
        /// </summary>
        public int SpriteFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets the description string ID.
        /// </summary>
        public string? DescriptionStringID { get; set; }
    }
}
