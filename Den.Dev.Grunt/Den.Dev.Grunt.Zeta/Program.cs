using System;
using System.Threading.Tasks;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var app = new App();
                await app.RunAsync();
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenPaths);
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[dim]Press any key to exit...[/]");
                Console.ReadKey(true);
            }
        }
    }
}
