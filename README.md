[![Donate](https://img.shields.io/badge/-%E2%99%A5%20Donate-%23ff69b4)](https://hmlendea.go.ro/fund.html)
[![Latest Release](https://img.shields.io/github/v/release/hmlendea/nucitext.conversion)](https://github.com/hmlendea/nucitext.conversion/releases/latest)
[![Build Status](https://github.com/hmlendea/nucitext.conversion/actions/workflows/dotnet.yml/badge.svg)](https://github.com/hmlendea/nucitext.conversion/actions/workflows/dotnet.yml)
[![Security Scan](https://github.com/hmlendea/nucitext.conversion/actions/workflows/security.yml/badge.svg)](https://github.com/hmlendea/nucitext.conversion/actions/workflows/security.yml)
[![NuGet](https://img.shields.io/nuget/v/NuciText.Conversion)](https://nuget.org/packages/NuciText.Conversion)
[![License](https://img.shields.io/github/license/hmlendea/nucitext.conversion)](https://github.com/hmlendea/nucitext.conversion/blob/master/LICENSE)

# NuciText.Conversion

.NET C# library for text conversions.

## 📑 Table of Contents

- [Table of Contents](#-table-of-contents)
- [Capabilities](#-capabilities)
- [Usage](#-usage)
- [Known Limitations](#-known-limitations)
- [Installation](#-installation)
  - [Package Manager Installation](#-package-manager-installation)
- [Compatibility](#-compatibility)
- [Privacy and Data](#-privacy-and-data)
- [Development](#-development)
  - [Requirements](#requirements)
  - [Setup](#setup)
  - [Build](#build)
  - [Test](#test)
  - [Continuous Integration](#continuous-integration)
  - [Release](#release)
- [GitHub Actions](#-github-actions)
- [Project Structure](#-project-structure)
  - [Projects and Packages](#projects-and-packages)
  - [Directories](#directories)
- [Architecture](#-architecture)
- [Documentation](#-documentation)
- [API Reference](#-api-reference)
- [Troubleshooting](#-troubleshooting)
- [FAQ](#-faq)
- [Contributing](#-contributing)
- [Code of Conduct](#-code-of-conduct)
- [Security](#-security)
- [Support](#-support)
- [Project Engagement](#-project-engagement)
- [License](#-license)

## ✨ Capabilities

- Converts text containing characters outside the Windows-1252 character set into close Windows-1252-compatible equivalents.
- Applies an ordered transformation pipeline: common character mappings, Windows-1252 character mappings, and contextual regular-expression replacements.
- Replaces any character that no mapping covers with `?` as a last resort, so every result is representable in Windows-1252.
- Exposes a single synchronous contract through `INuciTextConverter` with a sealed implementation.
- Caches conversion results per converter instance in a concurrent dictionary.
- Returns blank input as an empty string without performing any substitution.

## 🚀 Usage

Reference the package and call `ToWindows1252` on a converter instance:

```csharp
using NuciText.Conversion;

INuciTextConverter converter = new NuciTextConverter();

string result = converter.ToWindows1252("Český Krumlov");
// result: "Cheský Krumlov"
```

The method returns a .NET string. When Windows-1252 bytes are required, the host must select and apply that encoding; the result already contains only Windows-1252 characters.

## ⚠️ Known Limitations

- Characters that no mapping covers are replaced with `?`, so information is lost silently and cannot be distinguished from literal question marks in the input.
- Conversion is neither idempotent nor reversible, because lossy substitutions merge distinct originals and later pipeline stages can introduce characters whose mapping stage has already passed.
- The result cache cannot be cleared or bounded through the public API; instance lifetime is the only available control.

## 📦 Installation

[![Obtain it from NuGet](https://raw.githubusercontent.com/hmlendea/readme-assets/master/badges/stores/nuget.png)](https://nuget.org/packages/NuciText.Conversion)

### Package Manager Installation

```bash
dotnet add package NuciText.Conversion
```

Or, via the `Package Manager Console`:
```powershell
Install-Package NuciText.Conversion
```

## 🧩 Compatibility

| Component | Supported Versions | Notes |
|-----------|--------------------|-------|
| .NET runtime | .NET 10.0 (`net10.0`) | The library targets `net10.0`; a .NET SDK capable of targeting `net10.0` is required to build it. |

## 🛡️ Privacy and Data

NuciText.Conversion operates in memory on the strings supplied by the calling application and has no data collection, telemetry, or external integrations.

For the detailed description of how the application handles privacy and personal data, see the [Privacy Statement](./PRIVACY.md).

The library makes no network, filesystem, or database calls. The only state it retains is the converter's in-memory result cache, which lives for the lifetime of the converter instance.

| Data | Purpose | Storage | Retention | Optional |
|------|---------|---------|-----------|----------|
| Input text and conversion result | Avoid repeated substitution work for identical input | In-memory, per `NuciTextConverter` instance | Until the instance is collected | Yes; instance lifetime is chosen by the host |

## 🛠️ Development

### Requirements

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Setup

```bash
git clone https://github.com/hmlendea/nucitext.conversion.git
cd nucitext.conversion
dotnet restore
```

### Build

```bash
dotnet build --no-restore
```

### Test

```bash
dotnet test

# Run a specific test suite
dotnet test --filter FullyQualifiedName~NuciText.Conversion.UnitTests.NuciTextConverterTests
```

### Continuous Integration

The [.NET workflow](https://github.com/hmlendea/nucitext.conversion/blob/master/.github/workflows/dotnet.yml) restores, builds, and tests the solution on `ubuntu-latest` with the .NET 10.0 SDK. The same steps can be reproduced locally with `dotnet restore`, `dotnet build --no-restore`, and `dotnet test --no-build --verbosity normal`.

### Release

Maintainers publish a GitHub release, which triggers the [GitHub Release](https://github.com/hmlendea/nucitext.conversion/blob/master/.github/workflows/github-release.yml) and [NuGet.org Release](https://github.com/hmlendea/nucitext.conversion/blob/master/.github/workflows/nuget-release.yml) workflows. Together they pack the `NuciText.Conversion` NuGet package from the release tag, calculate its SHA256 checksum, upload it to the release, and publish it to NuGet.org.

## ⚙️ GitHub Actions

| Workflow | Purpose | What it does |
|----------|---------|--------------|
| [ .NET ](https://github.com/hmlendea/nucitext.conversion/blob/master/.github/workflows/dotnet.yml) | Continuous integration | Restores, builds and tests the solution on `ubuntu-latest` with the .NET 10.0 SDK for pushes and pull requests targeting `master`. |
| [ Security ](https://github.com/hmlendea/nucitext.conversion/blob/master/.github/workflows/security.yml) | Security scanning | Runs CodeQL analysis of the C# source, checks direct and transitive NuGet dependencies for known vulnerabilities, and publishes results to GitHub Code Scanning weekly and for pushes and pull requests targeting `master`. |
| [ GitHub Release ](https://github.com/hmlendea/nucitext.conversion/blob/master/.github/workflows/github-release.yml) | Release publication | Packs the NuGet package, calculates its SHA256 checksum, and uploads the artefacts to the published GitHub Release. |
| [ NuGet.org Release ](https://github.com/hmlendea/nucitext.conversion/blob/master/.github/workflows/nuget-release.yml) | Package publication | Packs the package and publishes it to NuGet.org through OIDC-authenticated login when a release is published. |

## 🗂️ Project Structure

The solution contains the conversion library and two test projects.

### Projects and Packages

| Project | Type | Purpose |
|---------|------|---------|
| `NuciText.Conversion/NuciText.Conversion.csproj` | Library | Public converter contract, implementation, and character mappings |
| `NuciText.Conversion.UnitTests/NuciText.Conversion.UnitTests.csproj` | NUnit test project | Conversion regression tests |
| `NuciText.Conversion.ArchitectureTests/NuciText.Conversion.ArchitectureTests.csproj` | ArchUnitNET test project | Architectural and convention checks |

### Directories

| Directory | Purpose |
|-----------|---------|
| `docs/` | Repository knowledge base covering API, behaviour, flows, and maintenance |
| `.github/workflows/` | Continuous integration, security scanning, and release workflows |

## 🏗️ Architecture

NuciText.Conversion is an in-process .NET library with a public converter interface, an ordered text-transformation pipeline, and an instance-scoped in-memory result cache.

See the [Architecture Documentation](./ARCHITECTURE.md) for the system context, principal components, runtime flows, ownership boundaries, dependencies, constraints, and extension points.

## 📚 Documentation

The [repository knowledge base](./docs/INDEX.md) documents the implementation, ordered conversion rules, cache lifecycle, verification boundaries, and maintenance guidance. It complements the root architecture rather than duplicating it.

| Resource | Description |
|----------|-------------|
| [Documentation index](./docs/INDEX.md) | Complete catalogue, navigation, evidence baseline, and review metadata |
| [Quick start](./docs/quick-start.md) | Local setup and a first conversion |
| [Conversion behaviour](./docs/behaviour/text-conversion.md) | Observable substitutions and edge conditions |
| [Conversion flow](./docs/flows/text-conversion.md) | Ordered transformations, alternate paths, and causal dependencies |
| [Testing](./docs/testing.md) | Verified test results and coverage limitations |
| [Change guide](./docs/change-guide.md) | Safe rule, cache, and public-contract modifications |
| [Open questions](./docs/ambiguities-and-open-questions.md) | Source-derived edge cases and existing documentation discrepancies |

## 🌐 API Reference

See the [public API reference](./docs/api-reference/INDEX.md) for the synchronous converter contract, examples, and limitations. This library has no HTTP controllers or endpoints. It returns a Unicode string, not Windows-1252 bytes, but constrains every result to the Windows-1252 character set.

## 🩺 Troubleshooting

See [Troubleshooting Guide](./docs/troubleshooting.md) for diagnostic procedures and resolutions to recurring problems.

## ❓ FAQ

See the [Frequently Asked Questions](./docs/faq.md) for responses to frequently raised inquiries.

## 🤝 Contributing

You are welcome to submit any suggestion, feedback, or modification to this project.

When doing so, please:
- Maintain cross-platform compatibility
- Preserve the existing public contract unless a breaking change is intentional
- Submit focused pull requests that conform to the existing code style
- Maintain your branch synchronised with `master`
- Revise the documentation when functionality changes
- Properly test all modifications, including edge cases and error conditions
- Include tests for additional or modified functionality
- Raise a new [issue](https://github.com/hmlendea/nucitext.conversion/issues) for problems or suggestions

See the [Contributing Guidelines](./CONTRIBUTING.md) for details on how to report issues, suggest enhancements, and submit changes.

## 📜 Code of Conduct

See the [Code of Conduct](./CODE_OF_CONDUCT.md) for the standards of conduct expected in all project spaces.

## 🔒 Security

NuciText.Conversion follows a coordinated disclosure policy. Only the latest version distributed via NuGet receives security maintenance.

For information on reporting security vulnerabilities, see the [Security Policy](./SECURITY.md).

## 💬 Support

For support, please [open an issue](https://github.com/hmlendea/nucitext.conversion/issues).

## 💝 Project Engagement

Discovered a problem with NuciText.Conversion or have a suggestion for it? [Open an issue](https://github.com/hmlendea/nucitext.conversion/issues)!

If you find this project useful, consider [funding it](https://hmlendea.go.ro/fund.html) or starring ⭐️ it on GitHub!

[![Donate](https://raw.githubusercontent.com/hmlendea/readme-assets/master/donate_generic.png)](https://hmlendea.go.ro/fund.html)

## 📄 License

This project is being distributed under the `GNU General Public License v3.0` or later.
See [LICENSE](./LICENSE) for additional information.
