// <copyright file="SortingMetadata.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Sorting configuration for in-game items.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SortingMetadata
    {
        /// <summary>
        /// Gets or sets the category weight.
        /// </summary>
        [JsonPropertyName("categoryWeight")]
        public int CategoryWeight { get; set; }

        /// <summary>
        /// Gets or sets the sub-category weight.
        /// </summary>
        [JsonPropertyName("subCategoryWeight")]
        public int SubCategoryWeight { get; set; }
    }
}
