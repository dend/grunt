// <copyright file="DisplayPath.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Item display path configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class DisplayPath
    {
        /// <summary>
        /// Gets or sets the item width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the item height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the media associated with the item.
        /// </summary>
        public Media? Media { get; set; }

        /// <summary>
        /// Gets or sets the item MIME type.
        /// </summary>
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the item folder path.
        /// </summary>
        public string? FolderPath { get; set; }

        /// <summary>
        /// Gets or sets the item file name.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the caption if the file is an image.
        /// </summary>
        public string? Caption { get; set; }

        /// <summary>
        /// Gets or sets the alternate text if the file is an image.
        /// </summary>
        public string? AlternateText { get; set; }
    }
}
