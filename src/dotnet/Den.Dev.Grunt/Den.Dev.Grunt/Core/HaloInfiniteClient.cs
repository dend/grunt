// <copyright file="HaloInfiniteClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Net.Http;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Core.Modules;
using Den.Dev.Grunt.Core.Modules.HaloInfinite;

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
    /// var inventory = await client.Economy.GetInventoryItemsAsync(player);
    ///
    /// // Access stats APIs
    /// var matchHistory = await client.Stats.GetMatchHistoryAsync(player, 0, 25, MatchType.All);
    ///
    /// // Access GameCMS APIs
    /// var medals = await client.GameCms.GetMedalMetadataAsync();
    /// </code>
    /// </example>
    /// </remarks>
    public sealed class HaloInfiniteClient : ClientBase, IHaloInfiniteClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloInfiniteClient"/> class.
        /// </summary>
        /// <param name="spartanToken">The Spartan token used for authentication.</param>
        /// <param name="xuid">Optional Xbox User ID.</param>
        /// <param name="clearanceToken">Optional clearance token for flighted content.</param>
        /// <param name="includeRawResponses">Whether to include raw API responses in results.</param>
        /// <param name="userAgent">Optional User-Agent header value for outbound requests.</param>
        public HaloInfiniteClient(string spartanToken, string xuid = "", string clearanceToken = "", bool includeRawResponses = false, string userAgent = "")
        {
            this.SpartanToken = spartanToken;
            this.Xuid = xuid;
            this.ClearanceToken = clearanceToken;
            this.IncludeRawResponses = includeRawResponses;
            this.UserAgent = userAgent;

            this.InitializeModules();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HaloInfiniteClient"/> class with a custom HttpClient.
        /// </summary>
        /// <param name="httpClient">The HttpClient instance to use for API requests.</param>
        /// <param name="spartanToken">The Spartan token used for authentication.</param>
        /// <param name="xuid">Optional Xbox User ID.</param>
        /// <param name="clearanceToken">Optional clearance token for flighted content.</param>
        /// <param name="includeRawResponses">Whether to include raw API responses in results.</param>
        /// <param name="userAgent">Optional User-Agent header value for outbound requests.</param>
        public HaloInfiniteClient(
            HttpClient httpClient,
            string spartanToken,
            string xuid = "",
            string clearanceToken = "",
            bool includeRawResponses = false,
            string userAgent = "")
            : base(httpClient)
        {
            this.SpartanToken = spartanToken;
            this.Xuid = xuid;
            this.ClearanceToken = clearanceToken;
            this.IncludeRawResponses = includeRawResponses;
            this.UserAgent = userAgent;

            this.InitializeModules();
        }

        /// <summary>
        /// Gets the Academy module for bot customization and drill-related APIs.
        /// </summary>
        public AcademyModule Academy { get; private set; } = null!;

        /// <summary>
        /// Gets the Ban Processor module for ban-related APIs.
        /// </summary>
        public BanProcessorModule BanProcessor { get; private set; } = null!;

        /// <summary>
        /// Gets the Configuration module for endpoint discovery APIs.
        /// </summary>
        public ConfigurationModule Configuration { get; private set; } = null!;

        /// <summary>
        /// Gets the Economy module for player customization, stores, and inventory APIs.
        /// </summary>
        public EconomyModule Economy { get; private set; } = null!;

        /// <summary>
        /// Gets the Game CMS module for content management APIs including achievements, metadata, and files.
        /// </summary>
        public GameCmsModule GameCms { get; private set; } = null!;

        /// <summary>
        /// Gets the Lobby module for lobby and presence APIs.
        /// </summary>
        public LobbyModule Lobby { get; private set; } = null!;

        /// <summary>
        /// Gets the Settings module for clearance and flight configuration APIs.
        /// </summary>
        public SettingsModule Settings { get; private set; } = null!;

        /// <summary>
        /// Gets the Skill module for CSR and match skill APIs.
        /// </summary>
        public SkillModule Skill { get; private set; } = null!;

        /// <summary>
        /// Gets the Stats module for match history and service record APIs.
        /// </summary>
        public StatsModule Stats { get; private set; } = null!;

        /// <summary>
        /// Gets the Text Moderation module for moderation key APIs.
        /// </summary>
        public TextModerationModule TextModeration { get; private set; } = null!;

        /// <summary>
        /// Gets the UGC module for user-generated content authoring APIs.
        /// </summary>
        public UgcModule Ugc { get; private set; } = null!;

        /// <summary>
        /// Gets the UGC Discovery module for UGC search and discovery APIs.
        /// </summary>
        public UgcDiscoveryModule UgcDiscovery { get; private set; } = null!;

        private void InitializeModules()
        {
            this.Academy = new AcademyModule(this);
            this.BanProcessor = new BanProcessorModule(this);
            this.Configuration = new ConfigurationModule(this);
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
    }
}
