/**
 * Core stats that apply to all game modes.
 */
export interface CoreStats {
  /** Total score earned */
  score?: number;
  /** Personal score (individual contribution) */
  personalScore?: number;
  /** Number of rounds won */
  roundsWon?: number;
  /** Number of rounds lost */
  roundsLost?: number;
  /** Number of rounds tied */
  roundsTied?: number;
  /** Total kills */
  kills?: number;
  /** Total deaths */
  deaths?: number;
  /** Total assists */
  assists?: number;
  /** Kill/Death/Assist ratio */
  kda?: number;
  /** Total suicides */
  suicides?: number;
  /** Total betrayals (team kills) */
  betrayals?: number;
  /** Average life duration in seconds */
  averageLifeDuration?: string;
  /** Grenade kills */
  grenadeKills?: number;
  /** Headshot kills */
  headshotKills?: number;
  /** Melee kills */
  meleeKills?: number;
  /** Power weapon kills */
  powerWeaponKills?: number;
  /** Shots fired */
  shotsFired?: number;
  /** Shots hit */
  shotsHit?: number;
  /** Accuracy percentage */
  accuracy?: number;
  /** Damage dealt */
  damageDealt?: number;
  /** Damage taken */
  damageTaken?: number;
  /** Callout assists */
  calloutAssists?: number;
  /** Vehicle destroys */
  vehicleDestroys?: number;
  /** Driver assists */
  driverAssists?: number;
  /** Hijacks */
  hijacks?: number;
  /** EMP assists */
  empAssists?: number;
  /** Maximum killing spree */
  maxKillingSpree?: number;
  /** Medals earned */
  medals?: MedalCount[];
  /** Personal scores breakdown */
  personalScores?: PersonalScoreEntry[];
  /** Deprecated Spartan Rank */
  deprecatedDamageDealt?: number;
  /** Deprecated Spartan Rank */
  deprecatedDamageTaken?: number;
  /** Spawns */
  spawns?: number;
  /** Objectives completed */
  objectivesCompleted?: number;
}

/**
 * Medal count entry.
 */
export interface MedalCount {
  /** Medal name identifier */
  nameId?: number;
  /** Number of times earned */
  count?: number;
  /** Total personal score from this medal */
  totalPersonalScoreAwarded?: number;
}

/**
 * Personal score breakdown entry.
 */
export interface PersonalScoreEntry {
  /** Score type name identifier */
  nameId?: number;
  /** Number of times earned */
  count?: number;
  /** Total score from this type */
  totalPersonalScoreAwarded?: number;
}

/**
 * Bomb game mode stats (Assault).
 */
export interface BombStats {
  /** Bombs planted */
  bombsPlanted?: number;
  /** Bombs defused */
  bombsDefused?: number;
  /** Bomb carriers killed */
  bombCarriersKilled?: number;
  /** Time as bomb carrier */
  timeAsBombCarrier?: string;
}

/**
 * Capture the Flag game mode stats.
 */
export interface CaptureTheFlagStats {
  /** Flag captures */
  flagCaptures?: number;
  /** Flag capture assists */
  flagCaptureAssists?: number;
  /** Flag carriers killed */
  flagCarriersKilled?: number;
  /** Flag grabs */
  flagGrabs?: number;
  /** Flags returned */
  flagsReturned?: number;
  /** Flags stolen */
  flagsStolen?: number;
  /** Time as flag carrier */
  timeAsFlagCarrier?: string;
  /** Kills as flag carrier */
  killsAsFlagCarrier?: number;
}

/**
 * Elimination game mode stats.
 */
export interface EliminationStats {
  /** Allies revived */
  alliesRevived?: number;
  /** Revives denied */
  revivesDenied?: number;
  /** Eliminations */
  eliminations?: number;
  /** Elimination assists */
  eliminationAssists?: number;
  /** Times revived */
  timesRevived?: number;
  /** Rounds survived */
  roundsSurvived?: number;
  /** Executions */
  executions?: number;
  /** Last spartans standing */
  lastSpartansStanding?: number;
}

/**
 * Extraction game mode stats.
 */
export interface ExtractionStats {
  /** Extractions initiated */
  extractionInitiated?: number;
  /** Extractions completed */
  extractionCompleted?: number;
  /** Extractions converted */
  extractionConverted?: number;
  /** Extractions denied */
  extractionDenied?: number;
  /** Successful extractions */
  successfulExtractions?: number;
  /** Seconds converting */
  secondsConverting?: number;
}

/**
 * Infection game mode stats.
 */
export interface InfectionStats {
  /** Infected killed */
  infectedKilled?: number;
  /** Spartans infected */
  spartansInfected?: number;
  /** Spartans infected as last spartan */
  spartansInfectedAsLastSpartan?: number;
  /** Infected killed as last spartan */
  infectedKilledAsLastSpartan?: number;
  /** Time as last spartan */
  timeAsLastSpartan?: string;
  /** Time as survivor */
  timeAsSurvivor?: string;
  /** Rounds as survivor */
  roundsAsSurvivor?: number;
  /** Rounds as infected */
  roundsAsInfected?: number;
  /** Rounds survived as spartan */
  roundsSurvivedAsSpartan?: number;
  /** Rounds survived as last spartan */
  roundsSurvivedAsLastSpartan?: number;
  /** Kills as last spartan */
  killsAsLastSpartan?: number;
  /** Alpha infections */
  alphaInfections?: number;
}

/**
 * Oddball game mode stats.
 */
export interface OddballStats {
  /** Time with ball */
  timeWithBall?: string;
  /** Ball carriers killed */
  ballCarriersKilled?: number;
  /** Kills as ball carrier */
  killsAsBallCarrier?: number;
  /** Ball grabs */
  ballGrabs?: number;
  /** Longest time with ball */
  longestTimeWithBall?: string;
}

/**
 * Zones game mode stats (Land Grab, Strongholds, etc.).
 */
export interface ZonesStats {
  /** Zones captured */
  zoneCaptures?: number;
  /** Zone defensive kills */
  zoneDefensiveKills?: number;
  /** Zone offensive kills */
  zoneOffensiveKills?: number;
  /** Zone securing kills */
  zoneSecuringKills?: number;
  /** Zone occupation time */
  zoneOccupationTime?: string;
  /** Zones scored */
  zonesScored?: number;
  /** Zone scoring ticks */
  zoneScoringTicks?: number;
}

/**
 * Stockpile game mode stats.
 */
export interface StockpileStats {
  /** Power seeds deposited */
  powerSeedsDeposited?: number;
  /** Power seeds stolen */
  powerSeedsStolen?: number;
  /** Kill as power seed carrier */
  killsAsPowerSeedCarrier?: number;
  /** Power seed carriers killed */
  powerSeedCarriersKilled?: number;
  /** Time as power seed carrier */
  timeAsPowerSeedCarrier?: string;
}

/**
 * VIP game mode stats.
 */
export interface VipStats {
  /** VIP kills */
  vipKills?: number;
  /** Kills as VIP */
  killsAsVip?: number;
  /** Time as VIP */
  timeAsVip?: string;
}

/**
 * PvE (Firefight) stats.
 */
export interface PveStats {
  /** Boss kills */
  bossKills?: number;
  /** Emplacement kills */
  emplacementKills?: number;
  /** Enemy vehicle kills */
  enemyVehicleKills?: number;
  /** Wave survived */
  wavesSurvived?: number;
  /** Last spartan standing */
  lastSpartanStanding?: boolean;
}

/**
 * PvP stats (used in some game modes).
 */
export interface PvpStats {
  /** Spartan kills */
  spartanKills?: number;
}

/**
 * Container for all mode-specific stats.
 */
export interface Stats {
  /** Core stats (applies to all modes) */
  coreStats?: CoreStats;
  /** Bomb stats */
  bombStats?: BombStats;
  /** CTF stats */
  captureTheFlagStats?: CaptureTheFlagStats;
  /** Elimination stats */
  eliminationStats?: EliminationStats;
  /** Extraction stats */
  extractionStats?: ExtractionStats;
  /** Infection stats */
  infectionStats?: InfectionStats;
  /** Oddball stats */
  oddballStats?: OddballStats;
  /** Zones stats */
  zonesStats?: ZonesStats;
  /** Stockpile stats */
  stockpileStats?: StockpileStats;
  /** VIP stats */
  vipStats?: VipStats;
  /** PvE stats */
  pveStats?: PveStats;
  /** PvP stats */
  pvpStats?: PvpStats;
}
