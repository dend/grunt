// <copyright file="Vector.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Class representing a positioning vector.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Vector
    {
        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        [JsonPropertyName("x")]
        public int? X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        [JsonPropertyName("y")]
        public int? Y { get; set; }

        /// <summary>
        /// Gets or sets the Z coordinate.
        /// </summary>
        [JsonPropertyName("z")]
        public int? Z { get; set; }
    }
}
