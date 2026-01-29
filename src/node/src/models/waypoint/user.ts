/**
 * User gamerpic information.
 */
export interface Gamerpic {
  /** Small gamerpic URL */
  small?: string;
  /** Medium gamerpic URL */
  medium?: string;
  /** Large gamerpic URL */
  large?: string;
  /** Extra large gamerpic URL */
  xlarge?: string;
}

/**
 * User profile information.
 */
export interface UserProfile {
  /** Xbox User ID */
  xuid?: string;
  /** Gamertag */
  gamertag?: string;
  /** Gamerpic URLs */
  gamerpic?: Gamerpic;
}

/**
 * User email settings.
 */
export interface UserEmail {
  /** Email address */
  emailAddress?: string;
  /** Whether email is verified */
  verified?: boolean;
}

/**
 * User notification settings.
 */
export interface UserNotificationsSettings {
  /** Halo Insider enrollment status */
  insider?: boolean;
}

/**
 * User settings.
 */
export interface UserSettings {
  /** Xbox User ID */
  xuid?: string;
  /** Email settings */
  email?: UserEmail;
  /** Notification settings */
  notifications?: UserNotificationsSettings;
}

/**
 * Service award.
 */
export interface ServiceAward {
  /** Award identifier */
  id?: number;
  /** Image URL */
  imageUri?: string;
  /** Image alt text */
  imageAlt?: string;
  /** Award title */
  title?: string;
  /** Short description */
  excerpt?: string;
  /** URL slug */
  slug?: string;
}

/**
 * Service award snapshot.
 */
export interface ServiceAwardSnapshot {
  /** Featured service awards */
  featuredAwards?: ServiceAward[];
  /** All earned awards */
  earnedAwards?: ServiceAward[];
}

/**
 * Notification data.
 */
export interface NotificationData {
  /** Notification type */
  type?: string;
  /** Item name */
  itemName?: string;
  /** Template identifier */
  templateId?: string;
  /** Coupon code (if applicable) */
  couponCode?: string;
  /** Details URL */
  detailsUrl?: string;
}

/**
 * User notification.
 */
export interface Notification {
  /** Xbox User ID */
  xuid?: string;
  /** Notification identifier */
  notificationId?: string;
  /** Creation date (ISO 8601) */
  createdDate?: string;
  /** Notification data */
  data?: NotificationData;
}
