// <copyright file="HaloAuthenticationClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models.Security;
using Den.Dev.Grunt.Util;

namespace Den.Dev.Grunt.Authentication
{
    /// <summary>
    /// Halo authentication client, used to provide the key authentication
    /// data to perform Halo API requests.
    /// </summary>
    public sealed class HaloAuthenticationClient : IHaloAuthenticationClient
    {
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="HaloAuthenticationClient"/> class.
        /// </summary>
        /// <param name="httpClient">Optional HttpClient instance to use. If not provided, a new instance will be created.</param>
        public HaloAuthenticationClient(HttpClient? httpClient = null)
        {
            this.client = httpClient ?? new HttpClient();
        }

        /// <summary>
        /// Gets the Spartan V4 token.
        /// </summary>
        /// <param name="xstsToken">XSTS token from the Xbox Live authentication flow.</param>
        /// <param name="version">Version for the Spartan token to be obtained. Halo Infinite uses 4, while Halo 5 uses 3.</param>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>If successful, returns an instance of <see cref="SpartanToken"/> representing the authentication token. Otherwise, returns null.</returns>
        public async Task<SpartanToken?> GetSpartanTokenAsync(string xstsToken, int version = 4, CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrEmpty(xstsToken);

            string? data = string.Empty;

            if (version == 4)
            {
                SpartanTokenRequest tokenRequest = new()
                {
                    Audience = "urn:343:s3:services",
                    MinVersion = version.ToString(),
                    Proof = new SpartanTokenProof[]
                    {
                        new SpartanTokenProof()
                        {
                            Token = xstsToken,
                            TokenType = "Xbox_XSTSv3",
                        },
                    },
                };

                data = JsonSerializer.Serialize(tokenRequest);
            }

            using var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(HaloCoreEndpoints.SpartanTokenEndpoint),
                Method = version == 4 ? HttpMethod.Post : HttpMethod.Get,
                Content = version == 4 ? new StringContent(data, Encoding.UTF8, "application/json") : null,
            };

            request.Headers.Add("User-Agent", GlobalConstants.HALO_PC_USER_AGENT);
            request.Headers.Add("Accept", "application/json");

            if (version == 3)
            {
                request.Headers.Add("X-343-Authorization-XBL3", $"XBL3.0 x=*;{xstsToken}");
            }

            var response = await this.client.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                response.Dispose();
                return JsonSerializer.Deserialize<SpartanToken>(responseContent);
            }

            response.Dispose();
            return null;
        }
    }
}
