// <copyright file="AuthoringResultContainerBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite.Foundation
{
    /// <summary>
    /// Base class for authoring asset containers.
    /// </summary>
    public abstract class AuthoringResultContainerBase
    {
        /// <summary>
        /// Gets or sets the estimated number of results.
        /// </summary>
        public int EstimatedTotal { get; set; }

        /// <summary>
        /// Gets or sets the start value for the query.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the count of results.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the number of results.
        /// </summary>
        public int ResultCount { get; set; }

        /// <summary>
        /// Gets or sets the links associated with the results.
        /// </summary>
        /// <remarks>
        /// Additional research is needed to determine what this field type is.
        /// </remarks>
        public List<dynamic>? Links { get; set; }
    }
}
