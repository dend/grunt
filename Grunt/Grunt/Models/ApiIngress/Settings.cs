// <copyright file="Settings.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.ApiIngress
{
    /// <summary>
    /// Container class for additional networking stack configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Settings
    {
        /// <summary>
        /// Gets or sets CELL configuration.
        /// </summary>
        public string? CELLConfig { get; set; }

        /// <summary>
        /// Gets or sets the client quality of service timeout, in milliseconds.
        /// </summary>
        public string? ClientQoSTimeoutMs { get; set; }

        /// <summary>
        /// Gets or sets the audience for the clearance.
        /// </summary>
        public string? ClearanceAudience { get; set; }

        /// <summary>
        /// Gets or sets the comma-separated list of endpoints related to the game CMS.
        /// </summary>
        [JsonPropertyName("GameCMS_GuideEndpoints")]
        public string? GameCMSGuideEndpoints { get; set; }

        /// <summary>
        /// Gets or sets the comma-separated list of excluded HTTP error codes.
        /// </summary>
        [JsonPropertyName("HttpEvent_ExcludedStatusCodes")]
        public string? HttpEventExcludedStatusCodes { get; set; }

        /// <summary>
        /// Gets or sets the comma-separated list of supported HTTP headers for API requests.
        /// </summary>
        [JsonPropertyName("HttpEvent_RequestHeaders")]
        public string? HttpEventRequestHeaders { get; set; }

        /// <summary>
        /// Gets or sets the comma-separated list of supported HTTP headers for API responses.
        /// </summary>
        [JsonPropertyName("HttpEvent_ResponseHeaders")]
        public string? HttpEventResponseHeaders { get; set; }

        /// <summary>
        /// Gets or sets the list of XUIDs for which logging is enabled.
        /// </summary>
        [JsonPropertyName("HttpEvent_UsersLoggingEnabled")]
        public string? HttpEventUsersLoggingEnabled { get; set; }

        /// <summary>
        /// Gets or sets the value for percentage of telemetry to upload (likely).
        /// </summary>
        /// <remarks>
        /// Additional research needs to be done around this specific value and field.
        /// </remarks>
        [JsonPropertyName("HttpEvent_UsersPercentageUpload")]
        public string? HttpEventUsersPercentageUpload { get; set; }

        /// <summary>
        /// Gets or sets the title ID value for use with PlayFab services.
        /// </summary>
        public string? PlayfabTitleId { get; set; }

        /// <summary>
        /// Gets or sets the purchase poll frequency, in seconds.
        /// </summary>
        public string? PurchasePollFrequencyInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the list of supported title IDs. Contents of this field are represented as a JSON blob.
        /// </summary>
        public string? TitleIdList { get; set; }

        /// <summary>
        /// Gets or sets whether full heap is uploaded for internal builds.
        /// </summary>
        public string? UploadFullHeapInInternalBuilds { get; set; }

        /// <summary>
        /// Gets or sets whether full heap is uploaded for external (release) builds.
        /// </summary>
        public string? UploadFullHeapInReleaseBuilds { get; set; }

        /// <summary>
        /// Gets or sets the destination for Gold trial upsell links.
        /// </summary>
        public string? GoldTrialDestinationUrl { get; set; }

        /// <summary>
        /// Gets or sets the Halo XSTS audience URI.
        /// </summary>
        public string? HaloXSTSAudienceUri { get; set; }

        /// <summary>
        /// Gets or sets the product access list.
        /// </summary>
        public string? ProductAccessList { get; set; }
    }
}
