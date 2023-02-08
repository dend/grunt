// <copyright file="DisplayString.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    /// <summary>
    /// Display string with additional translations.
    /// </summary>
    [IsAutomaticallySerializable]
    public class DisplayString
    {
        /// <summary>
        /// Gets or sets the string status.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets the dictionary of supported languages and translated strings in said languages.
        /// </summary>
        public Dictionary<string, string>? Translations { get; set; }
    }
}
