// <copyright file="PlayerClearance.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// In-game flight ID, also known as player clearance.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerClearance
    {
        /// <summary>
        /// Gets or sets the flight configuration ID.
        /// </summary>
        public string? FlightConfigurationId { get; set; }
    }
}
