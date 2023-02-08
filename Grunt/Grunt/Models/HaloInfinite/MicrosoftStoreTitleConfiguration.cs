// <copyright file="MicrosoftStoreTitleConfiguration.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Microsoft Store title configuration. Title split likely caused by the free-to-play (F2P) and Campaign editions of the game.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MicrosoftStoreTitleConfiguration
    {
        /// <summary>
        /// Gets or sets the mapping for Halo Infinite (343).
        /// </summary>
        [JsonPropertyName("hi343")]
        public MicrosoftStoreMapping? HaloInfinite343 { get; set; }

        /// <summary>
        /// Gets or sets the mapping for Halo Infinite.
        /// </summary>
        [JsonPropertyName("hi")]
        public MicrosoftStoreMapping? HaloInfinite { get; set; }
    }
}
