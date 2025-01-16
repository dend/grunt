// <copyright file="PlaylistCsrResultContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for CSR data associated with a playlst.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlaylistCsrResultContainer
    {
        /// <summary>
        /// Gets or sets the list of CSR values associated with a playlist.
        /// </summary>
        public List<PlaylistCsrResults>? Value { get; set; }
    }
}
