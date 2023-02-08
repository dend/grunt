// <copyright file="PlaylistConfiguration.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for a Halo Infinite playlist.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlaylistConfiguration
    {
        /// <summary>
        /// Gets or sets the name hint for the playlist configuration. Example value is "bot_arena".
        /// </summary>
        public string? NameHint { get; set; }

        /// <summary>
        /// Gets or sets the playlist version ID. Example value is "ac9b3a8e-e585-40ea-9bd7-b03b2a19b114".
        /// </summary>
        public Guid UgcPlaylistVersion { get; set; }

        /// <summary>
        /// Gets or sets the platform matchmaking hopper ID. Example value is "GA-RETAIL_bot_arena".
        /// </summary>
        public string? PlatformMatchmakingHopperId { get; set; }

        /// <summary>
        /// Gets or sets the game start rules ID. Example value is "arenaRules".
        /// </summary>
        public string? GameStartRulesId { get; set; }

        /// <summary>
        /// Gets or sets the content configuration for Thunderhead (Halo game server). Example is "NonCampaign".
        /// </summary>
        public string? ThunderheadContentConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the VM size for Thunderhead (Halo game server). Example is "Medium".
        /// </summary>
        public string? ThunderheadVmSize { get; set; }

        /// <summary>
        /// Gets or sets the settings for <see href="https://www.microsoft.com/research/project/truematch/">TrueMatch matchmaking system</see> configuration.
        /// </summary>
        public string? TrueMatchSettings { get; set; }

        /// <summary>
        /// Gets or sets whether the playlist has CSR impact.
        /// </summary>
        public bool? HasCsr { get; set; }

        /// <summary>
        /// Gets or sets the playlist experience. Example is "PveBots".
        /// </summary>
        public string? PlaylistExperience { get; set; }

        /// <summary>
        /// Gets or sets the matchmaking delay in seconds.
        /// </summary>
        public int MatchmakingDelaySec { get; set; }
    }
}
