import { ClientBase } from './base/client-base';
import type { HaloInfiniteClientOptions } from '../models/common/client-options';

// Module imports
import { AcademyModule } from '../modules/halo-infinite/academy.module';
import { BanProcessorModule } from '../modules/halo-infinite/ban-processor.module';
import { ConfigurationModule } from '../modules/halo-infinite/configuration.module';
import { EconomyModule } from '../modules/halo-infinite/economy.module';
import { GameCmsModule } from '../modules/halo-infinite/game-cms.module';
import { LobbyModule } from '../modules/halo-infinite/lobby.module';
import { SettingsModule } from '../modules/halo-infinite/settings.module';
import { SkillModule } from '../modules/halo-infinite/skill.module';
import { StatsModule } from '../modules/halo-infinite/stats.module';
import { TextModerationModule } from '../modules/halo-infinite/text-moderation.module';
import { UgcModule } from '../modules/halo-infinite/ugc.module';
import { UgcDiscoveryModule } from '../modules/halo-infinite/ugc-discovery.module';

/**
 * Main client for interacting with Halo Infinite APIs.
 *
 * Provides access to all API functionality through domain-specific modules.
 * Modules are lazily initialized on first access to minimize memory usage.
 *
 * @example
 * ```typescript
 * import { HaloInfiniteClient, MatchType, LifecycleMode, isSuccess } from '@dendev/grunt';
 *
 * // Create client with authentication
 * const client = new HaloInfiniteClient({
 *   spartanToken: 'your-spartan-token',
 *   xuid: '2533274855333605',
 *   clearanceToken: 'flight-clearance-id',
 * });
 *
 * // Get match history
 * const history = await client.stats.getMatchHistory('2533274855333605', 0, 25, MatchType.All);
 * if (isSuccess(history)) {
 *   console.log(`Found ${history.result.resultCount} matches`);
 * }
 *
 * // Get player inventory
 * const inventory = await client.economy.getInventoryItems('2533274855333605');
 *
 * // Get CSR for a playlist
 * const csr = await client.skill.getPlaylistCsr('playlist-id', ['2533274855333605']);
 *
 * // Search for UGC maps
 * const maps = await client.ugcDiscovery.search({
 *   assetKinds: [AssetKind.Map],
 *   term: 'blood gulch',
 * });
 * ```
 */
export class HaloInfiniteClient extends ClientBase {
  // Lazy-loaded module instances
  private _academy?: AcademyModule;
  private _banProcessor?: BanProcessorModule;
  private _configuration?: ConfigurationModule;
  private _economy?: EconomyModule;
  private _gameCms?: GameCmsModule;
  private _lobby?: LobbyModule;
  private _settings?: SettingsModule;
  private _skill?: SkillModule;
  private _stats?: StatsModule;
  private _textModeration?: TextModerationModule;
  private _ugc?: UgcModule;
  private _ugcDiscovery?: UgcDiscoveryModule;

  /**
   * Creates a new HaloInfiniteClient instance.
   *
   * @param options - Client configuration options
   *
   * @example
   * ```typescript
   * const client = new HaloInfiniteClient({
   *   spartanToken: 'your-spartan-token',
   *   xuid: '2533274855333605',
   *   includeRawResponses: true, // Enable for debugging
   * });
   * ```
   */
  constructor(options: HaloInfiniteClientOptions) {
    super({
      fetchFn: options.fetchFn,
      cacheTtlMs: options.cacheTtlMs,
      maxRetries: options.maxRetries,
    });

    this.spartanToken = options.spartanToken;
    this.xuid = options.xuid ?? '';
    this.clearanceToken = options.clearanceToken ?? '';
    this.includeRawResponses = options.includeRawResponses ?? false;
    this.userAgent = options.userAgent ?? '';
  }

  /**
   * Academy module for bot customization and drill-related APIs.
   *
   * Provides access to:
   * - Bot customization options
   * - Academy content manifest
   * - Star/scoring definitions for drills
   */
  get academy(): AcademyModule {
    return (this._academy ??= new AcademyModule(this));
  }

  /**
   * Ban Processor module for ban-related APIs.
   *
   * Provides access to:
   * - Ban summary queries for players
   */
  get banProcessor(): BanProcessorModule {
    return (this._banProcessor ??= new BanProcessorModule(this));
  }

  /**
   * Configuration module for endpoint discovery APIs.
   *
   * Provides access to:
   * - API settings and endpoint configuration
   */
  get configuration(): ConfigurationModule {
    return (this._configuration ??= new ConfigurationModule(this));
  }

  /**
   * Economy module for player customization, stores, and inventory APIs.
   *
   * Provides access to:
   * - Player inventory and currency balances
   * - Customization (armor, weapons, vehicles, AI)
   * - In-game stores and offerings
   * - Active boosts and rewards
   * - Operation/battle pass progress
   */
  get economy(): EconomyModule {
    return (this._economy ??= new EconomyModule(this));
  }

  /**
   * Game CMS module for static content and definitions.
   *
   * Provides access to:
   * - Item definitions
   * - Challenge definitions
   * - Season and career metadata
   * - Medal information
   * - News and guides
   */
  get gameCms(): GameCmsModule {
    return (this._gameCms ??= new GameCmsModule(this));
  }

  /**
   * Lobby module for multiplayer lobby and presence APIs.
   *
   * Provides access to:
   * - QoS servers
   * - Player presence in lobbies
   * - Join handles and lobby joining
   */
  get lobby(): LobbyModule {
    return (this._lobby ??= new LobbyModule(this));
  }

  /**
   * Settings module for clearance and flight configuration APIs.
   *
   * Provides access to:
   * - Clearance levels and feature flags
   */
  get settings(): SettingsModule {
    return (this._settings ??= new SettingsModule(this));
  }

  /**
   * Skill module for CSR (Competitive Skill Rank) APIs.
   *
   * Provides access to:
   * - Match skill results (CSR changes after a match)
   * - Playlist CSR for players
   */
  get skill(): SkillModule {
    return (this._skill ??= new SkillModule(this));
  }

  /**
   * Stats module for match history and service record APIs.
   *
   * Provides access to:
   * - Match history for players
   * - Individual match statistics
   * - Player service records (career stats)
   * - Challenge decks and progression
   */
  get stats(): StatsModule {
    return (this._stats ??= new StatsModule(this));
  }

  /**
   * Text Moderation module for moderation-related APIs.
   *
   * Provides access to:
   * - Moderation keys for text validation
   */
  get textModeration(): TextModerationModule {
    return (this._textModeration ??= new TextModerationModule(this));
  }

  /**
   * UGC (User Generated Content) module for authoring operations.
   *
   * Provides access to:
   * - Creating, editing, and deleting user content
   * - Managing asset permissions
   * - Rating and favoriting assets
   * - Publishing and unpublishing assets
   */
  get ugc(): UgcModule {
    return (this._ugc ??= new UgcModule(this));
  }

  /**
   * UGC Discovery module for searching and browsing user content.
   *
   * Provides access to:
   * - Searching for maps, game variants, and other content
   * - Browsing featured and popular content
   * - Getting recommended content
   */
  get ugcDiscovery(): UgcDiscoveryModule {
    return (this._ugcDiscovery ??= new UgcDiscoveryModule(this));
  }
}
