// <copyright file="APIContentType.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models
{
    /// <summary>
    /// Type of content to return for the Halo API.
    /// </summary>
    public enum APIContentType
    {
        /// <summary>
        /// JSON data format.
        /// </summary>
        [ContentType(HeaderValue = "application/json")]
        Json,

        /// <summary>
        /// <see href="https://microsoft.github.io/bond/manual/bond_cs.html#compact-binary">Microsoft Bond Compact Binary</see> data format.
        /// </summary>
        [ContentType(HeaderValue = "application/x-bond-compact-binary")]
        BondCompactBinary,
    }
}
