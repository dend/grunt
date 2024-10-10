// <copyright file="PersonalScore.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Personal score awarded through a match or other means in the game.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PersonalScore
    {
        /// <summary>
        /// Gets or sets the name ID.
        /// </summary>
        public long NameId { get; set; }

        /// <summary>
        /// Gets or sets the score value.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the total amount of personal score awarded.
        /// </summary>
        public int TotalPersonalScoreAwarded { get; set; }
    }
}
