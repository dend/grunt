import type { PlayerType } from '../enums/player-type';
import type { Outcome } from '../enums/outcome';
import type { Stats } from './stats';

/**
 * Bot-specific attributes.
 */
export interface BotAttributes {
  /** Bot difficulty */
  difficulty?: number;
  /** Bot skill level */
  skillLevel?: number;
}

/**
 * Participation information for a player.
 */
export interface ParticipationInfo {
  /** Time played (ISO 8601 duration) */
  timePlayed?: string;
  /** Whether player was present at start */
  presentAtStart?: boolean;
  /** Whether player was present at end */
  presentAtEnd?: boolean;
  /** Whether player joined mid-match */
  joinedInProgress?: boolean;
  /** When player joined (ISO 8601) */
  joinedAt?: string;
  /** When player left (ISO 8601, if applicable) */
  leftAt?: string;
  /** First joined time (ISO 8601) */
  firstJoinedTime?: string;
  /** Last joined time (ISO 8601) */
  lastJoinedTime?: string;
  /** Whether player left before completion */
  leftInProgress?: boolean;
  /** Confirmed participation */
  confirmedParticipation?: boolean;
}

/**
 * Player's stats for a specific team.
 */
export interface PlayerTeamStat {
  /** Team identifier */
  teamId?: number;
  /** Stats for this team */
  stats?: Stats;
}

/**
 * Player information from a match.
 */
export interface Player {
  /** Player identifier (format: "xuid(XUID)") */
  playerId?: string;
  /** Type of player (Human or Bot) */
  playerType?: PlayerType;
  /** Bot attributes (only if playerType is Bot) */
  botAttributes?: BotAttributes;
  /** Last team the player was on */
  lastTeamId?: number;
  /** Match outcome for this player */
  outcome?: Outcome;
  /** Final rank/placement */
  rank?: number;
  /** Participation details */
  participationInfo?: ParticipationInfo;
  /** Stats broken down by team */
  playerTeamStats?: PlayerTeamStat[];
}

/**
 * Team information from a match.
 */
export interface Team {
  /** Team identifier */
  teamId?: number;
  /** Match outcome for this team */
  outcome?: number;
  /** Final rank/placement */
  rank?: number;
  /** Team stats */
  stats?: Stats;
}
