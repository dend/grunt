using System;
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.UI.Components;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta.UI.Screens
{
    public enum MainMenuChoice
    {
        HaloInfinite,
        Waypoint,
        History,
        SessionInfo,
        Exit
    }

    public class MainMenuScreen
    {
        private readonly ExecutionContext _context;

        public MainMenuScreen(ExecutionContext context)
        {
            _context = context;
        }

        public MainMenuChoice Show()
        {
            Header.Render(_context);

            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .HighlightStyle(new Style(Color.Cyan1))
                    .AddChoices(
                        "[cyan]›[/] Halo Infinite API",
                        "[cyan]›[/] Waypoint API",
                        "[cyan]›[/] History",
                        "[cyan]›[/] Session Info",
                        "[dim]× Exit[/]"));

            return selection switch
            {
                "[cyan]›[/] Halo Infinite API" => MainMenuChoice.HaloInfinite,
                "[cyan]›[/] Waypoint API" => MainMenuChoice.Waypoint,
                "[cyan]›[/] History" => MainMenuChoice.History,
                "[cyan]›[/] Session Info" => MainMenuChoice.SessionInfo,
                _ => MainMenuChoice.Exit
            };
        }
    }
}
