# @dendotdev/grunt

Unofficial TypeScript client library for the Halo Infinite API.

This is the TypeScript implementation of the Grunt library, providing type-safe access to Halo Infinite and Halo Waypoint APIs. For the .NET version, see the [dotnet folder](../dotnet/).

## Installation

```bash
npm install @dendotdev/grunt
```

For authenticated API access, you'll also need the Xbox authentication library:

```bash
npm install @dendotdev/conch
```

## Quick Start

```typescript
import {
  HaloInfiniteClient,
  MatchType,
  LifecycleMode,
  isSuccess,
} from '@dendotdev/grunt';

// Create a client with your Spartan token
const client = new HaloInfiniteClient({
  spartanToken: 'your-spartan-token',
  xuid: 'xuid', // Your Xbox User ID
});

// Get match history
const history = await client.stats.getMatchHistory(
  'xuid',
  0,    // start index
  25,   // count (max 25)
  MatchType.All
);

if (isSuccess(history)) {
  console.log(`Found ${history.result.resultCount} matches`);
  for (const match of history.result.results ?? []) {
    console.log(`Match: ${match.matchId}`);
  }
}
```

## Authentication

To use authenticated endpoints, you need a Spartan token. The complete authentication flow is:

1. Authenticate with Xbox Live using OAuth to get an access token
2. Exchange the access token for an Xbox Live user token
3. Exchange the user token for an XSTS token (using the Halo Waypoint relying party)
4. Exchange the XSTS token for a Spartan token using `HaloAuthenticationClient`

The Xbox authentication steps (1-3) are handled by [`@dendotdev/conch`](https://github.com/dend/conch). Here's a complete example:

```typescript
import { XboxAuthenticationClient } from '@dendotdev/conch';
import { HaloAuthenticationClient, HaloInfiniteClient, isSuccess } from '@dendotdev/grunt';

// Step 1: Set up Xbox authentication
const xboxClient = new XboxAuthenticationClient();

// Generate the OAuth URL for the user to authorize
const clientId = 'your-azure-ad-client-id';
const redirectUrl = 'https://localhost:3000/callback';
const authUrl = xboxClient.generateAuthUrl(clientId, redirectUrl);

// User visits authUrl and authorizes your app, then gets redirected with a code
// ... handle the OAuth redirect and extract the authorization code ...

// Step 2: Exchange the authorization code for OAuth tokens
const oauthToken = await xboxClient.requestOAuthToken(clientId, authorizationCode, redirectUrl);

if (!oauthToken?.access_token) {
  throw new Error('Failed to get OAuth token');
}

// Step 3: Get Xbox Live user token
const userToken = await xboxClient.requestUserToken(oauthToken.access_token);

if (!userToken?.Token) {
  throw new Error('Failed to get user token');
}

// Step 4: Get XSTS token with Halo Waypoint relying party
const relyingParty = HaloAuthenticationClient.getRelyingParty();
const xstsToken = await xboxClient.requestXstsToken(userToken.Token, relyingParty);

if (!xstsToken?.Token) {
  throw new Error('Failed to get XSTS token');
}

// Step 5: Exchange XSTS token for Spartan token
const haloAuthClient = new HaloAuthenticationClient();
const spartanToken = await haloAuthClient.getSpartanToken(xstsToken.Token);

if (!spartanToken?.token) {
  throw new Error('Failed to get Spartan token');
}

// Step 6: Create the Halo Infinite API client
const xuid = xstsToken.DisplayClaims?.xui?.[0]?.xid;
const client = new HaloInfiniteClient({
  spartanToken: spartanToken.token,
  xuid: xuid,
});

// Now you can make authenticated API calls
const history = await client.stats.getMatchHistory(xuid, 0, 25);

if (isSuccess(history)) {
  console.log(`Found ${history.result.resultCount} matches`);
}
```

### OAuth Setup

To use this authentication flow, you'll need to register an application in Azure AD:

1. Go to the [Azure Portal](https://portal.azure.com) and navigate to Azure Active Directory
2. Register a new application with a redirect URI
3. Note your Application (client) ID - this is your `clientId`

For more details on Xbox authentication, see the [@dendotdev/conch documentation](https://github.com/dend/conch).

## API Overview

### HaloInfiniteClient

The main client for Halo Infinite APIs, with 12 specialized modules:

| Module | Description |
|--------|-------------|
| `stats` | Match history, service records, match stats |
| `skill` | CSR (Competitive Skill Rank) queries |
| `economy` | Inventory, stores, customization, currency |
| `gameCms` | Item definitions, challenges, medals, career ranks |
| `ugc` | User-generated content authoring |
| `ugcDiscovery` | Search and browse user content |
| `academy` | Bot customization, drills |
| `lobby` | QoS servers, lobby presence |
| `settings` | Clearance levels, feature flags |
| `configuration` | API endpoint discovery |
| `banProcessor` | Ban status queries |
| `textModeration` | Text moderation keys |

### WaypointClient

Client for Halo Waypoint APIs:

| Module | Description |
|--------|-------------|
| `profile` | User profiles and settings |
| `redemption` | Code redemption |
| `content` | News articles |
| `comms` | Notifications |

## Usage Examples

### Get Player Service Record

```typescript
const record = await client.stats.getPlayerServiceRecordByXuid(
  'xuid',
  LifecycleMode.Matchmade
);

if (isSuccess(record)) {
  const stats = record.result.stats?.coreStats;
  console.log(`K/D: ${stats?.kills}/${stats?.deaths}`);
}
```

### Get Match Details

```typescript
const match = await client.stats.getMatchStats('match-guid-here');

if (isSuccess(match)) {
  console.log(`Map: ${match.result.matchInfo?.mapVariant?.publicName}`);
  console.log(`Players: ${match.result.players?.length}`);
}
```

### Get Player CSR

```typescript
const csr = await client.skill.getPlaylistCsr(
  'playlist-guid',
  ['xuid']
);

if (isSuccess(csr)) {
  const playerCsr = csr.result.value?.[0];
  console.log(`CSR: ${playerCsr?.csr?.value} (${playerCsr?.csr?.tier})`);
}
```

### Get Player Inventory

```typescript
const inventory = await client.economy.getInventoryItems('xuid');

if (isSuccess(inventory)) {
  console.log(`Items owned: ${inventory.result.items?.length}`);
}
```

### Search UGC Maps

```typescript
import { AssetKind } from '@dendotdev/grunt';

const maps = await client.ugcDiscovery.search({
  assetKinds: [AssetKind.Map],
  term: 'blood gulch',
  count: 10,
});

if (isSuccess(maps)) {
  for (const map of maps.result.results ?? []) {
    console.log(`${map.publicName} by ${map.admin}`);
  }
}
```

### Get News Articles (No Auth Required)

```typescript
import { WaypointClient, isSuccess } from '@dendotdev/grunt';

const client = new WaypointClient(); // No auth needed

const articles = await client.content.getArticles(1, 10);

if (isSuccess(articles)) {
  for (const article of articles.result.articles ?? []) {
    console.log(article.title);
  }
}
```

## Result Handling

All API methods return `HaloApiResult<T>` which contains:
- `result`: The response data (or `null` on failure)
- `response`: Raw response info (status code, headers, etc.)

Use the helper functions to check results:

```typescript
import {
  isSuccess,      // 2xx status with data
  isNotModified,  // 304 (cached response valid)
  isClientError,  // 4xx errors
  isServerError,  // 5xx errors
} from '@dendotdev/grunt';

const result = await client.stats.getMatchStats('match-id');

if (isSuccess(result)) {
  // result.result is guaranteed non-null here
  console.log(result.result.matchId);
} else if (isClientError(result)) {
  console.error(`Client error: ${result.response.code}`);
} else if (isServerError(result)) {
  console.error(`Server error: ${result.response.code}`);
}
```

## Configuration Options

### HaloInfiniteClient Options

```typescript
const client = new HaloInfiniteClient({
  // Required
  spartanToken: 'your-spartan-token',

  // Optional
  xuid: 'xuid',        // Your Xbox User ID
  clearanceToken: 'flight-id',     // For flighted/preview content
  includeRawResponses: true,       // Include full request/response in results
  userAgent: 'MyApp/1.0',          // Custom User-Agent header
  cacheTtlMs: 3600000,             // Cache TTL (default: 60 minutes)
  maxRetries: 3,                   // Retry attempts (default: 3)
});
```

## Building from Source

### Prerequisites

- Node.js 18.0.0 or higher
- npm

### Install Dependencies

```bash
npm install
```

### Build

```bash
npm run build
```

This creates the `dist/` folder with:
- `index.js` - CommonJS build
- `index.mjs` - ES Module build
- `index.d.ts` - TypeScript declarations

### Development

```bash
# Watch mode (rebuild on changes)
npm run dev

# Type check without emitting
npm run typecheck

# Run tests
npm run test
```

## Features

- **Type-safe**: Full TypeScript support with comprehensive type definitions
- **Caching**: Built-in ETag-based caching with configurable TTL
- **Retry Logic**: Automatic retry with exponential backoff for transient failures
- **Lazy Loading**: Modules are initialized on first access to minimize memory usage
- **Minimal Dependencies**: Only one runtime dependency (`lru-cache`)
- **Universal**: Works in Node.js and modern browsers (uses native `fetch`)

## API Reference

For detailed API documentation, refer to the TypeScript type definitions included with the package, or explore the source code in the `src/` directory.

The API mirrors the [.NET Grunt library](../dotnet/) structure, so its documentation can also serve as a reference.

## Disclaimer

This is an unofficial library and is not affiliated with Microsoft, 343 Industries, or Xbox Game Studios. Use at your own risk. The Halo Infinite API is not officially documented and may change without notice.

## License

MIT License - see [LICENSE](LICENSE) for details.

## Credits

- Original Grunt project by [Den Delimarsky](https://den.dev)
- TypeScript implementation maintains API compatibility with the .NET version
