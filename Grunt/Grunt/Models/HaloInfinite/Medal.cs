// <copyright file="Medal.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Halo Infinite match performance medal.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Medal
    {
        /// <summary>
        /// Gets or sets the medal name ID.
        /// </summary>
        public long NameId { get; set; }

        /// <summary>
        /// Gets or sets the medal count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the amount of personal score awarded.
        /// </summary>
        public int TotalPersonalScoreAwarded { get; set; }

        /// <summary>
        /// Gets or sets the medal name. Includes translated strings.
        /// </summary>
        public DisplayString? Name { get; set; }

        /// <summary>
        /// Gets or sets the medal description. Includes translated strings.
        /// </summary>
        public DisplayString? Description { get; set; }

        /// <summary>
        /// Gets or sets the sprite index. The sprite index is zero-based, starting with 0 on the sprite sheet. Sprite sheets are generally 16 by 16.
        /// </summary>
        public int SpriteIndex { get; set; }

        /// <summary>
        /// Gets or sets the sorting weight.
        /// </summary>
        public int SortingWeight { get; set; }

        /// <summary>
        /// Gets or sets the difficulty index.
        /// </summary>
        public int DifficultyIndex { get; set; }

        /// <summary>
        /// Gets or sets the type index.
        /// </summary>
        public int TypeIndex { get; set; }

        /// <summary>
        /// Gets or sets the personal score.
        /// </summary>
        public int PersonalScore { get; set; }
    }
}
