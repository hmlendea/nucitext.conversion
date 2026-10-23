# Frequently Asked Questions

## Does The Method Return Windows-1252 Bytes?

No. `NuciText.Conversion.INuciTextConverter.ToWindows1252` returns a .NET string. A host must separately encode it and choose fallback behavior. See [API reference](./api-reference/text-converter.md).

## Is Every Output Guaranteed To Be Encodable?

No. Unmapped characters survive and there is no final encoding check. Current tests cover examples and one output-set sample. See [Behaviour](./behaviour/text-conversion.md) and [Testing](./testing.md).

## Is This ASCII Transliteration Or Unicode Normalization?

Neither. Some accented Latin characters are preserved, some sequences are composed, and selected marks are removed. There is no general normalization pass. See [Flow](./flows/text-conversion.md).

## Is Conversion Idempotent Or Reversible?

Neither property is guaranteed. Later stages can introduce characters whose mapping stage has passed, and lossy substitutions merge distinct originals. See [Invariants](./invariants.md).

## Can One Instance Be Shared?

Current code uses a concurrent result cache, read-only-by-usage maps and call-local intermediates. Concurrent misses can duplicate work; long-lived sharing also retains every distinct original/result pair. There are no dedicated concurrency tests. See [Concurrency](./concurrency-and-scheduling.md).

## Can I Clear Or Limit The Cache?

Not through the public API. Instance lifetime is the available host control; garbage collection does not promise immediate secure deletion. See [State](./state-and-persistence.md).

## Does The Library Collect Logs Or Call External Services?

No such runtime paths exist. Package restore and CI are separate tooling integrations. See [Logging](./logging.md) and [Integrations](./integrations.md).

## Where Are The Controllers And Startup Code?

There are none. This is a library, constructed and invoked by its consumer. The [API index](./api-reference/INDEX.md) explicitly records the absence of HTTP endpoints.

## Where Should A New Rule And Test Go?

Use the [Change guide](./change-guide.md), [Converter component](./components/text-converter.md) and [Test component](./components/unit-tests.md). Stage selection matters as much as the replacement text.

## Why Do Root Documents And These Pages Disagree About Caching?

The root privacy statement denies cache storage, but source retains originals/results. This knowledge base follows observed behavior and records the contradiction in [Open questions](./ambiguities-and-open-questions.md).