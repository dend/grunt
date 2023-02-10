using Den.Dev.Orion.Authentication;
using Den.Dev.Orion.Core;
using Den.Dev.Orion.Models;
using Den.Dev.Orion.Util;
using System;
using System.CommandLine;
using System.Text.Json;

namespace Den.Dev.Orion.Composer
{
    internal class Program
    {
        static HaloInfiniteClient? haloInfiniteClient = null;

        static async Task<int> Main(string[] args)
        {
            haloInfiniteClient = InstantiateClient();

            var rootCommand = new RootCommand();

            var domainArgument = new Option<string>(
                name: "--domain",
                description: "Path to the SQLite file that stores the data.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = true
            };
            rootCommand.AddGlobalOption(domainArgument);

            var isXuidFileArgument = new Option<bool>(
                name: "--is-xuid-file",
                description: "Determines whether the XUID parameter is a file or a singular XUID.",
                getDefaultValue: () => false)
            {
                IsRequired = true
            };

            var xuidArgument = new Option<string>(
                name: "--xuid",
                description: "Player XUID for whom the matches should be obtained or the path to the file that contains multiple XUIDs (one XUID per line). The latter requires that --is-xuid-file is set to true.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = true
            };

            var startArgument = new Option<int>(
                name: "--start",
                description: "If necessary to get only a subset of matches, can be used to limit the query. Default is zero.",
                getDefaultValue: () => 0)
            {
                IsRequired = false
            };

            var countArgument = new Option<int>(
                name: "--count",
                description: "Count of results to ingest. Default is all of the available matches, but if a limit is necessary it can be specified here.",
                getDefaultValue: () => -1)
            {
                IsRequired = false
            };

            var lifecycleModeArgument = new Option<string>(
                name: "--lifecycle-mode",
                description: "Kinds of matches to obtain. Default is all matches, but can also be set to 'matchmade' or 'custom'.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = true
            };

            var getCommand = new Command("get", "Gets data from the Halo API.");
            rootCommand.AddCommand(getCommand);

            var matchesCommand = new Command("matches", "Gets match data from the Halo API.")
            {
                isXuidFileArgument,
                xuidArgument,
                startArgument,
                countArgument,
                lifecycleModeArgument,
                domainArgument
            };
            getCommand.AddCommand(matchesCommand);

            matchesCommand.SetHandler(MatchCommandHandler, isXuidFileArgument, xuidArgument, startArgument, countArgument, lifecycleModeArgument, domainArgument);

            return await rootCommand.InvokeAsync(args);
        }

        /// <summary>
        /// Handles the 'get matches' command that obtains the comprehensive list of matches the customer and stores them in the specified SQLite database.
        /// </summary>
        /// <param name="isXuidFile">Determines whether the player ID parameter is a singular XUID or a XUID list.</param>
        /// <param name="xuid">The player XUID or the path to the XUID file. The latter requires that <paramref name="isXuidFile"/> is set to 'true'.</param>
        /// <param name="start">Starting position from which matches should be obtained.</param>
        /// <param name="count">Count of matches to obtain.</param>
        /// <param name="lifecycleMode">Type of matches to obtain. Can be either 'matchmade' or 'custom'. If not specified, all matches are obtained.</param>
        /// <param name="domain">The path to the SQLite database.</param>
        private static void MatchCommandHandler(bool isXuidFile, string xuid, int start, int count, string lifecycleMode, string domain)
        {            
            if (isXuidFile)
            {
                // We have a file full of XUIDs, so we need to iterate through all of them.
                if (File.Exists(xuid))
                {
                    string[] playerXuids = File.ReadAllLines(xuid);
                    foreach(var playerXuid in playerXuids)
                    {
                        string[]? rawMatchEntries = GetPlayerMatchStats(playerXuid, start, count, lifecycleMode);
                    }
                }
                else
                {
                    Console.WriteLine($"The file {xuid} could not be found. Make sure that the path is correct.");
                }
            }
            else
            {

            }
        }

        private static async Task<string[]?> GetPlayerMatchStats(string playerXuid, int start, int count, string lifecycleMode)
        {
            var matchCountSnapshot = await haloInfiniteClient.StatsGetMatchCount(playerXuid);

            if (matchCountSnapshot != null && matchCountSnapshot.Result != null)
            {
                Console.WriteLine($"Got match stats for {playerXuid}.");

                int counter = 0;

                switch (lifecycleMode)
                {
                    case "matchmade":
                        {
                            counter = matchCountSnapshot.Result.MatchmadeMatchesPlayedCount;
                            break;
                        }
                    case "custom":
                        {
                            counter = matchCountSnapshot.Result.CustomMatchesPlayedCount;
                            break;
                        }
                    case "local":
                        {
                            counter = matchCountSnapshot.Result.LocalMatchesPlayedCount;
                            break;
                        }
                    default:
                        {
                            counter = matchCountSnapshot.Result.MatchesPlayedCount;
                            break;
                        }
                }
            }
            else
            {
                return null;
            }
        }

        private static HaloInfiniteClient? InstantiateClient()
        {
            ClientConfiguration? clientConfig = new();

            if (System.IO.File.Exists("client.json"))
            {
                clientConfig = ConfigurationReader.ReadConfiguration<ClientConfiguration>("client.json");
            }
            else
            {
                Console.WriteLine("Could not get client information. Make sure you have a client configuration file (client.json) defined in the application folder.");
                return null;
            }

            if (clientConfig == null || clientConfig.ClientId == null || clientConfig.ClientSecret == null || clientConfig.RedirectUrl == null)
            {
                Console.WriteLine("Make sure that the client configuration contains the client ID, client secret, and the redirect URL.");
                return null;
            }

            XboxAuthenticationClient manager = new();
            var url = manager.GenerateAuthUrl(clientConfig.ClientId, clientConfig.RedirectUrl);

            HaloAuthenticationClient haloAuthClient = new();

            OAuthToken? currentOAuthToken = null;

            var ticket = new XboxTicket();
            var haloTicket = new XboxTicket();
            var extendedTicket = new XboxTicket();
            var haloToken = new SpartanToken();

            if (File.Exists("tokens.json"))
            {
                Console.WriteLine("Trying to use local tokens...");

                // If a local token file exists, load the file.
                currentOAuthToken = ConfigurationReader.ReadConfiguration<OAuthToken>("tokens.json");
            }
            else
            {
                currentOAuthToken = RequestNewToken(url, manager, clientConfig);
            }

            Task.Run(async () =>
            {
                ticket = await manager.RequestUserToken(currentOAuthToken.AccessToken);
                if (ticket == null)
                {
                    // There was a failure to obtain the user token, so likely we need to refresh.
                    currentOAuthToken = await manager.RefreshOAuthToken(
                        clientConfig.ClientId,
                        currentOAuthToken.RefreshToken,
                        clientConfig.RedirectUrl,
                        clientConfig.ClientSecret);

                    if (currentOAuthToken == null)
                    {
                        Console.WriteLine("Could not get the token even with the refresh token.");
                        currentOAuthToken = RequestNewToken(url, manager, clientConfig);
                    }
                    ticket = await manager.RequestUserToken(currentOAuthToken.AccessToken);
                }
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                haloTicket = await manager.RequestXstsToken(ticket.Token);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                extendedTicket = await manager.RequestXstsToken(ticket.Token, false);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                haloToken = await haloAuthClient.GetSpartanToken(haloTicket.Token, 4);
                Console.WriteLine("Your Halo token:");
                Console.WriteLine(haloToken.Token);
            }).GetAwaiter().GetResult();

            if (haloToken != null && extendedTicket != null)
            {
                //Let's create an instance to experiment with the Halo Infinite client.
                return new HaloInfiniteClient(haloToken.Token, extendedTicket.DisplayClaims.Xui[0].XUID);
            }
            else
            {
                return null;
            }
        }

        private static OAuthToken RequestNewToken(string url, XboxAuthenticationClient manager, ClientConfiguration clientConfig)
        {
            Console.WriteLine("Provide account authorization and grab the code from the URL:");
            Console.WriteLine(url);

            Console.WriteLine("Your code:");
            var code = Console.ReadLine();
            var currentOAuthToken = new OAuthToken();

            // If no local token file exists, request a new set of tokens.
            Task.Run(async () =>
            {
                currentOAuthToken = await manager.RequestOAuthToken(clientConfig.ClientId, code, clientConfig.RedirectUrl, clientConfig.ClientSecret);
                if (currentOAuthToken != null)
                {
                    var storeTokenResult = StoreTokens(currentOAuthToken, "tokens.json");
                    if (storeTokenResult)
                    {
                        Console.WriteLine("Stored the tokens locally.");
                    }
                    else
                    {
                        Console.WriteLine("There was an issue storing tokens locally. A new token will be requested on the next run.");
                    }
                }
                else
                {
                    Console.WriteLine("No token was obtained. There is no valid token to be used right now.");
                }
            }).GetAwaiter().GetResult();

            return currentOAuthToken;
        }

        private static bool StoreTokens(OAuthToken token, string path)
        {
            string json = JsonSerializer.Serialize(token);
            try
            {
                System.IO.File.WriteAllText(path, json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}