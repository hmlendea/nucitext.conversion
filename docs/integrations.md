# Integrations

## Runtime Host Integration

A consuming .NET application constructs `NuciText.Conversion.NuciTextConverter`, optionally holds it as `NuciText.Conversion.INuciTextConverter`, and calls the synchronous method. There is no registration extension, service container dependency, application host or lifecycle callback. See [API](./api-reference/text-converter.md).

Only strings cross the runtime boundary. The library performs no HTTP, database, filesystem, game-engine, telemetry or external-service call. The Crusader Kings II source comment identifies a replacement rule's context, not an implemented game integration.

## Build And Distribution Boundaries

- NuGet restore obtains test/tooling packages according to the consuming machine's package-source configuration.
- The [README](../README.md) advertises a NuGet package; the [Production project](../NuciText.Conversion/NuciText.Conversion.csproj) supplies packaging metadata.
- [GitHub Actions](../.github/workflows/dotnet.yml) validates changes using restore/build/test; it has no publication step.
- [Funding metadata](../.github/FUNDING.yml) affects repository funding presentation, not conversion execution.

This review did not query the registry or inspect an external application's composition. No claims about published package contents, deployment environments or downstream consumers follow from local source inspection.

## Host-Owned Encoding

Windows-1252 byte conversion is a separate integration. The package does not call an encoder, select fallback behavior or register code-page providers. Hosts must determine provider availability and validate output if their protocol requires strict representability. See [Usage](./api-usage-examples.md), [Errors](./error-handling.md) and [Security](./security.md).