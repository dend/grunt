// <copyright file="Extensions.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;
using System.Reflection;
using Den.Dev.Orion.Models;

namespace Den.Dev.Orion.Util
{
    /// <summary>
    /// Utilities used for extending core functionality offered by the CLR.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Determines the header value for a specific API content type.
        /// </summary>
        /// <param name="value">API content type represented by <see cref="APIContentType"/>.</param>
        /// <returns>If successful, returns the string value for the header associated with a content type.</returns>
        public static string? GetHeaderValue(this APIContentType value)
        {
            Type type = value.GetType();
            FieldInfo? fieldInfo = type.GetField(name: value.ToString());

            if (fieldInfo != null)
            {
                if (fieldInfo.GetCustomAttributes(typeof(ContentTypeAttribute), false) is ContentTypeAttribute[] attributes)
                {
                    return attributes.Length > 0 ? attributes[0].HeaderValue : null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
