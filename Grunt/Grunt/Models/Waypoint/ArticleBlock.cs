// <copyright file="ArticleBlock.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Text.Json.Serialization;
using OpenSpartan.Grunt.Converters;

namespace OpenSpartan.Grunt.Models.Waypoint
{
    /// <summary>
    /// Article block associated with an article on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ArticleBlock
    {
        /// <summary>
        /// Gets or sets the block name.
        /// </summary>
        public string? BlockName { get; set; }

        /// <summary>
        /// Gets or sets the list of attributes associated with a block.
        /// </summary>
        [JsonPropertyName("attrs")]
        [JsonConverter(typeof(SingleOrArrayJsonConverter<Attribute>))]
        public List<Attribute>? Attributes { get; set; }

        /// <summary>
        /// Gets or sets the collection of inner blocks.
        /// </summary>
        public List<string>? InnerBlocks { get; set; }

        /// <summary>
        /// Gets or sets the block inner HTML content.
        /// </summary>
        public string? InnerHTML { get; set; }

        /// <summary>
        /// Gets or sets the inner content for the block.
        /// </summary>
        public List<string>? InnerContent { get; set; }
    }
}
