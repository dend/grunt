import type { DisplayString } from '../economy/inventory';

/**
 * Halo Infinite news article.
 */
export interface NewsArticle {
  /** Short headline */
  ShortHeadline?: DisplayString;
  /** Full headline */
  FullHeadline?: DisplayString;
  /** Article body */
  Body?: DisplayString;
  /** Article image */
  ArticleImage?: Record<string, unknown>;
  /** Article actions */
  ArticleActions?: Record<string, unknown>[];
}

/**
 * News collection.
 */
export interface News {
  /** List of news articles */
  NewsArticles?: NewsArticle[];
}

/**
 * Season calendar entry.
 */
export interface SeasonCalendarEntry {
  /** CSR season file path */
  CsrSeasonFilePath?: string;
  /** Operation track path */
  OperationTrackPath?: string;
  /** Season metadata */
  SeasonMetadata?: string;
  /** Reward track path */
  RewardTrackPath?: string;
  /** Start date (ISO 8601) */
  StartDate?: string;
  /** End date (ISO 8601) */
  EndDate?: string;
}

/**
 * Season calendar.
 */
export interface SeasonCalendar {
  /** List of seasons */
  Seasons?: SeasonCalendarEntry[];
  /** List of events */
  Events?: SeasonCalendarEntry[];
  /** Career rank entry */
  CareerRank?: SeasonCalendarEntry;
}

/**
 * Matches privacy settings.
 */
export interface MatchesPrivacy {
  /** Matchmade games privacy setting */
  MatchmadeGames?: number;
  /** Other games privacy setting */
  OtherGames?: number;
}

/**
 * Player daily custom experience.
 */
export interface PlayerDailyCustomExperience {
  /** Daily experience earned */
  DailyExperience?: number;
}

/**
 * Player giveaways container.
 */
export interface PlayerGiveaways {
  /** Giveaway results */
  GiveawayResults?: unknown[];
}
