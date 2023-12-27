// <copyright file="HelmetOptions.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using Den.Dev.Orion.Models.HaloInfinite.Foundation;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Class for helmet configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class HelmetOptions : ArmorConfigurationOptionsBase
    {
        /// <summary>
        /// Gets or sets the helmet path.
        /// </summary>
        public string? HelmetPath { get; set; }

        /// <summary>
        /// Gets or sets the helmet attachments.
        /// </summary>
        public StandardConfigurationOptions? HelmetAttachments { get; set; }
    }
}
