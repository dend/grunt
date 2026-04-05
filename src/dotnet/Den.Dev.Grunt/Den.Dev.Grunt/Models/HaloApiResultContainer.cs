// <copyright file="HaloApiResultContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models
{
    /// <summary>
    /// Container class that encapsulates the result from a Halo API call.
    /// </summary>
    /// <typeparam name="T">The type of result to fetch.</typeparam>
    /// <typeparam name="TRawResponseContainer">Error container, available if an error occurred.</typeparam>
    public class HaloApiResultContainer<T, TRawResponseContainer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloApiResultContainer{T, TRawResponseContainer}"/> class.
        /// </summary>
        /// <param name="result">Result from the Halo API request.</param>
        /// <param name="container">Error information for the Halo API request.</param>
        public HaloApiResultContainer(T result, TRawResponseContainer container)
        {
            this.Result = result;
            this.Response = container;
        }

        /// <summary>
        /// Gets or sets the Halo API request result.
        /// </summary>
        public T? Result { get; set; }

        /// <summary>
        /// Gets or sets the Halo API request error information.
        /// </summary>
        public TRawResponseContainer? Response { get; set; }

        /// <summary>
        /// Gets a value indicating whether the API request was successful (HTTP 2xx status code).
        /// </summary>
        public bool IsSuccess => this.Response is RawResponseContainer raw && raw.Code >= 200 && raw.Code < 300;
    }
}
