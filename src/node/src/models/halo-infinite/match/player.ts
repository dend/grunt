import type { PlayerType } from '../enums/player-type';
import type { Outcome } from '../enums/outcome';
import type { Stats } from './stats';

/**
 * Bot-specific attributes.
 */
export interface BotAttributes {
  /** Bot difficulty */
  Difficulty?: number;
}

/**
 * Participation information for a player.
 */
export interface ParticipationInfo {
  /** When player first joined (ISO 8601) */
  FirstJoinedTime?: string;
  /** When player last left (ISO 8601) */
  LastLeaveTime?: string;
  /** Whether player was present at beginning */
  PresentAtBeginning?: boolean;
  /** Whether player joined mid-match */
  JoinedInProgress?: boolean;
  /** Whether player left before completion */
  LeftInProgress?: boolean;
  /** Whether player was present at completion */
  PresentAtCompletion?: boolean;
  /** Time played (ISO 8601 duration) */
  TimePlayed?: string;
  /** Confirmed participation */
  ConfirmedParticipation?: unknown;
}

/**
 * Player's stats for a specific team.
 */
export interface PlayerTeamStat {
  /** Team identifier */
  TeamId?: number;
  /** Stats for this team */
  Stats?: Stats;
}

/**
 * Player information from a match.
 */
export interface Player {
  /** Player identifier (format: "xuid(XUID)") */
  PlayerId?: string;
  /** Type of player (Human or Bot) */
  PlayerType?: PlayerType;
  /** Bot attributes (only if playerType is Bot) */
  BotAttributes?: BotAttributes;
  /** Last team the player was on */
  LastTeamId?: number;
  /** Match outcome for this player */
  Outcome?: Outcome;
  /** Final rank/placement */
  Rank?: number;
  /** Participation details */
  ParticipationInfo?: ParticipationInfo;
  /** Stats broken down by team */
  PlayerTeamStats?: PlayerTeamStat[];
}

/**
 * Team information from a match.
 */
export interface Team {
  /** Team identifier */
  TeamId?: number;
  /** Match outcome for this team */
  Outcome?: number;
  /** Final rank/placement */
  Rank?: number;
  /** Team stats */
  Stats?: Stats;
}
