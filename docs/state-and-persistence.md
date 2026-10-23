# State And Persistence

## State Inventory

All production state is instance-local in [NuciTextConverter.cs](../NuciText.Conversion/NuciTextConverter.cs):
- `NuciText.Conversion.NuciTextConverter.CommonCharacterMappings`: initialized rule dictionary, read during calls.
- `NuciText.Conversion.NuciTextConverter.Windows1252CharacterMappings`: initialized target dictionary, read during calls.
- `NuciText.Conversion.NuciTextConverter.windows1252cache`: concurrent dictionary, extended by successful nonblank conversions.

There is no shared static application cache, database, file store, migration, session, transaction or distributed consistency boundary. Regex/framework internals are not application-owned persistence.

## Retention Semantics

The result cache holds exact original strings and completed results for the instance lifetime. There is no eviction, expiry, capacity, clear/reset API or disposal implementation. Blank inputs bypass caching; nonblank inputs that become empty are still cached. Distinct originals with equal outputs remain distinct keys.

Dropping all references permits eventual garbage collection, not immediate deletion or zeroing. Strings may remain referenced by the caller independently. Memory retention must therefore be considered separately from disk persistence and external transfer.

## Consistency And Failure

Entries are published only after conversion. Concurrent misses can compute twice, but only one value is added per equal key. Under fixed rules both computations agree. Exceptions before publication leave no entry; no failure cache exists. See [Cache flow](./flows/cache-lifecycle.md).

## Documentation Caveat

The [Root privacy policy](../PRIVACY.md) says no data is stored in caches. That is contradicted by the implementation. The [Root architecture](../ARCHITECTURE.md) does acknowledge the cache. This knowledge base records the discrepancy rather than treating policy prose as implementation evidence. See [Open questions](./ambiguities-and-open-questions.md).

Consumers own lifecycle and any persistence after conversion. No public mechanism can erase one cached input selectively. A cache-management feature would affect concurrency, API compatibility, tests and privacy documentation together.