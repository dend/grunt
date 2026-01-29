// <copyright file="InGameItem.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// In-game item configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class InGameItem
    {
        /// <summary>
        /// Gets or sets the tag ID.
        /// </summary>
        public int? TagId { get; set; }

        /// <summary>
        /// Gets or sets the theme name.
        /// </summary>
        public IdentifierName? ThemeName { get; set; }

        /// <summary>
        /// Gets or sets the style ID. Used with vehicle coatings.
        /// </summary>
        public IdentifierName? StyleId { get; set; }

        /// <summary>
        /// Gets or sets the region part ID. Used with vehicle coatings.
        /// </summary>
        public IdentifierName? RegionPartId { get; set; }

        /// <summary>
        /// Gets or sets the emblem shader name.
        /// </summary>
        public IdentifierName? EmblemShaderName { get; set; }

        /// <summary>
        /// Gets or sets common data associated with the item.
        /// </summary>
        public CommonItemData? CommonData { get; set; }

        /// <summary>
        /// Gets or sets a list of available in-game item configurations.
        /// </summary>
        public List<InGameItemConfiguration>? AvailableConfigurations { get; set; }

        /// <summary>
        /// Gets or sets the media display path if the file is an image.
        /// </summary>
        public DisplayPath? ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the lightning scalar override. Used with vehicle coatings.
        /// </summary>
        public int? LightingScalarOverride { get; set; }

        /// <summary>
        /// Gets or sets part region data. Used with armor components.
        /// </summary>
        public List<RegionMetadata>? RegionData { get; set; }

        /// <summary>
        /// Gets or sets the marker location. Used with hip attachments.
        /// </summary>
        public MarkerLocation? MarkerLocation { get; set; }

        /// <summary>
        /// Gets or sets the mesh index. Used with hip attachments.
        /// </summary>
        public int? MeshIndex { get; set; }

        /// <summary>
        /// Gets or sets whether item ownership is required.
        /// </summary>
        public bool? IsItemOwnershipRequired { get; set; }

        /// <summary>
        /// Gets or sets the coating configuration.
        /// </summary>
        public StandardConfigurationOptions? Coatings { get; set; }

        /// <summary>
        /// Gets or sets the helmet configuration.
        /// </summary>
        public HelmetConfigurationOptions? Helmets { get; set; }

        /// <summary>
        /// Gets or sets the visor configuration.
        /// </summary>
        public StandardConfigurationOptions? Visors { get; set; }

        /// <summary>
        /// Gets or sets the left shoulder pads configuration.
        /// </summary>
        public StandardConfigurationOptions? LeftShoulderPads { get; set; }

        /// <summary>
        /// Gets or sets the right shoulder pads configuration.
        /// </summary>
        public StandardConfigurationOptions? RightShoulderPads { get; set; }

        /// <summary>
        /// Gets or sets the gloves configuration.
        /// </summary>
        public StandardConfigurationOptions? Gloves { get; set; }

        /// <summary>
        /// Gets or sets the knee pads configuration.
        /// </summary>
        public StandardConfigurationOptions? KneePads { get; set; }

        /// <summary>
        /// Gets or sets the chest attachments configuration.
        /// </summary>
        public StandardConfigurationOptions? ChestAttachments { get; set; }

        /// <summary>
        /// Gets or sets the wrist attachments configuration.
        /// </summary>
        public StandardConfigurationOptions? WristAttachments { get; set; }

        /// <summary>
        /// Gets or sets the hip attachments configuration.
        /// </summary>
        public StandardConfigurationOptions? HipAttachments { get; set; }

        /// <summary>
        /// Gets or sets the emblems configuration options.
        /// </summary>
        public EmblemConfigurationOptions? Emblems { get; set; }

        /// <summary>
        /// Gets or sets the armor effects configuration.
        /// </summary>
        public StandardConfigurationOptions? ArmorFx { get; set; }

        /// <summary>
        /// Gets or sets the mythic effects configuration.
        /// </summary>
        public StandardConfigurationOptions? MythicFx { get; set; }

        /// <summary>
        /// Gets or sets core region data.
        /// </summary>
        public CoreRegionData? CoreRegionData { get; set; }

        /// <summary>
        /// Gets or sets the variant ID.
        /// </summary>
        public IdentifierName? VariantId { get; set; }

        /// <summary>
        /// Gets or sets whether the armor is a kit.
        /// </summary>
        public bool? IsKit { get; set; }

        /// <summary>
        /// Gets or sets the kit base theme path.
        /// </summary>
        public string? KitBaseThemePath { get; set; }

        /// <summary>
        /// Gets or sets the stance ID.
        /// </summary>
        public IdentifierName? StanceId { get; set; }

        /// <summary>
        /// Gets or sets the weapon tag ID.
        /// </summary>
        public int? WeaponTagId { get; set; }

        /// <summary>
        /// Gets or sets the effect IDs.
        /// </summary>
        public List<int>? FxIds { get; set; }

        /// <summary>
        /// Gets or sets the effect cotnent type.
        /// </summary>
        public string? FxContentType { get; set; }

        /// <summary>
        /// Gets or sets the composer scene name.
        /// </summary>
        public IdentifierName? ComposerSceneName { get; set; }
    }
}
