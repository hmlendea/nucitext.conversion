# Text Converter Reference

## Construction

`NuciText.Conversion.NuciTextConverter` is public and sealed, implements `NuciText.Conversion.INuciTextConverter`, and exposes a parameterless constructor. Instance field initializers allocate both mapping dictionaries; the constructor allocates `ConcurrentDictionary<string, string>` for results. There are no options, injected dependencies, disposal hooks or public cache operations.

Sources: [Interface](../../NuciText.Conversion/INuciTextConverter.cs), [Implementation](../../NuciText.Conversion/NuciTextConverter.cs).

## ToWindows1252

Signature: `string NuciText.Conversion.INuciTextConverter.ToWindows1252(string text)`; implementation: `NuciText.Conversion.NuciTextConverter.ToWindows1252`.

| Input | Behavior |
|---|---|
| `null`, empty or `string.IsNullOrWhiteSpace(text)` | Returns `string.Empty` immediately; does not cache |
| Previously cached original string | Returns stored final result; no transformations |
| Other string | Applies ordered replacements, attempts to cache original-to-final pair, returns final string |

Parameter `text` has no explicit length constraint, allowed-character validation, normalization requirement or culture argument. Null is accepted at runtime despite the plain `string` signature; nullable annotations are not enabled in the production project.

The method is synchronous. It has no cancellation token, async counterpart, streaming overload, encoding argument or result metadata. No authentication or authorisation is performed. The only persistent-in-instance side effect is adding a successful nonblank input/result cache entry.

## Output And Failure Contract

- Output is a .NET Unicode string, not an encoded byte array.
- Unknown characters may remain; Windows-1252 encodability is not validated.
- Substitutions can expand, delete or change case; the conversion is lossy and not reversible.
- A blank result can mean blank input or removal of all characters; no status distinguishes them.
- No custom exception, result envelope, retry or fallback boundary exists. Runtime exceptions propagate.
- Repeating the same original input is deterministic with unchanged rules; converting the output again is not guaranteed to be idempotent.

See [Behaviour](../behaviour/text-conversion.md), [Flow](../flows/text-conversion.md) and [Errors](../error-handling.md).

## Sharing And Lifetime

Concurrent calls use a concurrent cache and local intermediate strings/builders; mapping tables are not mutated by conversion. Two misses for the same input may compute concurrently. Cache growth is unbounded and retains originals, so choose lifetime based on input cardinality and sensitivity. See [Concurrency](../concurrency-and-scheduling.md).

## Tests

The [Test source](../../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) exercises the concrete implementation, not an alternative interface implementation. It verifies 129 exact-output examples and one output-set sample; it does not directly verify null, whitespace, cache hits or concurrent callers. See [Testing](../testing.md).