// <copyright file="Film.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Game film configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Film : AssetBase
    {
        /// <summary>
        /// Gets or sets the film status.
        /// </summary>
        public int FilmStatusBond { get; set; }

        /// <summary>
        /// Gets or sets custom metadata for the film.
        /// </summary>
        public CustomFilmData? CustomData { get; set; }

        /// <summary>
        /// Gets or sets the blob storage prefix for the film files.
        /// </summary>
        public string? BlobStoragePathPrefix { get; set; }
    }
}
