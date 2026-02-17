/**
 * Core stats that apply to all game modes.
 */
export interface CoreStats {
  /** Total score earned */
  Score?: number;
  /** Personal score (individual contribution) */
  PersonalScore?: number;
  /** Number of rounds won */
  RoundsWon?: number;
  /** Number of rounds lost */
  RoundsLost?: number;
  /** Number of rounds tied */
  RoundsTied?: number;
  /** Total kills */
  Kills?: number;
  /** Total deaths */
  Deaths?: number;
  /** Total assists */
  Assists?: number;
  /** Kill/Death/Assist ratio */
  Kda?: number;
  /** Total suicides */
  Suicides?: number;
  /** Total betrayals (team kills) */
  Betrayals?: number;
  /** Average life duration in seconds */
  AverageLifeDuration?: string;
  /** Grenade kills */
  GrenadeKills?: number;
  /** Headshot kills */
  HeadshotKills?: number;
  /** Melee kills */
  MeleeKills?: number;
  /** Power weapon kills */
  PowerWeaponKills?: number;
  /** Shots fired */
  ShotsFired?: number;
  /** Shots hit */
  ShotsHit?: number;
  /** Accuracy percentage */
  Accuracy?: number;
  /** Damage dealt */
  DamageDealt?: number;
  /** Damage taken */
  DamageTaken?: number;
  /** Callout assists */
  CalloutAssists?: number;
  /** Vehicle destroys */
  VehicleDestroys?: number;
  /** Driver assists */
  DriverAssists?: number;
  /** Hijacks */
  Hijacks?: number;
  /** EMP assists */
  EmpAssists?: number;
  /** Maximum killing spree */
  MaxKillingSpree?: number;
  /** Medals earned */
  Medals?: MedalCount[];
  /** Personal scores breakdown */
  PersonalScores?: PersonalScoreEntry[];
  /** Deprecated Spartan Rank */
  DeprecatedDamageDealt?: number;
  /** Deprecated Spartan Rank */
  DeprecatedDamageTaken?: number;
  /** Spawns */
  Spawns?: number;
  /** Objectives completed */
  ObjectivesCompleted?: number;
}

/**
 * Medal count entry.
 */
export interface MedalCount {
  /** Medal name identifier */
  NameId?: number;
  /** Number of times earned */
  Count?: number;
  /** Total personal score from this medal */
  TotalPersonalScoreAwarded?: number;
}

/**
 * Personal score breakdown entry.
 */
export interface PersonalScoreEntry {
  /** Score type name identifier */
  NameId?: number;
  /** Number of times earned */
  Count?: number;
  /** Total score from this type */
  TotalPersonalScoreAwarded?: number;
}

/**
 * Bomb game mode stats (Assault).
 */
export interface BombStats {
  /** Bombs planted */
  BombsPlanted?: number;
  /** Bombs defused */
  BombsDefused?: number;
  /** Bomb carriers killed */
  BombCarriersKilled?: number;
  /** Time as bomb carrier */
  TimeAsBombCarrier?: string;
}

/**
 * Capture the Flag game mode stats.
 */
export interface CaptureTheFlagStats {
  /** Flag captures */
  FlagCaptures?: number;
  /** Flag capture assists */
  FlagCaptureAssists?: number;
  /** Flag carriers killed */
  FlagCarriersKilled?: number;
  /** Flag grabs */
  FlagGrabs?: number;
  /** Flags returned */
  FlagsReturned?: number;
  /** Flags stolen */
  FlagsStolen?: number;
  /** Time as flag carrier */
  TimeAsFlagCarrier?: string;
  /** Kills as flag carrier */
  KillsAsFlagCarrier?: number;
}

/**
 * Elimination game mode stats.
 */
export interface EliminationStats {
  /** Allies revived */
  AlliesRevived?: number;
  /** Revives denied */
  RevivesDenied?: number;
  /** Eliminations */
  Eliminations?: number;
  /** Elimination assists */
  EliminationAssists?: number;
  /** Times revived */
  TimesRevived?: number;
  /** Rounds survived */
  RoundsSurvived?: number;
  /** Executions */
  Executions?: number;
  /** Last spartans standing */
  LastSpartansStanding?: number;
}

/**
 * Extraction game mode stats.
 */
export interface ExtractionStats {
  /** Extractions initiated */
  ExtractionInitiated?: number;
  /** Extractions completed */
  ExtractionCompleted?: number;
  /** Extractions converted */
  ExtractionConverted?: number;
  /** Extractions denied */
  ExtractionDenied?: number;
  /** Successful extractions */
  SuccessfulExtractions?: number;
  /** Seconds converting */
  SecondsConverting?: number;
}

/**
 * Infection game mode stats.
 */
export interface InfectionStats {
  /** Infected killed */
  InfectedKilled?: number;
  /** Spartans infected */
  SpartansInfected?: number;
  /** Spartans infected as last spartan */
  SpartansInfectedAsLastSpartan?: number;
  /** Infected killed as last spartan */
  InfectedKilledAsLastSpartan?: number;
  /** Time as last spartan */
  TimeAsLastSpartan?: string;
  /** Time as survivor */
  TimeAsSurvivor?: string;
  /** Rounds as survivor */
  RoundsAsSurvivor?: number;
  /** Rounds as infected */
  RoundsAsInfected?: number;
  /** Rounds survived as spartan */
  RoundsSurvivedAsSpartan?: number;
  /** Rounds survived as last spartan */
  RoundsSurvivedAsLastSpartan?: number;
  /** Kills as last spartan */
  KillsAsLastSpartan?: number;
  /** Alpha infections */
  AlphaInfections?: number;
}

/**
 * Oddball game mode stats.
 */
export interface OddballStats {
  /** Time with ball */
  TimeWithBall?: string;
  /** Ball carriers killed */
  BallCarriersKilled?: number;
  /** Kills as ball carrier */
  KillsAsBallCarrier?: number;
  /** Ball grabs */
  BallGrabs?: number;
  /** Longest time with ball */
  LongestTimeWithBall?: string;
}

/**
 * Zones game mode stats (Land Grab, Strongholds, etc.).
 */
export interface ZonesStats {
  /** Zones captured */
  ZoneCaptures?: number;
  /** Zone defensive kills */
  ZoneDefensiveKills?: number;
  /** Zone offensive kills */
  ZoneOffensiveKills?: number;
  /** Zone securing kills */
  ZoneSecuringKills?: number;
  /** Zone occupation time */
  ZoneOccupationTime?: string;
  /** Zones scored */
  ZonesScored?: number;
  /** Zone scoring ticks */
  ZoneScoringTicks?: number;
}

/**
 * Stockpile game mode stats.
 */
export interface StockpileStats {
  /** Power seeds deposited */
  PowerSeedsDeposited?: number;
  /** Power seeds stolen */
  PowerSeedsStolen?: number;
  /** Kill as power seed carrier */
  KillsAsPowerSeedCarrier?: number;
  /** Power seed carriers killed */
  PowerSeedCarriersKilled?: number;
  /** Time as power seed carrier */
  TimeAsPowerSeedCarrier?: string;
}

/**
 * VIP game mode stats.
 */
export interface VipStats {
  /** VIP kills */
  VipKills?: number;
  /** Kills as VIP */
  KillsAsVip?: number;
  /** Time as VIP */
  TimeAsVip?: string;
}

/**
 * PvE (Firefight) stats.
 */
export interface PveStats {
  /** Boss kills */
  BossKills?: number;
  /** Emplacement kills */
  EmplacementKills?: number;
  /** Enemy vehicle kills */
  EnemyVehicleKills?: number;
  /** Wave survived */
  WavesSurvived?: number;
  /** Last spartan standing */
  LastSpartanStanding?: boolean;
}

/**
 * PvP stats (used in some game modes).
 */
export interface PvpStats {
  /** Spartan kills */
  SpartanKills?: number;
}

/**
 * Container for all mode-specific stats.
 */
export interface Stats {
  /** Core stats (applies to all modes) */
  CoreStats?: CoreStats;
  /** Bomb stats */
  BombStats?: BombStats;
  /** CTF stats */
  CaptureTheFlagStats?: CaptureTheFlagStats;
  /** Elimination stats */
  EliminationStats?: EliminationStats;
  /** Extraction stats */
  ExtractionStats?: ExtractionStats;
  /** Infection stats */
  InfectionStats?: InfectionStats;
  /** Oddball stats */
  OddballStats?: OddballStats;
  /** Zones stats */
  ZonesStats?: ZonesStats;
  /** Stockpile stats */
  StockpileStats?: StockpileStats;
  /** VIP stats */
  VipStats?: VipStats;
  /** PvE stats */
  PveStats?: PveStats;
  /** PvP stats */
  PvpStats?: PvpStats;
}
