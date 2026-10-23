# Testing

## Observed Verification

On 2026-10-23, SDK 10.0.112 executed `dotnet test --no-restore --verbosity minimal` successfully: 130 tests passed, zero failed and zero skipped. Both projects built. Existing restore assets were used; clean online restore, remote CI and package publication were not independently exercised.

The authoritative files are [Test source](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) and [Test project](../NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj). The fixture is `NuciText.Conversion.UnitTests.NameNormaliserTests`.

## Coverage Map

| Area | Current evidence | Limit |
|---|---|---|
| Exact conversions | 129 parameterized expected-output cases | Representative examples, not every rule or input |
| Contextual phi-like symbol | Beginning-of-word and internal symbol examples | Not exhaustive Unicode word-boundary cases |
| Suffixes and combining marks | Named examples for suffixes, R/H marks, joiners and accents | Not all normalization equivalents or stage interactions |
| Output character set | One mixed-character input and hand-written allowlist | Not a strict encoder and not universal input coverage |
| Cache | Source inspection only | Fixture constructs fresh converter before each case; no repeated-call assertion |
| Null/empty/whitespace | Public guard in source only | No dedicated cases |
| Concurrency/resource use | Implementation analysis only | No stress, allocation or timing tests |
| Host byte encoding | None | No code-page provider or strict-encoder integration test |

## Assertion Caveat

`NuciText.Conversion.UnitTests.NameNormaliserTests.TestCharsNotOutsideSet` filters output characters missing from the declared set plus ASCII space. If the mismatch string is entirely whitespace, it is changed to empty before assertion. Thus an output containing only unexpected whitespace mismatches can pass. The test constant is a sample allowlist, not a machine-verified complete Windows-1252 definition.

## Focused Commands

```bash
dotnet test NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj --no-restore --filter FullyQualifiedName~NameNormaliserTests
dotnet test --no-build --verbosity normal
```

The second command is the [CI test command](../.github/workflows/dotnet.yml). If discovery returns zero tests, check the test SDK and adapter before assuming verification succeeded.

## Tests Needed For Behavior Changes

Add exact before/after expectations and neighbouring nonmatching cases. Include stage interactions, repeated conversion, unknown characters, blankness, and combining forms when relevant. For cache changes, test repeated calls, separate instances and concurrent misses. A stronger encoding contract needs strict-encoder coverage, including unmapped Han text, supplementary characters and terminal contextual symbols.

No coverage collector, coverage threshold, integration suite, benchmark, security suite or architecture test is configured. See [Change guide](./change-guide.md) and [Open questions](./ambiguities-and-open-questions.md).