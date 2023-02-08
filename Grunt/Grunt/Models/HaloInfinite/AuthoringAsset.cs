// <copyright file="AuthoringAsset.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Authoring asset definition.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringAsset
    {
        /// <summary>
        /// Gets or sets the authoring asset ID.
        /// </summary>
        public string? AssetId { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset kind.
        /// </summary>
        public int Kind { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset original owner.
        /// </summary>
        public string? OriginalOwner { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset admin.
        /// </summary>
        public string? Admin { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset modification date.
        /// </summary>
        public APIFormattedDate? LastModifiedDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset creation date.
        /// </summary>
        public APIFormattedDate? CreatedDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset internal name.
        /// </summary>
        public string? InternalName { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the hard delete time for the asset.
        /// </summary>
        public APIFormattedDate? HardDeleteTimeUtc { get; set; }

        /// <summary>
        /// Gets or sets authoring asset permissions.
        /// </summary>
        public List<Permission>? Permissions { get; set; }

        /// <summary>
        /// Gets or sets authoring asset stats.
        /// </summary>
        public AuthoringAssetStats? AssetStats { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset home.
        /// </summary>
        public int AssetHome { get; set; }

        /// <summary>
        /// Gets or sets whether the authoring asset is currently being edited.
        /// </summary>
        public bool? IsCurrentlyBeingEdited { get; set; }
    }
}
