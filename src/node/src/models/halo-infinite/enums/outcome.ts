/**
 * Match outcome for a player or team.
 */
export const Outcome = {
  /** Won the match */
  Win: 'Win',
  /** Lost the match */
  Loss: 'Loss',
  /** Match ended in a tie */
  Tie: 'Tie',
  /** Did not finish the match */
  DidNotFinish: 'DidNotFinish',
} as const;

/**
 * Type representing valid outcome values.
 */
export type Outcome = (typeof Outcome)[keyof typeof Outcome];
