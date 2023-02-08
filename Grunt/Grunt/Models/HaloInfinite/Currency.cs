// <copyright file="Currency.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// In-game currency (CR).
    /// </summary>
    [IsAutomaticallySerializable]
    public class Currency
    {
        /// <summary>
        /// Gets or sets the currency ID.
        /// </summary>
        public string? CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the currency title. Includes translated strings.
        /// </summary>
        public DisplayString? Title { get; set; }

        /// <summary>
        /// Gets or sets the currency description. Includes translated strings.
        /// </summary>
        public DisplayString? Description { get; set; }

        /// <summary>
        /// Gets or sets the path to the currency image.
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// Gets or sets the currency icon type.
        /// </summary>
        public string? IconType { get; set; }
    }
}
