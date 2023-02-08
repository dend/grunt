// <copyright file="ChallengeDeckDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Definition for an in-game challenge deck.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ChallengeDeckDefinition
    {
        /// <summary>
        /// Gets or sets the capstone challenge path.
        /// </summary>
        public string? CapstoneChallengePath { get; set; }

        /// <summary>
        /// Gets or sets the challenge deck type. Example is "Weekly".
        /// </summary>
        public string? DeckType { get; set; }

        /// <summary>
        /// Gets or sets the shortened version of the challenge deck type. Example is "weekly".
        /// </summary>
        public string? Type { get; set; }
    }
}
