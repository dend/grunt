// <copyright file="Emblem.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Player emblem.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Emblem
    {
        /// <summary>
        /// Gets or sets the emblem path.
        /// </summary>
        public string? EmblemPath { get; set; }

        /// <summary>
        /// Gets or sets the configuration ID.
        /// </summary>
        public int ConfigurationId { get; set; }

        /// <summary>
        /// Gets or sets the emblem path. Alternative to <see cref="EmblemPath"/>, which needs to be validated.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the location ID for the emblem.
        /// </summary>
        public int? LocationId { get; set; }
    }
}
