// <copyright file="TestAcademyTutorial.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Test tutorial for the academy.
    /// </summary>
    [IsAutomaticallySerializable]
    public class TestAcademyTutorial
    {
        /// <summary>
        /// Gets or sets the tutorial title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the list of drills associated with the tutorial.
        /// </summary>
        public List<TestDrill>? Series { get; set; }

        /// <summary>
        /// Gets or sets the sprite frame index.
        /// </summary>
        public int SpriteFrameIndex { get; set; }
    }
}
