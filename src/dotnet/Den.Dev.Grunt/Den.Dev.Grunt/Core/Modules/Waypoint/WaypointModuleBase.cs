// <copyright file="WaypointModuleBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;

namespace Den.Dev.Grunt.Core.Modules.Waypoint
{
    /// <summary>
    /// Base class for all Waypoint API modules. Inherits shared HTTP helper methods
    /// from <see cref="ModuleBase"/> and overrides URL construction to use the
    /// Waypoint service domain.
    /// </summary>
    public abstract class WaypointModuleBase : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaypointModuleBase"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        /// <param name="origin">The origin/subdomain for this module's endpoints.</param>
        protected WaypointModuleBase(ClientBase client, string origin)
            : base(client, origin)
        {
        }

        /// <inheritdoc/>
        protected override string BuildUrl(string path) =>
            $"https://{this.Origin}.{WaypointEndpoints.ServiceDomain}{path}";
    }
}
