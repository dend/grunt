# Den.Dev.Grunt - .NET Library

The core .NET library for interacting with Halo Infinite APIs.

## Installation

```bash
dotnet add package Den.Dev.Grunt
```

Or via the NuGet Package Manager:

```
Install-Package Den.Dev.Grunt
```

## Components

| Component                 | Description |
|:--------------------------|:------------|
| `Den.Dev.Grunt`           | The core library that wraps the Halo Infinite web APIs. |
| `Den.Dev.Grunt.Zeta`      | Experimental ground for testing wrapped APIs in real scenarios. |
| `Den.Dev.Grunt.Librarian` | Code generator that produces production-quality API client modules from endpoint definitions. |
| `Den.Dev.Grunt.Composer`  | Data composition and transformation utilities. |
| `Den.Dev.Grunt.Auditor`   | Validates models against live API responses to detect discrepancies. |

## Quick Start

### Bring Your Own Token

If you have a Spartan token (obtained from Halo Waypoint), you can use it directly:

```csharp
HaloInfiniteClient client = new("<YOUR_SPARTAN_TOKEN>", clearanceToken: "<YOUR_CLEARANCE_TOKEN>");

// Get match stats
var example = await client.Stats.GetMatchStats("21416434-4717-4966-9902-af7097469f74");
Console.WriteLine("You have data.");
```

### Full Authentication Flow

For automatic token generation, first [register an Azure Active Directory application](https://docs.microsoft.com/azure/active-directory/develop/quickstart-register-app), then create a `client.json` file in your project:

```json
{
  "client_id": "<YOUR_CLIENT_ID_FROM_AAD>",
  "client_secret": "<YOUR_SECRET_FROM_AAD>",
  "redirect_url": "<YOUR_REDIRECT_URI_FROM_AAD>"
}
```

Set the file's `Build Action` to `None` and `Copy to Output Directory` to `Copy if newer`.

Then use the authentication flow:

```csharp
ConfigurationReader clientConfigReader = new();
var clientConfig = clientConfigReader.ReadConfiguration<ClientConfiguration>("client.json");

XboxAuthenticationClient manager = new();
var url = manager.GenerateAuthUrl(clientConfig.ClientId, clientConfig.RedirectUrl);

HaloAuthenticationClient haloAuthClient = new();

OAuthToken currentOAuthToken = null;

var ticket = new XboxTicket();
var haloTicket = new XboxTicket();
var extendedTicket = new XboxTicket();

var xblToken = string.Empty;
var haloToken = new SpartanToken();

if (System.IO.File.Exists("tokens.json"))
{
    Console.WriteLine("Trying to use local tokens...");
    currentOAuthToken = clientConfigReader.ReadConfiguration<OAuthToken>("tokens.json");
}
else
{
    currentOAuthToken = RequestNewToken(url, manager, clientConfig);
}

Task.Run(async () =>
{
    ticket = await manager.RequestUserToken(currentOAuthToken.AccessToken);
    if (ticket == null)
    {
        currentOAuthToken = await manager.RefreshOAuthToken(
            clientConfig.ClientId,
            currentOAuthToken.RefreshToken,
            clientConfig.RedirectUrl,
            clientConfig.ClientSecret);
        if (currentOAuthToken == null)
        {
            Console.WriteLine("Could not get the token even with the refresh token.");
            currentOAuthToken = RequestNewToken(url, manager, clientConfig);
        }
        ticket = await manager.RequestUserToken(currentOAuthToken.AccessToken);
    }
}).GetAwaiter().GetResult();

Task.Run(async () =>
{
    haloTicket = await manager.RequestXstsToken(ticket.Token);
}).GetAwaiter().GetResult();

Task.Run(async () =>
{
    extendedTicket = await manager.RequestXstsToken(ticket.Token, false);
}).GetAwaiter().GetResult();

if (ticket != null)
{
    xblToken = manager.GetXboxLiveV3Token(haloTicket.DisplayClaims.Xui[0].Uhs, haloTicket.Token);
}

Task.Run(async () =>
{
    haloToken = await haloAuthClient.GetSpartanToken(haloTicket.Token);
    Console.WriteLine("Your Halo token:");
    Console.WriteLine(haloToken.Token);
}).GetAwaiter().GetResult();

HaloInfiniteClient client = new(haloToken.Token, extendedTicket.DisplayClaims.Xui[0].Xid);

// Get clearance for API access
string localClearance = string.Empty;
Task.Run(async () =>
{
    var clearance = (await client.Settings.ActiveClearance("1.6")).Result;
    if (clearance != null)
    {
        localClearance = clearance.FlightConfigurationId;
        client.ClearanceToken = localClearance;
        Console.WriteLine($"Your clearance is {localClearance} and it's set in the client.");
    }
}).GetAwaiter().GetResult();

// Now you can make API calls
var stats = await client.Stats.GetMatchStats("21416434-4717-4966-9902-af7097469f74");
```

> **Note:** The clearance (`343-clearance` header) needs to be activated at least once with the game before API access is granted. Launch Halo Infinite at least once on your account before querying the API. If you get `403 Forbidden` errors, this is likely the cause.

## Librarian - API Code Generator

The Librarian automatically generates production-quality API client code from Halo Infinite endpoint definitions.

### Features

- **Automatic endpoint discovery** - Fetches all 177+ endpoints from the live Halo API
- **Strongly-typed responses** - Maps endpoints to specific response types via `response-types.json`
- **HTTP method inference** - Intelligently detects GET, POST, PUT, DELETE based on method names
- **XML documentation** - Generates proper `<summary>`, `<param>`, and `<returns>` tags
- **Module grouping** - Organizes endpoints into logical modules (Economy, Stats, GameCms, etc.)
- **Scriban templates** - Clean, maintainable template syntax for code generation

### Usage

```bash
# Navigate to the Librarian project
cd Den.Dev.Grunt.Librarian

# Generate to default output directory (./Output/Generated)
dotnet run

# Generate with response type mappings
dotnet run -- --response-types response-types.json

# Preview without writing files
dotnet run -- --dry-run

# Generate to a custom directory
dotnet run -- --output C:\MyGeneratedCode
```

### Command Line Options

| Option | Short | Description |
|:-------|:------|:------------|
| `--output` | `-o` | Output directory for generated files (default: `./Output/Generated`) |
| `--response-types` | `-r` | Path to `response-types.json` mapping file |
| `--dry-run` | `-d` | Preview output without writing files |
| `--help` | `-h` | Show help message |

### Response Type Mappings

The `response-types.json` file maps endpoint IDs to their response types:

```json
{
  "Economy_GetActiveBoosts": "ActiveBoostsContainer",
  "Economy_AiCoreCustomization": "AiCore",
  "Stats_GetMatchHistory": "MatchHistoryResponse"
}
```

Endpoints without explicit mappings default to `object` with a TODO comment for manual review.

### Generated Output

The Librarian generates partial class files that can be integrated into the main library:

```
Output/Generated/
├── EconomyModule.Generated.cs
├── GameCmsModule.Generated.cs
├── StatsModule.Generated.cs
├── UgcModule.Generated.cs
├── UgcDiscoveryModule.Generated.cs
└── ...
```

### Example Generated Code

```csharp
/// <summary>
/// Calls the Economy_GetActiveBoosts endpoint.
/// </summary>
/// <param name="player">The player's numeric XUID.</param>
/// <returns>An instance of HaloApiResultContainer containing the response.</returns>
public async Task<HaloApiResultContainer<ActiveBoostsContainer, RawResponseContainer>> GetActiveBoosts(string player)
{
    return await this.GetAsync<ActiveBoostsContainer>(
        $"/hi/players/xuid({player})/boosts",
        useClearance: true);
}
```

## API Modules

The `HaloInfiniteClient` provides access to various API modules:

| Module | Description |
|:-------|:------------|
| `Stats` | Match history, service records, match statistics |
| `Skill` | CSR (Competitive Skill Rank) queries |
| `Economy` | Inventory, stores, customization, currency |
| `GameCms` | Item definitions, challenges, medals, career ranks |
| `Ugc` | User-generated content authoring |
| `UgcDiscovery` | Search and browse user content |
| `Academy` | Bot customization, training drills |
| `Lobby` | QoS servers, lobby presence |
| `Settings` | Clearance levels, feature flags |
| `Configuration` | API endpoint discovery |
| `BanProcessor` | Ban status queries |
| `TextModeration` | Text moderation keys |

## Building from Source

### Prerequisites

- .NET 10.0 SDK or later
- Visual Studio 2022 (optional)

### Build

```bash
cd src/dotnet/Den.Dev.Grunt
dotnet build
```

### Run Tests

```bash
dotnet test
```

## Documentation

Full API documentation is available at [docs.gruntapi.com](https://docs.gruntapi.com).

## License

MIT License - see [LICENSE](../../../LICENSE) for details.
