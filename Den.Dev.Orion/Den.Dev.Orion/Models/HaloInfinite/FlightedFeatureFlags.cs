// <copyright file="FlightedFeatureFlags.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for flighted feature flags.
    /// </summary>
    [IsAutomaticallySerializable]
    public class FlightedFeatureFlags
    {
        /// <summary>
        /// Gets or sets the list of enabled features.
        /// </summary>
        public List<string>? EnabledFeatures { get; set; }

        /// <summary>
        /// Gets or sets the list of disabled features.
        /// </summary>
        public List<string>? DisabledFeatures { get; set; }
    }
}
