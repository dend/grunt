// <copyright file="DiscoveredParameters.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Den.Dev.Grunt.Auditor.Models
{
    /// <summary>
    /// Container for parameters discovered from seed API calls.
    /// </summary>
    public class DiscoveredParameters
    {
        /// <summary>
        /// Gets or sets the player's XUID (from authentication).
        /// </summary>
        public string PlayerXuid { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the player's gamertag (from authentication).
        /// </summary>
        public string Gamertag { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the clearance token (from ActiveClearance).
        /// </summary>
        public string ClearanceToken { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the flight configuration ID (from ActiveClearance).
        /// </summary>
        public string FlightId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of match IDs discovered from match history.
        /// </summary>
        public List<string> MatchIds { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of asset IDs discovered from UGC searches.
        /// </summary>
        public List<string> AssetIds { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of asset version IDs discovered from UGC searches.
        /// </summary>
        public List<string> VersionIds { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of map asset IDs discovered from UGC searches.
        /// </summary>
        public List<string> MapAssetIds { get; set; } = new();

        /// <summary>
        /// Gets or sets the list of playlist asset IDs discovered.
        /// </summary>
        public List<string> PlaylistAssetIds { get; set; } = new();

        /// <summary>
        /// Gets or sets custom parameters that don't fit other categories.
        /// Key is parameter name, value is the discovered value.
        /// </summary>
        public Dictionary<string, string> Custom { get; set; } = new();

        /// <summary>
        /// Gets the first match ID or empty string if none discovered.
        /// </summary>
        public string FirstMatchId => MatchIds.Count > 0 ? MatchIds[0] : string.Empty;

        /// <summary>
        /// Gets the first asset ID or empty string if none discovered.
        /// </summary>
        public string FirstAssetId => AssetIds.Count > 0 ? AssetIds[0] : string.Empty;

        /// <summary>
        /// Gets the first version ID or empty string if none discovered.
        /// </summary>
        public string FirstVersionId => VersionIds.Count > 0 ? VersionIds[0] : string.Empty;

        /// <summary>
        /// Gets the first map asset ID or empty string if none discovered.
        /// </summary>
        public string FirstMapAssetId => MapAssetIds.Count > 0 ? MapAssetIds[0] : string.Empty;
    }
}
