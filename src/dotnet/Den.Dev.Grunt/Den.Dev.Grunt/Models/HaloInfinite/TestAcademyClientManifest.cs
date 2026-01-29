// <copyright file="TestAcademyClientManifest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Test academy client manifest
    /// </summary>
    [IsAutomaticallySerializable]
    public class TestAcademyClientManifest
    {
        /// <summary>
        /// Gets or sets the test tutorial.
        /// </summary>
        public TestAcademyTutorial? Tutorial { get; set; }

        /// <summary>
        /// Gets or sets the list of test drill categories.
        /// </summary>
        public List<TestDrillCategory>? Categories { get; set; }
    }
}
