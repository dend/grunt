// <copyright file="Playlist.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Game playlist.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Playlist : AssetBase
    {
        /// <summary>
        /// Gets or sets custom playlist data.
        /// </summary>
        public PlaylistCustomData? CustomData { get; set; }

        /// <summary>
        /// Gets or sets the list of rotation entries in the playlist.
        /// </summary>
        public List<PlaylistRotationEntry>? RotationEntries { get; set; }
    }
}
