// <copyright file="IHaloInfiniteClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Grunt.Core.Modules.HaloInfinite;

namespace Den.Dev.Grunt.Core
{
    /// <summary>
    /// Interface for the Halo Infinite API client.
    /// </summary>
    public interface IHaloInfiniteClient
    {
        /// <summary>
        /// Gets the Academy module for bot customization and drill-related APIs.
        /// </summary>
        AcademyModule Academy { get; }

        /// <summary>
        /// Gets the Ban Processor module for ban-related APIs.
        /// </summary>
        BanProcessorModule BanProcessor { get; }

        /// <summary>
        /// Gets the Configuration module for endpoint discovery APIs.
        /// </summary>
        ConfigurationModule Configuration { get; }

        /// <summary>
        /// Gets the Economy module for player customization, stores, and inventory APIs.
        /// </summary>
        EconomyModule Economy { get; }

        /// <summary>
        /// Gets the Game CMS module for content management APIs including achievements, metadata, and files.
        /// </summary>
        GameCmsModule GameCms { get; }

        /// <summary>
        /// Gets the Lobby module for lobby and presence APIs.
        /// </summary>
        LobbyModule Lobby { get; }

        /// <summary>
        /// Gets the Settings module for clearance and flight configuration APIs.
        /// </summary>
        SettingsModule Settings { get; }

        /// <summary>
        /// Gets the Skill module for CSR and match skill APIs.
        /// </summary>
        SkillModule Skill { get; }

        /// <summary>
        /// Gets the Stats module for match history and service record APIs.
        /// </summary>
        StatsModule Stats { get; }

        /// <summary>
        /// Gets the Text Moderation module for moderation key APIs.
        /// </summary>
        TextModerationModule TextModeration { get; }

        /// <summary>
        /// Gets the UGC module for user-generated content authoring APIs.
        /// </summary>
        UgcModule Ugc { get; }

        /// <summary>
        /// Gets the UGC Discovery module for UGC search and discovery APIs.
        /// </summary>
        UgcDiscoveryModule UgcDiscovery { get; }
    }
}
