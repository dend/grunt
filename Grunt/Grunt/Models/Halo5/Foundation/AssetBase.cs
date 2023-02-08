// <copyright file="AssetBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.Halo5.Foundation
{
    /// <summary>
    /// Foundational class that represents Halo 5 assets.
    /// </summary>
    public class AssetBase
    {
        /// <summary>
        /// Gets or sets the asset ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the asset type.
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the asset identity.
        /// </summary>
        public string? Identity { get; set; }

        /// <summary>
        /// Gets or sets the list of links associated with an asset.
        /// </summary>
        public List<Link>? Links { get; set; }

        /// <summary>
        /// Gets or sets the number of child assets.
        /// </summary>
        public int ChildrenCount { get; set; }

        /// <summary>
        /// Gets or sets common asset properties.
        /// </summary>
        public CommonContentProperties? Common { get; set; }

        /// <summary>
        /// Gets or sets the asset status.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the asset title.
        /// </summary>
        public string? Title { get; set; }
    }
}
