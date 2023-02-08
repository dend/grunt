// <copyright file="PlayerCustomization.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for player customization.
    /// </summary>
    [IsAutomaticallySerializable]
    public class PlayerCustomization
    {
        /// <summary>
        /// Gets or sets the customization ID.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the result code.
        /// </summary>
        public string? ResultCode { get; set; }

        /// <summary>
        /// Gets or sets the player customization result.
        /// </summary>
        public CustomizationData? Result { get; set; }
    }
}
