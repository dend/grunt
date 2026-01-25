// <copyright file="CachedAPIResponse.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Core
{
    /// <summary>
    /// Container class for cached API responses.
    /// </summary>
    internal class CachedAPIResponse
    {
        /// <summary>
        /// Gets or sets the ETag value for the cached response.
        /// </summary>
        public string? ETag { get; set; }

        /// <summary>
        /// Gets or sets the cached content.
        /// </summary>
        public byte[]? Content { get; set; }
    }
}
