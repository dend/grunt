// <copyright file="ResultContainerBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite.Foundation
{
    /// <summary>
    /// Base class for query results.
    /// </summary>
    public abstract class ResultContainerBase
    {
        /// <summary>
        /// Gets or sets the ID for the result.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the result code.
        /// </summary>
        public int ResultCode { get; set; }
    }
}
