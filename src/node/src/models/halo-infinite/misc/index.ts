/**
 * Miscellaneous models.
 */

export type { BanResult, BansSummaryQueryResult } from './ban';

export type {
  Server,
  LobbyPresenceRequest,
  LobbyPresenceRequestContainer,
  LobbyPresenceResult,
  LobbyPresenceContainer,
  LobbyJoinHandle,
  JoinLobbyResponse,
} from './lobby';

export type { Medal, MedalMetadata, SpriteSheet } from './medal';

export type {
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
} from './academy';

export type {
  Configuration,
  Authority,
  RetryPolicyConfig,
  RetryOptionsConfig,
  OnlineUriReference,
  SettingsConfig,
  FlightedFeatureFlags,
  PlayerClearance,
} from './configuration';

export type {
  NewsArticle,
  News,
  SeasonCalendarEntry,
  SeasonCalendar,
  MatchesPrivacy,
  PlayerDailyCustomExperience,
  PlayerGiveaways,
  GiveawayReward,
} from './news';
