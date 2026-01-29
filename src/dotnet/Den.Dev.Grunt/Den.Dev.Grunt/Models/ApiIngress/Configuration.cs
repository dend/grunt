// <copyright file="Configuration.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.ApiIngress
{
    /// <summary>
    /// Configuration related to existing game endpoints. This class is used by <see cref="Core.HaloInfiniteClient"></see> to get the general list of available API endpoints.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Configuration
    {
        /// <summary>
        /// Gets or sets the list of available authorities.
        /// </summary>
        public Dictionary<string, Authority>? Authorities { get; set; }

        /// <summary>
        /// Gets or sets the list of designated retry policies for supported endpoints.
        /// </summary>
        public Dictionary<string, RetryPolicy>? RetryPolicies { get; set; }

        /// <summary>
        /// Gets or sets the generalized list of app settings such as HTTP request and response headers, whether logging is enabled or not, and others.
        /// </summary>
        public Settings? Settings { get; set; }

        /// <summary>
        /// Gets or sets the list of available API endpoints that map to <see cref="Authorities"/>.
        /// </summary>
        public Dictionary<string, OnlineUriReference>? Endpoints { get; set; }
    }
}
