# Architecture Reading Guide

The canonical structural account is the [Root architecture](../ARCHITECTURE.md). This page does not reproduce its component diagrams or ownership tables. It provides the implementation-reading order and qualifications needed to apply that account safely.

## Read Across The Boundaries

1. Read the [Public method](./api-reference/text-converter.md): its name describes a target, but its return type is a Unicode string, not bytes.
2. Follow the [Conversion flow](./flows/text-conversion.md): each stage consumes the preceding stage's output, and ordering is observable.
3. Follow the [Cache flow](./flows/cache-lifecycle.md): original input is retained alongside the result; concurrency safety does not imply exactly-once computation.
4. Compare the [Test inventory](./testing.md): selected examples establish exact outputs, not universal representability.
5. Review [Open questions](./ambiguities-and-open-questions.md): root prose has stronger claims than the implementation and test suite support.

## Architectural Consequences Of Local Details

- `NuciText.Conversion.NuciTextConverter.ReplaceUsingMap` preserves unknown UTF-16 code units. Consequently a successful method return is not proof that strict Windows-1252 byte encoding will succeed.
- `NuciText.Conversion.NuciTextConverter.ApplyCommonReplacements` is a single ordered pass, not a fixed-point normalization algorithm. A character introduced after its mapping stage can survive and change on a later invocation.
- The cache key is the original input, not an intermediate or final string. Equivalent-looking inputs occupy different entries, while a cache hit bypasses every replacement stage.
- The private dictionaries are read-only by usage, not immutable collection types. Their safe concurrent use depends on preserving the no-mutation convention.

These details explain why apparently local rule edits can affect compatibility, memory, privacy and host-side encoding. The [Invariants](./invariants.md) and [Change guide](./change-guide.md) are the maintenance contracts for those interactions.