/**
 * Article block attribute.
 */
export interface ArticleAttribute {
  /** Attribute identifier */
  id?: number;
  /** Size slug */
  sizeSlug?: string;
  /** Link destination */
  linkDestination?: string;
  /** URL */
  url?: string;
  /** Type */
  type?: string;
  /** Provider name slug */
  providerNameSlug?: string;
  /** Class name */
  className?: string;
}

/**
 * Article content block.
 */
export interface ArticleBlock {
  /** Block name */
  blockName?: string;
  /** Block attributes */
  attrs?: ArticleAttribute[];
  /** Inner blocks */
  innerBlocks?: string[];
  /** Inner HTML content */
  innerHTML?: string;
  /** Inner content */
  innerContent?: string[];
}

/**
 * Waypoint article.
 */
export interface Article {
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
  /** Full content (HTML) */
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
  /** Content blocks */
  blocks?: ArticleBlock[];
  /** Publish date (ISO 8601) */
  publishDate?: string;
  /** Medium featured image URL */
  featuredImageUriMedium?: string;
  /** Medium featured image alt */
  featuredImageAltMedium?: string;
  /** Small featured image URL */
  featuredImageUriSmall?: string;
  /** Small featured image alt */
  featuredImageAltSmall?: string;
}

/**
 * Article category.
 */
export interface ArticleCategory {
  /** Category identifier */
  id?: number;
  /** Category name */
  name?: string;
  /** Category description */
  description?: string;
  /** URL slug */
  slug?: string;
  /** Number of articles */
  count?: number;
  /** Parent category ID (0 for top-level) */
  parent?: number;
}
