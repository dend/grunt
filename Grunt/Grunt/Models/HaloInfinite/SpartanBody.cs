// <copyright file="SpartanBody.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for the Spartan body.
    /// </summary>
    [IsAutomaticallySerializable]
    public class SpartanBody
    {
        /// <summary>
        /// Gets or sets the date of last modification.
        /// </summary>
        public APIFormattedDate? LastModifiedDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the path to the asset on the left arm.
        /// </summary>
        public string? LeftArm { get; set; }

        /// <summary>
        /// Gets or sets the path to the asset on the right arm.
        /// </summary>
        public string? RightArm { get; set; }

        /// <summary>
        /// Gets or sets the path to the asset on the left leg.
        /// </summary>
        public string? LeftLeg { get; set; }

        /// <summary>
        /// Gets or sets the path to the asset on the right arm.
        /// </summary>
        public string? RightLeg { get; set; }

        /// <summary>
        /// Gets or sets the body type.
        /// </summary>
        public string? BodyType { get; set; }

        /// <summary>
        /// Gets or sets the voice ID.
        /// </summary>
        public int Voice { get; set; }
    }
}
