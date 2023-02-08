// <copyright file="AuthoringAssetVersion.cs" company="Den Delimarsky">
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
    /// Authoring asset version.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringAssetVersion : AssetBase
    {
        /// <summary>
        /// Gets or sets custom data associated with an authoring asset.
        /// </summary>
        public AuthoringAssetCustomData? CustomData { get; set; }

        /// <summary>
        /// Gets or sets files associated with an authoring asset version.
        /// </summary>
        public AssetVersionFile? AssetVersionFiles { get; set; }

        /// <summary>
        /// Gets or sets the list of links along with their types that are associated with an authoring asset version.
        /// </summary>
        public Dictionary<string, List<TargetAsset>>? Links { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset version creation date.
        /// </summary>
        public APIFormattedDate? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset version modification date.
        /// </summary>
        public APIFormattedDate? LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset version number.
        /// </summary>
        public int VersionNumber { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset version note.
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset version state.
        /// </summary>
        public int AssetState { get; set; }

        /// <summary>
        /// Gets or sets whether the authoring asset version is exempt from automatic deletion.
        /// </summary>
        public bool? ExemptFromAutoDelete { get; set; }

        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        public string? Player { get; set; }

        /// <summary>
        /// Gets or sets the string culture for the authoring asset version.
        /// </summary>
        public string? StringCulture { get; set; }

        /// <summary>
        /// Gets or sets the previous authoring asset version ID from which the current version was derived.
        /// </summary>
        public string? PreviousAssetVersionId { get; set; }
    }
}
