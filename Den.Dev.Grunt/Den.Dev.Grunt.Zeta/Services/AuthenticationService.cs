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
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.UI;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.Services
{
    public class AuthenticationService
    {
        private const string TokensFile = "tokens.json";
        private const string ClientConfigFile = "client.json";

        private readonly XboxAuthenticationClient _xboxAuthClient = new();
        private readonly HaloAuthenticationClient _haloAuthClient = new();

        public async Task<ExecutionContext?> AuthenticateAsync()
        {
            var clientConfig = LoadClientConfiguration();
            if (clientConfig == null)
            {
                AnsiConsole.MarkupLine("[red]●[/] Could not load [cyan]client.json[/] configuration file.");
                return null;
            }

            OAuthToken? oauthToken = await GetOrRefreshTokenAsync(clientConfig);
            if (oauthToken == null)
            {
                return null;
            }

            var context = new ExecutionContext { OAuthToken = oauthToken };

            XboxTicket? userTicket = null;
            XboxTicket? haloTicket = null;
            XboxTicket? extendedTicket = null;
            SpartanToken? spartanToken = null;

            await AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Theme.Spinner)
                .StartAsync("[yellow]Initializing authentication[/]", async ctx =>
                {
                    // User token
                    ctx.Status("[bold blue]Requesting user token[/]");
                    var accessToken = oauthToken.AccessToken;
                    if (string.IsNullOrEmpty(accessToken))
                    {
                        WriteLog("[red]Access token is null or empty[/]");
                        return;
                    }

                    userTicket = await _xboxAuthClient.RequestUserToken(accessToken);

                    if (userTicket == null)
                    {
                        WriteLog("[red]User token request failed[/]");
                        return;
                    }

                    WriteLog("User token [green]acquired[/]");

                    // XSTS tokens
                    ctx.Status("[bold blue]Requesting XSTS tokens[/]");
                    var userToken = userTicket.Token;
                    if (string.IsNullOrEmpty(userToken))
                    {
                        WriteLog("[red]User token is null or empty[/]");
                        return;
                    }

                    WriteLog("Requesting Halo XSTS token");
                    haloTicket = await _xboxAuthClient.RequestXstsToken(userToken, HaloCoreEndpoints.HaloWaypointXstsRelyingParty);
                    WriteLog("Requesting extended XSTS token");
                    extendedTicket = await _xboxAuthClient.RequestXstsToken(userToken);

                    if (haloTicket == null || extendedTicket == null)
                    {
                        WriteLog("[red]XSTS token request failed[/]");
                        return;
                    }

                    WriteLog("XSTS tokens [green]acquired[/]");

                    // Spartan token
                    ctx.Status("[bold blue]Requesting Spartan token[/]");
                    var xstsToken = haloTicket.Token;
                    if (string.IsNullOrEmpty(xstsToken))
                    {
                        WriteLog("[red]XSTS token is null or empty[/]");
                        return;
                    }

                    spartanToken = await _haloAuthClient.GetSpartanToken(xstsToken);

                    if (spartanToken == null)
                    {
                        WriteLog("[red]Spartan token request failed[/]");
                        return;
                    }

                    WriteLog("Spartan token [green]acquired[/]");

                    context.SpartanToken = spartanToken.Token ?? string.Empty;
                    context.Xuid = extendedTicket.DisplayClaims?.Xui?[0]?.XUID ?? string.Empty;
                    context.Gamertag = extendedTicket.DisplayClaims?.Xui?[0]?.Gamertag ?? string.Empty;

                    // Create clients
                    ctx.Status("[bold blue]Initializing API clients[/]");
                    context.HaloClient = new HaloInfiniteClient(context.SpartanToken, context.Xuid);
                    context.WaypointClient = new WaypointClient(context.SpartanToken, context.Xuid);
                    WriteLog("Halo Infinite client [green]online[/]");
                    WriteLog("Waypoint client [green]online[/]");

                    // Clearance
                    ctx.Status("[bold blue]Obtaining clearance[/]");
                    try
                    {
                        var clearance = (await context.HaloClient.Settings.GetClearance("RETAIL", "UNUSED", "268411.25.10.26.1801-0", "1.13")).Result;
                        if (clearance != null)
                        {
                            context.ClearanceToken = clearance.FlightConfigurationId ?? string.Empty;
                            context.HaloClient.ClearanceToken = context.ClearanceToken;
                            WriteLog("Clearance [green]granted[/]");
                        }
                        else
                        {
                            WriteLog("Clearance [yellow]not available[/]");
                        }
                    }
                    catch
                    {
                        WriteLog("Clearance [yellow]skipped[/]");
                    }
                });

            // Final status report
            if (userTicket == null)
            {
                AnsiConsole.MarkupLine("[red]●[/] User token failed");
                AnsiConsole.MarkupLine("[dim]Token expired, refreshing...[/]");
                var refreshToken = oauthToken.RefreshToken;
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    oauthToken = await RefreshTokenAsync(clientConfig, refreshToken);
                }

                if (oauthToken == null)
                {
                    oauthToken = await RequestNewTokenAsync(clientConfig);
                }

                if (oauthToken == null)
                {
                    return null;
                }

                context.OAuthToken = oauthToken;
                return await AuthenticateAsync(); // Retry
            }

            if (haloTicket == null || extendedTicket == null)
            {
                AnsiConsole.MarkupLine("[red]●[/] Authentication failed - XSTS tokens");
                return null;
            }

            if (spartanToken == null)
            {
                AnsiConsole.MarkupLine("[red]●[/] Authentication failed - Spartan token");
                return null;
            }

            AnsiConsole.MarkupLine($"[bold green]Authentication complete[/] - Welcome, [cyan]{context.Gamertag ?? context.Xuid}[/]");
            AnsiConsole.WriteLine();
            return context;
        }

        private static void WriteLog(string message)
        {
            AnsiConsole.MarkupLine($"[grey]LOG:[/] {message}[grey]...[/]");
        }

        private ClientConfiguration? LoadClientConfiguration()
        {
            if (!File.Exists(ClientConfigFile))
            {
                return null;
            }

            return ConfigurationReader.ReadConfiguration<ClientConfiguration>(ClientConfigFile);
        }

        private async Task<OAuthToken?> GetOrRefreshTokenAsync(ClientConfiguration clientConfig)
        {
            if (File.Exists(TokensFile))
            {
                AnsiConsole.MarkupLine("[dim]Loading saved tokens...[/]");
                var token = ConfigurationReader.ReadConfiguration<OAuthToken>(TokensFile);
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
                .Spinner(Theme.Spinner)
                .StartAsync("[yellow]Refreshing OAuth token[/]", async ctx =>
                {
                    WriteLog("Contacting Microsoft identity service");
                    token = await _xboxAuthClient.RefreshOAuthToken(
                        clientConfig.ClientId ?? string.Empty,
                        refreshToken,
                        clientConfig.RedirectUrl ?? string.Empty,
                        clientConfig.ClientSecret ?? string.Empty);

                    if (token != null)
                    {
                        WriteLog("Token refresh [green]successful[/]");
                        SaveToken(token);
                    }
                    else
                    {
                        WriteLog("Token refresh [red]failed[/]");
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

            var panel = new Panel(
                $"Visit the URL below to authenticate:\n\n" +
                $"[link={url}]{url}[/]\n\n" +
                $"[dim]Copy the code from the redirect URL[/]")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Yellow)
                .Header("[yellow]Authentication Required[/]")
                .Expand();

            AnsiConsole.Write(panel);
            AnsiConsole.WriteLine();

            var code = AnsiConsole.Prompt(
                new TextPrompt<string>("[cyan]Code[/]:")
                    .PromptStyle("green"));

            OAuthToken? token = null;

            await AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Theme.Spinner)
                .StartAsync("[yellow]Requesting OAuth token[/]", async ctx =>
                {
                    WriteLog("Exchanging authorization code");
                    token = await _xboxAuthClient.RequestOAuthToken(
                        clientId,
                        code,
                        redirectUrl,
                        clientSecret);

                    if (token != null)
                    {
                        WriteLog("OAuth token [green]received[/]");
                        ctx.Status("[bold blue]Saving credentials[/]");
                        SaveToken(token);
                        WriteLog("Credentials [green]saved[/]");
                    }
                    else
                    {
                        WriteLog("OAuth token request [red]failed[/]");
                    }
                });

            if (token != null)
            {
                AnsiConsole.MarkupLine("[bold green]Token obtained successfully[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]●[/] Could not obtain OAuth token.");
            }

            return token;
        }

        private void SaveToken(OAuthToken token)
        {
            try
            {
                var json = JsonSerializer.Serialize(token);
                File.WriteAllText(TokensFile, json);
            }
            catch
            {
                // Silent fail for token storage
            }
        }
    }
}
