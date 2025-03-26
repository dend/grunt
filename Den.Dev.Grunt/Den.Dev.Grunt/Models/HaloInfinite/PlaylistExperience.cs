// <copyright file="PlaylistExperience.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Playlist experience associated with a given match.
    /// </summary>
    public enum PlaylistExperience
    {
        /// <summary>
        /// Arena experience.
        /// </summary>
        Arena = 2,

        /// <summary>
        /// Big team battle.
        /// </summary>
        BTB = 3,

        /// <summary>
        /// Featured series experience.
        /// </summary>
        Featured = 5,
    }
}
