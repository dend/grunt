// <copyright file="OverrideSettings.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace OpenSpartan.Grunt.Models.HaloInfinite
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
    }
}
