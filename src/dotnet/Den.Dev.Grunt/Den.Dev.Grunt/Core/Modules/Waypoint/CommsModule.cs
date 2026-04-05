// <copyright file="CommsModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.Waypoint;

namespace Den.Dev.Grunt.Core.Modules.Waypoint
{
    /// <summary>
    /// Module for Halo Waypoint communication and notification APIs.
    /// </summary>
    public sealed class CommsModule : WaypointModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommsModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal CommsModule(ClientBase client)
            : base(client, WaypointEndpoints.CommsEndpoint)
        {
        }

        /// <summary>
        /// Marks the user's notifications as read on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="ReadNotificationsResult"/> containing the XUID and the date when notifications were marked as read. Otherwise, returns a null object and the error details.</returns>
        public Task<HaloApiResultContainer<ReadNotificationsResult, RawResponseContainer>> MarkNotificationsAsReadAsync(CancellationToken cancellationToken = default)
        {
            return this.PostAsync<ReadNotificationsResult>("/users/me/read-notifications", useSpartanToken: true, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Gets the user's notifications from <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
        /// </summary>
        /// <param name="offset">The number of notifications to skip. Defaults to 0.</param>
        /// <param name="limit">The maximum number of notifications to return. Defaults to 20.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns a list of <see cref="Notification"/> objects. Otherwise, returns a null object and the error details.</returns>
        public Task<HaloApiResultContainer<List<Notification>, RawResponseContainer>> GetNotificationsAsync(int offset = 0, int limit = 20, CancellationToken cancellationToken = default)
        {
            return this.GetAsync<List<Notification>>($"/users/me/notifications?offset={offset}&limit={limit}", useSpartanToken: true, cancellationToken: cancellationToken);
        }
    }
}
