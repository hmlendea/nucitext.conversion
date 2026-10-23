# Text Converter Component

## Ownership And Invocation

`NuciText.Conversion.NuciTextConverter` owns all production transformation logic and instance state in [NuciTextConverter.cs](../../NuciText.Conversion/NuciTextConverter.cs). Its [Interface](../../NuciText.Conversion/INuciTextConverter.cs) is the consumer contract, but there is only one implementation and no internal composition framework. The [Tests](../../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) construct the concrete type directly.

The component does not own decoding, byte encoding, file I/O, logging, configuration loading or validation of a complete output character set.

## Internal Responsibilities

| Symbol | Input and output | Responsibility |
|---|---|---|
| `NuciText.Conversion.NuciTextConverter.ToWindows1252` | Original string to final string | Blank guard, cache lookup, ordered pipeline, cache publication |
| `NuciText.Conversion.NuciTextConverter.ApplyCommonReplacements` | Intermediate string to intermediate string | Shared mappings, sequence substitutions, selected diacritic composition/removal and punctuation cleanup |
| `NuciText.Conversion.NuciTextConverter.ReplaceUsingMap` | String plus `Dictionary<char, string>` to string | Single UTF-16-code-unit scan, expanding known keys and preserving unknown keys |
| `NuciText.Conversion.NuciTextConverter.CommonCharacterMappings` | `char` to string entries | Script lookalikes, digraphs, detached apostrophes and reduced secondary accents |
| `NuciText.Conversion.NuciTextConverter.Windows1252CharacterMappings` | `char` to string entries | Target-oriented approximation of selected remaining characters |
| `NuciText.Conversion.NuciTextConverter.windows1252cache` | Original string to final string entries | Memoisation for instance lifetime |

## Mapping Algorithm

`NuciText.Conversion.NuciTextConverter.ReplaceUsingMap` returns null for a null input, although the public blank guard prevents that path in normal execution. Otherwise it creates a local `StringBuilder` with initial capacity equal to input length, iterates `char` values, and appends either the mapped string or the original code unit. Expansion is allowed, so capacity is not an output bound.

A replacement is appended, not recursively looked up in that same map pass. For example, a common-map replacement may deliberately remain outside Windows-1252 until the later target-map pass. Supplementary Unicode characters are surrogate pairs; table keys cannot represent an entire supplementary scalar, though literal regex/string rules can match selected pairs.

Dictionary iteration order is irrelevant because code iterates input, not dictionary entries. By contrast, the sequence of `.Replace` and `Regex.Replace` calls is fundamental. See [Execution flow](../flows/text-conversion.md).

## Important Local Contracts

- Initial suffix replacements must see original text before the common diacritic removal stage.
- Common maps execute before selected combining-sequence construction; newly constructed characters do not get a second common-map pass.
- Target mapping consumes common-stage results, allowing multi-stage reductions.
- Final rules see already-mapped and already-cleaned text, not original combining sequences.
- Results are cached only after the entire pipeline completes.

## Dependencies And State

The component uses framework strings, `Dictionary`, `ConcurrentDictionary`, `StringBuilder` and static `Regex.Replace`. Maps are allocated per instance and private; `readonly` prevents field reassignment but does not make their contents immutable. Conversion only reads them. All intermediate strings and builders are call-local.

No configuration changes the rules. No exception is caught or logged. No cache entry is published if a preceding transformation throws. Complexity scales with text length across many passes; intermediate allocations and unbounded cached originals/results matter more than the small public API suggests. No benchmark quantifies cost.

For edits, review [Change guide](../change-guide.md), [Invariants](../invariants.md), [Testing](../testing.md) and [Cache lifecycle](../flows/cache-lifecycle.md).