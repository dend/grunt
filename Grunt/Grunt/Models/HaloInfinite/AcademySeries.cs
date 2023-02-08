// <copyright file="AcademySeries.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Academy series.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AcademySeries
    {
        /// <summary>
        /// Gets or sets the game asset ID.
        /// </summary>
        public string? GameAssetID { get; set; }

        /// <summary>
        /// Gets or sets the map asset ID.
        /// </summary>
        public string? MapAssetID { get; set; }

        /// <summary>
        /// Gets or sets whether the series are available.
        /// </summary>
        public bool? Available { get; set; }

        /// <summary>
        /// Gets or sets the composite string for the title, including translations.
        /// </summary>
        public DisplayString? Title { get; set; }

        /// <summary>
        /// Gets or sets the composite string for the description, including translations.
        /// </summary>
        public DisplayString? Description { get; set; }
    }
}
