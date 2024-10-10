// <copyright file="Articleimage.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for a news article image.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ArticleImage
    {
        /// <summary>
        /// Gets or sets the image width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the image height.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        public string? Path { get; set; }
    }
}
