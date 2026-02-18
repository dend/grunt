/**
 * QoS server information.
 */
export interface Server {
  /** Server region */
  Region?: string;
  /** Server URL */
  ServerUrl?: string;
}

/**
 * Container for lobby presence requests.
 */
export interface LobbyPresenceRequestContainer {
  /** List of Xbox User IDs */
  Xuids?: number[];
}

/**
 * Lobby presence result.
 */
export interface LobbyPresenceResult {
  /** Fireteam details */
  FireteamDetails?: Record<string, unknown>;
  /** Match details */
  MatchDetails?: unknown;
}

/**
 * Container for lobby presence results.
 */
export interface LobbyPresenceContainer {
  /** List of presence results */
  Results?: LobbyPresenceResult[];
}

/**
 * Lobby join handle.
 */
export interface LobbyJoinHandle {
  /** Handle identifier */
  HandleId?: string;
  /** Lobby ID */
  LobbyId?: string;
  /** Handle value */
  Handle?: string;
  /** Expiration time (ISO 8601) */
  ExpiresAt?: string;
}

/**
 * Response when joining a lobby.
 */
export interface JoinLobbyResponse {
  /** Success status */
  Success?: boolean;
  /** Lobby ID */
  LobbyId?: string;
  /** Session details */
  Session?: Record<string, unknown>;
}
