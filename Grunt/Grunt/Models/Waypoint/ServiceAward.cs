// <copyright file="ServiceAward.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Waypoint
{
    /// <summary>
    /// Service award for the user's <see href="https://www.halowaypoint.com/">Halo Waypoint</see> profile.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ServiceAward
    {
        /// <summary>
        /// Gets or sets the service award ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the URI to the service award image.
        /// </summary>
        public string? ImageUri { get; set; }

        /// <summary>
        /// Gets or sets the alternative text for the service award image.
        /// </summary>
        public string? ImageAlt { get; set; }

        /// <summary>
        /// Gets or sets the service award title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the service award excerpt.
        /// </summary>
        public string? Excerpt { get; set; }

        /// <summary>
        /// Gets or sets the service award slug.
        /// </summary>
        public string? Slug { get; set; }
    }
}
