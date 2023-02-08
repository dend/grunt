// <copyright file="AiCoreTheme.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// AI core theme.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AiCoreTheme : Foundation.ThemeBase
    {
        /// <summary>
        /// Gets or sets the path to the AI core model.
        /// </summary>
        public string? ModelPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the AI core color.
        /// </summary>
        public string? ColorPath { get; set; }
    }
}
