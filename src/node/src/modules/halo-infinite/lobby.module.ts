import { ModuleBase } from '../base/module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import { HALO_CORE_ENDPOINTS } from '../../endpoints/halo-core-endpoints';
import type {
  Server,
  LobbyPresenceContainer,
  LobbyJoinHandle,
  JoinLobbyResponse,
} from '../../models/halo-infinite/misc';

/**
 * Lobby module for multiplayer lobby and presence APIs.
 *
 * @example
 * ```typescript
 * // Get QoS servers
 * const servers = await client.lobby.getQosServers();
 *
 * // Check player presence
 * const presence = await client.lobby.presence({
 *   requests: [{ playerId: 'xuid', sessionId: 'session-id' }]
 * });
 * ```
 */
export class LobbyModule extends ModuleBase {
  constructor(client: ClientBase) {
    super(client, HALO_CORE_ENDPOINTS.LOBBY_ORIGIN);
  }

  /**
   * Get available QoS (Quality of Service) servers.
   *
   * @returns List of servers
   */
  getQosServers(): Promise<HaloApiResult<Server[]>> {
    return this.get<Server[]>('/titles/hi/qosservers');
  }

  /**
   * Check presence for players in lobbies.
   *
   * @param presenceRequest - Presence request container
   * @returns Presence results
   */
  presence(): Promise<HaloApiResult<LobbyPresenceContainer>> {
    return this.get<LobbyPresenceContainer>('/hi/presence');
  }

  /**
   * Get a third-party join handle for a lobby.
   *
   * @param lobbyId - Lobby identifier
   * @param player - Player XUID
   * @param handleAudience - Handle audience
   * @param handlePlatform - Handle platform
   * @returns Join handle
   */
  getThirdPartyJoinHandle(
    lobbyId: string,
    player: string,
    handleAudience: string,
    handlePlatform: string
  ): Promise<HaloApiResult<LobbyJoinHandle>> {
    this.assertNotEmpty(lobbyId, 'lobbyId');
    this.assertNotEmpty(player, 'player');
    return this.get<LobbyJoinHandle>(
      `/hi/lobbies/${lobbyId}/players/xuid(${player})/thirdPartyJoinHandle?audience=${handleAudience}&platform=${handlePlatform}`
    );
  }

  /**
   * Join a lobby.
   *
   * @param lobbyId - Lobby identifier
   * @param player - Player XUID
   * @param auth - Auth string
   * @param lobbyBootstrapPayload - Bootstrap payload
   * @returns Join response
   */
  joinLobby(
    lobbyId: string,
    player: string,
    auth: string,
    lobbyBootstrapPayload: Uint8Array
  ): Promise<HaloApiResult<JoinLobbyResponse>> {
    this.assertNotEmpty(lobbyId, 'lobbyId');
    this.assertNotEmpty(player, 'player');

    return this.client.executeRequest<JoinLobbyResponse>(
      this.buildUrl(`/hi/lobbies/${lobbyId}/players/xuid(${player})?auth=${auth}`),
      'PUT',
      {
        body: lobbyBootstrapPayload,
        contentType: 'bond' as any,
      }
    );
  }
}
