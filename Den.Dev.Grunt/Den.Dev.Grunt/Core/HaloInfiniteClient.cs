// <copyright file="HaloInfiniteClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Net.Http;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Core.Modules;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.ApiIngress;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core
{
    /// <summary>
    /// Client for interacting with Halo Infinite APIs through domain-specific modules.
    /// </summary>
    /// <remarks>
    /// This client provides organized access to Halo Infinite APIs through specialized modules.
    /// Each module handles a specific domain of functionality (Economy, GameCms, Stats, etc.).
    ///
    /// <example>
    /// Example usage:
    /// <code>
    /// var client = new HaloInfiniteClient(spartanToken, xuid, clearanceToken);
    ///
    /// // Access economy APIs
    /// var inventory = await client.Economy.GetInventoryItems(player);
    ///
    /// // Access stats APIs
    /// var matchHistory = await client.Stats.GetMatchHistory(player, 0, 25, MatchType.All);
    ///
    /// // Access GameCMS APIs
    /// var medals = await client.GameCms.GetMedalMetadata();
    /// </code>
    /// </example>
    /// </remarks>
    public class HaloInfiniteClient : ClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloInfiniteClient"/> class.
        /// </summary>
        /// <param name="spartanToken">The Spartan token used for authentication.</param>
        /// <param name="xuid">Optional Xbox User ID.</param>
        /// <param name="clearanceToken">Optional clearance token for flighted content.</param>
        /// <param name="includeRawResponses">Whether to include raw API responses in results.</param>
        public HaloInfiniteClient(string spartanToken, string xuid = "", string clearanceToken = "", bool includeRawResponses = false)
        {
            this.SpartanToken = spartanToken;
            this.Xuid = xuid;
            this.ClearanceToken = clearanceToken;
            this.IncludeRawResponses = includeRawResponses;

            this.Academy = new AcademyModule(this);
            this.BanProcessor = new BanProcessorModule(this);
            this.Economy = new EconomyModule(this);
            this.GameCms = new GameCmsModule(this);
            this.Lobby = new LobbyModule(this);
            this.Settings = new SettingsModule(this);
            this.Skill = new SkillModule(this);
            this.Stats = new StatsModule(this);
            this.TextModeration = new TextModerationModule(this);
            this.Ugc = new UgcModule(this);
            this.UgcDiscovery = new UgcDiscoveryModule(this);
        }

        /// <summary>
        /// Gets the Academy module for bot customization and drill-related APIs.
        /// </summary>
        public AcademyModule Academy { get; }

        /// <summary>
        /// Gets the Ban Processor module for ban-related APIs.
        /// </summary>
        public BanProcessorModule BanProcessor { get; }

        /// <summary>
        /// Gets the Economy module for player customization, stores, and inventory APIs.
        /// </summary>
        public EconomyModule Economy { get; }

        /// <summary>
        /// Gets the Game CMS module for content management APIs including achievements, metadata, and files.
        /// </summary>
        public GameCmsModule GameCms { get; }

        /// <summary>
        /// Gets the Lobby module for lobby and presence APIs.
        /// </summary>
        public LobbyModule Lobby { get; }

        /// <summary>
        /// Gets the Settings module for clearance and flight configuration APIs.
        /// </summary>
        public SettingsModule Settings { get; }

        /// <summary>
        /// Gets the Skill module for CSR and match skill APIs.
        /// </summary>
        public SkillModule Skill { get; }

        /// <summary>
        /// Gets the Stats module for match history and service record APIs.
        /// </summary>
        public StatsModule Stats { get; }

        /// <summary>
        /// Gets the Text Moderation module for moderation key APIs.
        /// </summary>
        public TextModerationModule TextModeration { get; }

        /// <summary>
        /// Gets the UGC module for user-generated content authoring APIs.
        /// </summary>
        public UgcModule Ugc { get; }

        /// <summary>
        /// Gets the UGC Discovery module for UGC search and discovery APIs.
        /// </summary>
        public UgcDiscoveryModule UgcDiscovery { get; }

        /// <summary>
        /// Gets the API settings container, which has the full list of available endpoints.
        /// </summary>
        /// <returns>If successful, returns an instance of APISettingsContainer that contains the full list of available endpoints. Otherwise, returns null.</returns>
        public async Task<HaloApiResultContainer<Configuration, RawResponseContainer>> GetApiSettingsContainer()
        {
            return await this.ExecuteAPIRequest<Configuration>(
                HaloCoreEndpoints.HaloInfiniteEndpointsEndpoint,
                HttpMethod.Get,
                true,
                false,
                includeRawResponse: this.IncludeRawResponses);
        }
    }
}
