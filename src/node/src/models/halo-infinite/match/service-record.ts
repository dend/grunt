import type { Stats } from './stats';

/**
 * Subqueries for service record data.
 */
export interface ServiceRecordSubqueries {
  /** Seasonal breakdown of stats */
  Seasons?: SeasonServiceRecord[];
  /** Map breakdown of stats */
  Maps?: MapServiceRecord[];
  /** Game variant breakdown */
  GameVariants?: GameVariantServiceRecord[];
  /** Playlist breakdown */
  Playlists?: PlaylistServiceRecord[];
}

/**
 * Season-specific service record.
 */
export interface SeasonServiceRecord {
  /** Season identifier */
  SeasonId?: string;
  /** Stats for this season */
  Stats?: Stats;
}

/**
 * Map-specific service record.
 */
export interface MapServiceRecord {
  /** Map asset identifier */
  AssetId?: string;
  /** Stats for this map */
  Stats?: Stats;
}

/**
 * Game variant-specific service record.
 */
export interface GameVariantServiceRecord {
  /** Game variant category */
  GameVariantCategory?: number;
  /** Stats for this game variant */
  Stats?: Stats;
}

/**
 * Playlist-specific service record.
 */
export interface PlaylistServiceRecord {
  /** Playlist asset identifier */
  AssetId?: string;
  /** Stats for this playlist */
  Stats?: Stats;
}

/**
 * Time played breakdown.
 */
export interface TimePlayed {
  /** Total seconds played */
  Seconds?: number;
  /** Human-readable duration */
  Human?: string;
}

/**
 * Win-loss record.
 */
export interface WinLossRecord {
  /** Number of wins */
  Wins?: number;
  /** Number of losses */
  Losses?: number;
  /** Number of ties */
  Ties?: number;
  /** Number of games left early */
  Left?: number;
}

/**
 * Player service record (career stats).
 *
 * Contains aggregate statistics across all matches for a player.
 */
export interface PlayerServiceRecord {
  /** Player identifier */
  PlayerId?: string;
  /** Core aggregate stats */
  Stats?: Stats;
  /** Time played breakdown */
  TimePlayed?: TimePlayed;
  /** Win/loss/tie record */
  WinLossRecord?: WinLossRecord;
  /** Matches played */
  MatchesPlayed?: number;
  /** Subquery breakdowns (by season, map, etc.) */
  Subqueries?: ServiceRecordSubqueries;
}
