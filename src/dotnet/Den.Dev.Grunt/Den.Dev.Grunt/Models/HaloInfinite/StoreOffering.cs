// <copyright file="StoreOffering.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for an in-game store offering.
    /// </summary>
    [IsAutomaticallySerializable]
    public class StoreOffering
    {
        /// <summary>
        /// Gets or sets the offering title.
        /// </summary>
        public DisplayString? Title { get; set; }

        /// <summary>
        /// Gets or sets the offering description.
        /// </summary>
        public DisplayString? Description { get; set; }

        /// <summary>
        /// Gets or sets the offering quality.
        /// </summary>
        public string? Quality { get; set; }

        /// <summary>
        /// Gets or sets the offering width hint for display in the store.
        /// </summary>
        public int? WidthHint { get; set; }

        /// <summary>
        /// Gets or sets the offering height hint for display in the store.
        /// </summary>
        public int? HeightHint { get; set; }

        /// <summary>
        /// Gets or sets the flair text.
        /// </summary>
        public DisplayString? FlairText { get; set; }

        /// <summary>
        /// Gets or sets the flair icon path.
        /// </summary>
        public string? FlairIconPath { get; set; }

        /// <summary>
        /// Gets or sets the flair background path.
        /// </summary>
        public string? FlairBackgroundPath { get; set; }

        /// <summary>
        /// Gets or sets the object image path.
        /// </summary>
        public string? ObjectImagePath { get; set; }

        /// <summary>
        /// Gets or sets the HCS team index.
        /// </summary>
        public int? HCSTeamIndex { get; set; }

        /// <summary>
        /// Gets or sets the store tile type.
        /// </summary>
        public string? StoreTileType { get; set; }
        
        /// <summary>
        /// Gets or sets whether the object has gleam.
        /// </summary>
        public bool? HasGleam { get; set; }

        /// <summary>
        /// Gets or sets whether the item is on sale.
        /// </summary>
        public bool? IsOnSale { get; set; }

        /// <summary>
        /// Gets or sets the sale percentage.
        /// </summary>
        public int? SalePercentage { get; set; }

        /// <summary>
        /// Gets or sets whether the item is associated with an event.
        /// </summary>
        public bool? IsEventItem { get; set; }

        /// <summary>
        /// Gets or sets whether the item is new.
        /// </summary>
        public bool? IsNew { get; set; }

        /// <summary>
        /// Gets or sets the flair background color override.
        /// </summary>
        public string? FlairBackgroundColorOverrideRGB { get; set; }

        /// <summary>
        /// Gets or sets the flair text color override.
        /// </summary>
        public string? FlairTextColorOverrideRGB { get; set; }

        /// <summary>
        /// Gets or sets the title color override.
        /// </summary>
        public string? TitleColorOverrideRGB { get; set; }

        /// <summary>
        /// Gets or sets the price color override.
        /// </summary>
        public string? PriceColorOverrideRGB { get; set; }

        /// <summary>
        /// Gets or sets the price shadow color override.
        /// </summary>
        public string? PriceShadowColorOverrideRGB { get; set; }

        /// <summary>
        /// Gets or sets whether the object has flair.
        /// </summary>
        public bool? HasFlair { get; set; }

        /// <summary>
        /// Gets or sets the season number.
        /// </summary>
        public int? SeasonNumber { get; set; }
    }
}
