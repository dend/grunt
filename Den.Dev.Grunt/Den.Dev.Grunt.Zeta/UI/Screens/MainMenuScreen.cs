using Den.Dev.Grunt.Zeta.Models;
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
        private readonly ConsoleLayout _layout;

        public MainMenuScreen(ExecutionContext context, ConsoleLayout layout)
        {
            _context = context;
            _layout = layout;
        }

        public MainMenuChoice Show()
        {
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
