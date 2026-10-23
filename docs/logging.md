# Logging

## Implemented Observability Inventory

Inspection of all [Production source](../NuciText.Conversion/NuciTextConverter.cs) and the [Project](../NuciText.Conversion/NuciText.Conversion.csproj) found no logging or telemetry integration.

| Concern | Current implementation |
|---|---|
| Framework and configuration source | None; no logger dependency or configuration reader |
| Levels | No Debug, Info, Warning, Error or Fatal emissions |
| Structured fields | No standard properties, timestamps, request IDs or user IDs |
| Correlation | No correlation, trace or span identifiers; no tracing instrumentation |
| Sinks | No console, file, database or remote log destination |
| Redaction/masking | No log content is emitted; no masking subsystem exists |
| Retention and rotation | No library logs to retain, rotate or archive |
| Sampling | No sampling policy or emission path |
| Performance | No logging overhead path; conversion/cache allocation costs still apply |
| Exception logging | Exceptions propagate without library logging or stack-trace capture |
| Audit logging | No separate security-event or audit stream |
| Runtime configuration | No level switch, reload mechanism or diagnostic options |

## Host Responsibilities

A host can instrument duration, input length, output length or failures around `NuciText.Conversion.INuciTextConverter.ToWindows1252`, but that is not an existing library feature. Avoid logging raw text by default: both original and converted strings can contain sensitive data. A shorter or transformed output is not necessarily anonymized.

If logging is added to the library, define fields, correlation ownership, sinks, masking, exception policy, retention and performance behavior before claiming them in documentation. Reassess the [Privacy policy](../PRIVACY.md), [Security policy](../SECURITY.md) and [State](./state-and-persistence.md).

## Build Output Is Separate

The [CI workflow](../.github/workflows/dotnet.yml) emits .NET restore/build/test output; NUnit emits test-run diagnostics. These are tooling logs, not conversion events or an application audit trail. No repository-defined log retention setting is present; external runner retention is outside the checked-in configuration.