// <copyright file="LobbyPresenceResultContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container for lobby presence query results.
    /// </summary>
    [IsAutomaticallySerializable]
    public class LobbyPresenceResultContainer
    {
        /// <summary>
        /// Gets or sets the result ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the result code.
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the result to the query.
        /// </summary>
        public LobbyPresenceResult? Result { get; set; }
    }
}
