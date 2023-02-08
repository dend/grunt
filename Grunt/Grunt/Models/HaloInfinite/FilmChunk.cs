// <copyright file="FilmChunk.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Metadata for an individual game film chunk.
    /// </summary>
    [IsAutomaticallySerializable]
    public class FilmChunk
    {
        /// <summary>
        /// Gets or sets the film chunk index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the offsets in milliseconds for the film chunk start.
        /// </summary>
        public int ChunkStartTimeOffsetMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the film chunk duration in milliseconds.
        /// </summary>
        public int DurationMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the chunk size.
        /// </summary>
        public int ChunkSize { get; set; }

        /// <summary>
        /// Gets or sets the relative file path for the film chunk.
        /// </summary>
        public string? FileRelativePath { get; set; }

        /// <summary>
        /// Gets or sets the chunk type.
        /// </summary>
        public int ChunkType { get; set; }
    }
}
