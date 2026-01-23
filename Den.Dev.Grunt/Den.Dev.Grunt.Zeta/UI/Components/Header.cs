using System;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Components
{
    /// <summary>
    /// Simple header rendering for startup screens (before layout is initialized).
    /// For main app navigation, use ConsoleLayout instead.
    /// </summary>
    public static class Header
    {
        public static void RenderSimple(string? title = null)
        {
            Console.Clear();

            AnsiConsole.MarkupLine("[bold cyan]Grunt Zeta[/]");

            if (!string.IsNullOrEmpty(title))
            {
                AnsiConsole.Write(new Rule($"[bold]{title}[/]").LeftJustified().RuleStyle("dim"));
            }
            else
            {
                AnsiConsole.Write(new Rule().RuleStyle("dim"));
            }

            AnsiConsole.WriteLine();
        }

        public static void RenderMinimal()
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold cyan]Grunt Zeta[/]");
            AnsiConsole.Write(new Rule().RuleStyle("dim"));
            AnsiConsole.WriteLine();
        }
    }
}
