# Build And Deployment

## Local Verification

From the repository root, the [CI workflow](../.github/workflows/dotnet.yml) defines:

```bash
dotnet restore
dotnet build --no-restore
dotnet test --no-build --verbosity normal
```

The [Solution](../NuciText.Conversion.sln) includes the library and tests. Both target `net10.0`; no SDK pinning file or custom package-source configuration exists in the inspected inventory. Default build configuration is Debug unless overridden. Build artefacts are generated under project-local `bin` and `obj` directories.

Review verification used SDK 10.0.112 and `dotnet test --no-restore --verbosity minimal`: build succeeded and all 130 tests passed. This used existing restore assets; it does not independently prove a clean online restore or remote CI success.

## GitHub Actions

The sole workflow is named `.NET`, runs on `ubuntu-latest`, and triggers on pushes and pull requests targeting `master`. It uses `actions/checkout@v2`, `actions/setup-dotnet@v1`, and SDK selector `10.0.x`, then restores, builds and tests in that order. It does not publish packages, upload test results, produce coverage, create releases or deploy a service.

Action references and the runner image are not immutable pins. No conclusion about their present remote execution health follows from local tests.

## Packaging And Runtime Deployment

The [Production project](../NuciText.Conversion/NuciText.Conversion.csproj) declares version `1.0.0`, description `Text conversion library`, repository metadata and `GPL-3.0-or-later`. It has no third-party runtime package references. The [Test project](../NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj) is non-packable.

The [README](../README.md) advertises NuGet installation. No checked-in release workflow or publishing script establishes how the advertised package is published. Do not infer automatic publication, registry credentials, tag conventions, signing or package provenance guarantees. SDK packaging can be performed by maintainers, but this review did not create or publish a package.

There is no standalone application deployment, port, health probe, startup sequence, database migration or shutdown operation. A consumer loads the library and owns runtime resources. See [Integrations](./integrations.md), [Dependencies](./dependencies.md) and [Configuration](./configuration.md).