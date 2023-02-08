// <copyright file="CommonContentProperties.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.Halo5
{
    /// <summary>
    /// Class representing a collection of common content properties.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CommonContentProperties
    {
        /// <summary>
        /// Gets or sets the asset owner.
        /// </summary>
        public string? Owner { get; set; }

        /// <summary>
        /// Gets or sets the asset creation time.
        /// </summary>
        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// Gets or sets the asset modification time.
        /// </summary>
        public DateTime? ModifiedUtc { get; set; }

        /// <summary>
        /// Gets or sets the asset publication date.
        /// </summary>
        public DateTime? PublishedUtc { get; set; }

        /// <summary>
        /// Gets or sets the asset container.
        /// </summary>
        public int Container { get; set; }
    }
}
