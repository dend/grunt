/**
 * QoS server information.
 */
export interface Server {
  /** Server region */
  region?: string;
  /** Server address */
  address?: string;
  /** Server port */
  port?: number;
  /** Server name */
  name?: string;
  /** Whether server is available */
  available?: boolean;
}

/**
 * Lobby presence request.
 */
export interface LobbyPresenceRequest {
  /** Player identifier */
  playerId?: string;
  /** Session ID */
  sessionId?: string;
}

/**
 * Container for lobby presence requests.
 */
export interface LobbyPresenceRequestContainer {
  /** List of presence requests */
  requests?: LobbyPresenceRequest[];
}

/**
 * Lobby presence result.
 */
export interface LobbyPresenceResult {
  /** Player identifier */
  playerId?: string;
  /** Session ID */
  sessionId?: string;
  /** Lobby ID */
  lobbyId?: string;
  /** Whether player is in lobby */
  inLobby?: boolean;
}

/**
 * Container for lobby presence results.
 */
export interface LobbyPresenceContainer {
  /** List of presence results */
  results?: LobbyPresenceResult[];
}

/**
 * Lobby join handle.
 */
export interface LobbyJoinHandle {
  /** Handle identifier */
  handleId?: string;
  /** Lobby ID */
  lobbyId?: string;
  /** Handle value */
  handle?: string;
  /** Expiration time (ISO 8601) */
  expiresAt?: string;
}

/**
 * Response when joining a lobby.
 */
export interface JoinLobbyResponse {
  /** Success status */
  success?: boolean;
  /** Lobby ID */
  lobbyId?: string;
  /** Session details */
  session?: Record<string, unknown>;
}
