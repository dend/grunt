/**
 * Match types for querying match history.
 *
 * Used with StatsModule.getMatchHistory() to filter matches.
 */
export const MatchType = {
  /** Return all match types */
  All: 'all',
  /** Return only matchmaking matches */
  Matchmaking: 'matchmaking',
  /** Return only custom games */
  Custom: 'custom',
  /** Return only local/offline matches */
  Local: 'local',
} as const;

/**
 * Type representing valid match type values.
 */
export type MatchType = (typeof MatchType)[keyof typeof MatchType];
