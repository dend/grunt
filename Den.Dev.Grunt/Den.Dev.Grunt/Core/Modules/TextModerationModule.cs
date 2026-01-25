// <copyright file="TextModerationModule.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.Threading.Tasks;
using Den.Dev.Grunt.Core.Foundation;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;

namespace Den.Dev.Grunt.Core.Modules
{
    /// <summary>
    /// Module for text moderation related API operations.
    /// </summary>
    public class TextModerationModule : ModuleBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextModerationModule"/> class.
        /// </summary>
        /// <param name="client">The client instance to use for API requests.</param>
        internal TextModerationModule(ClientBase client)
            : base(client, HaloCoreEndpoints.TextOrigin)
        {
        }

        /// <summary>
        /// Gets a specific moderation proof signing key.
        /// </summary>
        /// <param name="keyId">Key ID. Full list can be obtained by a call to GetSigningKeys.</param>
        /// <returns>An instance of Key containing a single signing key data if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<Key, RawResponseContainer>> GetSigningKey(string keyId)
        {
            return await this.GetAsync<Key>(
                $"/hi/moderation-proof-keys/{keyId}",
                useSpartanToken: false);
        }

        /// <summary>
        /// Gets a list of available moderation proof signing keys.
        /// </summary>
        /// <returns>An instance of ModerationProofKeys containing signing key data if request was successful. Return value is null otherwise.</returns>
        public async Task<HaloApiResultContainer<ModerationProofKeys, RawResponseContainer>> GetSigningKeys()
        {
            return await this.GetAsync<ModerationProofKeys>(
                "/hi/moderation-proof-keys",
                useSpartanToken: false);
        }
    }
}
