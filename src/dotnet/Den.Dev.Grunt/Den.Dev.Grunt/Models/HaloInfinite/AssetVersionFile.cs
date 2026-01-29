// <copyright file="AssetVersionFile.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using Den.Dev.Grunt.Models.ApiIngress;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Contents for an asset version file snapshot.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AssetVersionFile
    {
        /// <summary>
        /// Gets or sets the prefix for file paths.
        /// </summary>
        public string? Prefix { get; set; }

        /// <summary>
        /// Gets or sets the list of relative file path.
        /// </summary>
        /// <remarks>
        /// File paths in this property need to be combined with the prefix to ensure that they can be obtained.
        /// </remarks>
        public List<string>? FileRelativePaths { get; set; }

        /// <summary>
        /// Gets or sets the reference to the prefix endpoint.
        /// </summary>
        public OnlineUriReference? PrefixEndpoint { get; set; }
    }
}
