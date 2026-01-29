import type { DisplayString } from '../economy/inventory';

/**
 * News article.
 */
export interface NewsArticle {
  /** Article identifier */
  id?: number;
  /** Featured image URL */
  featuredImageUri?: string;
  /** Featured image alt text */
  featuredImageAlt?: string;
  /** Article title */
  title?: string;
  /** Article subtitle */
  subtitle?: string;
  /** Article content (HTML) */
  content?: string;
  /** Short excerpt */
  excerpt?: string;
  /** URL slug */
  slug?: string;
  /** Creator slug */
  creatorSlug?: string;
  /** Creator title */
  creatorTitle?: string;
  /** Category IDs */
  categories?: number[];
  /** Tags */
  tags?: string[];
  /** Publish date (ISO 8601) */
  publishDate?: string;
  /** Medium image URL */
  featuredImageUriMedium?: string;
  /** Medium image alt */
  featuredImageAltMedium?: string;
  /** Small image URL */
  featuredImageUriSmall?: string;
  /** Small image alt */
  featuredImageAltSmall?: string;
}

/**
 * News collection.
 */
export interface News {
  /** List of articles */
  articles?: NewsArticle[];
  /** Total count */
  total?: number;
}

/**
 * Season calendar entry.
 */
export interface SeasonCalendarEntry {
  /** Season identifier */
  seasonId?: string;
  /** Season name */
  name?: DisplayString;
  /** Start date (ISO 8601) */
  startDate?: string;
  /** End date (ISO 8601) */
  endDate?: string;
  /** CSR season identifier */
  csrSeasonId?: string;
}

/**
 * Season calendar.
 */
export interface SeasonCalendar {
  /** List of seasons */
  seasons?: SeasonCalendarEntry[];
  /** Current season */
  currentSeason?: string;
}

/**
 * Matches privacy settings.
 */
export interface MatchesPrivacy {
  /** Player identifier */
  playerId?: string;
  /** Privacy setting */
  privacySetting?: string;
  /** Whether matches are public */
  matchesPublic?: boolean;
}

/**
 * Player daily custom experience.
 */
export interface PlayerDailyCustomExperience {
  /** Player identifier */
  playerId?: string;
  /** Custom XP remaining */
  remainingXp?: number;
  /** Custom XP earned today */
  earnedToday?: number;
  /** Daily limit */
  dailyLimit?: number;
  /** Reset time (ISO 8601) */
  resetTime?: string;
}

/**
 * Giveaway rewards.
 */
export interface PlayerGiveaways {
  /** List of pending giveaways */
  giveaways?: GiveawayReward[];
}

/**
 * Individual giveaway reward.
 */
export interface GiveawayReward {
  /** Giveaway identifier */
  id?: string;
  /** Title */
  title?: DisplayString;
  /** Items included */
  items?: import('../economy/inventory').InventoryAmount[];
  /** Claim deadline (ISO 8601) */
  claimDeadline?: string;
}
