using System;
using Den.Dev.Grunt.Zeta.Models;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Components
{
    public static class Header
    {
        public static void Render(ExecutionContext context, string? title = null)
        {
            Console.Clear();

            var gamertag = Markup.Escape(context.Gamertag ?? "Unknown");
            var xuid = context.Xuid ?? "N/A";
            var clearance = string.IsNullOrEmpty(context.ClearanceToken)
                ? "None"
                : context.ClearanceToken;

            // Compact horizontal header
            AnsiConsole.MarkupLine(
                $"[bold cyan]Grunt Zeta[/] [dim]│[/] " +
                $"[dim]GT:[/] [cyan]{gamertag}[/] [dim]│[/] " +
                $"[dim]XUID:[/] [cyan]{xuid}[/] [dim]│[/] " +
                $"[dim]Clearance:[/] [cyan]{Markup.Escape(clearance)}[/]");

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
