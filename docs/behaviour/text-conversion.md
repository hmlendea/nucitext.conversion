# Text Conversion Behaviour

## Observable Outcomes

`NuciText.Conversion.INuciTextConverter.ToWindows1252` approximates a finite set of Unicode characters and sequences. Existing accented Latin letters can remain; this is not ASCII transliteration. Substitutions are case-specific and may expand one input character to several output characters.

Examples established by the [Regression tests](../../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs):

| Input written with escapes where useful | Output | Meaning |
|---|---|---|
| `\u010Cesk\u00FD Krumlov` | `Chesk\u00FD Krumlov` | Caron C expands, accented y remains |
| `Sveti \u0110or\u0111e` | `Sveti \u00D0ord\u017Ee` | Uppercase D-stroke and lowercase d-stroke have different approximations |
| `Mosco\u01BF` | `Moscouu` | Wynn expands to two letters |
| `Test \u0278l\u0101ryo\u0278` | `Test Pl\u00E3ryop` | Phi-like symbol becomes uppercase P at a regex word boundary, otherwise lowercase p |
| `\u1E24am\u0101\u1E97` | `Ham\u00E3h` | The special macron-a suffix path precedes target mapping |
| `Br\u0325\u0304sels` | `Brusels` | Ordered combining-sequence reduction |

## Blankness And Spacing

The public method returns empty for null, empty or any whitespace-only input. Nonblank text is not trimmed and internal whitespace is not generally collapsed. A nonblank string made only of removed marks or zero-width characters can also become empty, but unlike blank input it goes through the pipeline and is cached.

## Context And Ordering

- Exact `i\u012B\u1E97` is replaced before `\u012B\u1E97`; reversing them would change the longer pattern's result.
- The special `\u0101\u1E97` suffix becomes `\u0101h` before macron-a is mapped.
- U+0120 and U+0121 use a following-character condition to produce `Gh`/`gh`; existing `h` sequences have separate rules. Terminal occurrences have no matching contextual rule.
- U+140B has separate space-prefixed and preceding-non-space rules; a bare leading occurrence is not universally handled.
- Selected apostrophe-like marks become acute accent, backtick or apostrophe according to distinct source rules; they are not one unified punctuation class.
- En/em dashes become ASCII hyphens, middle dot is removed, and selected joiners/invisible characters are deleted.

See the exact [Flow](../flows/text-conversion.md) before changing rule order.

## Unsupported Input And Non-Guarantees

The following conclusions come from the [Implementation](../../NuciText.Conversion/NuciTextConverter.cs), not dedicated executed regression cases:
- Unmapped Han characters and most emoji pass through unchanged.
- A terminal U+0120 can remain outside Windows-1252 because its regex requires a following non-`h` character.
- `a\u030D` first becomes U+0227 after the common-map pass. It survives the first call, but a second conversion maps U+0227 to U+00E1. Thus conversion is not globally idempotent.
- `v\u0303` can become U+1E7D, which is not in the target map; the combining-mark removal stage no longer sees a separate tilde.
- Decomposed and precomposed spellings can differ: only selected compositions exist, and no general Unicode normalization call runs.

Do not infer encodability, collision resistance, reversibility, spoofing protection or linguistic correctness from a successful return. See [Invariants](../invariants.md) and [Security](../security.md).