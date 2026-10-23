# Repository Overview

## Purpose And Scope

The package supplies `NuciText.Conversion.INuciTextConverter.ToWindows1252` and its sealed implementation `NuciText.Conversion.NuciTextConverter`. A caller provides a string containing historical, transliterated, accented or visually similar characters; ordered substitutions produce a more suitable string for Windows-1252-oriented text. Many regression examples are place names. A production comment explicitly identifies one rule as Crusader Kings II-related; there is no game adapter or game runtime dependency.

The library owns substitution rules and memoisation. The host owns input acquisition, converter lifetime, byte encoding, validation, output persistence, permissions and diagnostics. There is no CLI, application startup method, controller, dependency-injection registration, database or scheduler.

## Entry Points And Dependants

- Public interface: [INuciTextConverter.cs](../NuciText.Conversion/INuciTextConverter.cs).
- Concrete constructor and implementation: [NuciTextConverter.cs](../NuciText.Conversion/NuciTextConverter.cs).
- Only in-repository caller: [NuciTextConverterTests.cs](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs), which instantiates the concrete class directly.
- External consumer applications are outside this repository; their behavior and deployment cannot be inferred from package metadata.

## Principal Capabilities

- Replace selected script lookalikes and transliteration characters.
- Compose selected combining sequences, remove selected marks and invisible characters, and apply contextual rules.
- Approximate selected characters with one or more output characters.
- Return an empty string for null or whitespace-only input.
- Reuse completed results for repeated, exactly equal original strings on the same instance.

These are finite rules, not comprehensive Unicode transliteration, Unicode normalization, sanitisation or actual byte encoding. See [Behaviour](./behaviour/text-conversion.md) and [Invariants](./invariants.md).

## System Boundaries

Input and output remain in the caller's process. No external work occurs during conversion. Two mapping dictionaries and one result cache belong to each instance. Calls are synchronous, and a cache miss performs all transformations before publishing the result. Package restore and CI have network interactions, but those are build-time boundaries, not conversion-time dependencies.

The [Root architecture](../ARCHITECTURE.md) gives the structural view; the [Conversion flow](./flows/text-conversion.md) supplies execution details and qualifications.