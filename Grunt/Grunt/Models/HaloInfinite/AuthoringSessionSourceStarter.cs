// <copyright file="AuthoringSessionSourceStarter.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Starter configuration for an authoring session.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringSessionSourceStarter
    {
        /// <summary>
        /// Gets or sets the source ID for the session.
        /// </summary>
        public string? SourceId { get; set; }

        /// <summary>
        /// Gets or sets the source for the session.
        /// </summary>
        public int Source { get; set; }
    }
}
