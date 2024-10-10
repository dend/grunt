// <copyright file="Prefab.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using Den.Dev.Orion.Models.HaloInfinite.Foundation;

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// In-game Forge prefab.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Prefab : AssetBase
    {
        /// <summary>
        /// Gets or sets custom data associated with an in-game Forge prefab.
        /// </summary>
        public PrefabCustomData? CustomData { get; set; }
    }
}
