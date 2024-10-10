// <copyright file="PrefabCustomData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Custom data associated with a prefab.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PrefabCustomData
    {
        /// <summary>
        /// Gets or sets the number of parts in a prefab.
        /// </summary>
        public int Parts { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the prefab has a node graph.
        /// </summary>
        public bool? HasNodeGraph { get; set; }
    }
}
