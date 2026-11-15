# API Usage Examples

There is no request/response transport. These examples call `NuciText.Conversion.INuciTextConverter.ToWindows1252` directly; JSON and HTTP examples would invent an API that does not exist. See the [Reference](./api-reference/text-converter.md).

## Regression-Backed Calls

```csharp
NuciText.Conversion.INuciTextConverter converter =
    new NuciText.Conversion.NuciTextConverter();

string place = converter.ToWindows1252("\u010Cesk\u00FD Krumlov");
// "Chesk\u00FD Krumlov": capital C with caron expands to Ch.

string name = converter.ToWindows1252("Sveti \u0110or\u0111e");
// "Sveti \u00D0ord\u017Ee": uppercase and lowercase mappings differ.

string repeated = converter.ToWindows1252("\u010Cesk\u00FD Krumlov");
// Same result; this original input now has an instance cache entry.
```

The expected conversions appear in the [Tests](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs). The second call illustrates cache behavior established from [Implementation](../NuciText.Conversion/NuciTextConverter.cs), not a dedicated cache regression test.

## Source-Derived Boundary Examples

```csharp
string blank = converter.ToWindows1252(" \t\n");
// string.Empty: blank guard runs before cache lookup.

string unknown = converter.ToWindows1252("\u4E2D");
// "?": no rule maps this character, so the final fallback replaces it.

string terminal = converter.ToWindows1252("\u0120");
// "?": contextual G-with-dot rules need a following character.
```

These examples are reasoned from the rule tables and patterns, not cases in the current test suite. The fallback guarantees representability but silently discards information. See [Open questions](./ambiguities-and-open-questions.md) for missing regression coverage.

## Byte Encoding Boundary

After conversion, select a Windows-1252 encoder in the host if bytes are required. Provider availability depends on the consuming application; this package neither references a code-page provider package nor registers one. Output is already limited to that character set by the final `?` fallback, so strict and replacement fallbacks encode it identically. No byte-encoding example is claimed as an exercised repository integration.