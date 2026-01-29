/**
 * Match-related models.
 */

export type { MatchInfo, GenericAsset, UgcGameVariant, PlaylistExperience, GameplayInteraction } from './match-info';
export type {
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
} from './stats';
export type {
  BotAttributes,
  ParticipationInfo,
  PlayerTeamStat,
  Player,
  Team,
} from './player';
export type { MatchStats } from './match-stats';
export type {
  MatchLinks,
  PlayerMatchHistoryRecord,
  MatchHistoryResponse,
  PlayerMatchCount,
} from './match-history';
export type {
  ServiceRecordSubqueries,
  SeasonServiceRecord,
  MapServiceRecord,
  GameVariantServiceRecord,
  PlaylistServiceRecord,
  TimePlayed,
  WinLossRecord,
  PlayerServiceRecord,
} from './service-record';
