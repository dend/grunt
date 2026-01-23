using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Den.Dev.Grunt.Zeta.Models;
using Den.Dev.Grunt.Zeta.Registry;
using Den.Dev.Grunt.Zeta.Services;
using Den.Dev.Grunt.Zeta.UI;
using Den.Dev.Grunt.Zeta.UI.Components;
using Den.Dev.Grunt.Zeta.UI.Screens;
using Spectre.Console;

namespace Den.Dev.Grunt.Zeta
{
    public class App
    {
        private readonly AuthenticationService _authService;
        private readonly HistoryService _historyService;
        private readonly ApiMethodRegistry _registry;
        private ApiExecutionService? _executionService;
        private ExecutionContext? _context;

        public App()
        {
            _authService = new AuthenticationService();
            _historyService = new HistoryService();
            _registry = new ApiMethodRegistry();
        }

        public async Task RunAsync()
        {
            Console.Title = "Grunt Zeta";

            Header.RenderSimple("Grunt Zeta");
            AnsiConsole.MarkupLine("[dim]Halo Infinite API Testing Tool[/]");
            AnsiConsole.WriteLine();

            _context = await _authService.AuthenticateAsync();
            if (_context == null || !_context.IsAuthenticated)
            {
                AnsiConsole.MarkupLine("[red]●[/] Authentication failed.");
                return;
            }

            _executionService = new ApiExecutionService(_historyService);

            AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Theme.Spinner)
                .Start("[yellow]Discovering API methods[/]", ctx =>
                {
                    ctx.Status("[bold blue]Scanning Halo Infinite modules[/]");
                    _registry.DiscoverMethods(_context.HaloClient!, _context.WaypointClient!);
                });

            var totalMethods = _registry.HaloModules.Sum(m => m.Methods.Count) +
                               _registry.WaypointModules.Sum(m => m.Methods.Count);

            AnsiConsole.MarkupLine($"[green]●[/] Discovered [cyan]{totalMethods}[/] API methods");
            System.Threading.Thread.Sleep(500);

            await MainLoopAsync();
        }

        private async Task MainLoopAsync()
        {
            var mainMenuScreen = new MainMenuScreen(_context!);
            var apiBrowserScreen = new ApiBrowserScreen(_context!);
            var historyScreen = new HistoryScreen(_historyService, _context!);
            var sessionInfoScreen = new SessionInfoScreen(_context!);

            while (true)
            {
                var choice = mainMenuScreen.Show();

                switch (choice)
                {
                    case MainMenuChoice.HaloInfinite:
                        await BrowseApiAsync(apiBrowserScreen, _registry.HaloModules, "Halo Infinite API");
                        break;

                    case MainMenuChoice.Waypoint:
                        await BrowseApiAsync(apiBrowserScreen, _registry.WaypointModules, "Waypoint API");
                        break;

                    case MainMenuChoice.History:
                        historyScreen.Show();
                        break;

                    case MainMenuChoice.SessionInfo:
                        sessionInfoScreen.Show();
                        break;

                    case MainMenuChoice.Exit:
                        return;
                }
            }
        }

        private async Task BrowseApiAsync(
            ApiBrowserScreen browser,
            IReadOnlyList<ModuleMetadata> modules,
            string apiName)
        {
            while (true)
            {
                var (module, method) = browser.Browse(modules, apiName);
                if (module == null || method == null)
                {
                    return;
                }

                await ExecuteMethodAsync(module, method);
            }
        }

        private async Task ExecuteMethodAsync(ModuleMetadata module, MethodMetadata method)
        {
            Header.Render(_context!, $"{module.DisplayName} > {method.DisplayName}");

            // Show parameters info
            if (method.Parameters.Length > 0)
            {
                AnsiConsole.MarkupLine("[dim]Parameters:[/]");
                foreach (var p in method.Parameters)
                {
                    var optional = p.IsOptional ? " [yellow](optional)[/]" : "";
                    AnsiConsole.MarkupLine($"  [cyan]{p.Name}[/] : [dim]{p.ParameterType.Name}[/]{optional}");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[dim]No parameters required[/]");
            }

            AnsiConsole.WriteLine();

            var parameters = ParameterForm.CollectParameters(method.Parameters, _context!.Xuid);

            AnsiConsole.WriteLine();
            if (!AnsiConsole.Confirm("Execute API call?", true))
            {
                return;
            }

            ApiCallRecord? record = null;

            await AnsiConsole.Status()
                .AutoRefresh(true)
                .Spinner(Theme.Spinner)
                .StartAsync($"[yellow]Executing API call[/]", async ctx =>
                {
                    ctx.Status($"[bold blue]Calling {method.DisplayName}[/]");
                    record = await _executionService!.ExecuteMethodAsync(module, method, parameters);
                });

            Header.Render(_context!, $"{module.DisplayName} > {method.DisplayName}");

            ResponseRenderer.RenderResponse(record!);

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[dim]Press any key to continue...[/]");
            Console.ReadKey(true);
        }
    }
}
