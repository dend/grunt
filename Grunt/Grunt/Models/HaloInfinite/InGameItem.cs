// <copyright file="InGameItem.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
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
        public int TagId { get; set; }

        /// <summary>
        /// Gets or sets the theme name.
        /// </summary>
        public IdentifierName? ThemeName { get; set; }

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
    }
}
