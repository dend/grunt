// <copyright file="IHaloAuthenticationClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Threading;
using System.Threading.Tasks;
using Den.Dev.Grunt.Models.Security;

namespace Den.Dev.Grunt.Authentication
{
    /// <summary>
    /// Interface for the Halo authentication client.
    /// </summary>
    public interface IHaloAuthenticationClient
    {
        /// <summary>
        /// Gets the Spartan V4 token.
        /// </summary>
        /// <param name="xstsToken">XSTS token from the Xbox Live authentication flow.</param>
        /// <param name="version">Version for the Spartan token to be obtained. Halo Infinite uses 4, while Halo 5 uses 3.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="SpartanToken"/> representing the authentication token. Otherwise, returns null.</returns>
        Task<SpartanToken?> GetSpartanTokenAsync(string xstsToken, int version = 4, CancellationToken cancellationToken = default);
    }
}
