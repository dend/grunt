// <copyright file="MarketplaceRecord.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Record for a marketplace entry.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MarketplaceRecord
    {
        /// <summary>
        /// Gets or sets the marketplace source.
        /// </summary>
        public string? MarketplaceSource { get; set; }

        /// <summary>
        /// Gets or sets the last checked date for a product.
        /// </summary>
        public APIFormattedDate? ProductsLastCheckedDate { get; set; }

        /// <summary>
        /// Gets or sets the last created date for workflow entities.
        /// </summary>
        public APIFormattedDate? WorkflowEntitiesLastCreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last consumed date for a product.
        /// </summary>
        public APIFormattedDate? ProductsLastConsumedDate { get; set; }
    }
}
