// <copyright file="Team.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Information about a team participating in a matchmade game.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Team
    {
        /// <summary>
        /// Gets or sets the team ID.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Gets or sets the outcome for the team.
        /// </summary>
        /// <remarks>
        /// This needs to be mapped to real outcome enums.
        /// </remarks>
        public int Outcome { get; set; }

        /// <summary>
        /// Gets or sets the rank for the team.
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets detailed team stats.
        /// </summary>
        public Stats? Stats { get; set; }
    }
}
