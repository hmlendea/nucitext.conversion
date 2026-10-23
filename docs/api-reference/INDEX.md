# API Reference Index

## Public Library Surface

| Symbol | Entry | Result | Authentication |
|---|---|---|---|
| `NuciText.Conversion.NuciTextConverter.NuciTextConverter()` | Parameterless constructor | New instance with private maps and empty cache | None in library |
| `NuciText.Conversion.INuciTextConverter.ToWindows1252(string)` | Synchronous instance call | Transformed Unicode string | None in library |
| `NuciText.Conversion.NuciTextConverter.ToWindows1252(string)` | Sealed implementation of interface operation | Same method contract | None in library |

See the [Converter reference](./text-converter.md) and [Usage examples](../api-usage-examples.md).

## HTTP Endpoint Inventory

| Method | Path | Controller | Action | Auth |
|---|---|---|---|---|
| Not applicable | No HTTP routes | No controllers | No HTTP actions | No HTTP schemes |

There are no controller files to document. Requests, response statuses, headers, JSON schemas, API version negotiation and HTTP error payloads are not part of this library. No rate limiting, throttling or global authentication scheme is implemented. The package version is `1.0.0` in the [Project](../../NuciText.Conversion/NuciText.Conversion.csproj); a version-shaped value alone does not establish a compatibility policy.

HTTP hosts must define their own transport contract; do not interpret empty output as an HTTP status or an exception as a library-defined HTTP response.

## Related Evidence

- [Interface source](../../NuciText.Conversion/INuciTextConverter.cs).
- [Implementation](../../NuciText.Conversion/NuciTextConverter.cs).
- [Execution flow](../flows/text-conversion.md).
- [Component](../components/text-converter.md).
- [Data model](../data-model.md).