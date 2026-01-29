import { WaypointModuleBase } from '../base/waypoint-module-base';
import type { ClientBase } from '../../clients/base/client-base';
import type { HaloApiResult } from '../../models/common/api-result';
import type { Notification } from '../../models/waypoint';

/**
 * Notifications response container.
 */
export interface NotificationsResponse {
  /** List of notifications */
  notifications?: Notification[];
  /** Unread count */
  unreadCount?: number;
}

/**
 * Read notifications result.
 */
export interface ReadNotificationsResult {
  /** Whether the operation succeeded */
  success?: boolean;
  /** Number of notifications marked as read */
  count?: number;
}

/**
 * Comms module for notifications and communications APIs.
 *
 * @example
 * ```typescript
 * // Get notifications
 * const notifications = await client.comms.getNotifications();
 *
 * // Mark notifications as read
 * await client.comms.markNotificationsAsRead(['notification-id-1', 'notification-id-2']);
 * ```
 */
export class CommsModule extends WaypointModuleBase {
  constructor(client: ClientBase) {
    super(client);
  }

  /**
   * Get notifications for the current user.
   *
   * @returns Notifications response
   */
  getNotifications(): Promise<HaloApiResult<NotificationsResponse>> {
    return this.get<NotificationsResponse>('/hi/users/me/notifications');
  }

  /**
   * Mark notifications as read.
   *
   * @param notificationIds - IDs of notifications to mark as read
   * @returns Result of the operation
   */
  markNotificationsAsRead(
    notificationIds: string[]
  ): Promise<HaloApiResult<ReadNotificationsResult>> {
    if (!notificationIds.length) {
      throw new Error('notificationIds cannot be empty');
    }

    return this.postJson<ReadNotificationsResult, { notificationIds: string[] }>(
      '/hi/users/me/notifications/read',
      { notificationIds }
    );
  }

  /**
   * Delete a notification.
   *
   * @param notificationId - ID of notification to delete
   * @returns Success status
   */
  deleteNotification(notificationId: string): Promise<HaloApiResult<boolean>> {
    this.assertNotEmpty(notificationId, 'notificationId');
    return this.client.executeRequest<boolean>(
      this.buildUrl(`/hi/users/me/notifications/${notificationId}`),
      'DELETE',
      { useSpartanToken: true }
    );
  }
}
