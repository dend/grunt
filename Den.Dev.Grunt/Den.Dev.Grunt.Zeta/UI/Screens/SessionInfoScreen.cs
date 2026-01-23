using System;
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.UI.Components;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Screens
{
    public class SessionInfoScreen
    {
        private readonly ExecutionContext _context;

        public SessionInfoScreen(ExecutionContext context)
        {
            _context = context;
        }

        public void Show()
        {
            Header.Render(_context, "Session Info");

            var status = _context.IsAuthenticated ? "[green]● Authenticated[/]" : "[red]● Not authenticated[/]";
            var clearance = string.IsNullOrEmpty(_context.ClearanceToken)
                ? "[dim]None[/]"
                : $"[cyan]{_context.ClearanceToken}[/]";

            var content = new Markup(
                $"[dim]Gamertag[/]     [cyan]{Markup.Escape(_context.Gamertag ?? "Unknown")}[/]\n" +
                $"[dim]XUID[/]         [cyan]{_context.Xuid ?? "N/A"}[/]\n" +
                $"[dim]Player ID[/]    [cyan]xuid({_context.Xuid})[/]\n" +
                $"[dim]Clearance[/]    {clearance}\n" +
                $"[dim]Status[/]       {status}");

            var panel = new Panel(content)
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.Cyan1)
                .Header("[cyan]Account[/]")
                .Padding(1, 0, 1, 0);

            AnsiConsole.Write(panel);

            if (!string.IsNullOrEmpty(_context.SpartanToken))
            {
                AnsiConsole.WriteLine();
                var truncatedToken = _context.SpartanToken.Length > 80
                    ? _context.SpartanToken.Substring(0, 80) + "..."
                    : _context.SpartanToken;

                var tokenPanel = new Panel($"[dim]{Markup.Escape(truncatedToken)}[/]")
                    .Border(BoxBorder.Rounded)
                    .BorderColor(Color.Grey)
                    .Header("[dim]Spartan Token[/]")
                    .Padding(1, 0, 1, 0);

                AnsiConsole.Write(tokenPanel);
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[dim]Press any key to return...[/]");
            Console.ReadKey(true);
        }
    }
}
