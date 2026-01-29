// <copyright file="AuthenticationManager.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Conch.Authentication;
using Den.Dev.Conch.Models.Security;
using Den.Dev.Grunt.Authentication;
using Den.Dev.Grunt.Core;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.Security;
using Den.Dev.Grunt.Util;
using Spectre.Console;

namespace Den.Dev.Grunt.Auditor.Services
{
    /// <summary>
    /// Manages authentication for the Auditor, reusing the existing auth flow.
    /// </summary>
    public class AuthenticationManager
    {
        private const string DefaultTokensFile = "tokens.json";
        private const string DefaultClientConfigFile = "client.json";

        private readonly XboxAuthenticationClient _xboxAuthClient = new();
        private readonly HaloAuthenticationClient _haloAuthClient = new();
        private readonly string _tokensFile;
        private readonly string _clientConfigFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationManager"/> class.
        /// </summary>
        /// <param name="clientConfigFile">Path to client.json configuration file.</param>
        /// <param name="tokensFile">Path to tokens.json cache file.</param>
        public AuthenticationManager(string? clientConfigFile = null, string? tokensFile = null)
        {
            _clientConfigFile = clientConfigFile ?? DefaultClientConfigFile;
            _tokensFile = tokensFile ?? DefaultTokensFile;
        }

        /// <summary>
        /// Gets the authenticated player's XUID.
        /// </summary>
        public string? Xuid { get; private set; }

        /// <summary>
        /// Gets the authenticated player's gamertag.
        /// </summary>
        public string? Gamertag { get; private set; }

        /// <summary>
        /// Gets the authenticated HaloInfiniteClient.
        /// </summary>
        public HaloInfiniteClient? Client { get; private set; }

        /// <summary>
        /// Gets the clearance token.
        /// </summary>
        public string? ClearanceToken { get; private set; }

        /// <summary>
        /// Authenticates and returns a configured HaloInfiniteClient.
        /// </summary>
        /// <returns>True if authentication succeeded, false otherwise.</returns>
        public async Task<bool> AuthenticateAsync()
        {
            var clientConfig = LoadClientConfiguration();
            if (clientConfig == null)
            {
                AnsiConsole.MarkupLine("[red]Could not load client.json configuration file.[/]");
                return false;
            }

            OAuthToken? oauthToken = await GetOrRefreshTokenAsync(clientConfig);
            if (oauthToken == null)
            {
                return false;
            }

            XboxTicket? userTicket = null;
            XboxTicket? haloTicket = null;
            XboxTicket? extendedTicket = null;
            SpartanToken? spartanToken = null;

            await AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Spinner.Known.Dots)
                .StartAsync("[yellow]Authenticating...[/]", async ctx =>
                {
                    // User token
                    ctx.Status("[blue]Requesting user token...[/]");
                    var accessToken = oauthToken.AccessToken;
                    if (string.IsNullOrEmpty(accessToken))
                    {
                        return;
                    }

                    userTicket = await _xboxAuthClient.RequestUserToken(accessToken);
                    if (userTicket == null)
                    {
                        return;
                    }

                    // XSTS tokens
                    ctx.Status("[blue]Requesting XSTS tokens...[/]");
                    var userToken = userTicket.Token;
                    if (string.IsNullOrEmpty(userToken))
                    {
                        return;
                    }

                    haloTicket = await _xboxAuthClient.RequestXstsToken(userToken, HaloCoreEndpoints.HaloWaypointXstsRelyingParty);
                    extendedTicket = await _xboxAuthClient.RequestXstsToken(userToken);

                    if (haloTicket == null || extendedTicket == null)
                    {
                        return;
                    }

                    // Spartan token
                    ctx.Status("[blue]Requesting Spartan token...[/]");
                    var xstsToken = haloTicket.Token;
                    if (string.IsNullOrEmpty(xstsToken))
                    {
                        return;
                    }

                    spartanToken = await _haloAuthClient.GetSpartanToken(xstsToken);
                    if (spartanToken == null)
                    {
                        return;
                    }

                    Xuid = extendedTicket.DisplayClaims?.Xui?[0]?.XUID ?? string.Empty;
                    Gamertag = extendedTicket.DisplayClaims?.Xui?[0]?.Gamertag ?? string.Empty;

                    // Create client with raw responses enabled for validation
                    ctx.Status("[blue]Initializing API client...[/]");
                    Client = new HaloInfiniteClient(spartanToken.Token ?? string.Empty, Xuid, string.Empty, includeRawResponses: true);

                    // Get clearance
                    ctx.Status("[blue]Obtaining clearance...[/]");
                    try
                    {
                        var clearance = (await Client.Settings.GetClearance("RETAIL", "UNUSED", "268411.25.10.26.1801-0", "1.13")).Result;
                        if (clearance != null)
                        {
                            ClearanceToken = clearance.FlightConfigurationId ?? string.Empty;
                            Client.ClearanceToken = ClearanceToken;
                        }
                    }
                    catch
                    {
                        // Clearance is optional
                    }
                });

            if (userTicket == null)
            {
                AnsiConsole.MarkupLine("[red]User token request failed. Token may be expired.[/]");

                // Try refreshing
                var refreshToken = oauthToken.RefreshToken;
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    oauthToken = await RefreshTokenAsync(clientConfig, refreshToken);
                    if (oauthToken != null)
                    {
                        return await AuthenticateAsync();
                    }
                }

                // Request new token
                oauthToken = await RequestNewTokenAsync(clientConfig);
                if (oauthToken != null)
                {
                    return await AuthenticateAsync();
                }

                return false;
            }

            if (haloTicket == null || extendedTicket == null || spartanToken == null || Client == null)
            {
                AnsiConsole.MarkupLine("[red]Authentication failed.[/]");
                return false;
            }

            AnsiConsole.MarkupLine($"[green]Authenticated as[/] [cyan]{Gamertag ?? Xuid}[/]");
            return true;
        }

        private ClientConfiguration? LoadClientConfiguration()
        {
            if (!File.Exists(_clientConfigFile))
            {
                return null;
            }

            return ConfigurationReader.ReadConfiguration<ClientConfiguration>(_clientConfigFile);
        }

        private async Task<OAuthToken?> GetOrRefreshTokenAsync(ClientConfiguration clientConfig)
        {
            if (File.Exists(_tokensFile))
            {
                var token = ConfigurationReader.ReadConfiguration<OAuthToken>(_tokensFile);
                if (token != null)
                {
                    return token;
                }
            }

            return await RequestNewTokenAsync(clientConfig);
        }

        private async Task<OAuthToken?> RefreshTokenAsync(ClientConfiguration clientConfig, string refreshToken)
        {
            OAuthToken? token = null;

            await AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Spinner.Known.Dots)
                .StartAsync("[yellow]Refreshing token...[/]", async ctx =>
                {
                    token = await _xboxAuthClient.RefreshOAuthToken(
                        clientConfig.ClientId ?? string.Empty,
                        refreshToken,
                        clientConfig.RedirectUrl ?? string.Empty,
                        clientConfig.ClientSecret ?? string.Empty);

                    if (token != null)
                    {
                        SaveToken(token);
                    }
                });

            return token;
        }

        private async Task<OAuthToken?> RequestNewTokenAsync(ClientConfiguration clientConfig)
        {
            var clientId = clientConfig.ClientId ?? string.Empty;
            var redirectUrl = clientConfig.RedirectUrl ?? string.Empty;
            var clientSecret = clientConfig.ClientSecret ?? string.Empty;

            var url = _xboxAuthClient.GenerateAuthUrl(clientId, redirectUrl);

            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Panel(
                $"Visit this URL to authenticate:\n\n[link={url}]{url}[/]\n\nCopy the code from the redirect URL.")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Yellow)
                .Header("[yellow]Authentication Required[/]"));
            AnsiConsole.WriteLine();

            var code = AnsiConsole.Prompt(
                new TextPrompt<string>("[cyan]Code:[/]")
                    .PromptStyle("green"));

            OAuthToken? token = null;

            await AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Spinner.Known.Dots)
                .StartAsync("[yellow]Requesting OAuth token...[/]", async ctx =>
                {
                    token = await _xboxAuthClient.RequestOAuthToken(
                        clientId,
                        code,
                        redirectUrl,
                        clientSecret);

                    if (token != null)
                    {
                        SaveToken(token);
                    }
                });

            if (token == null)
            {
                AnsiConsole.MarkupLine("[red]Could not obtain OAuth token.[/]");
            }

            return token;
        }

        private void SaveToken(OAuthToken token)
        {
            try
            {
                var json = JsonSerializer.Serialize(token);
                File.WriteAllText(_tokensFile, json);
            }
            catch
            {
                // Silent fail for token storage
            }
        }
    }
}
