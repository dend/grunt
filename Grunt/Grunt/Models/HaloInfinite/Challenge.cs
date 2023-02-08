// <copyright file="Challenge.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// In-game challenge that a user needs to complete in order to progress in the battle pass.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Challenge
    {
        /// <summary>
        /// Gets or sets the description for the challenge. Includes translated strings.
        /// </summary>
        public DisplayString? Description { get; set; }

        /// <summary>
        /// Gets or sets the challenge difficulty.
        /// </summary>
        public string? Difficulty { get; set; }

        /// <summary>
        /// Gets or sets the challenge category.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the challenge reward.
        /// </summary>
        public Reward? Reward { get; set; }

        /// <summary>
        /// Gets or sets the threshold for success for the challenge.
        /// </summary>
        public int? ThresholdForSuccess { get; set; }

        /// <summary>
        /// Gets or sets the challenge title. Includes translated strings.
        /// </summary>
        public DisplayString? Title { get; set; }

        /// <summary>
        /// Gets or sets the icon path representing the type of the challenge.
        /// </summary>
        public string? TypeIconPath { get; set; }

        /// <summary>
        /// Gets or sets whether the challenge is a user-specific event.
        /// </summary>
        public bool? IsUserEvent { get; set; }

        /// <summary>
        /// Gets or sets the path to the challenge.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the challenge progress.
        /// </summary>
        public int? Progress { get; set; }

        /// <summary>
        /// Gets or sets the challenge ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets whether the challenge can be re-rolled.
        /// </summary>
        public bool? CanReroll { get; set; }
    }
}
