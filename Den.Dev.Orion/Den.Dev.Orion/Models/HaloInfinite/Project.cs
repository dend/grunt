// <copyright file="Project.cs" company="Den Delimarsky">
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
    /// Class containing information about a custom game project.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Project : AssetBase
    {
        /// <summary>
        /// Gets or sets custom data associated with the current project.
        /// </summary>
        public CustomProjectData? CustomData { get; set; }

        /// <summary>
        /// Gets or sets a list of maps available for the game through the selected custom game project.
        /// </summary>
        public List<AssetLink>? MapLinks { get; set; }

        /// <summary>
        /// Gets or sets a list of playlists available for the game through the selected custom game project.
        /// </summary>
        public List<AssetLink>? PlaylistLinks { get; set; }

        /// <summary>
        /// Gets or sets a list of prefabs available for the game through the selected custom game project.
        /// </summary>
        public List<AssetLink>? PrefabLinks { get; set; }

        /// <summary>
        /// Gets or sets a list of game variants available for the game through the selected custom game project.
        /// </summary>
        public List<AssetLink>? UgcGameVariantLinks { get; set; }

        /// <summary>
        /// Gets or sets a list of map mode pairs available for the game through the selected custom game project.
        /// </summary>
        public List<AssetLink>? MapModePairLinks { get; set; }
    }
}
