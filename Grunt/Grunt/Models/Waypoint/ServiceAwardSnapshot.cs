// <copyright file="ServiceAwardSnapshot.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.Waypoint
{
    /// <summary>
    /// Snapshot of service awards used on <see href="https://www.halowaypoint.com/">Halo Waypoint</see>.
    /// </summary>
    [IsAutomaticallySerializable]
    public class ServiceAwardSnapshot
    {
        /// <summary>
        /// Gets or sets the player XUID.
        /// </summary>
        [JsonPropertyName("xuid")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Xuid { get; set; }

        /// <summary>
        /// Gets or sets the player's Halo gamerscore.
        /// </summary>
        [JsonPropertyName("haloGamerscore")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int HaloGamerscore { get; set; }

        /// <summary>
        /// Gets or sets the list of featured awards.
        /// </summary>
        [JsonPropertyName("featuredAwards")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? FeaturedAwards { get; set; }

        /// <summary>
        /// Gets or sets the complete list of awards.
        /// </summary>
        [JsonPropertyName("awards")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Awards { get; set; }
    }
}
