import type { Medal } from '../misc/medal';

/**
 * Core stats that apply to all game modes.
 */
export interface CoreStats {
  /** Total score earned */
  Score?: number;
  /** Objectives completed */
  ObjectivesCompleted?: number;
  /** Number of spawns */
  Spawns?: number;
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
  /** Average Kill/Death/Assist ratio (used in service records) */
  AverageKDA?: number;
  /** Kill/Death/Assist ratio (used in match stats) */
  KDA?: number;
  /** Total suicides */
  Suicides?: number;
  /** Total betrayals (team kills) */
  Betrayals?: number;
  /** Average life duration (ISO 8601 duration) */
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
  Medals?: Medal[];
  /** Personal scores breakdown */
  PersonalScores?: PersonalScore[];
  /** @deprecated No longer included in the API */
  DeprecatedDamageDealt?: number;
  /** @deprecated No longer included in the API */
  DeprecatedDamageTaken?: number;
}

/**
 * Personal score breakdown entry.
 */
export interface PersonalScore {
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
  /** Bomb carriers killed */
  BombCarriersKilled?: number;
  /** Bomb defusals */
  BombDefusals?: number;
  /** Bomb defusers killed */
  BombDefusersKilled?: number;
  /** Bomb detonations */
  BombDetonations?: number;
  /** Bomb pick-ups */
  BombPickUps?: number;
  /** Bomb plants */
  BombPlants?: number;
  /** Bomb returns */
  BombReturns?: number;
  /** Kills as bomb carrier */
  KillsAsBombCarrier?: number;
  /** Time as bomb carrier (ISO 8601 duration) */
  TimeAsBombCarrier?: string;
}

/**
 * Capture the Flag game mode stats.
 */
export interface CaptureTheFlagStats {
  /** Flag capture assists */
  FlagCaptureAssists?: number;
  /** Flag captures */
  FlagCaptures?: number;
  /** Flag carriers killed */
  FlagCarriersKilled?: number;
  /** Flag grabs */
  FlagGrabs?: number;
  /** Flag returners killed */
  FlagReturnersKilled?: number;
  /** Flags returned */
  FlagReturns?: number;
  /** Flag secures */
  FlagSecures?: number;
  /** Flags stolen */
  FlagSteals?: number;
  /** Kills as flag carrier */
  KillsAsFlagCarrier?: number;
  /** Kills as flag returner */
  KillsAsFlagReturner?: number;
  /** Time as flag carrier (ISO 8601 duration) */
  TimeAsFlagCarrier?: string;
}

/**
 * Elimination game mode stats.
 */
export interface EliminationStats {
  /** Allies revived */
  AlliesRevived?: number;
  /** Elimination assists */
  EliminationAssists?: number;
  /** Eliminations */
  Eliminations?: number;
  /** Enemy revives denied */
  EnemyRevivesDenied?: number;
  /** Executions */
  Executions?: number;
  /** Kills as last player standing */
  KillsAsLastPlayerStanding?: number;
  /** Last players standing killed */
  LastPlayersStandingKilled?: number;
  /** Rounds survived */
  RoundsSurvived?: number;
  /** Times revived by ally */
  TimesRevivedByAlly?: number;
  /** Lives remaining */
  LivesRemaining?: number;
  /** Elimination order */
  EliminationOrder?: number;
}

/**
 * Extraction game mode stats.
 */
export interface ExtractionStats {
  /** Extraction conversions completed */
  ExtractionConversionsCompleted?: number;
  /** Extraction conversions denied */
  ExtractionConversionsDenied?: number;
  /** Extraction initiations completed */
  ExtractionInitiationsCompleted?: number;
  /** Extraction initiations denied */
  ExtractionInitiationsDenied?: number;
  /** Successful extractions */
  SuccessfulExtractions?: number;
}

/**
 * Infection game mode stats.
 */
export interface InfectionStats {
  /** Humans infected */
  HumansInfected?: number;
  /** Humans infected as alpha zombie */
  HumansInfectedAsAlpha?: number;
  /** Last humans standing infected */
  LastHumansStandingInfected?: number;
  /** Zombies killed */
  ZombiesKilled?: number;
  /** Alpha zombies killed */
  AlphaZombiesKilled?: number;
  /** Kills as last human standing */
  KillsAsLastHumanStanding?: number;
  /** Rounds survived as human */
  RoundsSurvivedAsHuman?: number;
  /** Rounds survived as last human standing */
  RoundsSurvivedAsLastHumanStanding?: number;
  /** Rounds as last human standing */
  RoundsAsLastHumanStanding?: number;
  /** Time as last human standing (ISO 8601 duration) */
  TimeAsLastHumanStanding?: string;
  /** Rounds as alpha zombie */
  RoundsAsAlphaZombie?: number;
  /** Rounds finished as zombie */
  RoundsFinishedAsZombie?: number;
}

/**
 * Oddball game mode stats.
 */
export interface OddballStats {
  /** Kills as skull carrier */
  KillsAsSkullCarrier?: number;
  /** Longest time as skull carrier (ISO 8601 duration) */
  LongestTimeAsSkullCarrier?: string;
  /** Skull carriers killed */
  SkullCarriersKilled?: number;
  /** Skull grabs */
  SkullGrabs?: number;
  /** Time as skull carrier (ISO 8601 duration) */
  TimeAsSkullCarrier?: string;
  /** Skull scoring ticks */
  SkullScoringTicks?: number;
}

/**
 * Zones game mode stats (Land Grab, Strongholds, KOTH).
 */
export interface ZonesStats {
  /** Zones captured */
  ZoneCaptures?: number;
  /** Zone defensive kills */
  ZoneDefensiveKills?: number;
  /** Zone offensive kills */
  ZoneOffensiveKills?: number;
  /** Zones secured */
  ZoneSecures?: number;
  /** Total zone occupation time (ISO 8601 duration) */
  TotalZoneOccupationTime?: string;
  /** Zone scoring ticks */
  ZoneScoringTicks?: number;
}

/**
 * Stockpile game mode stats.
 */
export interface StockpileStats {
  /** Kills as power seed carrier */
  KillsAsPowerSeedCarrier?: number;
  /** Power seeds deposited */
  PowerSeedsDeposited?: number;
  /** Power seeds stolen */
  PowerSeedsStolen?: number;
  /** Power seed carriers killed */
  PowerSeedCarriersKilled?: number;
  /** Time as power seed carrier (ISO 8601 duration) */
  TimeAsPowerSeedCarrier?: string;
  /** Time as power seed driver (ISO 8601 duration) */
  TimeAsPowerSeedDriver?: string;
}

/**
 * VIP game mode stats.
 */
export interface VIPStats {
  // No properties defined yet in the API.
}

/**
 * PvE (Firefight) stats.
 */
export interface PveStats {
  /** Kills */
  Kills?: number;
  /** Deaths */
  Deaths?: number;
  /** Assists */
  Assists?: number;
  /** Kill/Death/Assist ratio */
  KDA?: number;
  /** Marine kills */
  MarineKills?: number;
  /** Grunt kills */
  GruntKills?: number;
  /** Jackal kills */
  JackalKills?: number;
  /** Elite kills */
  EliteKills?: number;
  /** Brute kills */
  BruteKills?: number;
  /** Hunter kills */
  HunterKills?: number;
  /** Skimmer kills */
  SkimmerKills?: number;
  /** Sentinel kills */
  SentinelKills?: number;
  /** Boss kills */
  BossKills?: number;
}

/**
 * PvP stats.
 */
export interface PvpStats {
  /** PvP assists */
  Assists?: number;
  /** PvP deaths */
  Deaths?: number;
  /** PvP KDA */
  KDA?: number;
  /** PvP kills */
  Kills?: number;
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
  VipStats?: VIPStats;
  /** PvE stats */
  PveStats?: PveStats;
  /** PvP stats */
  PvpStats?: PvpStats;
}
