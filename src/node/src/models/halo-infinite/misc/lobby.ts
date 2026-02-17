/**
 * QoS server information.
 */
export interface Server {
  /** Server region */
  Region?: string;
  /** Server address */
  Address?: string;
  /** Server port */
  Port?: number;
  /** Server name */
  Name?: string;
  /** Whether server is available */
  Available?: boolean;
}

/**
 * Lobby presence request.
 */
export interface LobbyPresenceRequest {
  /** Player identifier */
  PlayerId?: string;
  /** Session ID */
  SessionId?: string;
}

/**
 * Container for lobby presence requests.
 */
export interface LobbyPresenceRequestContainer {
  /** List of presence requests */
  Requests?: LobbyPresenceRequest[];
}

/**
 * Lobby presence result.
 */
export interface LobbyPresenceResult {
  /** Player identifier */
  PlayerId?: string;
  /** Session ID */
  SessionId?: string;
  /** Lobby ID */
  LobbyId?: string;
  /** Whether player is in lobby */
  InLobby?: boolean;
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
