// <copyright file="Attribute.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.Waypoint
{
    /// <summary>
    /// Article block attribute.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Attribute
    {
        /// <summary>
        /// Gets or sets the block attribute ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the size slug for the block.
        /// </summary>
        /// <remarks>
        /// Usually associated with "core/image" blocks.
        /// </remarks>
        public string? SizeSlug { get; set; }

        /// <summary>
        /// Gets or sets the link destination for the block.
        /// </summary>
        /// <remarks>
        /// Usually associated with "core/image" blocks.
        /// </remarks>
        public string? LinkDestination { get; set; }

        /// <summary>
        /// Gets or sets the attribute URL.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the attribute type.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the attribute provider slug.
        /// </summary>
        public string? ProviderNameSlug { get; set; }

        /// <summary>
        /// Gets or sets the attribute class.
        /// </summary>
        public string? ClassName { get; set; }
    }
}
