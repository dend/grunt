// <copyright file="Counterfactuals.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Match counterfactuals.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Counterfactuals
    {
        /// <summary>
        /// Gets or sets personal counterfactuals.
        /// </summary>
        public KillDeathStats? SelfCounterfactuals { get; set; }

        /// <summary>
        /// Gets or sets counterfactuals related to the player tier.
        /// </summary>
        public TierCounterfactuals? TierCounterfactuals { get; set; }
    }
}
