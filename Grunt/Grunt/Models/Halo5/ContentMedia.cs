// <copyright file="ContentMedia.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Halo5
{
    /// <summary>
    /// Container class for media embedded with Halo 5 content.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ContentMedia
    {
        /// <summary>
        /// Gets or sets the URL to the media file.
        /// </summary>
        public string? MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the media MIME type.
        /// </summary>
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the media caption.
        /// </summary>
        public string? Caption { get; set; }

        /// <summary>
        /// Gets or sets the media alternative text.
        /// </summary>
        public string? AlternateText { get; set; }

        /// <summary>
        /// Gets or sets the media folder path.
        /// </summary>
        public string? FolderPath { get; set; }

        /// <summary>
        /// Gets or sets the media file name.
        /// </summary>
        public string? FileName { get; set; }
    }
}
