# Repository Knowledge Base

NuciText.Conversion is a synchronous .NET 10 library that applies ordered, lossy text substitutions intended for Windows-1252-oriented consumers. It accepts and returns Unicode strings, caches results per converter instance, and has no application host, HTTP API, storage service, or runtime external integration. These documents explain actual execution and its limits rather than promising universal Windows-1252 representability.

## Start Here

1. Read [Quick start](./quick-start.md) and [Repository overview](./repository-overview.md).
2. Check the [Public API](./api-reference/INDEX.md) before integrating.
3. Read [Conversion behaviour](./behaviour/text-conversion.md), especially unsupported input.

## Root Document Map

- [Architecture](../ARCHITECTURE.md): existing structural overview; not reproduced here.
- [Security policy](../SECURITY.md): supported versions and private reporting.
- [Privacy policy](../PRIVACY.md): root data-handling statement; see the cache discrepancy in [Open questions](./ambiguities-and-open-questions.md).
- [Licence](../LICENSE): licence text; package metadata specifies GPL-3.0-or-later.
- [README](../README.md): installation and project entry point.
- [Contributing](../CONTRIBUTING.md): contribution process.
- [Code of conduct](../CODE_OF_CONDUCT.md): participation standards.

No root roadmap exists at this review; no roadmap is generated here.

## Documentation Catalogue

### Orientation And Contracts

- [Repository overview](./repository-overview.md): purpose, scope, ownership and consumers.
- [Repository structure](./repository-structure.md): exact source and configuration map.
- [Architecture reading guide](./architecture.md): how to use the root architecture alongside implementation evidence.
- [Quick start](./quick-start.md): local setup and first conversion.
- [API reference index](./api-reference/INDEX.md): public surface and absence of HTTP endpoints.
- [Converter reference](./api-reference/text-converter.md): constructor, method contract and limitations.
- [API usage examples](./api-usage-examples.md): calls and expected string results.
- [Data model](./data-model.md): strings, UTF-16 mapping keys and result entries.
- [Invariants](./invariants.md): preserved contracts and non-guarantees.

### Deep Dive

- [Converter component](./components/text-converter.md): ownership, algorithms and helper semantics.
- [Verification component](./components/unit-tests.md): fixture, assertions and coverage boundaries.
- [Conversion behaviour](./behaviour/text-conversion.md): observable substitutions and edge conditions.
- [Conversion flow](./flows/text-conversion.md): causal stage-by-stage execution.
- [Cache flow](./flows/cache-lifecycle.md): hits, misses, races and retained data.
- [State and persistence](./state-and-persistence.md): memory retention and lifecycle.
- [Concurrency and scheduling](./concurrency-and-scheduling.md): sharing, races and synchronous execution.
- [Error handling](./error-handling.md): blank input, exceptions and missing validation.

### Build And Operations

- [Build and deployment](./build-and-deployment.md): CI, local commands and packaging boundaries.
- [Configuration](./configuration.md): build settings and absence of runtime options.
- [Dependencies](./dependencies.md): framework and direct test dependencies.
- [Integrations](./integrations.md): host, NuGet and GitHub boundaries.
- [Logging](./logging.md): explicit observability inventory and host responsibilities.
- [Security engineering](./security.md): technical threats, cache retention and encoding boundary.
- [Testing](./testing.md): observed results, test matrix and missing cases.
- [Troubleshooting](./troubleshooting.md): targeted diagnosis by symptom.

### Maintenance

- [Change guide](./change-guide.md): safe edits and affected contracts.
- [Design decisions](./design-decisions.md): observed choices versus unknown rationale.
- [Open questions](./ambiguities-and-open-questions.md): contradictions, gaps and unresolved intent.
- [FAQ](./faq.md): common integration questions.
- [Documentation maintenance](./documentation-maintenance.md): evidence, ownership and refresh procedure.

## Navigation By Task

- Component owners: [Converter](./components/text-converter.md), [Invariants](./invariants.md), [Change guide](./change-guide.md).
- Debuggers: [Conversion flow](./flows/text-conversion.md), [Cache flow](./flows/cache-lifecycle.md), [Troubleshooting](./troubleshooting.md).
- Host integrators: [API](./api-reference/text-converter.md), [State](./state-and-persistence.md), [Security](./security.md).

## Maintenance Metadata

| Field | Value |
|---|---|
| Last reviewed | 2026-10-23 |
| Owner | Repository maintainers; no per-area owner assignment is present |
| Source baseline | Git revision `59a6f0e` before this documentation addition |
| Coverage | All production source, test source, project files, solution and CI workflow inspected |
| Executed verification | SDK 10.0.112; `dotnet test --no-restore --verbosity minimal`: 130 passed, zero failed or skipped |
| Evidence limits | Edge cases identified from source are distinguished from executed tests; no external consumer inventory or published package verification |