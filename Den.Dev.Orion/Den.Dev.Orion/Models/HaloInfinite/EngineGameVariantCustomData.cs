// <copyright file="EngineGameVariantCustomData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
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
