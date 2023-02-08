// <copyright file="AuthenticationMethod.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace OpenSpartan.Grunt.Models.ApiIngress
{
    /// <summary>
    /// Enum with the types of supported API authentication methods for Halo Infinite endpoints.
    /// </summary>
    [IsAutomaticallySerializable]
    public enum AuthenticationMethod
    {
        /// <summary>
        /// No authentication is required.
        /// </summary>
        None = 0,

        /// <summary>
        /// Classic Spartan token is required.
        /// </summary>
        SpartanToken = 1,

        /// <summary>
        /// XSTS V3 token is required, assigned to the Halo audience scope.
        /// </summary>
        XSTSv3HaloAudience = 4,

        /// <summary>
        /// XSTS V3 token is required, assigned to the Xbox audience scope.
        /// </summary>
        XSTSv3XboxAudience = 9,

        /// <summary>
        /// Client certificate is required.
        /// </summary>
        ClientCertificate = 10,

        /// <summary>
        /// Spartan token V4 is required.
        /// </summary>
        SpartanTokenV4 = 15,
    }
}
