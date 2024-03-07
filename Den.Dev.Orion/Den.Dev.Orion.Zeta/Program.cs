using Den.Dev.Orion.Authentication;
using Den.Dev.Orion.Core;
using Den.Dev.Orion.Models;
using Den.Dev.Orion.Util;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Den.Dev.Orion.Models.HaloInfinite;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Den.Dev.Orion.Models.Security;

namespace Den.Dev.Orion.Zeta
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
                // Previous build values:
                // - 222249.22.06.08.1730-0
                var clearance = (await client.SettingsGetClearance("RETAIL", "UNUSED", "245613.23.06.01.1708-0", "1.6")).Result;
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
                //var emblem = await client.GameCmsGetItem("Inventory/Spartan/Emblems/104-001-cp-chibinerfn-66459761.json", client.ClearanceToken);

                //var settings = await client.GetApiSettingsContainer();
                Dictionary<string, string> inspectionPlaylists = new Dictionary<string, string>()
                {
                    {"edfef3ac-9cbe-4fa2-b949-8f29deafd483", "b8681883-23a7-4d27-9cf8-3730ec274c12"}
                };

                List<PlaylistRotationEntry> existingMapModePairs = new List<PlaylistRotationEntry>();
                List<Tuple<int, int>> playlistMmpMapping = new List<Tuple<int, int>>();

                List<Tuple<string, string>> existingMaps = new List<Tuple<string, string>>();
                List<Tuple<int, int>> mmpMapMapping = new List<Tuple<int, int>>();

                List<Tuple<string, string>> existingGameVariants = new List<Tuple<string, string>>();
                List<Tuple<int, int>> mmpGVMapping = new List<Tuple<int, int>>();

                foreach (var playlist in inspectionPlaylists)
                {
                    var playlistData = await client.HIUGCDiscoveryGetPlaylist(playlist.Key, playlist.Value, client.ClearanceToken);
                    Console.WriteLine(playlistData.Result.PublicName + $"    {playlist.Key}/{playlist.Value}");

                    //string entries = string.Join("','", playlistData.Result.RotationEntries.Select(x => x.PublicName));
                    //Console.WriteLine(entries);

                    //entries = string.Join(",", playlistData.Result.RotationEntries.Select(x => x.Metadata.Weight));
                    //Console.WriteLine(entries);

                    Console.WriteLine("========");

                    foreach (var mmp in playlistData.Result.RotationEntries)
                    {
                        Console.WriteLine($"      {mmp.PublicName} ({mmp.AssetId} / {mmp.VersionId}) - {mmp.Metadata.Weight}");
                        //var existingPRE = (from c in existingMapModePairs where c.AssetId.ToString() == mmp.AssetId.ToString() && c.VersionId.ToString() == mmp.VersionId.ToString() select c).FirstOrDefault();

                        //if (existingPRE == null)
                        //{
                        //    existingMapModePairs.Add(mmp);
                        //}

                        //var playlistIndex = inspectionPlaylists.ToList().IndexOf(playlist);
                        //var mapModePairIndex = existingMapModePairs.IndexOf(mmp);

                        //playlistMmpMapping.Add(new Tuple<int, int>(playlistIndex, mapModePairIndex));

                        //var mmpMeta = await client.HIUGCDiscoveryGetMapModePair(mmp.AssetId.ToString(), mmp.VersionId.ToString(), client.ClearanceToken);

                        //Console.WriteLine($"            {mmpMeta.Result.MapLink.PublicName} ({mmpMeta.Result.MapLink.AssetId} / {mmpMeta.Result.MapLink.VersionId})");
                        //Console.WriteLine($"            {mmpMeta.Result.UgcGameVariantLink.PublicName} ({mmpMeta.Result.UgcGameVariantLink.AssetId} / {mmpMeta.Result.UgcGameVariantLink.VersionId})");

                        //var existingMap = (from c in existingMaps where c.Item1 == mmpMeta.Result.MapLink.AssetId.ToString() && c.Item2.ToString() == mmpMeta.Result.MapLink.VersionId.ToString() select c).FirstOrDefault();

                        //if (existingMap == null)
                        //{
                        //    existingMaps.Add(new Tuple<string, string>(mmpMeta.Result.MapLink.AssetId.ToString(), mmpMeta.Result.MapLink.VersionId.ToString()));
                        //}

                        //existingMap = (from c in existingMaps where c.Item1 == mmpMeta.Result.MapLink.AssetId.ToString() && c.Item2.ToString() == mmpMeta.Result.MapLink.VersionId.ToString() select c).FirstOrDefault();

                        //var mapIndex = existingMaps.IndexOf(existingMap);
                        //mmpMapMapping.Add(new Tuple<int, int>(mapModePairIndex, mapIndex));

                        //var existingUgcGameVariant = (from c in existingGameVariants where c.Item1 == mmpMeta.Result.UgcGameVariantLink.AssetId.ToString() && c.Item2.ToString() == mmpMeta.Result.UgcGameVariantLink.VersionId.ToString() select c).FirstOrDefault();

                        //if (existingUgcGameVariant == null)
                        //{
                        //    existingGameVariants.Add(new Tuple<string, string>(mmpMeta.Result.UgcGameVariantLink.AssetId.ToString(), mmpMeta.Result.UgcGameVariantLink.VersionId.ToString()));
                        //}

                        //existingUgcGameVariant = (from c in existingGameVariants where c.Item1 == mmpMeta.Result.UgcGameVariantLink.AssetId.ToString() && c.Item2.ToString() == mmpMeta.Result.UgcGameVariantLink.VersionId.ToString() select c).FirstOrDefault();

                        //var gameVariantIndex = existingGameVariants.IndexOf(existingUgcGameVariant);
                        //mmpGVMapping.Add(new Tuple<int, int>(mapModePairIndex, gameVariantIndex));
                    }
                }

                //foreach (var playlist in inspectionPlaylists)
                //{
                //    var playlistData = await client.HIUGCDiscoveryGetPlaylist(playlist.Key, playlist.Value, client.ClearanceToken);
                //    var playlistIndex = inspectionPlaylists.ToList().IndexOf(playlist);

                //    Console.WriteLine($"playlist{playlistIndex}[{playlistData.Result.PublicName}];");
                //}

                //foreach (var rotationEntry in playlistMmpMapping)
                //{
                //    Console.WriteLine($"mapModePair{rotationEntry.Item2}[{existingMapModePairs[rotationEntry.Item2].PublicName}];");   
                //}

                //foreach (var rotationEntry in playlistMmpMapping)
                //{
                //    Console.WriteLine($"playlist{rotationEntry.Item1} --> mapModePair{rotationEntry.Item2};");
                //}

                //foreach (var map in existingMaps)
                //{
                //    var mapData = await client.HIUGCDiscoveryGetMap(map.Item1, map.Item2);

                //    Console.WriteLine($"map{existingMaps.IndexOf(map)}[{mapData.Result.PublicName}];");
                //    Console.WriteLine($"click map{existingMaps.IndexOf(map)} \"https://www.halowaypoint.com/halo-infinite/ugc/maps/{map.Item1}\"");
                //}

                //foreach (var rotationEntry in mmpMapMapping)
                //{
                //    Console.WriteLine($"mapModePair{rotationEntry.Item1} --> map{rotationEntry.Item2};");
                //}

                //foreach (var rotationEntry in existingGameVariants)
                //{
                //    var gv = await client.HIUGCDiscoveryGetUgcGameVariant(rotationEntry.Item1, rotationEntry.Item2);

                //    Console.WriteLine($"gameVariant{existingGameVariants.IndexOf(rotationEntry)}[{gv.Result.PublicName}];");
                //    Console.WriteLine($"click gameVariant{existingGameVariants.IndexOf(rotationEntry)} \"https://www.halowaypoint.com/halo-infinite/ugc/modes/{rotationEntry.Item1}\"");
                //}

                //foreach (var rotationEntry in mmpGVMapping)
                //{
                //    Console.WriteLine($"mapModePair{rotationEntry.Item1} --> gameVariant{rotationEntry.Item2};");
                //}

            }).GetAwaiter().GetResult();

            //Task.Run(async () =>
            //{
            //    var serviceRecord = (await client.StatsGetPlayerServiceRecord("zebond", LifecycleMode.Matchmade))!.Result;

            //    if (serviceRecord != null && serviceRecord.Subqueries != null && serviceRecord.Subqueries.PlaylistAssetIds != null)
            //    {
            //        foreach (var playlist in serviceRecord.Subqueries.PlaylistAssetIds)
            //        {
            //            var playlistConfiguration = (await client.GameCmsGetMultiplayerPlaylistConfiguration($"{playlist}.json")).Result;
            //            if (playlistConfiguration != null)
            //            {
            //                Console.WriteLine($"Playlist configration for {playlist} obtained.");
            //                var playlistAssetManifest = (await client.HIUGCDiscoveryGetPlaylist(playlist.ToString(), playlistConfiguration.UgcPlaylistVersion.ToString(), client.ClearanceToken)).Result;
            //                if (playlistAssetManifest != null && playlistAssetManifest.RotationEntries != null)
            //                {
            //                    foreach (var rotationEntry in playlistAssetManifest.RotationEntries)
            //                    {
            //                        Console.WriteLine($"{rotationEntry.PublicName} has weight of {rotationEntry.Metadata!.Weight}");
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    Console.WriteLine("Got service record.");
            //}).GetAwaiter().GetResult();

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
