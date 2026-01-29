/**
 * Lifecycle modes for service records and stats.
 *
 * Determines which game mode's statistics to retrieve.
 */
export const LifecycleMode = {
  /** Matchmade multiplayer games */
  Matchmade: 'matchmade',
  /** Custom games */
  Custom: 'custom',
  /** Local/offline games */
  Local: 'local',
} as const;

/**
 * Type representing valid lifecycle mode values.
 */
export type LifecycleMode = (typeof LifecycleMode)[keyof typeof LifecycleMode];
