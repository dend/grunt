// <copyright file="AuthoringSessionStarter.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for an authoring session starter.
    /// </summary>
    [IsAutomaticallySerializable]
    public class AuthoringSessionStarter
    {
        /// <summary>
        /// Gets or sets the previous version ID of the asset used for the session.
        /// </summary>
        public string? PreviousVersionId { get; set; }

        /// <summary>
        /// Gets or sets the session origin.
        /// </summary>
        public string? SessionOrigin { get; set; }
    }
}
