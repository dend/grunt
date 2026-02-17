import type {
  CoreStats,
  BombStats,
  CaptureTheFlagStats,
  EliminationStats,
  ExtractionStats,
  InfectionStats,
  OddballStats,
  ZonesStats,
  StockpileStats,
} from './stats';

/**
 * Container for service record subqueries.
 */
export interface SubqueryContainer {
  /** List of season IDs */
  SeasonIds?: string[];
  /** List of game variant categories */
  GameVariantCategories?: number[];
  /** Whether the player is ranked */
  IsRanked?: boolean[];
  /** List of playlist asset IDs */
  PlaylistAssetIds?: string[];
}

/**
 * Player service record (career stats).
 *
 * Contains aggregate statistics across all matches for a player.
 */
export interface PlayerServiceRecord {
  /** Container for all subqueries for the service record */
  Subqueries?: SubqueryContainer;
  /** Total time played (ISO 8601 duration) */
  TimePlayed?: string;
  /** Total number of matches completed */
  MatchesCompleted?: number;
  /** Total number of wins */
  Wins?: number;
  /** Total number of losses */
  Losses?: number;
  /** Total number of ties */
  Ties?: number;
  /** Core player stats */
  CoreStats?: CoreStats;
  /** Bomb game mode stats */
  BombStats?: BombStats;
  /** Capture The Flag game mode stats */
  CaptureTheFlagStats?: CaptureTheFlagStats;
  /** Elimination game mode stats */
  EliminationStats?: EliminationStats;
  /** Extraction game mode stats */
  ExtractionStats?: ExtractionStats;
  /** Infection game mode stats */
  InfectionStats?: InfectionStats;
  /** Oddball game mode stats */
  OddballStats?: OddballStats;
  /** Zones (Land Grab, Strongholds, KOTH) game mode stats */
  ZonesStats?: ZonesStats;
  /** Stockpile game mode stats */
  StockpileStats?: StockpileStats;
}
