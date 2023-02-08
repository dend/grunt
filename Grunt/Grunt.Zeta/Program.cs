using OpenSpartan.Grunt.Authentication;
using OpenSpartan.Grunt.Core;
using OpenSpartan.Grunt.Models;
using OpenSpartan.Grunt.Util;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using OpenSpartan.Grunt.Models.HaloInfinite;

namespace OpenSpartan.Grunt.Zeta
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientConfiguration? clientConfig = new ClientConfiguration();

            if (System.IO.File.Exists("client.json"))
            {
                clientConfig = ConfigurationReader.ReadConfiguration<ClientConfiguration>("client.json");
            }
            else
            {
                Console.WriteLine("Make sure you have a client configuration file (client.json) defined in the application folder.");
                Environment.Exit(0);
            }

            XboxAuthenticationClient manager = new();
            var url = manager.GenerateAuthUrl(clientConfig.ClientId, clientConfig.RedirectUrl);

            HaloAuthenticationClient haloAuthClient = new();

            OAuthToken currentOAuthToken = null;

            var ticket = new XboxTicket();
            var haloTicket = new XboxTicket();
            var extendedTicket = new XboxTicket();
            var haloToken = new SpartanToken();

            if (System.IO.File.Exists("tokens.json"))
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

            //Let's create an instance to experiment with the Halo Infinite client.
            HaloInfiniteClient client = new(haloToken.Token, extendedTicket.DisplayClaims.Xui[0].XUID);

            //// Let's also create an instance to experiment with the Halo Waypoint APIs.
            //WaypointClient waypointClient = new(haloToken.Token, extendedTicket.DisplayClaims.Xui[0].XUID);

            //Console.WriteLine($"Your XUID is {extendedTicket.DisplayClaims.Xui[0].XUID}");

            // Test getting the clearance for local execution.
            string localClearance = string.Empty;
            Task.Run(async () =>
            {
                var clearance = (await client.SettingsGetClearance("RETAIL", "UNUSED", "222249.22.06.08.1730-0")).Result;
                if (clearance != null)
                {
                    localClearance = clearance.FlightConfigurationId;
                    client.ClearanceToken = localClearance;
                    Console.WriteLine($"Your clearance is {localClearance} and it's set in the client.");
                }
                else
                {
                    Console.WriteLine("Could not obtain the clearance.");
                }
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                var serviceRecord = (await client.StatsGetPlayerServiceRecord("zebond", LifecycleMode.Matchmade))!.Result;

                if (serviceRecord != null && serviceRecord.Subqueries != null && serviceRecord.Subqueries.PlaylistAssetIds != null)
                {
                    foreach (var playlist in serviceRecord.Subqueries.PlaylistAssetIds)
                    {
                        var playlistConfiguration = (await client.GameCmsGetMultiplayerPlaylistConfiguration($"{playlist}.json")).Result;
                        if (playlistConfiguration != null)
                        {
                            Console.WriteLine($"Playlist configration for {playlist} obtained.");
                            var playlistAssetManifest = (await client.HIUGCDiscoveryGetPlaylist(playlist.ToString(), playlistConfiguration.UgcPlaylistVersion.ToString(), client.ClearanceToken)).Result;
                            if (playlistAssetManifest != null && playlistAssetManifest.RotationEntries != null)
                            {
                                foreach (var rotationEntry in playlistAssetManifest.RotationEntries)
                                {
                                    Console.WriteLine($"{rotationEntry.PublicName} has weight of {rotationEntry.Metadata!.Weight}");
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Got service record.");
            }).GetAwaiter().GetResult();

            //Task.Run(async () =>
            //{
            //    ServiceAwardSnapshot snapshot = new ServiceAwardSnapshot();
            //    snapshot.FeaturedAwards = new List<string>();
            //    snapshot.FeaturedAwards.Add("hi-event-ritualEagleStrike");
            //    snapshot.FeaturedAwards.Add("h5-csr-tier1");

            //    var stats = (await waypointClient.PutFeaturedServiceAwards(snapshot));
            //    Console.WriteLine("Got articles.");
            //}).GetAwaiter().GetResult();

            //Halo5Client h5client = new(haloToken.Token, extendedTicket.DisplayClaims.Xui[0].XUID);
            //Task.Run(async () =>
            //{
            //    var seasonPass = (await h5client.ContentHacsGetActiveSeasonPass()).Result;
            //    Console.WriteLine("Got season pass manifest.");
            //}).GetAwaiter().GetResult();

            Console.ReadLine();
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
