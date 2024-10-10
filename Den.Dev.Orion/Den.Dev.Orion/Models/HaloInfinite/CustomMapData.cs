// <copyright file="CustomMapData.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Custom data for in-game maps.
    /// </summary>
    [IsAutomaticallySerializable]
    public class CustomMapData
    {
        /// <summary>
        /// Gets or sets the number of objects on the map.
        /// </summary>
        public int NumOfObjectsOnMap { get; set; }

        /// <summary>
        /// Gets or sets the tag level ID.
        /// </summary>
        public int TagLevelId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the map is baked (ready).
        /// </summary>
        public bool IsBaked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the map has a node graph.
        /// </summary>
        public bool HasNodeGraph { get; set; }
    }
}
