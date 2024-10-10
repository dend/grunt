// <copyright file="HelmetConfigurationOptions.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Collections.Generic;
using Den.Dev.Orion.Models.HaloInfinite.Foundation;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Class that represents helmet configurations in an armor kit.
    /// </summary>
    [IsAutomaticallySerializable]
    public class HelmetConfigurationOptions : ArmorConfigurationOptionsBase
    {
        /// <summary>
        /// Gets or sets helmet options.
        /// </summary>
        public List<HelmetOptions>? Options { get; set; }
    }
}
