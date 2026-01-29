// <copyright file="Sprite.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Individual sprite configuration. Mostly used for medals.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Sprite
    {
        /// <summary>
        /// Gets or sets the path to the sprite.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the number of columns for the sprite.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// Gets or sets the size, in pixels, for component images.
        /// </summary>
        public int Size { get; set; }
    }
}
