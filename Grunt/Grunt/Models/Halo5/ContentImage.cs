// <copyright file="ContentImage.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Halo5
{
    /// <summary>
    /// Container class for image details.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ContentImage
    {
        /// <summary>
        /// Gets or sets the image width.
        /// </summary>
        public int Width { get; set; }
        
        /// <summary>
        /// Gets or sets the image height.
        /// </summary>
        public int Height { get; set; }
    }
}
