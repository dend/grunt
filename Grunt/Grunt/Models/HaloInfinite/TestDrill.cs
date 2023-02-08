// <copyright file="TestDrill.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Test academy drill.
    /// </summary>
    [IsAutomaticallySerializable]
    public class TestDrill
    {
        /// <summary>
        /// Gets or sets the identifier for the associated UGC game variant.
        /// </summary>
        public string? GameVariant { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the associated map variant.
        /// </summary>
        public string? MapVariant { get; set; }

        /// <summary>
        /// Gets or sets the identifier for the assocaited gameplay reference.
        /// </summary>
        public string? GameplayGUID { get; set; }

        /// <summary>
        /// Gets or sets whether the drill is available.
        /// </summary>
        public bool? Available { get; set; }

        /// <summary>
        /// Gets or sets the description for the drill, including translated strings.
        /// </summary>
        public DisplayString? Description { get; set; }
    }
}
