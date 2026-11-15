# Invariants And Contracts

## Established By Implementation

| Contract | Evidence and modification impact |
|---|---|
| Public call is synchronous and returns a string | [Interface](../NuciText.Conversion/INuciTextConverter.cs); changing shape affects every consumer |
| Null/blank input becomes empty before caching | [Implementation](../NuciText.Conversion/NuciTextConverter.cs), public guard; currently not directly tested |
| Cache keys are original inputs and values are completed outputs | Same source; preserve this relationship when changing pipeline/cache code |
| Rule order is observable | [Flow](./flows/text-conversion.md); reordering can break suffix and combining-sequence behavior |
| Map passes preserve unknown code units | `NuciText.Conversion.NuciTextConverter.ReplaceUsingMap`; deleting unknowns would change compatibility |
| Final fallback replaces unsupported characters with `?` | `NuciText.Conversion.NuciTextConverter.ReplaceUnsupportedCharacters`; output is always Windows-1252 representable but lossy and unmarked |
| One map invocation does not remap its own appended replacement | Same helper; recursive replacement would change semantics and termination analysis |
| Maps are not mutated during calls | Private initialization and read-only usage; concurrent-sharing reasoning relies on this |
| No runtime network, storage or logging work occurs | Complete production source; adding these changes privacy and operational boundaries |

## Established By Current Tests

The [Test fixture](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) establishes 136 exact conversions plus five fallback cases for unmapped Han and supplementary characters, and membership in a hand-written set for one mixed-character sample. See [Testing](./testing.md) for the assertion's whitespace caveat. Preserve these expected outputs unless an intentional behavior change is reviewed.

## Not Established

- Lossless handling of unsupported characters: they become `?` with no report.
- Global idempotence: selected compositions introduce characters whose earlier map stage has passed.
- Unicode canonical equivalence: there is no general normalization.
- Reversibility, unique output or preserved input length: mappings expand, delete and merge inputs.
- Preservation of all Windows-1252 characters: dashes, middle dot and other explicitly handled characters can change.
- Exactly-once conversion, bounded memory, secure erasure or fixed latency.
- Complete transliteration or linguistic correctness for any supported language.

Do not convert these non-guarantees into documentation promises without implementation, tests and a compatibility decision. [Open questions](./ambiguities-and-open-questions.md) separates desired behavior from observed behavior.