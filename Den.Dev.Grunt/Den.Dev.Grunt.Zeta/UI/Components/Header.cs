using System;
using System.Text;
using Den.Dev.Grunt.Zeta.Models;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Components
{
    public static class Header
    {
        public static void Render(ExecutionContext context, params string[] breadcrumbs)
        {
            Console.Clear();

            var gamertag = Markup.Escape(context.Gamertag ?? "Unknown");
            var xuid = context.Xuid ?? "N/A";
            var clearance = string.IsNullOrEmpty(context.ClearanceToken)
                ? "None"
                : context.ClearanceToken;

            // Build status bar content
            var statusContent = new Markup(
                $"[bold cyan]Grunt Zeta[/] [dim]│[/] " +
                $"[dim]GT:[/] [cyan]{gamertag}[/] [dim]│[/] " +
                $"[dim]XUID:[/] [cyan]{xuid}[/] [dim]│[/] " +
                $"[dim]Clearance:[/] [cyan]{Markup.Escape(clearance)}[/]");

            // Wrap in rounded panel with muted border
            var panel = new Panel(statusContent)
                .Border(BoxBorder.Rounded)
                .BorderColor(Theme.Muted)
                .Padding(0, 0, 0, 0);

            AnsiConsole.Write(panel);

            // Render breadcrumbs if provided
            if (breadcrumbs.Length > 0)
            {
                RenderBreadcrumbs(breadcrumbs);
            }

            AnsiConsole.Write(new Rule().RuleStyle("dim"));
            AnsiConsole.WriteLine();
        }

        private static void RenderBreadcrumbs(string[] breadcrumbs)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < breadcrumbs.Length; i++)
            {
                if (i == breadcrumbs.Length - 1)
                {
                    // Current segment - highlighted
                    sb.Append($"[bold cyan]{Markup.Escape(breadcrumbs[i])}[/]");
                }
                else
                {
                    // Parent segments - muted, separator in yellow
                    sb.Append($"[dim]{Markup.Escape(breadcrumbs[i])}[/]");
                    sb.Append($"[yellow]{Theme.BreadcrumbSeparator}[/]");
                }
            }

            var breadcrumbContent = new Markup(sb.ToString());
            var panel = new Panel(breadcrumbContent)
                .Border(BoxBorder.Rounded)
                .BorderColor(Theme.Muted)
                .Padding(0, 0, 0, 0);

            AnsiConsole.Write(panel);
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
