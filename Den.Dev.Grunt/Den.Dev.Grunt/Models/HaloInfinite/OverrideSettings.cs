// <copyright file="OverrideSettings.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Graphics override settings.
    /// </summary>
    [IsAutomaticallySerializable]
    public class OverrideSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the spec control async compute is enabled.
        /// </summary>
        [JsonPropertyName("spec_control_async_compute")]
        public bool? SpecControlAsyncCompute { get; set; }

        /// <summary>
        /// Gets or sets the basic spec control level.
        /// </summary>
        [JsonPropertyName("spec_control_basic")]
        public string? SpecControlBasic { get; set; }
    }
}
