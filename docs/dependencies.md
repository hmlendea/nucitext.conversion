# Dependencies

## Dependency Direction

The [Test project](../NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj) references the [Production project](../NuciText.Conversion/NuciText.Conversion.csproj). Production does not reference tests and declares no external NuGet packages. Both target .NET 10.

Production uses .NET framework libraries for collections, concurrent collections, strings, `StringBuilder` and regular expressions. There is no HTTP client, serializer, logging package, encoding-provider package, database driver or dependency-injection framework.

## Direct Test Packages

| Package | Declared version | Actual use |
|---|---|---|
| Microsoft.NET.Test.Sdk | 18.3.0 | Integrates test execution with .NET tooling |
| Moq | 4.20.72 | Referenced; no mock usage in current test source |
| NUnit | 4.5.0 | Test attributes, setup and assertions |
| NUnit3TestAdapter | 6.1.0 | NUnit discovery/execution adapter |

Versions above are direct declarations, not a complete transitive dependency inventory. Restore-generated asset files are build outputs, not a checked-in dependency lock. No locked restore policy was found.

## Tooling Dependencies

The [CI workflow](../.github/workflows/dotnet.yml) uses `actions/checkout@v2`, `actions/setup-dotnet@v1`, `ubuntu-latest` and SDK `10.0.x`. Local review ran SDK 10.0.112 successfully; no additional native library or external runtime service was needed for tests.

Changing package versions can affect test discovery even if production behavior is unchanged. Changing the target framework affects both consumer compatibility and CI SDK selection. See [Build](./build-and-deployment.md), [Testing](./testing.md) and [Change guide](./change-guide.md).