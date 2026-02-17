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
 * Match skill information for a single player.
 */
export interface PlayerMatchSkill {
  /** Player identifier */
  Id?: string;
  /** CSR before the match */
  PreMatchCsr?: Csr;
  /** CSR after the match */
  PostMatchCsr?: Csr;
  /** Expected CSR at ranking */
  ExpectedRankCsr?: Csr;
  /** Result code */
  ResultCode?: number;
}

/**
 * Container for match skill results.
 */
export interface MatchSkillInfo {
  /** Match identifier */
  MatchId?: string;
  /** Skill results for each player */
  Value?: PlayerMatchSkill[];
}

/**
 * Playlist CSR result for a single player.
 */
export interface PlayerPlaylistCsr {
  /** Player identifier */
  Id?: string;
  /** Current CSR for this playlist */
  Csr?: Csr;
  /** Result code */
  ResultCode?: number;
}

/**
 * Container for playlist CSR results.
 */
export interface PlaylistCsrResultContainer {
  /** Playlist identifier */
  PlaylistId?: string;
  /** Season identifier (if applicable) */
  SeasonId?: string;
  /** CSR results for each player */
  Value?: PlayerPlaylistCsr[];
}
