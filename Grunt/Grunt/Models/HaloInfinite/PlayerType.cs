// <copyright file="PlayerType.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Type of player participating in a match.
    /// </summary>
    public enum PlayerType
    {
        /// <summary>
        /// Player is a human.
        /// </summary>
        Human = 1,

        /// <summary>
        /// Player is a bot.
        /// </summary>
        Bot = 2,
    }
}
