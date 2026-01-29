import { WaypointModuleBase } from '../base/waypoint-module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import type { Article, ArticleCategory } from '../../models/waypoint';

/**
 * Articles response container.
 */
export interface ArticlesResponse {
  /** List of articles */
  articles?: Article[];
  /** Total count */
  total?: number;
  /** Page number */
  page?: number;
  /** Per page count */
  perPage?: number;
}

/**
 * Content module for articles and news APIs.
 *
 * @example
 * ```typescript
 * // Get articles
 * const articles = await client.content.getArticles(1, 10);
 *
 * // Get article categories
 * const categories = await client.content.getCategories();
 * ```
 */
export class ContentModule extends WaypointModuleBase {
  constructor(client: ClientBase) {
    super(client);
  }

  /**
   * Get articles from Halo Waypoint.
   *
   * @param page - Page number (1-based)
   * @param perPage - Articles per page
   * @param category - Optional category filter
   * @returns Articles response
   */
  getArticles(
    page: number = 1,
    perPage: number = 10,
    category?: number
  ): Promise<HaloApiResult<ArticlesResponse>> {
    const categoryParam = category !== undefined ? `&category=${category}` : '';
    return this.get<ArticlesResponse>(
      `/hi/articles?page=${page}&per_page=${perPage}${categoryParam}`,
      { useSpartanToken: false }
    );
  }

  /**
   * Get a specific article by slug.
   *
   * @param slug - Article URL slug
   * @returns Article details
   */
  getArticle(slug: string): Promise<HaloApiResult<Article>> {
    this.assertNotEmpty(slug, 'slug');
    return this.get<Article>(`/hi/articles/${encodeURIComponent(slug)}`, {
      useSpartanToken: false,
    });
  }

  /**
   * Get article categories.
   *
   * @returns List of categories
   */
  getCategories(): Promise<HaloApiResult<ArticleCategory[]>> {
    return this.get<ArticleCategory[]>('/hi/articles/categories', {
      useSpartanToken: false,
    });
  }
}
