// <copyright file="AuthoringAssetCustomData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Custom data associated with an authoring asset.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringAssetCustomData
    {
        /// <summary>
        /// Gets or sets a set of keys and associated values for the custom data.
        /// </summary>
        public Dictionary<string, string>? KeyValues { get; set; }

        /// <summary>
        /// Gets or sets the film length.
        /// </summary>
        public int FilmLength { get; set; }

        /// <summary>
        /// Gets or sets the collection of film chunks.
        /// </summary>
        public List<FilmChunk>? Chunks { get; set; }

        /// <summary>
        /// Gets or sets whether the game has ended.
        /// </summary>
        public bool? HasGameEnded { get; set; }

        /// <summary>
        /// Gets or sets the value for the manifest refresh frequency.
        /// </summary>
        public int ManifestRefreshSeconds { get; set; }

        /// <summary>
        /// Gets or sets the match ID.
        /// </summary>
        public string? MatchId { get; set; }

        /// <summary>
        /// Gets or sets the film major version.
        /// </summary>
        public int FilmMajorVersion { get; set; }
    }
}
