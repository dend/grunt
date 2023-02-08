// <copyright file="HaloApiResultContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models
{
    /// <summary>
    /// Container class that encapsulates the result from a Halo API call.
    /// </summary>
    /// <typeparam name="T">The type of result to fetch.</typeparam>
    /// <typeparam name="THaloApiErrorContainer">Error container, available if an error occurred.</typeparam>
    public class HaloApiResultContainer<T, THaloApiErrorContainer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HaloApiResultContainer{T, THaloApiErrorContainer}"/> class.
        /// </summary>
        /// <param name="result">Result from the Halo API request.</param>
        /// <param name="container">Error information for the Halo API request.</param>
        public HaloApiResultContainer(T result, THaloApiErrorContainer container)
        {
            this.Result = result;
            this.Error = container;
        }

        /// <summary>
        /// Gets or sets the Halo API request result.
        /// </summary>
        public T? Result { get; set; }

        /// <summary>
        /// Gets or sets the Halo API request error information.
        /// </summary>
        public THaloApiErrorContainer? Error { get; set; }
    }
}
