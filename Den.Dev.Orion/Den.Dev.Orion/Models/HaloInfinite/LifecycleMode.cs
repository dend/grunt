// <copyright file="LifecycleMode.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Halo Infinite game lifecycle mode.
    /// </summary>
    public enum LifecycleMode
    {
        /// <summary>
        /// Matchmade game.
        /// </summary>
        Matchmade = 3,

        /// <summary>
        /// Local game.
        /// </summary>
        Local = 2,

        /// <summary>
        /// Custom game.
        /// </summary>
        Custom = 1,
    }
}
