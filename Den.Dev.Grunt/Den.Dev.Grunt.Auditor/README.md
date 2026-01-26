# Den.Dev.Grunt.Auditor

A CLI tool for validating Halo Infinite API response models against live API data.

## Purpose

The Halo Infinite API evolves over time - fields get added, types change, and new data appears. The Auditor helps detect **model drift** by:

1. Calling live API endpoints with real parameters
2. Comparing JSON responses against C# model definitions
3. Reporting discrepancies (missing properties, type mismatches)
4. Optionally updating XMLDocsExamples with fresh snapshots

## Quick Start

```bash
# From the Den.Dev.Grunt directory
cd Den.Dev.Grunt.Auditor

# Discover available test parameters
dotnet run -- discover --config client.json

# Validate all configured endpoints
dotnet run -- validate --config client.json

# Update XML documentation examples
dotnet run -- update-snapshots --config client.json
```

## The Discovery Chain Problem

Most Halo Infinite API endpoints require **dynamic parameters** that can't be hardcoded:

| Endpoint | Required Parameter | Where It Comes From |
|----------|-------------------|---------------------|
| `Stats.GetMatchStats(matchId)` | A valid match GUID | Player's match history |
| `Economy.GetActiveBoosts(player)` | Player's XUID | Authentication |
| `Academy.GetBotCustomization(flightId)` | Flight config ID | Clearance endpoint |
| `HIUGC.GetAsset(assetId)` | UGC asset GUID | UGC search results |

You can't just call `GetMatchStats("some-match-id")` - you need a **real match ID** that exists in the system. And to get that, you need to call `GetMatchHistory` first, which itself requires your player XUID.

### The Solution: Discovery Chain

The Auditor solves this with a **discovery chain** - a sequence of API calls that progressively builds up a registry of valid parameters:

```
┌─────────────────────┐
│   Authentication    │
│  (OAuth → Spartan)  │
└──────────┬──────────┘
           │
           ▼
    ┌──────────────┐
    │ playerXuid   │  ← From XSTS ticket claims
    │ gamertag     │
    └──────┬───────┘
           │
           ▼
┌─────────────────────────┐
│  Settings.GetClearance  │  Step 1
└──────────┬──────────────┘
           │
           ▼
    ┌──────────────┐
    │ flightId     │  ← Extracted from response
    │ clearanceId  │
    └──────┬───────┘
           │
           ▼
┌─────────────────────────┐
│  Stats.GetMatchHistory  │  Step 2 (uses $playerXuid)
└──────────┬──────────────┘
           │
           ▼
    ┌──────────────┐
    │ matchId      │  ← First result's MatchId
    │ matchIds[]   │  ← All result MatchIds
    └──────┬───────┘
           │
           ▼
┌─────────────────────────┐
│   Validation Targets    │  Now we can call any endpoint
│   (using discovered     │  that needs these parameters
│    parameters)          │
└─────────────────────────┘
```

### How It Works in Code

The discovery chain is defined in `Config/endpoint-test-config.json`:

```json
{
  "discoveryChain": [
    {
      "step": 1,
      "endpointId": "Settings_GetClearance",
      "method": "Settings.GetClearance",
      "args": {
        "releaseId": "RETAIL",
        "sandbox": "UNUSED",
        "build": "268411.25.10.26.1801-0",
        "release": "1.13"
      },
      "extractors": {
        "flightId": "$.FlightConfigurationId"
      }
    },
    {
      "step": 2,
      "endpointId": "Stats_GetMatchHistory",
      "method": "Stats.GetMatchHistory",
      "args": {
        "player": "$playerXuid",
        "start": 0,
        "count": 10,
        "type": "All"
      },
      "extractors": {
        "matchId": "$.Results[0].MatchId",
        "matchIds": "$.Results[*].MatchId"
      }
    }
  ]
}
```

Key concepts:

1. **Steps are ordered** - Step 1 runs before Step 2
2. **Args can reference parameters** - `"$playerXuid"` is resolved from the registry
3. **Extractors use JSONPath** - `$.Results[0].MatchId` pulls the first match ID from the response
4. **Extracted values are stored** - Later steps and validation targets can use them

### Parameter Resolution

When an endpoint needs a parameter, the registry resolves it:

| Reference | Resolves To |
|-----------|-------------|
| `$playerXuid` or `$player` | Authenticated player's XUID |
| `$flightId` | Flight config ID from clearance |
| `$matchId` | First discovered match ID |
| `$assetId` | First discovered UGC asset ID |
| `$customName` | Any custom parameter from extractors |

### Extractor Syntax

Extractors use a simplified JSONPath syntax:

| Pattern | Meaning |
|---------|---------|
| `$.Property` | Direct property access |
| `$.Nested.Property` | Nested property |
| `$.Array[0]` | First array element |
| `$.Array[*].Id` | All `Id` values from array (returns list) |

### Why Not Hardcode Test Data?

1. **IDs expire** - Match IDs from 2022 may no longer resolve
2. **Player-specific** - Your XUID is different from mine
3. **Environment changes** - Flight IDs change with game updates
4. **Real validation** - Using live data catches real issues

## Validation Targets

After discovery, the Auditor validates each configured endpoint:

```json
{
  "validationTargets": [
    {
      "endpointId": "Stats_GetMatchStats",
      "method": "Stats.GetMatchStats",
      "args": { "matchId": "$matchId" },
      "expectedModel": "MatchStats"
    }
  ]
}
```

The validator:
1. Resolves `$matchId` from the registry
2. Calls `client.Stats.GetMatchStats(matchId)` via reflection
3. Captures the raw JSON response
4. Walks the JSON structure comparing against `MatchStats` class properties
5. Reports any discrepancies

## Validation Results

| Status | Meaning |
|--------|---------|
| **Pass** | JSON matches model perfectly |
| **Warning** | Unexpected properties in JSON (model may need updates) |
| **Fail** | Type mismatches or deserialization would fail |
| **Skipped** | Missing required parameter or destructive operation |
| **Error** | API call failed (network, auth, etc.) |

### Discrepancy Types

| Type | Severity | Action Needed |
|------|----------|---------------|
| `UnexpectedProperty` | Warning | Add property to C# model |
| `TypeMismatch` | Fail | Fix property type in model |
| `NullabilityIssue` | Warning | Make property nullable |
| `DeserializationFailure` | Fail | Model structure is wrong |

## Skip Patterns

Some endpoints should never be called during validation:

```json
{
  "skipEndpoints": [
    { "pattern": "*_Delete*", "reason": "Destructive operation" },
    { "pattern": "*_Update*", "reason": "Modifying operation" },
    { "pattern": "HIUGC_FavoriteAnAsset", "reason": "Modifying operation" }
  ]
}
```

Patterns support `*` wildcards and are matched case-insensitively.

## Commands

### discover

Authenticates and shows all discovered parameters:

```bash
dotnet run -- discover --config client.json
```

Useful for debugging parameter discovery or seeing what IDs are available.

### validate

Validates endpoints against their models:

```bash
# All configured endpoints
dotnet run -- validate --config client.json

# Specific endpoint
dotnet run -- validate --endpoint Stats_GetMatchStats

# With JSON report output
dotnet run -- validate --output report.json
```

### update-snapshots

Refreshes XMLDocsExamples with live responses:

```bash
# All endpoints
dotnet run -- update-snapshots --config client.json

# Specific endpoint
dotnet run -- update-snapshots --endpoint Stats_GetMatchHistory

# Without sanitizing XUIDs
dotnet run -- update-snapshots --no-sanitize
```

By default, XUIDs are replaced with `xuid(PLAYER_XUID_HERE)` for privacy.

### validate-json

Validates a local JSON file against a model (no authentication needed):

```bash
dotnet run -- validate-json --model MatchStats --input captured-response.json
```

Useful for validating responses captured from other tools (like Zeta's API recording).

## Configuration Files

### client.json

Azure AD credentials for authentication:

```json
{
  "client_id": "your-azure-client-id",
  "client_secret": "your-azure-client-secret",
  "redirect_url": "https://localhost"
}
```

### endpoint-test-config.json

Discovery chain and validation targets. See `Config/endpoint-test-config.json` for the full example.

## Extending the Discovery Chain

To add a new parameter source:

1. Add a discovery step that calls an endpoint providing the data:
   ```json
   {
     "step": 3,
     "endpointId": "UgcDiscovery_Search",
     "method": "UgcDiscovery.Search",
     "args": { "start": 0, "count": 5 },
     "extractors": {
       "assetId": "$.Results[0].AssetId",
       "versionId": "$.Results[0].VersionId"
     }
   }
   ```

2. Use the new parameters in validation targets:
   ```json
   {
     "endpointId": "HIUGC_GetAsset",
     "method": "Ugc.GetAsset",
     "args": { "assetId": "$assetId" },
     "expectedModel": "AuthoringAsset"
   }
   ```

## Architecture

```
┌────────────────────┐
│  AuthenticationManager  │ ← Reuses Zeta's OAuth/Spartan flow
└──────────┬─────────┘
           │
           ▼
┌────────────────────┐
│  ParameterRegistry │ ← Stores discovered values, resolves $refs
└──────────┬─────────┘
           │
           ▼
┌────────────────────┐
│  ParameterDiscovery │ ← Runs discovery chain, extracts values
└──────────┬─────────┘
           │
           ▼
┌────────────────────┐
│  EndpointExecutor  │ ← Reflection-based method invocation
└──────────┬─────────┘
           │
           ▼
┌────────────────────┐
│  ResponseValidator │ ← JSON ↔ C# model comparison
└──────────┬─────────┘
           │
           ▼
┌────────────────────┐
│  ReportGenerator   │ ← Console tables + JSON output
└────────────────────┘
```

## Limitations

- **Read-only validation** - Cannot test POST/PUT/DELETE endpoints safely
- **Player-specific data** - Some endpoints only return data for the authenticated player
- **Rate limits** - Running validation too frequently may hit API limits
- **Transient failures** - Network issues can cause false negatives
