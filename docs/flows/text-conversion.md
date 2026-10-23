# Text Conversion Execution Flow

All steps occur synchronously in [NuciTextConverter.cs](../../NuciText.Conversion/NuciTextConverter.cs). The entry is `NuciText.Conversion.NuciTextConverter.ToWindows1252`; the only in-repository caller is the [Test fixture](../../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs).

## Entry And Alternate Paths

1. `string.IsNullOrWhiteSpace(text)` returns empty before any lookup or write. This both accepts null and prevents null keys reaching the concurrent dictionary.
2. `windows1252cache.TryGetValue(text, out value)` returns the cached final string on a hit. No intermediate stages run.
3. A miss keeps the original input unchanged as the future key and transforms a separate local `processedName` variable.

## Miss Pipeline

| Order | Operation | Why this boundary matters |
|---|---|---|
| 1 | Exact `i\u012B\u1E97` to `iyyah`, then `\u012B\u1E97` to `iyah` | Longer original suffix must win before the shorter one |
| 2 | `NuciText.Conversion.NuciTextConverter.ApplyCommonReplacements` | Resolves script variants, sequences and marks before target mapping |
| 3 | Exact `\u0101\u1E97` to `\u0101h` | CKII-labelled suffix rule needs macron-a still present |
| 4 | `NuciText.Conversion.NuciTextConverter.ReplaceUsingMap` with target map | Approximates common-stage results and original remaining characters |
| 5 | Regex `[\u0120]([^h])` to `Gh$1` | Keeps the following character, but requires one and excludes lowercase h |
| 6 | Regex `[a]*[\u1E97]` to `ah` | Consumes zero or more consecutive ASCII a characters followed by t-with-diaeresis |
| 7 | Regex `[\u0121]([^h])` to `gh$1` | Lowercase contextual counterpart; matching consumes the following character |
| 8 | Exact dotted-G-plus-h reductions, J-plus-caron, selected O-plus-mark and T-plus-diaeresis replacements | Last cleanup sees already-mapped output, so some original-sequence patterns can no longer match |
| 9 | `windows1252cache.TryAdd(text, processedName)` | Publishes original-to-final result without replacing an existing entry |
| 10 | Return local result | A failed `TryAdd` due to another writer does not fail conversion |

There is no final encoder check, output allowlist, Unicode normalization or recursive rerun. A return means the substitutions completed, not that an encoding constraint was proven.

## Common Replacement Subflow

`NuciText.Conversion.NuciTextConverter.ApplyCommonReplacements` executes these groups in order:
1. Regex word-boundary U+0278 to uppercase `P`.
2. Common character map; remaining U+0278 maps to lowercase `p`.
3. Uppercase literal sequences including selected joiners, H-plus-line-below, N-plus-line-below and R-plus-ring-below sequences. Longer R sequences precede shorter ones.
4. Uppercase regex groups for selected mathematical characters, joiners and script variants.
5. Lowercase literals, including U+1E21 to U+011F, Arabic U+06AD to U+0121, digraphs and selected combining sequences.
6. Lowercase regex groups including the non-space-prefixed U+140B case.
7. Selected floating vertical-line, acute, grave, diaeresis and tilde compositions, then a circumflex and an apostrophe-mark composition.
8. Removal of enumerated combining marks and selected Indic/invisible sequences.
9. Ordered quote-like mark rewrites, joiner removal, dash replacement, colon replacement, dot and other mark deletion, and selected invisible/control deletion.

The helper returns its intermediate string to the public method; it neither caches nor encodes. Replacement strings appended by a map are not reprocessed by that same map invocation. Later literal composition may create characters whose map stage has already passed. See [Component](../components/text-converter.md).

## Causal Example

The tested input `al-H\u0331\u0101n\u012B\u1E97` becomes `al-H\u0331\u0101niyah` in the initial suffix rule. Common sequence replacement turns `H\u0331` into `Kh`. Target mapping turns macron-a into U+00E3. Final output is `al-Kh\u00E3niyah`, matching the test's expected result.

If combining marks were deleted before sequence replacement, the `Kh` result would be lost. If target mapping ran before the initial suffix rule, macron-i would change before the suffix could match. This is why stage order is a compatibility contract.

## Failures And Dependants

Any runtime exception escapes immediately; later stages and cache publication do not run. There is no retry or partial result response. Output feeds the host or the exact-output/character-set assertions, not another production service in this repository. See [Errors](../error-handling.md), [Tests](../testing.md) and [Change guide](../change-guide.md).