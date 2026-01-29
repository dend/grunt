// <copyright file="MarkerLocation.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Container class for marker location data. Used for hip attachments.
    /// </summary>
    [IsAutomaticallySerializable]
    public class MarkerLocation
    {
        /// <summary>
        /// Gets or sets the marker name.
        /// </summary>
        public IdentifierName? MarkerName { get; set; }

        /// <summary>
        /// Gets or sets the variant ID.
        /// </summary>
        public IdentifierName? VariantId { get; set; }
    }
}
