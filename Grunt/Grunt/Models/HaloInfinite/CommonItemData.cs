// <copyright file="CommonItemData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Common in-game item data.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CommonItemData
    {
        /// <summary>
        /// Gets or sets the ID for the item.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets whether the item should be hidden until owned.
        /// </summary>
        public bool? HideUntilOwned { get; set; }

        /// <summary>
        /// Gets or sets the title for the item. Includes translated strings.
        /// </summary>
        public DisplayString? Title { get; set; }

        /// <summary>
        /// Gets or sets the description for the item. Includes translated strings.
        /// </summary>
        public DisplayString? Description { get; set; }

        /// <summary>
        /// Gets or sets whether the item is gated behind a feature flag.
        /// </summary>
        public bool? FeatureFlag { get; set; }

        /// <summary>
        /// Gets or sets item availability. Includes translated strings.
        /// </summary>
        public DisplayString? ItemAvailability { get; set; }

        /// <summary>
        /// Gets or sets the date the item will be released.
        /// </summary>
        public APIFormattedDate? DateReleased { get; set; }

        /// <summary>
        /// Gets or sets the item alternative name. Includes translated strings.
        /// </summary>
        public DisplayString? AltName { get; set; }

        /// <summary>
        /// Gets or sets the icon string ID.
        /// </summary>
        public IdentifierName? IconStringId { get; set; }

        /// <summary>
        /// Gets or sets the sprite bitmap index.
        /// </summary>
        public int SpriteBitmap { get; set; }

        /// <summary>
        /// Gets or sets the sprite frame index.
        /// </summary>
        public int SpriteFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets the alternative sprite bitmap index.
        /// </summary>
        public int AltSpriteBitmap { get; set; }

        /// <summary>
        /// Gets or sets the alternative sprite frame index.
        /// </summary>
        public int AltSpriteFrameIndex { get; set; }

        /// <summary>
        /// Gets or sets the display path for the item.
        /// </summary>
        public DisplayPath? DisplayPath { get; set; }

        /// <summary>
        /// Gets or sets the item quality.
        /// </summary>
        public string? Quality { get; set; }

        /// <summary>
        /// Gets or sets the item manufacturer ID.
        /// </summary>
        public int ManufacturerId { get; set; }

        /// <summary>
        /// Gets or sets the item type.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the item reward track.
        /// </summary>
        public string? RewardTrack { get; set; }

        /// <summary>
        /// Gets or sets the list of item parent paths.
        /// </summary>
        public List<ItemPath>? ParentPaths { get; set; }

        /// <summary>
        /// Gets or sets the item sorting metadata.
        /// </summary>
        public SortingMetadata? SortingMetadata { get; set; }

        /// <summary>
        /// Gets or sets the item acquisition season number.
        /// </summary>
        public int SeasonNumber { get; set; }

        /// <summary>
        /// Gets or sets the item release season number.
        /// </summary>
        public int OriginalSeasonNumber { get; set; }

        /// <summary>
        /// Gets or sets the season string. Includes translated strings.
        /// </summary>
        public DisplayString? Season { get; set; }
    }
}
