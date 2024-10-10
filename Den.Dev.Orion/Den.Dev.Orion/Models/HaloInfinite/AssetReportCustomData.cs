// <copyright file="AssetReportCustomData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Asset report custom data.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AssetReportCustomData
    {
        /// <summary>
        /// Gets or sets the text for the asset report.
        /// </summary>
        public string? ReportText { get; set; }
    }
}
