import type { DisplayString } from '../economy/inventory';

/**
 * News article.
 */
export interface NewsArticle {
  /** Article identifier */
  Id?: number;
  /** Featured image URL */
  FeaturedImageUri?: string;
  /** Featured image alt text */
  FeaturedImageAlt?: string;
  /** Article title */
  Title?: string;
  /** Article subtitle */
  Subtitle?: string;
  /** Article content (HTML) */
  Content?: string;
  /** Short excerpt */
  Excerpt?: string;
  /** URL slug */
  Slug?: string;
  /** Creator slug */
  CreatorSlug?: string;
  /** Creator title */
  CreatorTitle?: string;
  /** Category IDs */
  Categories?: number[];
  /** Tags */
  Tags?: string[];
  /** Publish date (ISO 8601) */
  PublishDate?: string;
  /** Medium image URL */
  FeaturedImageUriMedium?: string;
  /** Medium image alt */
  FeaturedImageAltMedium?: string;
  /** Small image URL */
  FeaturedImageUriSmall?: string;
  /** Small image alt */
  FeaturedImageAltSmall?: string;
}

/**
 * News collection.
 */
export interface News {
  /** List of articles */
  Articles?: NewsArticle[];
  /** Total count */
  Total?: number;
}

/**
 * Season calendar entry.
 */
export interface SeasonCalendarEntry {
  /** Season identifier */
  SeasonId?: string;
  /** Season name */
  Name?: DisplayString;
  /** Start date (ISO 8601) */
  StartDate?: string;
  /** End date (ISO 8601) */
  EndDate?: string;
  /** CSR season identifier */
  CsrSeasonId?: string;
}

/**
 * Season calendar.
 */
export interface SeasonCalendar {
  /** List of seasons */
  Seasons?: SeasonCalendarEntry[];
  /** Current season */
  CurrentSeason?: string;
}

/**
 * Matches privacy settings.
 */
export interface MatchesPrivacy {
  /** Player identifier */
  PlayerId?: string;
  /** Privacy setting */
  PrivacySetting?: string;
  /** Whether matches are public */
  MatchesPublic?: boolean;
}

/**
 * Player daily custom experience.
 */
export interface PlayerDailyCustomExperience {
  /** Player identifier */
  PlayerId?: string;
  /** Custom XP remaining */
  RemainingXp?: number;
  /** Custom XP earned today */
  EarnedToday?: number;
  /** Daily limit */
  DailyLimit?: number;
  /** Reset time (ISO 8601) */
  ResetTime?: string;
}

/**
 * Giveaway rewards.
 */
export interface PlayerGiveaways {
  /** List of pending giveaways */
  Giveaways?: GiveawayReward[];
}

/**
 * Individual giveaway reward.
 */
export interface GiveawayReward {
  /** Giveaway identifier */
  Id?: string;
  /** Title */
  Title?: DisplayString;
  /** Items included */
  Items?: import('../economy/inventory').InventoryAmount[];
  /** Claim deadline (ISO 8601) */
  ClaimDeadline?: string;
}
