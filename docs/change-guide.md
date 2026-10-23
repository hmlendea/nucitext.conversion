# Change Guide

## Change A Character Or Sequence Rule

1. Locate the exact code point or sequence in [Implementation](../NuciText.Conversion/NuciTextConverter.cs), distinguishing source literals from lookalike glyphs.
2. Trace its earlier and later stages using the [Flow](./flows/text-conversion.md). Decide whether it is a one-code-unit mapping, contextual regex or sequence composition.
3. Check whether a replacement creates a character consumed by a later stage or misses an earlier stage. Do not assume map output is recursively converted.
4. Inspect the [Existing tests](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) for both exact expectations and related cases.
5. Add focused expected-output cases, including nonmatching contexts, terminal/boundary positions and combining/precomposed forms where relevant.
6. Run the focused fixture, then the full suite as described in [Testing](./testing.md).
7. Update [Behaviour](./behaviour/text-conversion.md), [Invariants](./invariants.md) and any affected [Examples](./api-usage-examples.md).

Do not reorder large groups to fix one case without comparing neighboring rules. Changing an approximation is observable API behavior even when the method signature stays unchanged.

## Change Caching Or Instance Lifetime

Preserve original-input-to-completed-output semantics or explicitly document the break. Add tests for blank input, repeated calls, separate instances, concurrent misses and exception-before-publication behavior when practical. If rules become mutable, design invalidation with synchronization; current race reasoning assumes fixed maps.

Review [State](./state-and-persistence.md), [Concurrency](./concurrency-and-scheduling.md), [Logging](./logging.md), the [Privacy policy](../PRIVACY.md) and [Security engineering](./security.md). Retaining less output does not remove original-key sensitivity.

## Change The Public API Or Framework

Update the [Interface](../NuciText.Conversion/INuciTextConverter.cs), implementation, tests and [API reference](./api-reference/text-converter.md) together. The class is sealed, so a new implementation belongs behind the interface rather than subclassing.

Framework changes require coordination of both [Production](../NuciText.Conversion/NuciText.Conversion.csproj) and [Test](../NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj) projects and the [CI SDK selector](../.github/workflows/dotnet.yml). Do not infer a release policy from the current version string; publishing automation is absent.

## Strengthen Encoding Guarantees

First resolve the desired contract with maintainers. Unknown-character deletion, replacement or exceptions each change behavior. Add real strict-encoder tests and define provider ownership, invalid Unicode treatment and failure semantics before claiming universal representability. Avoid silently turning a string-transformation helper into an application-specific encoder.

## Documentation Impact

Use [Documentation maintenance](./documentation-maintenance.md). Root architecture and policies are separate canonical documents; revise their affected sections with applicable templates rather than copying them under this directory.