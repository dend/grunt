// <copyright file="UserEmail.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.Waypoint
{
    /// <summary>
    /// Entity representing the user's email.
    /// </summary>
    [IsAutomaticallySerializable]
    public class UserEmail
    {
        /// <summary>
        /// Gets or sets the email address string.
        /// </summary>
        public string? EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the email address is verified.
        /// </summary>
        public bool? Verified { get; set; }
    }
}
