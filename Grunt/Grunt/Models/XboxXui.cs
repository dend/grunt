// <copyright file="XboxXui.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models
{
    /// <summary>
    /// Xbox XUI
    /// </summary>
    public class XboxXui
    {
        /// <summary>
        /// Gets or sets the user hash.
        /// </summary>
        [JsonPropertyName("uhs")]
        public string? UserHash { get; set; }

        /// <summary>
        /// Gets or sets the user gamertag.
        /// </summary>
        [JsonPropertyName("gtg")]
        public string? Gamertag { get; set; }

        /// <summary>
        /// Gets or sets the user Xbox Live ID (XUID).
        /// </summary>
        [JsonPropertyName("xid")]
        public string? XUID { get; set; }

        /// <summary>
        /// Gets or sets the account age group.
        /// </summary>
        [JsonPropertyName("agg")]
        public string? AgeGroup { get; set; }

        /// <summary>
        /// Gets or sets the user settings restrictions.
        /// </summary>
        [JsonPropertyName("usr")]
        public string? UserSettingsRestrictions { get; set; }

        /// <summary>
        /// Gets or sets the user title restrictions.
        /// </summary>
        [JsonPropertyName("utr")]
        public string? UserTitleRestrictions { get; set; }

        /// <summary>
        /// Gets or sets the account privileges.
        /// </summary>
        [JsonPropertyName("prv")]
        public string? Privileges { get; set; }
    }
}
