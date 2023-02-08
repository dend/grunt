// <copyright file="Outfit.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Configuration for an in-game outfit.
    /// </summary>
    [IsAutomaticallySerializable]
    public class Outfit
    {
        /// <summary>
        /// Gets or sets outfit customization data.
        /// </summary>
        public CustomizationData? CustomizationData { get; set; }

        /// <summary>
        /// Gets or sets the outfit ID.
        /// </summary>
        public int OutfitID { get; set; }
    }
}
