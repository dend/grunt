// <copyright file="HaloApiResult.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models
{
    /// <summary>
    /// Simplified result container for Halo API responses.
    /// This is a convenience alias for <see cref="HaloApiResultContainer{T, RawResponseContainer}"/>.
    /// </summary>
    /// <typeparam name="T">The type of result to fetch.</typeparam>
    public class HaloApiResult<T> : HaloApiResultContainer<T, RawResponseContainer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloApiResult{T}"/> class.
        /// </summary>
        /// <param name="result">Result from the Halo API request.</param>
        /// <param name="container">Raw response information for the Halo API request.</param>
        public HaloApiResult(T result, RawResponseContainer container)
            : base(result, container)
        {
        }
    }
}
