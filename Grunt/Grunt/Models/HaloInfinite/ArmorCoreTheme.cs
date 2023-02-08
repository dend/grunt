// <copyright file="ArmorCoreTheme.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using OpenSpartan.Grunt.Models.HaloInfinite.Foundation;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for an armor coating theme.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ArmorCoreTheme : ThemeBase
    {
        /// <summary>
        /// Gets or sets the path to the coating.
        /// </summary>
        public string? CoatingPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the glove.
        /// </summary>
        public string? GlovePath { get; set; }

        /// <summary>
        /// Gets or sets the path to the helmet.
        /// </summary>
        public string? HelmetPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the helmet attachment.
        /// </summary>
        public string? HelmetAttachmentPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the chest attachment.
        /// </summary>
        public string? ChestAttachmentPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the kneepad.
        /// </summary>
        public string? KneePadPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the left shoulder pad.
        /// </summary>
        public string? LeftShoulderPadPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the left shoulder path.
        /// </summary>
        public string? RightShoulderPadPath { get; set; }

        /// <summary>
        /// Gets or sets the list of used emblems.
        /// </summary>
        public List<Emblem>? Emblems { get; set; }

        /// <summary>
        /// Gets or sets the path to armor effects.
        /// </summary>
        public string? ArmorFxPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the mythic effect.
        /// </summary>
        public string? MythicFxPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the visor.
        /// </summary>
        public string? VisorPath { get; set; }

        /// <summary>
        /// Gets or sets the path to teh hip attachment.
        /// </summary>
        public string? HipAttachmentPath { get; set; }

        /// <summary>
        /// Gets or sets the path to the wrist attachment.
        /// </summary>
        public string? WristAttachmentPath { get; set; }

        /// <summary>
        /// Gets or sets the big emblem.
        /// </summary>
        public Emblem? BigEmblem { get; set; }
    }
}
