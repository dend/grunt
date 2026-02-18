/**
 * Miscellaneous models.
 */

export type { BanResult, TargetBanSummary, BansSummaryQueryResult } from './ban';

export type {
  Server,
  LobbyPresenceRequestContainer,
  LobbyPresenceResult,
  LobbyPresenceContainer,
  LobbyJoinHandle,
  JoinLobbyResponse,
} from './lobby';

export type { Medal, MedalMetadata, Sprite } from './medal';

export type {
  AcademyTutorial,
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
} from './news';
