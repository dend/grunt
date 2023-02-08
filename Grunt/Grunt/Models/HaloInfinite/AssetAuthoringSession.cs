// <copyright file="AssetAuthoringSession.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for an asset authoring session.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AssetAuthoringSession
    {
        /// <summary>
        /// Gets or sets the content container URI.
        /// </summary>
        public string? ContainerUri { get; set; }

        /// <summary>
        /// Gets or sets the content container SAS token.
        /// </summary>
        public string? ContainerSas { get; set; }

        /// <summary>
        /// Gets or sets the authoring session expiration time.
        /// </summary>
        public APIFormattedDate? ExpirationTime { get; set; }

        /// <summary>
        /// Gets or sets whether the authoring session is read-only.
        /// </summary>
        public bool? ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets the session ID.
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// Gets or sets the asset ID.
        /// </summary>
        public string? AssetId { get; set; }

        /// <summary>
        /// Gets or sets custom asset data.
        /// </summary>
        public CustomAssetData? CustomData { get; set; }

        /// <summary>
        /// Gets or sets the asset public name.
        /// </summary>
        public string? PublicName { get; set; }

        /// <summary>
        /// Gets or sets the asset description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the asset tags.
        /// </summary>
        public List<string>? Tags { get; set; }

        /// <summary>
        /// Gets or sets the authoring asset links.
        /// </summary>
        public AuthoringAssetLinks? Links { get; set; }

        /// <summary>
        /// Gets or sets the list of asset contributors.
        /// </summary>
        public List<string>? Contributors { get; set; }

        /// <summary>
        /// Gets or sets the string culture for the asset.
        /// </summary>
        public string? StringCulture { get; set; }

        /// <summary>
        /// Gets or sets the previous asset version ID.
        /// </summary>
        public string? PreviousAssetVersionId { get; set; }

        /// <summary>
        /// Gets or sets the file collection for the asset.
        /// </summary>
        public AssetVersionFile? ContainerFiles { get; set; }
    }
}
