// <copyright file="InGameItemConfiguration.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for an in-game item.
    /// </summary>
    [IsAutomaticallySerializable]
    public class InGameItemConfiguration
    {
        /// <summary>
        /// Gets or sets the configuration ID.
        /// </summary>
        public int ConfigurationId { get; set; }

        /// <summary>
        /// Gets or sets the configuration path.
        /// </summary>
        public string? ConfigurationPath { get; set; }
    }
}
