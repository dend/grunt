/**
 * Competitive Skill Rank (CSR) information.
 *
 * CSR is the ranked rating system in Halo Infinite multiplayer.
 */
export interface Csr {
  /** Current CSR value */
  Value?: number;
  /** Measurement matches remaining before placement */
  MeasurementMatchesRemaining?: number;
  /** Current tier name (e.g., "Diamond", "Onyx") */
  Tier?: string;
  /** CSR value at start of current tier */
  TierStart?: number;
  /** Current sub-tier within the tier (1-6) */
  SubTier?: number;
  /** Next tier name */
  NextTier?: string;
  /** CSR value at start of next tier */
  NextTierStart?: number;
  /** Next sub-tier */
  NextSubTier?: number;
  /** Initial number of placement matches required */
  InitialMeasurementMatches?: number;
  /** Initial demotion protection matches */
  InitialDemotionProtectionMatches?: number;
  /** Remaining demotion protection matches */
  DemotionProtectionMatchesRemaining?: number;
}

/**
 * Skill result from a match.
 */
export interface SkillResult {
  /** Result data */
  Result?: unknown;
}

/**
 * Container for match skill results.
 */
export interface MatchSkillInfo {
  /** Skill results for each player */
  Value?: SkillResult[];
}

/**
 * Playlist CSR container for a single playlist.
 */
export interface PlaylistCsrContainer {
  /** Current CSR */
  Current?: Csr;
  /** Season max CSR */
  SeasonMax?: Csr;
  /** All-time max CSR */
  AllTimeMax?: Csr;
}

/**
 * Container for playlist CSR results.
 */
export interface PlaylistCsrResultContainer {
  /** CSR results */
  Value?: unknown[];
}
