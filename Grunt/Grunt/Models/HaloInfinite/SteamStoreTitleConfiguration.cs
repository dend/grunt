// <copyright file="SteamStoreTitleConfiguration.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for the title in the Steam store.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SteamStoreTitleConfiguration
    {
        /// <summary>
        /// Gets or sets the title for 343 Industries in the Steam store.
        /// </summary>
        [JsonPropertyName("hi343")]
        public SteamStoreMapping? HaloInfinite343 { get; set; }

        /// <summary>
        /// Gets or sets the title for Halo Infinite in the Steam Store.
        /// </summary>
        [JsonPropertyName("hi")]
        public SteamStoreMapping? HaloInfinite { get; set; }
    }
}
