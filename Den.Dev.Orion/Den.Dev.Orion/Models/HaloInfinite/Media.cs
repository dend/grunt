// <copyright file="Media.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Orion.Models.ApiIngress;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Reference information for in-game media.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Media
    {
        /// <summary>
        /// Gets or sets the fully-qualified URL to the media content.
        /// </summary>
        public OnlineUriReference? MediaUrl { get; set; }

        /// <summary>
        /// Gets or sets the media MIME type.
        /// </summary>
        public string? MimeType { get; set; }

        /// <summary>
        /// Gets or sets the media caption. Includes translated strings.
        /// </summary>
        public DisplayString? Caption { get; set; }

        /// <summary>
        /// Gets or sets the media alternate text. Includes translated strings.
        /// </summary>
        public DisplayString? AlternateText { get; set; }

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
