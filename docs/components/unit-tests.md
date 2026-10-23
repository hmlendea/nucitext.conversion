# Unit-Test Component

## Purpose And Scope

The [Test project](../../NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj) verifies observable string conversions through the production public method. It references the [Library project](../../NuciText.Conversion/NuciText.Conversion.csproj), not a packaged binary, and is marked non-packable.

The sole [Test source](../../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) defines `NuciText.Conversion.UnitTests.NameNormaliserTests`. The fixture name does not match the filename; search either name when navigating tooling output.

## Execution And Data

`NuciText.Conversion.UnitTests.NameNormaliserTests.SetUp` creates a fresh `NuciText.Conversion.NuciTextConverter` before every case. Therefore cache entries are not shared across cases.

`NuciText.Conversion.UnitTests.NameNormaliserTests.WhenNormalisingForWindow1252_ReturnsTheExpectedNormalisedName` receives 129 input/expected pairs and asserts exact string equality. Examples cover digraph expansion, selected script substitutions, joiner removal, combining marks, suffixes and preserved Latin characters.

`NuciText.Conversion.UnitTests.NameNormaliserTests.WhenNormalisingForWindow1252_ReturnsTheNameWithoutCharsOutsideCharset` converts one `StringOfVariousCharacters` sample. Its helper, `NuciText.Conversion.UnitTests.NameNormaliserTests.TestCharsNotOutsideSet`, concatenates characters absent from `Windows1252Characters` plus ASCII space, treats a whitespace-only mismatch string as empty, then asserts emptiness.

This is a hand-written membership test, not a strict byte encoder test. Because the helper applies `string.IsNullOrWhiteSpace` to mismatches, unexpected whitespace characters alone would not fail it. The fixture's allowed-character constant is also not a complete machine-checked encoding specification.

## Dependencies And Limits

NUnit supplies setup, parameterisation and assertions. NUnit3TestAdapter provides discovery for Microsoft.NET.Test.Sdk. LINQ supports the character-set helper. Moq is referenced but unused in this source.

No test doubles, external resources, persistence, concurrency orchestration or host integration exist. The fixture establishes examples, not universal algorithmic properties. [Testing](../testing.md) lists gaps and the observed run; [Change guide](../change-guide.md) identifies targeted tests needed for rule edits.