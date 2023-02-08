// <copyright file="SeasonRewardTrack.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for a season reward track.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SeasonRewardTrack
    {
        /// <summary>
        /// Gets or sets the date range during which the season reward track is active. Includes translated strings.
        /// </summary>
        public DisplayString? DateRange { get; set; }

        /// <summary>
        /// Gets or sets the name for the season reward track. Includes translated strings.
        /// </summary>
        public DisplayString? Name { get; set; }

        /// <summary>
        /// Gets or sets the path to the season reward track logo.
        /// </summary>
        public string? Logo { get; set; }

        /// <summary>
        /// Gets or sets the season number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the description for the season reward track. Includes translated strings.
        /// </summary>
        public DisplayString? Description { get; set; }

        /// <summary>
        /// Gets or sets the path to the background image for the season.
        /// </summary>
        public string? SummaryBackgroundPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the background image for challenges.
        /// </summary>
        public string? ChallengesBackgroundPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the battle pass logo image.
        /// </summary>
        public string? BattlePassLogoImage { get; set; }

        /// <summary>
        /// Gets or sets the path to the season logo image.
        /// </summary>
        public string? SeasonLogoImage { get; set; }

        /// <summary>
        /// Gets or sets the path to the ritual logo image.
        /// </summary>
        public string? RitualLogoImage { get; set; }

        /// <summary>
        /// Gets or sets the path to the storefront background image.
        /// </summary>
        public string? StorefrontBackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the path to the card background image.
        /// </summary>
        public string? CardBackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the narrative blurb for the reward track. Includes translated strings.
        /// </summary>
        public DisplayString? NarrativeBlurb { get; set; }

        /// <summary>
        /// Gets or sets the path to the battle pass upsell background image.
        /// </summary>
        public string? BattlePassSeasonUpsellBackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the path to the progression background image.
        /// </summary>
        public string? ProgressionBackgroundImage { get; set; }
    }
}
