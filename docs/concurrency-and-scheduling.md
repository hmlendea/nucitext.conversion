# Concurrency And Scheduling

## Execution Model

`NuciText.Conversion.NuciTextConverter.ToWindows1252` runs entirely on the calling thread. There are no tasks, awaits, worker threads, timers, queues, scheduled jobs, cancellation tokens or explicit locks in [Production source](../NuciText.Conversion/NuciTextConverter.cs).

Parallelism exists only when the host invokes the converter concurrently. The host controls concurrency limits and request cancellation outside this synchronous operation.

## Shared Instance State

- The result cache is a `ConcurrentDictionary<string, string>` and uses `TryGetValue` and `TryAdd`.
- Mapping dictionaries are private and only read after instance initialization.
- Strings are immutable; intermediate variables and `StringBuilder` instances are call-local.
- Static regex calls use fixed patterns and replacement strings, not mutable match evaluators.

These observations support concurrent sharing under the current no-map-mutation design. No dedicated concurrency test or load measurement verifies a broader throughput guarantee.

## Race Semantics And Resource Limits

Lookup and computation are not atomic together. Concurrent misses can duplicate all transformations. `TryAdd` prevents overwriting a completed entry, and losing insertion is ignored. See [Cache flow](./flows/cache-lifecycle.md) for the causal argument that equal inputs still produce equal outputs.

No fairness, deadline, bounded memory or exactly-once execution guarantee exists. A long-lived shared instance retains every distinct nonblank original input and result. Creating per-call instances avoids cross-call retention but loses cache reuse and reallocates maps; this trade-off is consumer-owned and unbenchmarked.

If adding mutable mappings, expiry or asynchronous APIs, revisit synchronization, cache invalidation, cancellation semantics and [Invariants](./invariants.md) together.