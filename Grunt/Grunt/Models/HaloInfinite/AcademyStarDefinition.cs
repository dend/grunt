// <copyright file="AcademyStarDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Information related to a star earned through the Academy in Halo Infinite.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AcademyStarDefinition
    {
        /// <summary>
        /// Gets or sets the star GUID.
        /// </summary>
        public string? GUID { get; set; }

        /// <summary>
        /// Gets or sets the star title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the star title string across multiple locales.
        /// </summary>
        public DisplayString? DisplayString { get; set; }
    }
}
