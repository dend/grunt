// <copyright file="MicrosoftStoreMapping.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Item mapping within the Microsoft Store.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MicrosoftStoreMapping
    {
        /// <summary>
        /// Gets or sets a list of products mapped within the store.
        /// </summary>
        public List<StoreProductMapping>? ProductMapping { get; set; }

        /// <summary>
        /// Gets or sets the container ID.
        /// </summary>
        public string? ContainerId { get; set; }
    }
}
