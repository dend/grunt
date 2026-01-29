// <copyright file="Program.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Den.Dev.Grunt is managed by Halo Studios and Microsoft. This wrapper is not endorsed by Halo Studios or Microsoft.
// </copyright>

using System.CommandLine;
using Den.Dev.Grunt.Auditor.Commands;

namespace Den.Dev.Grunt.Auditor
{
    /// <summary>
    /// Entry point for the Auditor CLI tool.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Exit code.</returns>
        public static int Main(string[] args)
        {
            var rootCommand = new RootCommand("Den.Dev.Grunt.Auditor - Halo Infinite API Model Validation Tool")
            {
                new DiscoverCommand(),
                new ValidateCommand(),
                new UpdateSnapshotsCommand(),
                new ValidateJsonCommand(),
            };

            return rootCommand.Invoke(args);
        }
    }
}
