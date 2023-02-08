// <copyright file="ChallengeDecksResponse.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Response issued to a request for acquiring player challenge decks.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ChallengeDecksResponse
    {
        /// <summary>
        /// Gets or sets the list of assigned challenge decks.
        /// </summary>
        public List<ChallengeDeck>? AssignedDecks { get; set; }

        /// <summary>
        /// Gets or sets the clearance ID.
        /// </summary>
        public string? ClearanceId { get; set; }

        /// <summary>
        /// Gets or sets the currently active reward track.
        /// </summary>
        public RewardTrack? ActiveRewardTrack { get; set; }
    }
}
