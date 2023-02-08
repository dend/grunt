// <copyright file="OEConfiguration.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for in-game message configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class OEConfiguration
    {
        /// <summary>
        /// Gets or sets the message. Includes translated strings.
        /// </summary>
        public DisplayString? Message { get; set; }
    }
}
