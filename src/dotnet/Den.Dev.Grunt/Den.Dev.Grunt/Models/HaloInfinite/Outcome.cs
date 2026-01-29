// <copyright file="Outcome.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Played game outcome.
    /// </summary>
    public enum Outcome
    {
        /// <summary>
        /// Game was tied.
        /// </summary>
        Tie = 1,

        /// <summary>
        /// Game was won.
        /// </summary>
        Win = 2,

        /// <summary>
        /// Game was lost.
        /// </summary>
        Loss = 3,

        /// <summary>
        /// Game was not completed.
        /// </summary>
        DidNotFinish = 4,
    }
}
