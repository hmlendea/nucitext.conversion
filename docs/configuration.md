# Configuration

## Runtime Configuration

There is none in the public converter API. [NuciTextConverter.cs](../NuciText.Conversion/NuciTextConverter.cs) does not read files, environment variables, command-line arguments, service providers or host configuration. Rules and regular expressions are source literals. The constructor has no options. There is no configuration schema, precedence, profile selection, hot reload or runtime log-level switch.

The consumer can choose converter lifetime and supply different text; these are integration choices, not configurable library policies. Cache capacity, expiry and character mappings cannot be changed through a supported API.

## Build Settings

| Source | Settings | Effect |
|---|---|---|
| [Production project](../NuciText.Conversion/NuciText.Conversion.csproj) | `TargetFramework=net10.0`, `RootNamespace=NuciText.Conversion`, `Version=1.0.0`, `ImplicitUsings=disable` | Target compatibility, namespace default, package version and explicit imports |
| [Production project](../NuciText.Conversion/NuciText.Conversion.csproj) | Description, author/copyright, repository URL, package tags, `PackageLicenseExpression=GPL-3.0-or-later` | Package metadata, not conversion behavior |
| [Test project](../NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj) | `TargetFramework=net10.0`, `IsPackable=false`, package references | Test build, discovery and packaging exclusion |
| [CI workflow](../.github/workflows/dotnet.yml) | `ubuntu-latest`, `10.0.x`, `master` triggers | Validation environment and trigger scope |
| [Solution](../NuciText.Conversion.sln) | Debug/Release and platform mappings | Which project configurations build |

Neither project enables nullable reference annotations explicitly. No root SDK pin or repository package-source override was found. Machine/user NuGet settings and SDK resolution may affect builds, but there is no repository-specific precedence to document.

See [Build](./build-and-deployment.md) for exact commands and [Change guide](./change-guide.md) before making runtime rules configurable.