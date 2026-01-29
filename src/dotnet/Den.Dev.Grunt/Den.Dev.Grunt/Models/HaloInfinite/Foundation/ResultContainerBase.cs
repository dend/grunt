// <copyright file="ResultContainerBase.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite.Foundation
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
        public ResultCode ResultCode { get; set; }
    }
}
