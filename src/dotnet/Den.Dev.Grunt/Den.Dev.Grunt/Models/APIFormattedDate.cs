// <copyright file="APIFormattedDate.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;

namespace Den.Dev.Grunt.Models
{
    /// <summary>
    /// Container for an ISO8601-formatted date that is used across the API.
    /// </summary>
    public class APIFormattedDate
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime? ISO8601Date { get; set; }
    }
}
