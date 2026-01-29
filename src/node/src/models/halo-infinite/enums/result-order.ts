/**
 * Sort order for search results.
 */
export const ResultOrder = {
  /** Sort in ascending order */
  Ascending: 'asc',
  /** Sort in descending order */
  Descending: 'desc',
} as const;

/**
 * Type representing valid result order values.
 */
export type ResultOrder = (typeof ResultOrder)[keyof typeof ResultOrder];
