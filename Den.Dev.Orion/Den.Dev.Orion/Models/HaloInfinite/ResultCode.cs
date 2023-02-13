// <copyright file="ResultCode.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Den.Dev.Orion.Models.HaloInfinite
{
    /// <summary>
    /// Code representing the result of the query.
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// The result was not found.
        /// </summary>
        NotFound = 1,

        /// <summary>
        /// The result was successfully found.
        /// </summary>
        Success = 0,
    }
}
