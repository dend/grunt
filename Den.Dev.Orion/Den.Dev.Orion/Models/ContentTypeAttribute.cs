// <copyright file="ContentTypeAttribute.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;

namespace Den.Dev.Orion.Models
{
    /// <summary>
    /// Attribute defining the response or request content type, that can be used as an attribute on API-related classes.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ContentTypeAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the content type header value.
        /// </summary>
        public string? HeaderValue { get; set; }
    }
}
