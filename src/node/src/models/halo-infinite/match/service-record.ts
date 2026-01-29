import type { Stats } from './stats';

/**
 * Subqueries for service record data.
 */
export interface ServiceRecordSubqueries {
  /** Seasonal breakdown of stats */
  seasons?: SeasonServiceRecord[];
  /** Map breakdown of stats */
  maps?: MapServiceRecord[];
  /** Game variant breakdown */
  gameVariants?: GameVariantServiceRecord[];
  /** Playlist breakdown */
  playlists?: PlaylistServiceRecord[];
}

/**
 * Season-specific service record.
 */
export interface SeasonServiceRecord {
  /** Season identifier */
  seasonId?: string;
  /** Stats for this season */
  stats?: Stats;
}

/**
 * Map-specific service record.
 */
export interface MapServiceRecord {
  /** Map asset identifier */
  assetId?: string;
  /** Stats for this map */
  stats?: Stats;
}

/**
 * Game variant-specific service record.
 */
export interface GameVariantServiceRecord {
  /** Game variant category */
  gameVariantCategory?: number;
  /** Stats for this game variant */
  stats?: Stats;
}

/**
 * Playlist-specific service record.
 */
export interface PlaylistServiceRecord {
  /** Playlist asset identifier */
  assetId?: string;
  /** Stats for this playlist */
  stats?: Stats;
}

/**
 * Time played breakdown.
 */
export interface TimePlayed {
  /** Total seconds played */
  seconds?: number;
  /** Human-readable duration */
  human?: string;
}

/**
 * Win-loss record.
 */
export interface WinLossRecord {
  /** Number of wins */
  wins?: number;
  /** Number of losses */
  losses?: number;
  /** Number of ties */
  ties?: number;
  /** Number of games left early */
  left?: number;
}

/**
 * Player service record (career stats).
 *
 * Contains aggregate statistics across all matches for a player.
 */
export interface PlayerServiceRecord {
  /** Player identifier */
  playerId?: string;
  /** Core aggregate stats */
  stats?: Stats;
  /** Time played breakdown */
  timePlayed?: TimePlayed;
  /** Win/loss/tie record */
  winLossRecord?: WinLossRecord;
  /** Matches played */
  matchesPlayed?: number;
  /** Subquery breakdowns (by season, map, etc.) */
  subqueries?: ServiceRecordSubqueries;
}
