// <copyright file="CustomGameDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Definition for a custom game in Halo Infinite.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CustomGameDefinition
    {
        /// <summary>
        /// Gets or sets the maximum player count.
        /// </summary>
        public int MaxPlayerCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum player count per game client.
        /// </summary>
        public int MaxPlayersPerClient { get; set; }

        /// <summary>
        /// Gets or sets the ID for the rules configuration.
        /// </summary>
        public string? RulesId { get; set; }

        /// <summary>
        /// Gets or sets the maximum fireteam size.
        /// </summary>
        public int DefaultMaxFireteamSizeSliderValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum team count.
        /// </summary>
        public int MaxTeamCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of players per medium virtual machine (VM) instance.
        /// </summary>
        public int MaxPlayersInMediumVmInstance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether observers are allowed.
        /// </summary>
        public bool DefaultObserversAllowed { get; set; }

        /// <summary>
        /// Gets or sets the default map.
        /// </summary>
        public GenericAsset? DefaultMap { get; set; }

        /// <summary>
        /// Gets or sets the default game variant.
        /// </summary>
        public GenericAsset? DefaultGameVariant { get; set; }
    }
}
