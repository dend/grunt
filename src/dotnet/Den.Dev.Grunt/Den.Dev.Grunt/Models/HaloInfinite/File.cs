// <copyright file="File.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Grunt.Models.ApiIngress;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// File metadata.
    /// </summary>
    [IsAutomaticallySerializable]
    public class File
    {
        /// <summary>
        /// Gets or sets the URL configuration for the file.
        /// </summary>
        public OnlineUriReference? Uri { get; set; }

        /// <summary>
        /// Gets or sets the content <see href="https://developer.mozilla.org/docs/Web/HTTP/Headers/ETag">ETag</see>.
        /// </summary>
        public string? ETag { get; set; }

        /// <summary>
        /// Gets or sets the content length.
        /// </summary>
        public int ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the usage type.
        /// </summary>
        public int Usage { get; set; }
    }
}
