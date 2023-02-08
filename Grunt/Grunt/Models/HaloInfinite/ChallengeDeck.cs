// <copyright file="ChallengeDeck.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Information related to a challenge deck.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ChallengeDeck
    {
        /// <summary>
        /// Gets or sets the challenge deck ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the challenge deck path.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the list of active challenges.
        /// </summary>
        public List<Challenge>? ActiveChallenges { get; set; }

        /// <summary>
        /// Gets or sets the list of upcoming challenges.
        /// </summary>
        public List<Challenge>? UpcomingChallenges { get; set; }

        /// <summary>
        /// Gets or sets the challenge expiration time.
        /// </summary>
        public APIFormattedDate? Expiration { get; set; }

        /// <summary>
        /// Gets or sets the list of completed challenges.
        /// </summary>
        public List<Challenge>? CompletedChallenges { get; set; }
    }
}
