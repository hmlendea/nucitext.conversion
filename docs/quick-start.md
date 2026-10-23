# Quick Start

## Requirements

Use a .NET SDK capable of targeting `net10.0`. Both the [Library project](../NuciText.Conversion/NuciText.Conversion.csproj) and [Test project](../NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj) target that framework. Review used SDK 10.0.112; no repository SDK pin exists.

From the repository root:

```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build --verbosity normal
```

These are the commands in the [CI workflow](../.github/workflows/dotnet.yml). Restore needs access to configured package sources unless dependencies are already available locally. There are no application secrets, databases or services to provision.

## First Conversion

Reference the production project from your application, or follow the existing [README installation instructions](../README.md) for the advertised NuGet distribution.

```csharp
NuciText.Conversion.INuciTextConverter converter =
    new NuciText.Conversion.NuciTextConverter();

string result = converter.ToWindows1252("\u010Cesk\u00FD Krumlov");
// result: "Chesk\u00FD Krumlov"
```

The expected value is covered by a repository regression case. The result is still a .NET string. If a file or protocol needs Windows-1252 bytes, the host must separately select that encoding and decide how to handle unsupported output. The converter does not register an encoding provider.

## Before Production Use

- Review [API limitations](./api-reference/text-converter.md) and [Security engineering](./security.md).
- Select an instance lifetime with [Cache retention](./state-and-persistence.md) in mind.
- Do not use conversion as validation, authentication canonicalisation or HTML escaping.
- Use [Troubleshooting](./troubleshooting.md) for unexpected characters or test discovery issues.