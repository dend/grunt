# Den.Dev.Grunt - Halo Infinite API for .NET

[![NuGet](https://img.shields.io/nuget/v/Den.Dev.Grunt)](https://www.nuget.org/packages/Den.Dev.Grunt)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Den.Dev.Grunt)](https://www.nuget.org/packages/Den.Dev.Grunt)

A .NET library for the Halo Infinite API. Get player stats, match history, inventory, ranks, and more with strongly-typed responses.

## Install

```bash
dotnet add package Den.Dev.Grunt
```

## Quick Start (2 minutes)

All you need is a **Spartan token**. Get one by inspecting network traffic on [halowaypoint.com](https://halowaypoint.com):

1. Sign in to Halo Waypoint
2. Open browser DevTools (F12) → Network tab
3. Find any API call returning JSON
4. Copy the `x-343-authorization-spartan` header value (that's your Spartan token)
5. Optionally copy the `343-clearance` header value (needed for some endpoints)

Then use it:

```csharp
using Den.Dev.Grunt.Core;

var client = new HaloInfiniteClient("<YOUR_SPARTAN_TOKEN>", clearanceToken: "<YOUR_CLEARANCE_TOKEN>");

// Get match stats
var result = await client.Stats.GetMatchStatsAsync("match-guid-here");
if (result.IsSuccess)
{
    Console.WriteLine($"Map: {result.Result.MatchInfo.MapVariant.AssetId}");
}

// Get player service record
var record = await client.Stats.GetPlayerServiceRecordByGamertagAsync("BreadKrtek", LifecycleMode.Matchmade);

// Get player inventory
var inventory = await client.Economy.GetInventoryItemsAsync("player-xuid");

// Get medal metadata
var medals = await client.GameCms.GetMedalMetadataAsync();
```

## Available Modules

Access API domains through the client's module properties:

```csharp
var client = new HaloInfiniteClient(spartanToken, clearanceToken: clearanceToken);
```

| Module | What You Can Do | Example |
|:-------|:----------------|:--------|
| `client.Stats` | Match history, service records, stats | `GetMatchHistoryAsync(xuid, 0, 25, MatchType.All)` |
| `client.Economy` | Inventory, stores, customization, currency | `GetInventoryItemsAsync(xuid)` |
| `client.GameCms` | Medals, challenges, seasons, items | `GetMedalMetadataAsync()` |
| `client.Skill` | Competitive Skill Rank (CSR) | `GetPlaylistCsrAsync(playlistId, playerIds)` |
| `client.Ugc` | Create/edit maps, modes, prefabs | `SpawnAssetAsync(title, assetType, asset)` |
| `client.UgcDiscovery` | Search/browse community content | `SearchAsync(start: 0, count: 25)` |
| `client.Academy` | Bot customization, drills | `GetBotCustomizationAsync(flightId)` |
| `client.Lobby` | QoS servers, presence | `GetQosServersAsync()` |
| `client.Settings` | Clearance, feature flags | `GetActiveClearanceAsync(flightId)` |
| `client.Configuration` | API endpoint discovery | `GetApiSettingsContainerAsync()` |
| `client.BanProcessor` | Ban status checks | `GetBanSummaryAsync(targetList)` |
| `client.TextModeration` | Text moderation keys | `GetSigningKeysAsync()` |

## Handling Responses

Every method returns `HaloApiResultContainer<T, RawResponseContainer>`:

```csharp
var result = await client.Stats.GetMatchStatsAsync("match-guid");

// Check if the request succeeded
if (result.IsSuccess)
{
    var matchData = result.Result;  // Strongly-typed response
}
else
{
    Console.WriteLine($"Error {result.Response.Code}: {result.Response.Message}");
}
```

### Raw Response Inspection

Enable raw responses to see full HTTP details (useful for debugging):

```csharp
var client = new HaloInfiniteClient(spartanToken, includeRawResponses: true);

var result = await client.Stats.GetMatchStatsAsync("match-guid");
Console.WriteLine(result.Response.RequestUrl);
Console.WriteLine(result.Response.RequestMethod);
Console.WriteLine(result.Response.Message);  // Raw JSON response body
```

## Cancellation Support

All async methods accept a `CancellationToken`:

```csharp
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

try
{
    var result = await client.Stats.GetMatchStatsAsync("match-guid", cts.Token);
}
catch (OperationCanceledException)
{
    Console.WriteLine("Request timed out.");
}
```

## Authentication

### Option 1: Manual Token (Quickest)

Grab the Spartan token from Halo Waypoint (see Quick Start above). Tokens expire frequently — get a new one if you see `401 Unauthorized`.

### Option 2: Programmatic Token

Use `HaloAuthenticationClient` to exchange an Xbox Live XSTS token for a Spartan token:

```csharp
using Den.Dev.Grunt.Authentication;

var authClient = new HaloAuthenticationClient();
var spartanToken = await authClient.GetSpartanTokenAsync(xstsToken);

var client = new HaloInfiniteClient(spartanToken.Token, xuid: playerXuid);
```

To get an XSTS token, you need an [Azure AD app registration](https://docs.microsoft.com/azure/active-directory/develop/quickstart-register-app) and the Xbox Live authentication flow via the [Den.Dev.Conch](https://www.nuget.org/packages/Den.Dev.Conch) package.

### Getting Clearance

Some endpoints require a clearance token. Get one after authenticating:

```csharp
var clearance = await client.Settings.GetActiveClearanceAsync("1.6");
if (clearance.IsSuccess)
{
    client.ClearanceToken = clearance.Result.FlightConfigurationId;
}
```

> **Note:** You must launch Halo Infinite at least once on your account before the clearance API will work. If you get `403 Forbidden`, this is the likely cause.

## Testability

The library provides interfaces for dependency injection and mocking:

```csharp
// Register in DI container
services.AddSingleton<IHaloInfiniteClient>(
    new HaloInfiniteClient(spartanToken, clearanceToken: clearanceToken));

// Inject in your services
public class MyService
{
    private readonly IHaloInfiniteClient _client;

    public MyService(IHaloInfiniteClient client) => _client = client;
}

// Mock in tests
var mock = new Mock<IHaloInfiniteClient>();
mock.Setup(c => c.Stats).Returns(mockStatsModule);
```

You can also inject a custom `HttpClient` for full control over the HTTP pipeline:

```csharp
var httpClient = new HttpClient(new MyLoggingHandler(new HttpClientHandler()));
var client = new HaloInfiniteClient(httpClient, spartanToken);
```

## Halo Waypoint APIs

For Halo Waypoint content (articles, profiles, service awards):

```csharp
using Den.Dev.Grunt.Core;

var waypointClient = new WaypointClient();

// Get articles (no authentication required)
var articles = await waypointClient.Content.GetArticlesAsync(language: "en", count: 10);

// Get player profile (requires Spartan token)
var wpClient = new WaypointClient(spartanToken, xuid: playerXuid);
var profile = await wpClient.Profile.GetMyProfileAsync();
```

## Building from Source

```bash
cd src/dotnet/Den.Dev.Grunt
dotnet build
```

Requires .NET 10.0 SDK or later.

## API Reference

Full documentation: [docs.gruntapi.com](https://docs.gruntapi.com)

## License

MIT - see [LICENSE](../../../LICENSE).
