// <copyright file="MicrosoftStoreInventory.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for Microsoft Store inventory configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MicrosoftStoreInventory
    {
        /// <summary>
        /// Gets or sets the Microsoft Store title configuration.
        /// </summary>
        public MicrosoftStoreTitleConfiguration? TitleConfiguration { get; set; }
    }
}
