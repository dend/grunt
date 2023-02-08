// <copyright file="Appearance.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Player appearance configuration.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Appearance
    {
        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        public APIFormattedDate? LastModifiedDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the service tag.
        /// </summary>
        public string? ServiceTag { get; set; }

        /// <summary>
        /// Gets or sets the path for the action pose.
        /// </summary>
        public string? ActionPosePath { get; set; }

        /// <summary>
        /// Gets or sets the path for the stance.
        /// </summary>
        public string? StancePath { get; set; }

        /// <summary>
        /// Gets or sets the path to the backdrop image for the Spartan ID.
        /// </summary>
        public string? BackdropImagePath { get; set; }

        /// <summary>
        /// Gets or sets the emblem.
        /// </summary>
        public Emblem? Emblem { get; set; }

        /// <summary>
        /// Gets or sets the intro emote path.
        /// </summary>
        public string? IntroEmotePath { get; set; }
    }
}
