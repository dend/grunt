/**
 * @dendev/grunt - Unofficial TypeScript client library for the Halo Infinite API
 *
 * This library provides a type-safe interface to the Halo Infinite and
 * Halo Waypoint APIs, enabling developers to build applications that
 * interact with Halo game services.
 *
 * @example
 * ```typescript
 * import {
 *   HaloInfiniteClient,
 *   MatchType,
 *   LifecycleMode,
 *   isSuccess,
 * } from '@dendev/grunt';
 *
 * // Create a client with your Spartan token
 * const client = new HaloInfiniteClient({
 *   spartanToken: 'your-spartan-token',
 *   xuid: '2533274855333605',
 * });
 *
 * // Get match history
 * const history = await client.stats.getMatchHistory(
 *   '2533274855333605',
 *   0,
 *   25,
 *   MatchType.All
 * );
 *
 * if (isSuccess(history)) {
 *   for (const match of history.result.results ?? []) {
 *     console.log(`Match: ${match.matchId}`);
 *   }
 * }
 *
 * // Get player service record
 * const record = await client.stats.getPlayerServiceRecordByXuid(
 *   '2533274855333605',
 *   LifecycleMode.Matchmade
 * );
 * ```
 *
 * @packageDocumentation
 */

// ─────────────────────────────────────────────────────────────────
// Main Clients
// ─────────────────────────────────────────────────────────────────

export { HaloInfiniteClient } from './clients/halo-infinite-client';
export { WaypointClient } from './clients/waypoint-client';
export { HaloAuthenticationClient } from './auth/halo-auth-client';

// ─────────────────────────────────────────────────────────────────
// Client Options
// ─────────────────────────────────────────────────────────────────

export type {
  HaloInfiniteClientOptions,
  WaypointClientOptions,
  RequestOptions,
} from './models/common/client-options';

// ─────────────────────────────────────────────────────────────────
// Result Types and Helpers
// ─────────────────────────────────────────────────────────────────

export type { HaloApiResult, RawResponse } from './models/common/api-result';
export {
  isSuccess,
  isNotModified,
  isClientError,
  isServerError,
} from './models/common/api-result';
export { ApiContentType, getContentTypeHeader } from './models/common/api-content-type';

// ─────────────────────────────────────────────────────────────────
// Enums
// ─────────────────────────────────────────────────────────────────

export { MatchType } from './models/halo-infinite/enums/match-type';
export { LifecycleMode } from './models/halo-infinite/enums/lifecycle-mode';
export { AssetKind } from './models/halo-infinite/enums/asset-kind';
export { PlayerType } from './models/halo-infinite/enums/player-type';
export { Outcome } from './models/halo-infinite/enums/outcome';
export { ResultOrder } from './models/halo-infinite/enums/result-order';

// ─────────────────────────────────────────────────────────────────
// Error Types
// ─────────────────────────────────────────────────────────────────

export { HaloApiError } from './errors/halo-api-error';

// ─────────────────────────────────────────────────────────────────
// Authentication Types
// ─────────────────────────────────────────────────────────────────

export type {
  SpartanToken,
  SpartanTokenProof,
  SpartanTokenRequest,
} from './auth/models';

// ─────────────────────────────────────────────────────────────────
// Halo Infinite Models
// ─────────────────────────────────────────────────────────────────

// Match models
export type {
  MatchInfo,
  GenericAsset,
  UgcGameVariant,
  PlaylistExperience,
  GameplayInteraction,
  CoreStats,
  MedalCount,
  PersonalScoreEntry,
  BombStats,
  CaptureTheFlagStats,
  EliminationStats,
  ExtractionStats,
  InfectionStats,
  OddballStats,
  ZonesStats,
  StockpileStats,
  VipStats,
  PveStats,
  PvpStats,
  Stats,
  BotAttributes,
  ParticipationInfo,
  PlayerTeamStat,
  Player,
  Team,
  MatchStats,
  MatchLinks,
  PlayerMatchHistoryRecord,
  MatchHistoryResponse,
  PlayerMatchCount,
  ServiceRecordSubqueries,
  SeasonServiceRecord,
  MapServiceRecord,
  GameVariantServiceRecord,
  PlaylistServiceRecord,
  TimePlayed,
  WinLossRecord,
  PlayerServiceRecord,
} from './models/halo-infinite/match';

// Skill models
export type {
  Csr,
  PlayerMatchSkill,
  MatchSkillInfo,
  PlayerPlaylistCsr,
  PlaylistCsrResultContainer,
} from './models/halo-infinite/skill';

// Economy models
export type {
  PlayerItem,
  PlayerInventory,
  CurrencyAmount,
  CurrencySnapshot,
  CurrencyDefinition,
  DisplayString,
  InventoryAmount,
  TransactionSnapshot,
  StoreOffering,
  StorePrice,
  StoreItem,
  ActiveBoost,
  ActiveBoostsContainer,
  RewardSnapshot,
} from './models/halo-infinite/economy';

// Customization models
export type {
  CoreBase,
  ThemeBase,
  ArmorCore,
  ArmorCoreTheme,
  WeaponCore,
  WeaponCoreTheme,
  VehicleCore,
  VehicleCoreTheme,
  AiCore,
  AiCoreTheme,
  ArmorCoreCollection,
  WeaponCoreCollection,
  VehicleCoreCollection,
  AiCoreContainer,
  SpartanBody,
  Appearance,
  EmblemConfiguration,
  CustomizationData,
  AppearanceCustomization,
} from './models/halo-infinite/customization';

// UGC models
export type {
  AssetVersionFile,
  PlayAssetStats,
  AssetBase,
  AuthoringAsset,
  AuthoringAssetVersion,
  AuthoringAssetContainer,
  AuthoringAssetVersionContainer,
  AssetLinks,
  AuthoringAssetRating,
  FavoriteAsset,
  AuthoringFavoritesContainer,
  Permission,
  AssetReport,
  AssetAuthoringSession,
  AuthoringSessionSourceStarter,
  UgcSearchParams,
  UgcSearchResult,
  SearchLinks,
  MapAsset,
  MapCustomData,
  UgcGameVariantAsset,
  GameVariantCustomData,
  FilmAsset,
  FilmCustomData,
  PrefabAsset,
  PrefabCustomData,
} from './models/halo-infinite/ugc';

// Progression models
export type {
  Reward,
  Challenge,
  ChallengeDeck,
  ChallengeDeckDefinition,
  ChallengeDecksResponse,
  RewardTrack,
  RewardTrackMetadata,
  OperationRewardTrackSnapshot,
  CareerRank,
  CareerTrackContainer,
  PlayerCareerRankResult,
  RewardTrackResultContainer,
  MatchProgression,
  ChallengeProgress,
  XpBreakdown,
  CareerRankProgress,
} from './models/halo-infinite/progression';

// Miscellaneous models
export type {
  BanResult,
  BansSummaryQueryResult,
  Server,
  LobbyPresenceRequest,
  LobbyPresenceRequestContainer,
  LobbyPresenceResult,
  LobbyPresenceContainer,
  LobbyJoinHandle,
  JoinLobbyResponse,
  Medal,
  MedalMetadata,
  SpriteSheet,
  AcademyClientManifest,
  AcademyCategory,
  AcademySeries,
  AcademyDrill,
  AcademyStarDefinitions,
  AcademyStarDefinition,
  BotCustomizationData,
  BotDifficulty,
  BotAppearance,
  TestAcademyClientManifest,
  Configuration,
  Authority,
  RetryPolicyConfig,
  RetryOptionsConfig,
  OnlineUriReference,
  SettingsConfig,
  FlightedFeatureFlags,
  NewsArticle,
  News,
  SeasonCalendarEntry,
  SeasonCalendar,
  MatchesPrivacy,
  PlayerDailyCustomExperience,
  PlayerGiveaways,
  GiveawayReward,
} from './models/halo-infinite/misc';

// ─────────────────────────────────────────────────────────────────
// Waypoint Models
// ─────────────────────────────────────────────────────────────────

export type {
  Gamerpic,
  UserProfile,
  UserEmail,
  UserNotificationsSettings,
  UserSettings,
  ServiceAward,
  ServiceAwardSnapshot,
  NotificationData,
  Notification,
  CodeRedemptionResult,
  ArticleAttribute,
  ArticleBlock,
  Article,
  ArticleCategory,
} from './models/waypoint';

// ─────────────────────────────────────────────────────────────────
// Constants
// ─────────────────────────────────────────────────────────────────

export {
  USER_AGENTS,
  DEFAULT_AUTH_SCOPES,
  HEADERS,
  DEFAULT_TIMEOUT_MS,
  DEFAULT_CACHE_TTL_MS,
  DEFAULT_MAX_RETRIES,
} from './utils/constants';

export {
  HALO_CORE_ENDPOINTS,
  WAYPOINT_ENDPOINTS,
  buildServiceUrl,
} from './endpoints/halo-core-endpoints';
