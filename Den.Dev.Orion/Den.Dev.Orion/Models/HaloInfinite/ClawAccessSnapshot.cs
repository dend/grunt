// <copyright file="ClawAccessSnapshot.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Snapshot for CLAW access.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ClawAccessSnapshot
    {
        /// <summary>
        /// Gets or sets the list of XUIDs with full CLAW access.
        /// </summary>
        public List<long>? FullClawXuids { get; set; }

        /// <summary>
        /// Gets or sets the list of consumer XUIDs with CLAW access.
        /// </summary>
        public List<long>? ConsumerClawXuids { get; set; }
    }
}
