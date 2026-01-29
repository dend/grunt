/**
 * Type of player in a match.
 */
export const PlayerType = {
  /** Human player */
  Human: 'Human',
  /** Bot/AI player */
  Bot: 'Bot',
} as const;

/**
 * Type representing valid player type values.
 */
export type PlayerType = (typeof PlayerType)[keyof typeof PlayerType];
