// <copyright file="AssetBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite.Foundation
{
    /// <summary>
    /// Inheritable class representing an in-game asset in Halo Infinite.
    /// </summary>
    public abstract class AssetBase
    {
        /// <summary>
        /// Gets or sets the asset ID.
        /// </summary>
        public Guid? AssetId { get; set; }

        /// <summary>
        /// Gets or sets the version ID.
        /// </summary>
        public Guid? VersionId { get; set; }

        /// <summary>
        /// Gets or sets the version ID.
        /// </summary>
        /// <remarks>
        /// In certain API calls, this property is used instead of <see cref="VersionId"/>. It's likely an inconsistency from the legacy code base.
        /// </remarks>
        public Guid? AssetVersionId { get; set; }

        /// <summary>
        /// Gets or sets the asset public name.
        /// </summary>
        public string? PublicName { get; set; }

        /// <summary>
        /// Gets or sets the asset name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the asset description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the inspection result.
        /// </summary>
        public int InspectionResult { get; set; }

        /// <summary>
        /// Gets or sets the clone behavior.
        /// </summary>
        public int CloneBehavior { get; set; }

        /// <summary>
        /// Gets or sets the asset home.
        /// </summary>
        public int AssetHome { get; set; }

        /// <summary>
        /// Gets or sets the list of asset tags.
        /// </summary>
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Gets or sets the list of asset contributors.
        /// </summary>
        public List<string>? Contributors { get; set; }

        /// <summary>
        /// Gets or sets the list of asset files.
        /// </summary>
        public AssetVersionFile? Files { get; set; }

        /// <summary>
        /// Gets or sets the asset kind.
        /// </summary>
        public AssetKind AssetKind { get; set; }

        /// <summary>
        /// Gets or sets the asset order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the asset stats.
        /// </summary>
        public PlayAssetStats? AssetStats { get; set; }
    }
}
