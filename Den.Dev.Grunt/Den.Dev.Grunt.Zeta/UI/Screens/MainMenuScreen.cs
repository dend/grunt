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
        Settings,
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
                        "Halo Infinite API",
                        "Waypoint API",
                        "History",
                        "Session Info",
                        "Settings",
                        "[dim]Exit[/]"));

            return selection switch
            {
                "Halo Infinite API" => MainMenuChoice.HaloInfinite,
                "Waypoint API" => MainMenuChoice.Waypoint,
                "History" => MainMenuChoice.History,
                "Session Info" => MainMenuChoice.SessionInfo,
                "Settings" => MainMenuChoice.Settings,
                _ => MainMenuChoice.Exit
            };
        }
    }
}
