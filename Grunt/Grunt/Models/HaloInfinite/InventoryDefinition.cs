// <copyright file="InventoryDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for player inventory.
    /// </summary>
    [IsAutomaticallySerializable]
    public class InventoryDefinition
    {
        /// <summary>
        /// Gets or sets the list of player items.
        /// </summary>
        public List<PlayerItem>? Items { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor chest attachments.
        /// </summary>
        public int ArmorChestAttachmentsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor coatings.
        /// </summary>
        public int ArmorCoatingsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor gloves.
        /// </summary>
        public int ArmorGlovesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor helmet attachments.
        /// </summary>
        public int ArmorHelmetAttachmentsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor helmets.
        /// </summary>
        public int ArmorHelmetsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor knee pads.
        /// </summary>
        public int ArmorKneePadsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor themes.
        /// </summary>
        public int ArmorThemesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor left shoulder pads.
        /// </summary>
        public int ArmorLeftShoulderPadsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor right shoulder pads.
        /// </summary>
        public int ArmorRightShoulderPadsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor hip attachments.
        /// </summary>
        public int ArmorHipAttachmentsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor wrist attachments.
        /// </summary>
        public int ArmorWristAttachmentsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor visors.
        /// </summary>
        public int ArmorVisorsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor emblems.
        /// </summary>
        public int ArmorEmblemsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor effects.
        /// </summary>
        public int ArmorFxsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor mythic effects.
        /// </summary>
        public int ArmorMythicFxsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable armor cores.
        /// </summary>
        public int ArmorCoresOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable Spartan action poses.
        /// </summary>
        public int SpartanActionPosesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable Spartan stances. 
        /// </summary>
        public int SpartanStancesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable Spartan backdrop images.
        /// </summary>
        public int SpartanBackdropImagesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable Spartan emblems.
        /// </summary>
        public int SpartanEmblemsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon charms.
        /// </summary>
        public int WeaponCharmsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon ammo counter colors.
        /// </summary>
        public int WeaponAmmoCounterColorsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon coatings.
        /// </summary>
        public int WeaponCoatingsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon death effects.
        /// </summary>
        public int WeaponDeathFxsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon emblems.
        /// </summary>
        public int WeaponEmblemsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon themes.
        /// </summary>
        public int WeaponThemesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon stat trackers.
        /// </summary>
        public int WeaponStatTrackersOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon cores.
        /// </summary>
        public int WeaponCoresOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable weapon alternate geometry regions.
        /// </summary>
        public int WeaponAlternateGeometryRegionsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable AI colors.
        /// </summary>
        public int AiColorsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable AI models.
        /// </summary>
        public int AiModelsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable AI themes.
        /// </summary>
        public int AiThemesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable AI cores.
        /// </summary>
        public int AiCoresOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable vehicle alternate geometry regions.
        /// </summary>
        public int VehicleAlternateGeometryRegionsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable vehicle charms.
        /// </summary>
        public int VehicleCharmsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable vehicle coatings.
        /// </summary>
        public int VehicleCoatingsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable vehicle emblems.
        /// </summary>
        public int VehicleEmblemsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable vehicle effects.
        /// </summary>
        public int VehicleFxsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable vehicle horns.
        /// </summary>
        public int VehicleHornsOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable vehicle themes.
        /// </summary>
        public int VehicleThemesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable vehicle cores.
        /// </summary>
        public int VehicleCoresOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the list of cores.
        /// </summary>
        public List<PlayerItem>? Cores { get; set; }

        /// <summary>
        /// Gets or sets the count of ownable Spartan voices.
        /// </summary>
        public int SpartanVoicesOwnableCount { get; set; }

        /// <summary>
        /// Gets or sets the list of emblem coatings.
        /// </summary>
        public List<PlayerItem>? EmblemCoatings { get; set; }

        /// <summary>
        /// Gets or sets the list of consumables.
        /// </summary>
        public List<PlayerItem>? Consumables { get; set; }
    }
}
