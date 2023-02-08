// <copyright file="RetryOptions.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.ApiIngress
{
    /// <summary>
    /// Configuration for request retry options.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RetryOptions
    {
        /// <summary>
        /// Gets or sets the maximum number of retries before failing.
        /// </summary>
        public int MaxRetryCount { get; set; }

        /// <summary>
        /// Gets or sets the retry delay, in milliseconds.
        /// </summary>
        public int RetryDelayMs { get; set; }

        /// <summary>
        /// Gets or sets the retry growth rate for exponential back-off (presumably).
        /// </summary>
        public float RetryGrowth { get; set; }

        /// <summary>
        /// Gets or sets the retry jitter value, in milliseconds.
        /// </summary>
        public int RetryJitterMs { get; set; }

        /// <summary>
        /// Gets or sets whether retry should be attempted if the resource is not found.
        /// </summary>
        public bool? RetryIfNotFound { get; set; }
    }
}
