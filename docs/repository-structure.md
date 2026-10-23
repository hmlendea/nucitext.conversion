# Repository Structure

| Location | Meaning and navigation |
|---|---|
| [Solution](../NuciText.Conversion.sln) | Builds production and unit-test projects; Debug/Release and Any CPU/x64/x86 solution choices map projects to Any CPU |
| [Production project](../NuciText.Conversion/NuciText.Conversion.csproj) | `net10.0`, package metadata and version `1.0.0` |
| [Interface](../NuciText.Conversion/INuciTextConverter.cs) | `NuciText.Conversion.INuciTextConverter.ToWindows1252` |
| [Implementation](../NuciText.Conversion/NuciTextConverter.cs) | `NuciText.Conversion.NuciTextConverter`, both mapping tables, common replacements, final replacements and cache |
| [Test project](../NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj) | Test-only packages and production project reference; non-packable |
| [Test source](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) | Fixture is named `NuciText.Conversion.UnitTests.NameNormaliserTests`, despite the filename |
| [CI workflow](../.github/workflows/dotnet.yml) | Restore/build/test on pushes and pull requests to `master` |
| [Funding configuration](../.github/FUNDING.yml) | Repository funding metadata, not a runtime integration |
| [Ignore rules](../.gitignore) | Generated build and editor artefacts |
| [Root architecture](../ARCHITECTURE.md) | Structural documentation |
| [Documentation index](./INDEX.md) | Detailed behaviour, flow, operational and maintenance navigation |

The `bin` and `obj` directories visible after a build are generated output, not authoritative rules or configuration. There are no controllers, migrations, environment configuration files, container definitions, deployment manifests, SDK pinning file or release workflow in the inspected repository inventory.

When looking for a conversion rule, begin with the [Implementation](../NuciText.Conversion/NuciTextConverter.cs), not generated assembly metadata. Use the [Change guide](./change-guide.md) to choose its stage.