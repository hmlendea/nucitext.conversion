# Cache Lifecycle Flow

Source: [NuciTextConverter.cs](../../NuciText.Conversion/NuciTextConverter.cs), field `NuciText.Conversion.NuciTextConverter.windows1252cache` and method `NuciText.Conversion.NuciTextConverter.ToWindows1252`.

## Construction To Collection

1. Consumer constructs a converter; both private maps and an empty concurrent result dictionary are allocated.
2. Blank input returns immediately, adding no entry.
3. A nonblank original input is looked up using the dictionary's default string equality: case-sensitive ordinal equality, not culture-aware or Unicode-normalized equivalence.
4. A hit returns the final stored result.
5. A miss computes all replacement stages locally.
6. Successful completion attempts `TryAdd(original, final)` and returns the locally computed final result.
7. Entries remain reachable while the converter is reachable; there is no expiry, maximum count, clear operation or disposal API.
8. Once the instance and any other references are unreachable, ordinary garbage collection may reclaim its state. There is no prompt erasure guarantee.

## Concurrent Misses

Two callers can both miss the same key, both perform the full pipeline and both attempt insertion. One insertion succeeds; the other returns false and is ignored. Each caller still returns its own computed output. Unchanged rule tables make those outputs equal, so duplicate work does not alter semantics.

This is not a lock spanning lookup and computation and not an exactly-once promise. A future mutable-rule feature would invalidate the equal-result reasoning unless cache invalidation and synchronization were designed together.

## Retention And Failure

Original strings are retained as keys even when transformation removes sensitive or invisible content. Final strings are retained as values. Multiple originals yielding the same visible output still create distinct entries. Inputs transforming to empty are cached if the original was nonblank.

An exception before insertion does not cache a result or failure. A later call retries computation naturally because the key is absent, not because the library has retry logic.

See [State](../state-and-persistence.md), [Concurrency](../concurrency-and-scheduling.md), [Privacy discrepancy](../ambiguities-and-open-questions.md) and [Change guide](../change-guide.md). Dedicated cache tests are absent from the current [Suite](../../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs).