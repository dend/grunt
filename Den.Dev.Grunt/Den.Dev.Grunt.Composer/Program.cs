#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8604 // Possible null reference argument.

using Den.Dev.Conch.Authentication;
using Den.Dev.Conch.Models.Security;
using Den.Dev.Grunt.Authentication;
using Den.Dev.Grunt.Composer.Models;
using Den.Dev.Grunt.Core;
using Den.Dev.Grunt.Endpoints;
using Den.Dev.Grunt.Models;
using Den.Dev.Grunt.Models.HaloInfinite;
using Den.Dev.Grunt.Models.Security;
using Den.Dev.Grunt.Util;
using SQLite;
using System.CommandLine;
using System.Globalization;
using System.Text.Json;

namespace Den.Dev.Grunt.Composer
{
    internal class Program
    {
        static HaloInfiniteClient? haloInfiniteClient = null;
        static XboxAuthenticationClient manager = new();
        static HaloAuthenticationClient haloAuthClient = new();

        static async Task<int> Main(string[] args)
        {
            var rootCommand = new RootCommand();

            var clientIdArgument = new Option<string>(
                name: "--client-id",
                description: "Client ID used for token refreshes. Otherwise, loaded from config.json.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = false
            };

            var clientSecretArgument = new Option<string>(
                name: "--client-secret",
                description: "Client secret used for token refreshes. Otherwise, loaded from config.json.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = false
            };

            var redirectUrlArgument = new Option<string>(
                name: "--redirect-url",
                description: "Redirect URL used for token refreshes. Otherwise, loaded from config.json.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = false
            };

            var refreshTokenArgument = new Option<string>(
                name: "--refresh-token",
                description: "Existing refresh token used for token refreshes.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = false
            };

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

            var matchTypeArgument = new Option<Den.Dev.Grunt.Models.HaloInfinite.MatchType>(
                name: "--match-type",
                description: "Kinds of matches to obtain. Default is all matches, but can also be set to 'matchmade' or 'custom'.",
                getDefaultValue: () => Den.Dev.Grunt.Models.HaloInfinite.MatchType.All)
            {
                IsRequired = true
            };

            var playlistIdArgument = new Option<string>(
                name: "--playlist-id",
                description: "The GUID that identifies the playlist for which the record needs to be obtained.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = true
            };

            var projectIdArgument = new Option<string>(
                name: "--project-id",
                description: "The GUID representing a project for which stats need to be obtained.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = true
            };

            var buildIdArgument = new Option<string>(
                name: "--build-id",
                description: "The GUID representing the build for which stats need to be obtained.",
                getDefaultValue: () => string.Empty)
            {
                IsRequired = true
            };

            var refreshCommand = new Command("refresh", "Refresh an existing access token.")
            {
                clientIdArgument,
                clientSecretArgument,
                redirectUrlArgument,
            };
            rootCommand.AddCommand(refreshCommand);

            var getCommand = new Command("get", "Gets data from the Halo API.");
            rootCommand.AddCommand(getCommand);

            var getMatchesCommand = new Command("matches", "Gets match data from the Halo API.")
            {
                isXuidFileArgument,
                xuidArgument,
                startArgument,
                countArgument,
                matchTypeArgument,
            };
            getCommand.AddCommand(getMatchesCommand);

            var getServiceRecordCommand = new Command("sr", "Gets service record information.")
            {
                isXuidFileArgument,
                xuidArgument
            };
            getCommand.AddCommand(getServiceRecordCommand);

            var getRankSnapshotCommand = new Command("rank", "Gets rank snapshot information.")
            {
                playlistIdArgument,
                isXuidFileArgument,
                xuidArgument
            };
            getCommand.AddCommand(getRankSnapshotCommand);

            var getProjectStats = new Command("projectstats", "Gets stats from a project.")
            {
                projectIdArgument
            };
            getCommand.AddCommand(getProjectStats);

            var getBuildStats = new Command("buildstats", "Gets stats from a build.")
            {
                buildIdArgument
            };
            getCommand.AddCommand(getBuildStats);

            var getMedalMetadata = new Command("medalmetadata", "Gets service record information.");
            getCommand.AddCommand(getMedalMetadata);

            getMatchesCommand.SetHandler(GetMatchesCommandHandler, isXuidFileArgument, xuidArgument, startArgument, countArgument, matchTypeArgument, domainArgument);
            getServiceRecordCommand.SetHandler(GetServiceRecordCommandHandler, isXuidFileArgument, xuidArgument, domainArgument);
            getMedalMetadata.SetHandler(GetMedalsCommandHandler, domainArgument);
            refreshCommand.SetHandler(RefreshCommandHandler, clientIdArgument, clientSecretArgument, redirectUrlArgument);
            getRankSnapshotCommand.SetHandler(RankSnapshotCommandHandler, playlistIdArgument, isXuidFileArgument, xuidArgument, domainArgument);
            getProjectStats.SetHandler(ProjectStatsCommandHandler, projectIdArgument, domainArgument);
            getBuildStats.SetHandler(BuildStatsCommandHandler, buildIdArgument, domainArgument);

            return await rootCommand.InvokeAsync(args);
        }

        private static void ProcessUncertainAssetData(List<AssetLink>? assets, AssetClass assetClass, SQLiteConnection domain)
        {
            if (assets != null)
            {
                foreach (var asset in assets)
                {
                    WriteTimedLogEntry($"Getting {asset.AssetId} with version {asset.VersionId} of class {assetClass}...");

                    if (assetClass == AssetClass.Map)
                    {
                        Task.Run(async () =>
                        {
                            var container = await haloInfiniteClient.UgcDiscovery.GetMap(asset.AssetId.ToString(), asset.VersionId.ToString());
                            var buildInsertionString = $"INSERT OR REPLACE INTO MapMetadata (ResponseBody, SnapshotTimestamp) VALUES(?, ?)";
                            domain.Execute(buildInsertionString, new string[] { container.Response.Message, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) });

                        }).GetAwaiter().GetResult();
                    }
                    else if (assetClass == AssetClass.EngineGameVariant)
                    {
                        Task.Run(async () =>
                        {
                            var container = await haloInfiniteClient.UgcDiscovery.GetEngineGameVariant(asset.AssetId.ToString(), asset.VersionId.ToString());
                            var buildInsertionString = $"INSERT OR REPLACE INTO EngineGameVariantMetadata (ResponseBody, SnapshotTimestamp) VALUES(?, ?)";
                            domain.Execute(buildInsertionString, new string[] { container.Response.Message, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) });
                        }).GetAwaiter().GetResult();
                    }
                    else if (assetClass == AssetClass.GameVariant)
                    {
                        Task.Run(async () =>
                        {
                            var container = await haloInfiniteClient.UgcDiscovery.GetUgcGameVariant(asset.AssetId.ToString(), asset.VersionId.ToString());
                            var buildInsertionString = $"INSERT OR REPLACE INTO UgcGameVariantMetadata (ResponseBody, SnapshotTimestamp) VALUES(?, ?)";
                            domain.Execute(buildInsertionString, new string[] { container.Response.Message, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) });
                        }).GetAwaiter().GetResult();
                    }

                    WriteTimedLogEntry($"Asset {asset.AssetId} with version {asset.VersionId} of class {assetClass} stored.");
                }
            }
        }

        private static async Task<bool> BuildStatsCommandHandler(string buildId, string domain)
        {
            haloInfiniteClient = InstantiateClient();

            var domainDatabase = new SQLiteConnection(domain);

            var buildData = await haloInfiniteClient!.UgcDiscovery.GetManifestByBuildGuid(buildId);
            if (buildData.Response!.Code == 401)
            {
                // The token is no longer working - need to acquire a new one.
                WriteTimedLogEntry("Token expired. Refreshing...");
                haloInfiniteClient = InstantiateClient();
                buildData = await haloInfiniteClient!.UgcDiscovery.GetManifestByBuildGuid(buildId);
            }

            if (buildData != null && buildData.Result != null)
            {
                var buildInsertionString = $"INSERT OR REPLACE INTO BuildMetadata (ResponseBody, BuildId, SnapshotTimestamp) VALUES(?, ?, ?)";
                domainDatabase.Execute(buildInsertionString, new string[] { buildData.Response.Message, buildId, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) });
                WriteTimedLogEntry($"Stored build snapshot in the database.");

                ProcessUncertainAssetData(buildData.Result.MapLinks, AssetClass.Map, domainDatabase);
                ProcessUncertainAssetData(buildData.Result.EngineGameVariantLinks, AssetClass.EngineGameVariant, domainDatabase);
                ProcessUncertainAssetData(buildData.Result.UgcGameVariantLinks, AssetClass.GameVariant, domainDatabase);

                WriteTimedLogEntry("Finished storing build-related data.");
            }
            else
            {
                WriteTimedLogEntry($"Data storage failed for build snapshot.");
                return false;
            }

            return true;
        }

        private static async Task<bool> ProjectStatsCommandHandler(string projectId, string domain)
        {
            haloInfiniteClient = InstantiateClient();

            var domainDatabase = new SQLiteConnection(domain);

            var projectData = await haloInfiniteClient!.UgcDiscovery.GetProjectWithoutVersion(projectId);
            if (projectData.Response!.Code == 401)
            {
                // The token is no longer working - need to acquire a new one.
                WriteTimedLogEntry("Token expired. Refreshing...");
                haloInfiniteClient = InstantiateClient();
                projectData = await haloInfiniteClient!.UgcDiscovery.GetProjectWithoutVersion(projectId);
            }

            if (projectData != null && projectData.Result != null)
            {
                var buildInsertionString = $"INSERT OR REPLACE INTO ProjectMetadata (ResponseBody, ProjectId, SnapshotTimestamp) VALUES(?, ?, ?)";
                domainDatabase.Execute(buildInsertionString, new string[] { projectData.Response.Message, projectId, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) });
                WriteTimedLogEntry($"Stored project snapshot in the database.");
            }
            else
            {
                WriteTimedLogEntry($"Data storage failed for project snapshot.");
                return false;
            }

            return true;
        }

        private static async Task<bool> RankSnapshotCommandHandler(string playlistId, bool isXuidFile, string xuid, string domain)
        {
            haloInfiniteClient = InstantiateClient();

            string[] playerXuids;

            if (isXuidFile)
            {
                // We have a file full of XUIDs, so we need to iterate through all of them.
                if (System.IO.File.Exists(xuid))
                {
                    playerXuids = System.IO.File.ReadAllLines(xuid);
                }
                else
                {
                    WriteTimedLogEntry($"The file {xuid} could not be found. Make sure that the path is correct.");
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(xuid))
                {
                    playerXuids = new string[] { xuid };
                }
                else
                {
                    WriteTimedLogEntry($"XUID was not specified.");
                    return false;
                }
            }

            var domainDatabase = new SQLiteConnection(domain);

            var rankData = await haloInfiniteClient!.Skill.GetPlaylistCsr(playlistId, playerXuids.ToList());
            if (rankData.Response!.Code == 401)
            {
                // The token is no longer working - need to acquire a new one.
                WriteTimedLogEntry("Token expired. Refreshing...");
                haloInfiniteClient = InstantiateClient();
                rankData = await haloInfiniteClient!.Skill.GetPlaylistCsr(playlistId, playerXuids.ToList());
            }

            if (rankData != null && rankData.Result != null)
            {
                var matchInsertionString = $"INSERT OR REPLACE INTO PlayerRankSnapshots (ResponseBody, PlaylistId, SnapshotTimestamp) VALUES(?, ?, ?)";
                domainDatabase.Execute(matchInsertionString, new string[] { rankData.Response.Message, playlistId, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) });
                WriteTimedLogEntry($"Stored rank snapshot in the database.");
            }
            else
            {
                WriteTimedLogEntry($"Data storage failed for rank snapshots.");
                return false;
            }

            return true;
        }

        private static async Task<bool> RefreshCommandHandler(string clientId, string clientSecret, string redirectUrl)
        {
            OAuthToken currentOAuthToken;

            if (System.IO.File.Exists("tokens.json"))
            {
                WriteTimedLogEntry("Trying to use local tokens for refresh...");

                // If a local token file exists, load the file.
                currentOAuthToken = ConfigurationReader.ReadConfiguration<OAuthToken>("tokens.json");

                currentOAuthToken = await manager.RefreshOAuthToken(clientId, currentOAuthToken.RefreshToken, redirectUrl, clientSecret);

                if (currentOAuthToken != null)
                {
                    _ = StoreTokens(currentOAuthToken, "tokens.json");

                    WriteTimedLogEntry("Tokens refreshed inside the file.");
                    return true;
                }
            }
            else
            {
                WriteTimedLogEntry("Could not refresh the token. The tokens.json file was not there.");
            }

            return false;
        }

        private static async Task<bool> GetMedalsCommandHandler(string domain)
        {
            haloInfiniteClient = InstantiateClient();

            var domainDatabase = new SQLiteConnection(domain);

            var medalMetadata = await haloInfiniteClient.GameCms.GetMedalMetadata();
            if (medalMetadata.Response!.Code == 401)
            {
                // The token is no longer working - need to acquire a new one.
                WriteTimedLogEntry("Token expired. Refreshing...");
                haloInfiniteClient = InstantiateClient();
                medalMetadata = await haloInfiniteClient.GameCms.GetMedalMetadata();
            }

            if (medalMetadata != null && medalMetadata.Result != null)
            {
                var matchInsertionString = $"INSERT OR REPLACE INTO MedalMetadata (ResponseBody, SnapshotTimestamp) VALUES(?, ?)";
                domainDatabase.Execute(matchInsertionString, new string[] { medalMetadata.Response.Message, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) });
                WriteTimedLogEntry($"Stored medal metadata in the database.");
            }
            else
            {
                WriteTimedLogEntry($"Data storage failed for medal metadata");
                return false;
            }

            return true;
        }
        
        private static async Task<bool> GetServiceRecordCommandHandler(bool isXuidFile, string xuid, string domain)
        {
            haloInfiniteClient = InstantiateClient();

            string[] playerXuids;

            if (isXuidFile)
            {
                // We have a file full of XUIDs, so we need to iterate through all of them.
                if (System.IO.File.Exists(xuid))
                {
                    playerXuids = System.IO.File.ReadAllLines(xuid);
                }
                else
                {
                    WriteTimedLogEntry($"The file {xuid} could not be found. Make sure that the path is correct.");
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(xuid))
                {
                    playerXuids = new string[] { xuid };
                }
                else
                {
                    WriteTimedLogEntry($"XUID was not specified.");
                    return false;
                }
            }

            var domainDatabase = new SQLiteConnection(domain);

            foreach (var playerXuid in playerXuids)
            {
                var srData = await haloInfiniteClient.Stats.GetPlayerServiceRecord(playerXuid, LifecycleMode.Matchmade);
                if (srData.Response!.Code == 401)
                {
                    // The token is no longer working - need to acquire a new one.
                    WriteTimedLogEntry("Token expired. Refreshing...");
                    haloInfiniteClient = InstantiateClient();
                    srData = await haloInfiniteClient.Stats.GetPlayerServiceRecord(playerXuid, LifecycleMode.Matchmade);
                }

                if (srData != null && srData.Result != null)
                {
                    var matchInsertionString = $"INSERT OR REPLACE INTO ServiceRecordSnapshots (ResponseBody, SnapshotTimestamp) VALUES(?, ?)";
                    domainDatabase.Execute(matchInsertionString, new string[] { srData.Response.Message, DateTime.Now.ToString("o", CultureInfo.InvariantCulture) });
                    WriteTimedLogEntry($"Stored service record for {playerXuid} in the database.");

                }
                else
                {
                    WriteTimedLogEntry($"Data storage failed for {playerXuid}");
                    continue;
                }
            }

            return true;
        }

        /// <summary>
        /// Handles the 'get matches' command that obtains the comprehensive list of matches the customer and stores them in the specified SQLite database.
        /// </summary>
        /// <param name="isXuidFile">Determines whether the player ID parameter is a singular XUID or a XUID list.</param>
        /// <param name="xuid">The player XUID or the path to the XUID file. The latter requires that <paramref name="isXuidFile"/> is set to 'true'.</param>
        /// <param name="start">Starting position from which matches should be obtained.</param>
        /// <param name="count">Count of matches to obtain.</param>
        /// <param name="matchType">Type of matches to obtain. Can be matchmade, custom, local, or all. If not specified, all matches are obtained.</param>
        /// <param name="domain">The path to the SQLite database.</param>
        private static async Task<bool> GetMatchesCommandHandler(bool isXuidFile, string xuid, int start, int count, Den.Dev.Grunt.Models.HaloInfinite.MatchType matchType, string domain)
        {
            haloInfiniteClient = InstantiateClient();

            string[] playerXuids;

            if (isXuidFile)
            {
                // We have a file full of XUIDs, so we need to iterate through all of them.
                if (System.IO.File.Exists(xuid))
                {
                    playerXuids = System.IO.File.ReadAllLines(xuid);
                }
                else
                {
                    WriteTimedLogEntry($"The file {xuid} could not be found. Make sure that the path is correct.");
                    return false;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(xuid))
                {
                    playerXuids = new string[] { xuid };
                }
                else
                {
                    WriteTimedLogEntry($"XUID was not specified.");
                    return false;
                }
            }

            // We will need a globally de-duped list of matches since stats are the same
            // and if two players participated in them we don't need to re-acquire the data.
            List<Guid> combinedMatchIds = new();

            await Task.WhenAll(playerXuids.Select(async playerXuid =>
            {
                var matchIds = await GetPlayerMatchIds(playerXuid, start, count, matchType);
                if (matchIds != null)
                {
                    WriteTimedLogEntry($"Got {matchIds.Count} matches for {playerXuid}");
                    lock (combinedMatchIds)
                    {
                        combinedMatchIds.AddRange(matchIds);
                    }
                }
            }));

            var distinctMatchIds = combinedMatchIds.DistinctBy(x => x.ToString()).ToList();
            var domainDatabase = new SQLiteConnection(domain);

            var walQuery = "PRAGMA journal_mode=WAL;";
            var wal = domainDatabase.ExecuteScalar<string>(walQuery);

            int matchesTotal = distinctMatchIds.Count;
            int matchCounter = 0;

            await Task.WhenAll(distinctMatchIds.Select(async matchId =>
            {
                try
                {
                    var completionProgress = (double)Interlocked.Increment(ref matchCounter) / matchesTotal * 100.0;
                    var matchAvailabilityString = $"SELECT EXISTS(SELECT 1 FROM MatchStats WHERE MatchId='{matchId}') AS MATCH_AVAILABLE, EXISTS(SELECT 1 FROM PlayerMatchStats WHERE MatchId='{matchId}') AS PLAYER_STATS_AVAILABLE;";
                    var availability = domainDatabase.Query<EntityAvailabilityModel>(matchAvailabilityString).FirstOrDefault();

                    if (availability != null)
                    {
                        HaloApiResultContainer<MatchStats, RawResponseContainer>? matchStats = null;

                        if (!availability.MatchAvailable)
                        {
                            WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Getting match stats for {matchId}...");
                            matchStats = await SafeAPICall(async () => await haloInfiniteClient!.Stats.GetMatchStats(matchId.ToString()));

                            if (matchStats != null && matchStats.Result != null)
                            {
                                var matchInsertionString = $"INSERT OR REPLACE INTO MatchStats (ResponseBody) VALUES(?)";
                                domainDatabase.Execute(matchInsertionString, new string[] { matchStats.Response.Message });
                                WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Stored match data for {matchId} in the database.");

                            }
                            else
                            {
                                WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Match stats were not available for {matchId}.");
                                return;
                            }
                        }
                        else
                        {
                            WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Match {matchId} already available. Not requesting new data.");
                        }

                        if (!availability.PlayerStatsAvailable)
                        {
                            if (matchStats == null)
                            {
                                matchStats = await SafeAPICall(async () => await haloInfiniteClient!.Stats.GetMatchStats(matchId.ToString()));
                            }

                            if (matchStats != null && matchStats.Result != null && matchStats.Result.Players != null)
                            {
                                // Update asset records.
                                await UpdateMatchAssetRecords(matchStats.Result, domain);

                                // Anything that starts with "bid" is a bot and including that in the request for player stats will result in failure.
                                var targetPlayers = matchStats.Result.Players.Select(p => p.PlayerId).Where(p => !p.StartsWith("bid")).ToList();

                                WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Attempting to get player results for players for match {matchId}.");

                                var playerStatsSnapshot = await SafeAPICall(async () => await haloInfiniteClient.Skill.GetMatchPlayerResult(matchId.ToString(), targetPlayers!));

                                if (playerStatsSnapshot != null && playerStatsSnapshot.Result != null && playerStatsSnapshot.Result.Value != null)
                                {
                                    WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Got stats for {playerStatsSnapshot.Result.Value.Count} players.");

                                    if (playerStatsSnapshot.Response != null)
                                    {
                                        var insertionString = $"INSERT OR REPLACE INTO PlayerMatchStats (MatchId, ResponseBody) VALUES(?, ?)";
                                        domainDatabase.Execute(insertionString, new string[] { matchId.ToString(), playerStatsSnapshot.Response.Message });
                                        WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Stored player stats data for {matchId} in the database.");
                                    }
                                }
                                else
                                {
                                    WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Could not obtain player stats for match {matchId}. Requested {targetPlayers.Count} XUIDs.");
                                }
                            }
                            else
                            {
                                WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Could not obtain player stats for match {matchId} because the match metadata was unavailable.");
                            }
                        }
                        else
                        {
                            WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Match {matchId} player stats already available. Not requesting new data.");
                        }
                    }
                    else
                    {
                        WriteTimedLogEntry($"[{completionProgress:#.00}%] [{matchCounter}/{matchesTotal}] Something went wrong. Could not communicate with the database to get match availability.");
                    }
                }
                catch (Exception e)
                {
                    WriteTimedLogEntry($"Error getting match data for {matchId}. Details: {e.Message}");
                }
            }));

            return true;
        }


        internal static async Task<bool> UpdateMatchAssetRecords(MatchStats result, string domain)
        {
            try
            {
                UGCGameVariant? targetGameVariant = null;

                var domainDatabase = new SQLiteConnection(domain);

                string query = $"SELECT EXISTS(SELECT 1 FROM Maps WHERE AssetId = '{result.MatchInfo.MapVariant.AssetId.ToString()}' AND VersionId = '{result.MatchInfo.MapVariant.VersionId.ToString()}') AS MAP_AVAILABLE, " +
                      $"EXISTS(SELECT 1 FROM GameVariants WHERE AssetId = '{result.MatchInfo.UgcGameVariant.AssetId.ToString()}' AND VersionId = '{result.MatchInfo.UgcGameVariant.VersionId.ToString()}') AS GAMEVARIANT_AVAILABLE";

                if (result.MatchInfo.Playlist != null)
                {
                    query += $", EXISTS(SELECT 1 FROM Playlists WHERE AssetId = '{result.MatchInfo.Playlist.AssetId.ToString()}' AND VersionId = '{result.MatchInfo.Playlist.VersionId.ToString()}') AS PLAYLIST_AVAILABLE";
                }

                if (result.MatchInfo.PlaylistMapModePair != null)
                {
                    query += $", EXISTS(SELECT 1 FROM PlaylistMapModePairs WHERE AssetId = '{result.MatchInfo.PlaylistMapModePair.AssetId.ToString()}' AND VersionId = '{result.MatchInfo.PlaylistMapModePair.VersionId.ToString()}') AS PLAYLISTMAPMODEPAIR_AVAILABLE";
                }

                var availability = domainDatabase.Query<AssetAvailability>(query).FirstOrDefault();

                if (!availability.MapAvailable)
                {
                    var map = await SafeAPICall(async () => await haloInfiniteClient.UgcDiscovery.GetMap(result.MatchInfo.MapVariant.AssetId.ToString(), result.MatchInfo.MapVariant.VersionId.ToString()));
                    if (map != null && map.Result != null && map.Response.Code == 200)
                    {
                        var record = new MapRecord { ResponseBody = map.Response.Message };
                        var insertionResult = domainDatabase.Insert(record);

                        if (insertionResult > 0)
                        {
                            WriteTimedLogEntry($"Stored map: {result.MatchInfo.MapVariant.AssetId}/{result.MatchInfo.MapVariant.VersionId}");
                        }
                    }
                }

                if (!availability.PlaylistAvailable)
                {
                    if (result.MatchInfo.Playlist != null)
                    {
                        var playlist = await SafeAPICall(async () => await haloInfiniteClient!.UgcDiscovery.GetPlaylist(result.MatchInfo.Playlist.AssetId.ToString(), result.MatchInfo.Playlist.VersionId.ToString(), haloInfiniteClient.ClearanceToken));
                        if (playlist != null && playlist.Result != null && playlist.Response.Code == 200)
                        {
                            var record = new PlaylistRecord { ResponseBody = playlist.Response.Message };
                            var insertionResult = domainDatabase.Insert(record);

                            if (insertionResult > 0)
                            {
                                WriteTimedLogEntry($"Stored playlist: {result.MatchInfo.Playlist.AssetId}/{result.MatchInfo.Playlist.VersionId}");
                            }
                        }
                    }
                }

                if (!availability.PlaylistMapModePairAvailable)
                {
                    if (result.MatchInfo.PlaylistMapModePair != null)
                    {
                        var playlistMmp = await SafeAPICall(async () => await haloInfiniteClient.UgcDiscovery.GetMapModePair(result.MatchInfo.PlaylistMapModePair.AssetId.ToString(), result.MatchInfo.PlaylistMapModePair.VersionId.ToString(), haloInfiniteClient.ClearanceToken));
                        if (playlistMmp != null && playlistMmp.Result != null && playlistMmp.Response.Code == 200)
                        {
                            var record = new PlaylistMapModePairRecord { ResponseBody = playlistMmp.Response.Message };
                            var insertionResult = domainDatabase.Insert(record);

                            if (insertionResult > 0)
                            {
                                WriteTimedLogEntry($"Stored playlist + map mode pair: {result.MatchInfo.PlaylistMapModePair.AssetId}/{result.MatchInfo.PlaylistMapModePair.VersionId}");
                            }
                        }
                    }
                }

                if (!availability.GameVariantAvailable)
                {
                    var gameVariant = await SafeAPICall(async () => await haloInfiniteClient.UgcDiscovery.GetUgcGameVariant(result.MatchInfo.UgcGameVariant.AssetId.ToString(), result.MatchInfo.UgcGameVariant.VersionId.ToString()));
                    if (gameVariant != null && gameVariant.Result != null && gameVariant.Response.Code == 200)
                    {
                        targetGameVariant = gameVariant.Result;

                        var record = new GameVariantRecord { ResponseBody = gameVariant.Response.Message };
                        var insertionResult = domainDatabase.Insert(record);

                        if (insertionResult > 0)
                        {
                            WriteTimedLogEntry($"Stored game variant: {result.MatchInfo.UgcGameVariant.AssetId}/{result.MatchInfo.UgcGameVariant.VersionId}");
                        }

                        var engineQuery = $"SELECT EXISTS(SELECT 1 FROM EngineGameVariants WHERE AssetId='{gameVariant.Result.EngineGameVariantLink.AssetId}' AND VersionId='{gameVariant.Result.EngineGameVariantLink.VersionId}') AS ENGINEGAMEVARIANT_AVAILABLE";
                        availability.EngineGameVariantAvailable = domainDatabase.Query<AssetAvailability>(engineQuery).FirstOrDefault().EngineGameVariantAvailable;
                    }
                }

                if (!availability.EngineGameVariantAvailable && targetGameVariant != null)
                {
                    var engineGameVariant = await SafeAPICall(async () => await haloInfiniteClient.UgcDiscovery.GetEngineGameVariant(targetGameVariant.EngineGameVariantLink.AssetId.ToString(), targetGameVariant.EngineGameVariantLink.VersionId.ToString()));

                    if (engineGameVariant != null && engineGameVariant.Result != null && engineGameVariant.Response.Code == 200)
                    {
                        var record = new EngineGameVariantRecord { ResponseBody = engineGameVariant.Response.Message };
                        var insertionResult = domainDatabase.Insert(record);

                        if (insertionResult > 0)
                        {
                            WriteTimedLogEntry($"Stored engine game variant: {engineGameVariant.Result.AssetId}/{engineGameVariant.Result.VersionId}");
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                WriteTimedLogEntry($"Error updating match stats. {ex.Message}");
                return false;
            }
        }

        public static async Task<HaloApiResultContainer<T, RawResponseContainer>> SafeAPICall<T>(Func<Task<HaloApiResultContainer<T, RawResponseContainer>>> GruntAPICall)
        {
            try
            {
                var result = await GruntAPICall();

                if (result.Response.Code == 401)
                {
                    haloInfiniteClient = InstantiateClient();

                    if (haloInfiniteClient == null)
                    {
                        WriteTimedLogEntry("Could not reacquire tokens.");

                        return default;
                    }

                    return await GruntAPICall();
                }

                return result;
            }
            catch (Exception ex)
            {
                WriteTimedLogEntry($"Failed to make Halo Infinite API call. {ex.Message}");
                return null;
            }
        }

        private static async Task<List<Guid>?> GetPlayerMatchIds(string playerXuid, int start, int count, Den.Dev.Grunt.Models.HaloInfinite.MatchType matchType)
        {
            var matchCountSnapshot = await haloInfiniteClient!.Stats.GetMatchCount(playerXuid);

            if (matchCountSnapshot.Response!.Code == 401)
            {
                // The token is no longer working - need to acquire a new one.
                WriteTimedLogEntry($"Token expired. Refreshing...");
                haloInfiniteClient = InstantiateClient();

                // The counter is not accurate.
                matchCountSnapshot = await haloInfiniteClient!.Stats.GetMatchCount(playerXuid);
            }

            if (matchCountSnapshot != null && matchCountSnapshot.Result != null)
            {
                WriteTimedLogEntry($"Got match counts for {playerXuid}.");

                List<Guid> matchIds = new();
                int queryCount = (count == -1) ? 25 : count;
                int queryStart = start;
                int counter = 0;

                //switch (matchType)
                //{
                //    case Den.Dev.Grunt.Models.HaloInfinite.MatchType.Matchmaking:
                //        {
                //            counter = matchCountSnapshot.Result.MatchmadeMatchesPlayedCount;
                //            break;
                //        }
                //    case Den.Dev.Grunt.Models.HaloInfinite.MatchType.Custom:
                //        {
                //            counter = matchCountSnapshot.Result.CustomMatchesPlayedCount;
                //            break;
                //        }
                //    case Den.Dev.Grunt.Models.HaloInfinite.MatchType.Local:
                //        {
                //            counter = matchCountSnapshot.Result.LocalMatchesPlayedCount;
                //            break;
                //        }
                //    default:
                //        {
                //            counter = matchCountSnapshot.Result.MatchesPlayedCount;
                //            break;
                //        }
                //}

                // Need to make sure that the player has more than zero matches played.

                do
                {
                    var matches = await haloInfiniteClient.Stats.GetMatchHistory(playerXuid, queryStart, queryCount, matchType);
                    if (matches != null && matches.Result != null && matches.Result.Results != null && matches.Result.ResultCount > 0)
                    {
                        var matchIdBatch = matches.Result.Results.Select(item => item.MatchId).ToList();
                        WriteTimedLogEntry($"Got matches starting from {queryStart} up to {queryCount} entries. Counter at {counter} and last query yielded {matchIdBatch.Count} results.");
                        matchIds.AddRange(matchIdBatch);
                        counter += matchIdBatch.Count;
                        queryStart = queryStart + matchIdBatch.Count;
                    }
                    else
                    {
                        break;
                    }
                } while (counter > 0);

                return matchIds;
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
                WriteTimedLogEntry("Could not get client information. Make sure you have a client configuration file (client.json) defined in the application folder.");
                return null;
            }

            if (clientConfig == null || clientConfig.ClientId == null || clientConfig.ClientSecret == null || clientConfig.RedirectUrl == null)
            {
                WriteTimedLogEntry("Make sure that the client configuration contains the client ID, client secret, and the redirect URL.");
                return null;
            }

            
            var url = manager.GenerateAuthUrl(clientConfig.ClientId, clientConfig.RedirectUrl);

            OAuthToken? currentOAuthToken = null;

            var ticket = new XboxTicket();
            var haloTicket = new XboxTicket();
            var extendedTicket = new XboxTicket();
            var haloToken = new SpartanToken();

            if (System.IO.File.Exists("tokens.json"))
            {
                WriteTimedLogEntry("Trying to use local tokens...");

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
                        WriteTimedLogEntry("Could not get the token even with the refresh token.");
                        currentOAuthToken = RequestNewToken(url, manager, clientConfig);
                    }
                    ticket = await manager.RequestUserToken(currentOAuthToken.AccessToken);
                }
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                haloTicket = await manager.RequestXstsToken(ticket.Token, HaloCoreEndpoints.HaloWaypointXstsRelyingParty);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                extendedTicket = await manager.RequestXstsToken(ticket.Token);
            }).GetAwaiter().GetResult();

            Task.Run(async () =>
            {
                haloToken = await haloAuthClient.GetSpartanToken(haloTicket.Token, 4);
                WriteTimedLogEntry("Your Halo token:");
                WriteTimedLogEntry(haloToken.Token);
            }).GetAwaiter().GetResult();

            if (haloToken != null && extendedTicket != null)
            {
                //Let's create an instance to experiment with the Halo Infinite client.
                return new HaloInfiniteClient(haloToken.Token, extendedTicket.DisplayClaims.Xui[0].XUID, includeRawResponses: true);
            }
            else
            {
                return null;
            }
        }

        private static OAuthToken RequestNewToken(string url, XboxAuthenticationClient manager, ClientConfiguration clientConfig)
        {
            WriteTimedLogEntry("Provide account authorization and grab the code from the URL:");
            WriteTimedLogEntry(url);

            WriteTimedLogEntry("Your code:");
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
                        WriteTimedLogEntry("Stored the tokens locally.");
                    }
                    else
                    {
                        WriteTimedLogEntry("There was an issue storing tokens locally. A new token will be requested on the next run.");
                    }
                }
                else
                {
                    WriteTimedLogEntry("No token was obtained. There is no valid token to be used right now.");
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

        private static void WriteTimedLogEntry(string message)
        {
            Console.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
}
