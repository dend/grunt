// <copyright file="Authority.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.ApiIngress
{
    /// <summary>
    /// Container class for API authority information.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Authority
    {
        /// <summary>
        /// Gets or sets the authority ID.
        /// </summary>
        public string? AuthorityId { get; set; }

        /// <summary>
        /// Gets or sets the request scheme.
        /// </summary>
        public int? Scheme { get; set; }

        /// <summary>
        /// Gets or sets the authority hostname.
        /// </summary>
        public string? Hostname { get; set; }

        /// <summary>
        /// Gets or sets the request port.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the container for supported authentication methods.
        /// </summary>
        public List<AuthenticationMethod>? AuthenticationMethods { get; set; }
    }
}
