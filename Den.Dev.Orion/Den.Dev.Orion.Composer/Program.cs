using System;
using System.CommandLine;

namespace Den.Dev.Orion.Composer
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
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
                lifecycleModeArgument
            };
            getCommand.AddCommand(matchesCommand);

            matchesCommand.SetHandler(MatchCommandHandler, isXuidFileArgument, xuidArgument, startArgument, countArgument, lifecycleModeArgument);

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
        private static void MatchCommandHandler(bool isXuidFile, string xuid, int start, int count, string lifecycleMode)
        {
            
        }
    }
}