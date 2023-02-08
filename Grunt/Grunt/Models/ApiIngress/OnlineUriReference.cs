// <copyright file="OnlineUriReference.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.ApiIngress
{
    /// <summary>
    /// Container class for URI references through the API surface.
    /// </summary>
    [IsAutomaticallySerializable]
    public class OnlineUriReference
    {
        /// <summary>
        /// Gets or sets the unique endpoint ID.
        /// </summary>
        public string? EndpointId { get; set; }

        /// <summary>
        /// Gets or sets the authority ID.
        /// </summary>
        public string? AuthorityId { get; set; }

        /// <summary>
        /// Gets or sets the API path.
        /// </summary>
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the query string contents.
        /// </summary>
        public string? QueryString { get; set; }

        /// <summary>
        /// Gets or sets the retry policy ID.
        /// </summary>
        public string? RetryPolicyId { get; set; }

        /// <summary>
        /// Gets or sets the topic name.
        /// </summary>
        public string? TopicName { get; set; }

        /// <summary>
        /// Gets or sets the acknowledgement type.
        /// </summary>
        public AcknowledgementType AcknowledgementTypeId { get; set; }

        /// <summary>
        /// Gets or sets whether authentication lifetime extension is supported.
        /// </summary>
        public bool? AuthenticationLifetimeExtensionSupported { get; set; }

        /// <summary>
        /// Gets or sets the requirement for a valid clearance attached to the request.
        /// </summary>
        public bool? ClearanceAware { get; set; }
    }
}
