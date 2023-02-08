// <copyright file="Link.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Halo5
{
    /// <summary>
    /// Link embedded with game assets.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Link
    {
        /// <summary>
        /// Gets or sets whether the links is an absolute one.
        /// </summary>
        public bool? Absolute { get; set; }

        /// <summary>
        /// Gets or sets the link relation.
        /// </summary>
        public string? Relation { get; set; }

        /// <summary>
        /// Gets or sets the link URI.
        /// </summary>
        public string? URI { get; set; }
    }
}
