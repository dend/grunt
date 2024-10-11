// <copyright file="XboxAuthenticationClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Orion is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Den.Dev.Orion.Endpoints;
using Den.Dev.Orion.Models.Security;
using Den.Dev.Orion.Util;

namespace Den.Dev.Orion.Authentication
{
    /// <summary>
    /// Xbox authentication client, used to provide the scaffolding to get the
    /// proper Xbox Live tokens.
    /// </summary>
    public class XboxAuthenticationClient
    {
        private readonly HttpClient client = new();
        private readonly ECDCertificatePoPCryptoProvider popCryptoProvider = new();
        private readonly string codeVerifier;
        private readonly string codeChallenge;

        /// <summary>
        /// Initializes a new instance of the <see cref="XboxAuthenticationClient"/> class.
        /// </summary>
        public XboxAuthenticationClient()
        {
            this.codeVerifier = this.GenerateCodeVerifier();
            this.codeChallenge = this.GenerateCodeChallenge(this.codeVerifier);
        }

        /// <summary>
        /// Generates the authentication URL that can be used to produce the temporary code
        /// for subsequent Xbox Live authentication flows.
        /// </summary>
        /// <param name="clientId">Client ID defined for the registered application in the Azure Portal.</param>
        /// <param name="redirectUrl">Redirect URL defined for the registered application in the Azure Portal.</param>
        /// <param name="scopes">A list of scopes used for authentication against the Xbox Live APIs.</param>
        /// <param name="state">Temporary state indicator.</param>
        /// <returns>Returns the full authentication URL that can be pasted in a web browser.</returns>
        public string GenerateAuthUrl(string clientId, string redirectUrl, string[]? scopes = null, string state = "")
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("client_id", clientId);
            queryString.Add("response_type", "code");
            queryString.Add("approval_prompt", "auto");

            if (scopes != null && scopes.Length > 0)
            {
                queryString.Add("scope", string.Join(" ", scopes));
            }
            else
            {
                queryString.Add("scope", string.Join(" ", GlobalConstants.DEFAULT_AUTH_SCOPES));
            }

            queryString.Add("redirect_uri", redirectUrl);

            if (!string.IsNullOrEmpty(state))
            {
                queryString.Add("state", state);
            }

            return XboxEndpoints.XboxLiveAuthorize + "?" + queryString.ToString();
        }

        /// <summary>
        /// Requests the OAuth token for the Xbox Live authentication flow.
        /// </summary>
        /// <param name="clientId">Client ID defined for the registered application in the Azure Portal.</param>
        /// <param name="authorizationCode">Authorization code provided by visiting the URL from the <see cref="GenerateAuthUrl"/> function.</param>
        /// <param name="redirectUrl">Redirect URL defined for the registered application in the Azure Portal.</param>
        /// <param name="clientSecret">Client secret defined for the registered application in the Azure Portal.</param>
        /// <param name="scopes">A list of scopes used for authentication against the Xbox Live APIs.</param>
        /// <param name="useCodeVerifier">Determines whether the code verifier should be used. If not using SISU flows, this can be ignored.</param>
        /// <returns>If successful, returns an instance of <see cref="OAuthToken"/> representing the OAuth token used for authentication. Otherwise, returns null.</returns>
        public async Task<OAuthToken?> RequestOAuthToken(string clientId, string authorizationCode, string redirectUrl, string clientSecret = "", string[]? scopes = null, bool useCodeVerifier = false)
        {
            Dictionary<string, string> tokenRequestContent = new()
            {
                { "grant_type", "authorization_code" },
                { "code", authorizationCode },
                { "approval_prompt", "auto" },
            };

            if (scopes != null && scopes.Length > 0)
            {
                tokenRequestContent.Add("scope", string.Join(" ", scopes));
            }
            else
            {
                tokenRequestContent.Add("scope", string.Join(" ", GlobalConstants.DEFAULT_AUTH_SCOPES));
            }

            tokenRequestContent.Add("redirect_uri", redirectUrl);
            tokenRequestContent.Add("client_id", clientId);
            if (!string.IsNullOrEmpty(clientSecret))
            {
                tokenRequestContent.Add("client_secret", clientSecret);
            }

            if (useCodeVerifier)
            {
                tokenRequestContent.Add("code_verifier", this.codeVerifier);
            }

            var response = await this.client.PostAsync(XboxEndpoints.XboxLiveToken, new FormUrlEncodedContent(tokenRequestContent));

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<OAuthToken>(response.Content.ReadAsStringAsync().Result)
                : null;
        }

        /// <summary>
        /// Refreshes an existing OAuth token.
        /// </summary>
        /// <param name="clientId">Client ID defined for the registered application in the Azure Portal.</param>
        /// <param name="refreshToken">Refresh token obtained from a previous authorization flow.</param>
        /// <param name="redirectUrl">Redirect URL defined for the registered application in the Azure Portal.</param>
        /// <param name="clientSecret">Client secret defined for the registered application in the Azure Portal.</param>
        /// <param name="scopes">A list of scopes used for authentication against the Xbox Live APIs.</param>
        /// <returns>If successful, returns an instance of <see cref="OAuthToken"/> representing the OAuth token used for authentication. Otherwise, returns null.</returns>
        public async Task<OAuthToken?> RefreshOAuthToken(string clientId, string refreshToken, string redirectUrl, string clientSecret = "", string[]? scopes = null)
        {
            Dictionary<string, string> tokenRequestContent = new();

            tokenRequestContent.Add("grant_type", "refresh_token");
            tokenRequestContent.Add("refresh_token", refreshToken);

            if (scopes != null && scopes.Length > 0)
            {
                tokenRequestContent.Add("scope", string.Join(" ", scopes));
            }
            else
            {
                tokenRequestContent.Add("scope", string.Join(" ", GlobalConstants.DEFAULT_AUTH_SCOPES));
            }

            tokenRequestContent.Add("redirect_uri", redirectUrl);
            tokenRequestContent.Add("client_id", clientId);
            if (!string.IsNullOrEmpty(clientSecret))
            {
                tokenRequestContent.Add("client_secret", clientSecret);
            }

            var response = await this.client.PostAsync(XboxEndpoints.XboxLiveToken, new FormUrlEncodedContent(tokenRequestContent));

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<OAuthToken>(response.Content.ReadAsStringAsync().Result)
                : null;
        }

        /// <summary>
        /// Requests a user token for Xbox Live API authentication.
        /// </summary>
        /// <param name="accessToken">Previously generated Xbox Live OAuth access token.</param>
        /// <returns>If successful, returns an instance of <see cref="XboxTicket"/> representing the authentication ticket. Otherwise, returns null.</returns>
        public async Task<XboxTicket?> RequestUserToken(string accessToken)
        {
            XboxTicketRequest ticketData = new()
            {
                RelyingParty = XboxEndpoints.XboxLiveAuthRelyingParty,
                TokenType = "JWT",
                Properties = new XboxTicketProperties()
                {
                    AuthMethod = "RPS",
                    SiteName = "user.auth.xboxlive.com",
                    RpsTicket = string.Concat("d=", accessToken),
                },
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(XboxEndpoints.XboxLiveUserAuthenticate),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonSerializer.Serialize(ticketData), Encoding.UTF8, "application/json"),
            };

            request.Headers.Add("x-xbl-contract-version", "1");

            var response = await this.client.SendAsync(request);
            var responseData = response.Content.ReadAsStringAsync().Result;

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<XboxTicket>(responseData)
                : null;
        }

        /// <summary>
        /// Requests the Xbox Live Security Token (XSTS) token for use with Halo API authentication flow.
        /// </summary>
        /// <param name="userToken">Previously generated Xbox Live user token.</param>
        /// <param name="useHaloRelyingParty">Determines whether the Halo relying party is used or a more generic Xbox Live one. Using the Xbox Live relying party will not enable you to access Halo APIs.</param>
        /// <param name="deviceToken">Optional device token, if available.</param>
        /// <param name="titleToken">Optional title token, if available.</param>
        /// <returns>If successful, returns an instance of <see cref="XboxTicket"/> representing the authentication ticket. Otherwise, returns null.</returns>
        public async Task<XboxTicket?> RequestXstsToken(string userToken, bool useHaloRelyingParty = true, string? deviceToken = null, string? titleToken = null)
        {
            XboxTicketRequest ticketData = new();

            if (useHaloRelyingParty)
            {
                ticketData.RelyingParty = HaloCoreEndpoints.HaloWaypointXstsRelyingParty;
            }
            else
            {
                ticketData.RelyingParty = XboxEndpoints.XboxLiveRelyingParty;
            }

            ticketData.TokenType = "JWT";
            ticketData.Properties = new XboxTicketProperties()
            {
                UserTokens = new string[] { userToken },
                SandboxId = "RETAIL",
                DeviceToken = deviceToken,
                TitleToken = titleToken,
            };

            var data = JsonSerializer.Serialize(ticketData);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(XboxEndpoints.XboxLiveXstsAuthorize),
                Method = HttpMethod.Post,
                Content = new StringContent(data, Encoding.UTF8, "application/json"),
            };

            request.Headers.Add("x-xbl-contract-version", "1");

            var response = await this.client.SendAsync(request);
            var responseData = response.Content.ReadAsStringAsync().Result;

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<XboxTicket>(responseData)
                : null;
        }

        /// <summary>
        /// Assemble existing token pieces into a valid Xbox Live 3.0 token.
        /// </summary>
        /// <param name="userHash">User has for the authenticating Xbox Live user.</param>
        /// <param name="userToken">Previously generated Xbox Live user token.</param>
        /// <returns>The assembled Xbox Live 3.0 token string.</returns>
        public string GetXboxLiveV3Token(string userHash, string userToken)
        {
            return $"XBL3.0 x={userHash};{userToken}";
        }

        /// <summary>
        /// Generates a device token that can be used for XSTS token acquisition.
        /// </summary>
        /// <param name="deviceType">Type of device. Default is Win32.</param>
        /// <param name="version">OS version on the device. Default is 10.0.22000 for Windows 11.</param>
        /// <param name="authMethod">Authentication method used. Default is ProofOfPossession.</param>
        /// <returns>If successful, returns an instance of <see cref="XboxTicket"/> that contains the device token. Otherwise, returns null."</returns>
        public async Task<XboxTicket?> RequestDeviceToken(string deviceType = "Win32", string version = "10.0.22000", string authMethod = "ProofOfPossession")
        {
            XboxTicketRequest ticketData = new()
            {
                RelyingParty = "http://auth.xboxlive.com",
                TokenType = "JWT",
                Properties = new()
                {
                    DeviceType = deviceType,
                    Id = $"{{{Guid.NewGuid().ToString().ToUpper()}}}",
                    Version = version,
                    AuthMethod = authMethod,
                    ProofKey = this.popCryptoProvider.ProofKey,
                },
            };

            var rawBody = JsonSerializer.Serialize(ticketData);
            var body = new StringContent(rawBody, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(XboxEndpoints.XboxLiveDeviceAuthenticate),
                Method = HttpMethod.Post,
                Content = body,
            };

            var signature = this.SignRequest(XboxEndpoints.XboxLiveDeviceAuthenticate, string.Empty, rawBody);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Signature", signature);
            request.Headers.Add("x-xbl-contract-version", "2");

            var response = await this.client.SendAsync(request);
            var responseData = response.Content.ReadAsStringAsync().Result;

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<XboxTicket>(responseData)
                : null;
        }

        /// <summary>
        /// Initializes a new SISU session.
        /// </summary>
        /// <param name="appId">Application ID.</param>
        /// <param name="titleId">Title ID.</param>
        /// <param name="deviceToken">Previously-generated device token.</param>
        /// <param name="offers">List of associated offers.</param>
        /// <param name="redirectUri">Redirect URI used for authentication.</param>
        /// <param name="tokenType">Token type. Default is "code".</param>
        /// <param name="sandbox">The sandbox to be used. Default is "RETAIL".</param>
        /// <returns>If successful, returns an instance of <see cref="SISUAuthenticationResponse"/>. Otherwise, returns null.</returns>
        public async Task<SISUAuthenticationResponse?> RequestSISUSession(string appId, string titleId, string deviceToken, List<string> offers, string redirectUri, string tokenType = "code", string sandbox = "RETAIL")
        {
            XboxTicketRequest ticketData = new()
            {
                AppId = appId,
                TitleId = titleId,
                DeviceToken = deviceToken,
                Sandbox = sandbox,
                TokenType = tokenType,
                Offers = offers,
                RedirectUri = redirectUri,
                ProofKey = this.popCryptoProvider.ProofKey,
                Query = new()
                {
                    CodeChallenge = this.codeChallenge,
                    CodeChallengeMethod = "S256",
                    State = Guid.NewGuid().ToString(),
                },
            };

            var rawBody = JsonSerializer.Serialize(ticketData);
            var body = new StringContent(rawBody, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(XboxEndpoints.XboxLiveSisuAuthenticate),
                Method = HttpMethod.Post,
                Content = body,
            };

            var signature = this.SignRequest(XboxEndpoints.XboxLiveSisuAuthenticate, string.Empty, rawBody);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Signature", signature);
            request.Headers.Add("x-xbl-contract-version", "2");

            var response = await this.client.SendAsync(request);
            var responseData = response.Content.ReadAsStringAsync().Result;

            SISUAuthenticationResponse? authResponse = null;

            if (response.IsSuccessStatusCode)
            {
                authResponse = JsonSerializer.Deserialize<SISUAuthenticationResponse>(responseData);
                IEnumerable<string>? headerValues;
                if (response.Headers.TryGetValues("X-SessionId", out headerValues))
                {
                    if (authResponse != null)
                    {
                        authResponse.SessionId = headerValues.First();
                    }
                }
            }

            return authResponse;
        }

        /// <summary>
        /// Uses the SISU endpoint to authorize the user, device, and the title.
        /// </summary>
        /// <remarks>
        /// Under most conditions, this will not be used and instead standard XSTS authorization should be relied upon. However, when special permission tokens are needed (e.g., when using the lobby endpoints), this is the way.
        /// </remarks>
        /// <param name="deviceToken">Previously generated device token.</param>
        /// <param name="accessToken">Access token from the OAuth authentication endpoint.</param>
        /// <param name="appId">Application ID.</param>
        /// <param name="sessionId">Session ID from the SISU authentication request.</param>
        /// <param name="sandbox">Sandbox to be used. Default value is "RETAIL".</param>
        /// <param name="siteName">Site name to be used for the request. Default value is "user.auth.xboxlive.com".</param>
        /// <param name="useModernGamertag">Determines whether modern gamertags are used. Default value is true.</param>
        /// <returns>If successful, returns an instance of <see cref="SISUAuthorizationResponse"/> that contains device, authorization, user, and title tokens. Otherwise, returns null.</returns>
        public async Task<SISUAuthorizationResponse?> RequestSISUTokens(string deviceToken, string accessToken, string appId, string? sessionId = null, string sandbox = "RETAIL", string siteName = "user.auth.xboxlive.com", bool useModernGamertag = true)
        {
            XboxTicketRequest ticketData = new()
            {
                AppId = appId,
                DeviceToken = deviceToken,
                ProofKey = this.popCryptoProvider.ProofKey,
                Sandbox = sandbox,
                AccessToken = $"t={accessToken}",
                UseModernGamertag = useModernGamertag,
                SessionId = sessionId,
                SiteName = siteName,
            };

            var rawBody = JsonSerializer.Serialize(ticketData);
            var body = new StringContent(rawBody, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(XboxEndpoints.XboxLiveSisuAuthorize),
                Method = HttpMethod.Post,
                Content = body,
            };

            var signature = this.SignRequest(XboxEndpoints.XboxLiveSisuAuthorize, string.Empty, rawBody);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Signature", signature);
            request.Headers.Add("x-xbl-contract-version", "2");

            var response = await this.client.SendAsync(request);
            var responseData = response.Content.ReadAsStringAsync().Result;

            return response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<SISUAuthorizationResponse>(responseData)
                : null;
        }

        private string SignRequest(string reqUri, string token, string body)
        {
            var timestamp = this.GetWindowsTimestamp();
            var data = this.GenerateSigningPayload(timestamp, reqUri, token, body);
            var signature = this.Sign(timestamp, data);
            return Convert.ToBase64String(signature);
        }

        private byte[] GenerateSigningPayload(ulong windowsTimestamp, string uri, string token, string payload)
        {
            var pathAndQuery = new Uri(uri).PathAndQuery;

            var allocSize =
                4 + 1 +
                8 + 1 +
                4 + 1 +
                pathAndQuery.Length + 1 +
                token.Length + 1 +
                payload.Length + 1;
            var bytes = new byte[allocSize];

            var policyVersion = BitConverter.GetBytes(1);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(policyVersion);
            }

            Array.Copy(policyVersion, 0, bytes, 0, 4);

            var windowsTimestampBytes = BitConverter.GetBytes(windowsTimestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(windowsTimestampBytes);
            }

            Array.Copy(windowsTimestampBytes, 0, bytes, 5, 8);

            var strs =
                $"POST\0" +
                $"{pathAndQuery}\0" +
                $"{token}\0" +
                $"{payload}\0";
            var strsBytes = Encoding.ASCII.GetBytes(strs);
            Array.Copy(strsBytes, 0, bytes, 14, strsBytes.Length);

            return bytes;
        }

        private ulong GetWindowsTimestamp()
        {
            var unixTimestamp = (ulong)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            ulong windowsTimestamp = (unixTimestamp + 11644473600u) * 10000000u;
            return windowsTimestamp;
        }

        private byte[] Sign(ulong windowsTimestamp, byte[] bytes)
        {
            var signature = this.popCryptoProvider.Sign(bytes);

            var policyVersion = BitConverter.GetBytes(1);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(policyVersion);
            }

            var windowsTimestampBytes = BitConverter.GetBytes(windowsTimestamp);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(windowsTimestampBytes);
            }

            var header = new byte[signature.Length + 12];
            Array.Copy(policyVersion, 0, header, 0, 4);
            Array.Copy(windowsTimestampBytes, 0, header, 4, 8);
            Array.Copy(signature, 0, header, 12, signature.Length);

            return header;
        }

        private string GenerateCodeVerifier()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRTSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var nonce = new char[32];
            for (int i = 0; i < nonce.Length; i++)
            {
                nonce[i] = chars[random.Next(chars.Length)];
            }

            var data = new string(nonce);

            char[] padding = { '=' };

            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data)).TrimEnd(padding).Replace('+', '-').Replace('/', '_');
        }

        private string GenerateCodeChallenge(string codeVerifier)
        {
            var hash = SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier));
            var b64Hash = Convert.ToBase64String(hash);
            var code = Regex.Replace(b64Hash, "\\+", "-");
            code = Regex.Replace(code, "\\/", "_");
            code = Regex.Replace(code, "=+$", string.Empty);
            return code;
        }
    }
}
