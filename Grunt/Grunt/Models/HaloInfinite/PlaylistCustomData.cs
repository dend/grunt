// <copyright file="PlaylistCustomData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Custom data associated with a playlist.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlaylistCustomData
    {
        /// <summary>
        /// Gets or sets the list of playlist entries.
        /// </summary>
        public List<PlaylistEntry>? PlaylistEntries { get; set; }

        /// <summary>
        /// Gets or sets the playlist strategy type.
        /// </summary>
        public int Strategy { get; set; }

        /// <summary>
        /// Gets or sets the minimum number of teams.
        /// </summary>
        public int MinTeams { get; set; }

        /// <summary>
        /// Gets or sets the minimum team size.
        /// </summary>
        public int MinTeamSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of teams.
        /// </summary>
        public int MaxTeams { get; set; }

        /// <summary>
        /// Gets or sets the maximum team size.
        /// </summary>
        public int MaxTeamSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum team imbalance.
        /// </summary>
        public int MaxTeamImbalance { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of splitscreen players allowed.
        /// </summary>
        public int MaxSplitscreenPlayersAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether friends can join an in-progress match.
        /// </summary>
        public bool? AllowFriendJoinInProgress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether matchmade players can join an in-progress match.
        /// </summary>
        public bool? AllowMatchmakingJoinInProgress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether bots can join an in-progress match.
        /// </summary>
        public bool? AllowBotJoinInProgress { get; set; }

        /// <summary>
        /// Gets or sets the duration of the exit experience in seconds.
        /// </summary>
        public int ExitExperienceDurationSec { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the fireteam leader is allowed to kick players.
        /// </summary>
        public bool? FireteamLeaderKickAllowed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether mid-game chat is disabled.
        /// </summary>
        public bool? DisableMidgameChat { get; set; }

        /// <summary>
        /// Gets or sets the array of device inputs acceptable in the playlist.
        /// </summary>
        public List<int>? AllowedDeviceInputs { get; set; }

        /// <summary>
        /// Gets or sets the bot difficulty for the playlist.
        /// </summary>
        public int BotDifficulty { get; set; }

        /// <summary>
        /// Gets or sets the minimum fireteam size.
        /// </summary>
        public int MinFireteamSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum fireteam size.
        /// </summary>
        public int MaxFireteamSize { get; set; }
    }
}
