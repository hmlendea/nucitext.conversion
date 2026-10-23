# Data Model

## Representations

There are no domain entities, DTOs, database schemas, serializers or identifiers. The complete production data model in [NuciTextConverter.cs](../NuciText.Conversion/NuciTextConverter.cs) is:

| Data | Representation | Meaning |
|---|---|---|
| Caller input | .NET `string` | Original Unicode UTF-16 code units; unchanged as cache key |
| Common rules | `Dictionary<char, string>` | One UTF-16 code unit to replacement sequence |
| Target rules | `Dictionary<char, string>` | Further selected approximations |
| Intermediate text | Local immutable strings | Output of each ordered transformation |
| Builder | Local `StringBuilder` | Accumulates one map pass; may grow beyond input length |
| Cached result | `ConcurrentDictionary<string, string>` entry | Original input paired with completed output |
| Return value | .NET `string` | Final Unicode text, not bytes or a validated encoding object |

## Relationships And Transformations

Many originals can produce the same output; there is no uniqueness constraint on result values. Cache identity is exact original string equality, not visual equality or normalized Unicode equivalence. A map can produce multiple letters, accented characters or apostrophe-bearing sequences. A later stage may map or delete part of an earlier result.

The table helper iterates `char`, not `Rune` or grapheme clusters. A supplementary scalar spans two iterations unless a later full-string/regex rule recognizes its sequence. Unrecognized code units are preserved, including potentially malformed surrogate sequences. No general normalization or validity check is performed.

## Lifecycle And Persistence

Inputs and results exist in process memory; cached pairs outlive a call for as long as the instance remains reachable. No disk format, migration, database transaction or backup exists. See [State](./state-and-persistence.md), [Flow](./flows/text-conversion.md) and [Security](./security.md).

Tests define their own sample strings and allowlist in [Test source](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs); these are verification fixtures, not additional production models.