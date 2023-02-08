// <copyright file="DisplayPath.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
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
    }
}
