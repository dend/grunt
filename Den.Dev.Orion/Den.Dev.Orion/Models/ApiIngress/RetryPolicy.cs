// <copyright file="RetryPolicy.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.ApiIngress
{
    /// <summary>
    /// Configuration for the API retry policy.
    /// </summary>
    [IsAutomaticallySerializable]
    public class RetryPolicy
    {
        /// <summary>
        /// Gets or sets the retry policy ID.
        /// </summary>
        public string? RetryPolicyId { get; set; }

        /// <summary>
        /// Gets or sets the timeout in milliseconds.
        /// </summary>
        public int? TimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets additional retry options.
        /// </summary>
        public RetryOptions? RetryOptions { get; set; }
    }
}
