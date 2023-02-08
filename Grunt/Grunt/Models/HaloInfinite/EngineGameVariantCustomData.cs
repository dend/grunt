// <copyright file="EngineGameVariantCustomData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Custom configuration data for an engine game variant.
    /// </summary>
    [IsAutomaticallySerializable]
    public class EngineGameVariantCustomData
    {
        /// <summary>
        /// Gets or sets the subset data container configuration.
        /// </summary>
        public SubsetDataContainer? SubsetData { get; set; }

        /// <summary>
        /// Gets or sets the localized configuration data.
        /// </summary>
        public LocalizedDataContainer? LocalizedData { get; set; }
    }
}
