// <copyright file="SteamStoreInventory.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for the title inventory in the Steam store.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SteamStoreInventory
    {
        /// <summary>
        /// Gets or sets the title configuration in the Steam store.
        /// </summary>
        public SteamStoreTitleConfiguration? TitleConfiguration { get; set; }
    }
}
