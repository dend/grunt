// <copyright file="EmblemMapping.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Mapping of an emblem to its associated nameplate.
    /// </summary>
    [IsAutomaticallySerializable]
    public class EmblemMapping
    {
        /// <summary>
        /// Gets or sets the emblem path in the CMS.
        /// </summary>
        public string? EmblemCmsPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the nameplate in the CMS.
        /// </summary>
        public string? NameplateCmsPath { get; set; }

        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        public string? TextColor { get; set; }
    }
}
