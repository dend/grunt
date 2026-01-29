/**
 * Competitive Skill Rank (CSR) information.
 *
 * CSR is the ranked rating system in Halo Infinite multiplayer.
 */
export interface Csr {
  /** Current CSR value */
  value?: number;
  /** Measurement matches remaining before placement */
  measurementMatchesRemaining?: number;
  /** Current tier name (e.g., "Diamond", "Onyx") */
  tier?: string;
  /** CSR value at start of current tier */
  tierStart?: number;
  /** Current sub-tier within the tier (1-6) */
  subTier?: number;
  /** Next tier name */
  nextTier?: string;
  /** CSR value at start of next tier */
  nextTierStart?: number;
  /** Next sub-tier */
  nextSubTier?: number;
  /** Initial number of placement matches required */
  initialMeasurementMatches?: number;
  /** Initial demotion protection matches */
  initialDemotionProtectionMatches?: number;
  /** Remaining demotion protection matches */
  demotionProtectionMatchesRemaining?: number;
}

/**
 * Match skill information for a single player.
 */
export interface PlayerMatchSkill {
  /** Player identifier */
  id?: string;
  /** CSR before the match */
  preMatchCsr?: Csr;
  /** CSR after the match */
  postMatchCsr?: Csr;
  /** Expected CSR at ranking */
  expectedRankCsr?: Csr;
  /** Result code */
  resultCode?: number;
}

/**
 * Container for match skill results.
 */
export interface MatchSkillInfo {
  /** Match identifier */
  matchId?: string;
  /** Skill results for each player */
  value?: PlayerMatchSkill[];
}

/**
 * Playlist CSR result for a single player.
 */
export interface PlayerPlaylistCsr {
  /** Player identifier */
  id?: string;
  /** Current CSR for this playlist */
  csr?: Csr;
  /** Result code */
  resultCode?: number;
}

/**
 * Container for playlist CSR results.
 */
export interface PlaylistCsrResultContainer {
  /** Playlist identifier */
  playlistId?: string;
  /** Season identifier (if applicable) */
  seasonId?: string;
  /** CSR results for each player */
  value?: PlayerPlaylistCsr[];
}
