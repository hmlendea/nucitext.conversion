# Error Handling

## Implemented Paths

Source: `NuciText.Conversion.NuciTextConverter.ToWindows1252` and `NuciText.Conversion.NuciTextConverter.ReplaceUsingMap` in [Implementation](../NuciText.Conversion/NuciTextConverter.cs).

| Condition | Result | Recovery or side effect |
|---|---|---|
| Null, empty or whitespace-only public input | Empty string | No cache access, exception or warning |
| Cache miss | Full conversion | Not considered an error |
| Unknown character | Preserved unless another explicit rule matches | No unsupported-character report |
| Concurrent insertion loses to another caller | Local converted string still returned | `TryAdd` false is ignored |
| Private map helper receives null | Null | Not reachable from ordinary public flow after the blank guard |
| Runtime operation throws | Exception escapes | No translation, logging, retry or partial return |

The source has no custom exception types, catch blocks, error codes, result discriminant or error logging boundary. No remote service, filesystem or database errors can originate from an implemented conversion integration because none exists.

## Resource And Regex Considerations

Strings/builders/cache allocation can encounter runtime resource failures. Regex calls use fixed source patterns with no explicit timeout argument. Input length has no library limit. This review did not execute exhaustion or adversarial timing tests, so no concrete failure threshold or regex performance guarantee is claimed.

An exception before cache insertion leaves no completed result for that input. A future call may compute again; this is an absent cache entry, not a retry policy.

## Host Boundary

Conversion success does not validate Windows-1252 representability. A subsequent strict encoder can reject preserved output; that failure belongs to the host's encoding step, not a library-defined exception contract. Lossy conversion can also collapse distinct inputs without signaling an error.

Hosts should apply size limits and validation appropriate to their domain, choose encoding fallback explicitly, and protect diagnostics from sensitive text. See [Security](./security.md), [Logging](./logging.md) and [Troubleshooting](./troubleshooting.md).

Current [Tests](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) do not exercise exception propagation, null or whitespace branches.