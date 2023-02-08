// <copyright file="SubsetDataContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Information about subset data for a game engine.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SubsetDataContainer
    {
        /// <summary>
        /// Gets or sets the stat bucket for the game type.
        /// </summary>
        public int StatBucketGameType { get; set; }

        /// <summary>
        /// Gets or sets the engine variant name.
        /// </summary>
        public string? EngineName { get; set; }

        /// <summary>
        /// Gets or sets the variant name.
        /// </summary>
        public string? VariantName { get; set; }
    }
}
