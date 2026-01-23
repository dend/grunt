using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.UI.Components;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Screens
{
    public class SettingsScreen
    {
        private readonly ExecutionContext _context;

        public SettingsScreen(ExecutionContext context)
        {
            _context = context;
        }

        public void Show()
        {
            while (true)
            {
                Header.Render(_context, "Settings");

                // Show current setting status
                var statusText = _context.VerboseDiagnosticsEnabled
                    ? "[green]● Enabled[/]"
                    : "[dim]○ Disabled[/]";

                AnsiConsole.MarkupLine($"Verbose HTTP Diagnostics: {statusText}");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[dim]When enabled, API calls will show full HTTP details:[/]");
                AnsiConsole.MarkupLine("[dim]• Request URL, method, headers, and body[/]");
                AnsiConsole.MarkupLine("[dim]• Response headers[/]");
                AnsiConsole.MarkupLine("[dim]• Method parameters[/]");
                AnsiConsole.WriteLine();

                var toggleChoice = _context.VerboseDiagnosticsEnabled
                    ? "Disable Verbose Diagnostics"
                    : "Enable Verbose Diagnostics";

                var selection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .HighlightStyle(Theme.Highlight)
                        .AddChoices(toggleChoice, "[dim]Back[/]"));

                if (selection.Contains("Enable"))
                {
                    _context.VerboseDiagnosticsEnabled = true;
                    if (_context.HaloClient != null)
                        _context.HaloClient.IncludeRawResponses = true;
                    if (_context.WaypointClient != null)
                        _context.WaypointClient.IncludeRawResponses = true;
                }
                else if (selection.Contains("Disable"))
                {
                    _context.VerboseDiagnosticsEnabled = false;
                    if (_context.HaloClient != null)
                        _context.HaloClient.IncludeRawResponses = false;
                    if (_context.WaypointClient != null)
                        _context.WaypointClient.IncludeRawResponses = false;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
