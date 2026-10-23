# Documentation Maintenance

## Ownership And Evidence

Repository maintainers own this knowledge base; no file-specific ownership assignment was found. The [Index](./INDEX.md) records review date, baseline and coverage. These pages describe source at that baseline, not future intended behavior.

Evidence priority is implementation and callers, then tests and project/workflow configuration, then existing prose. Contradictions must be recorded rather than resolved by choosing the most reassuring statement. Historical rationale requires explicit evidence; otherwise label it unknown.

## Update Procedure

1. Follow the changed input through callers, transformations, cache publication and consumers.
2. Compare source behavior with exact tests, including alternate branches and unsupported cases.
3. Update affected API, component, behavior, flow and invariant pages together.
4. Assess the [README](../README.md), [Root architecture](../ARCHITECTURE.md), [Security policy](../SECURITY.md) and [Privacy policy](../PRIVACY.md); use their mandated templates when editing them.
5. Add every new documentation file to the grouped catalogue in the [Index](./INDEX.md) and verify relative links.
6. Record executed commands/results separately from source-derived examples and untested assumptions.
7. Refresh review metadata and close resolved items in [Open questions](./ambiguities-and-open-questions.md).

## Naming And Navigation

Files use kebab-case except required `INDEX.md` names. Collection directories are plural except the requested `behaviour` category. Repository instructions require title-case headings, so these pages follow that rule rather than the requested sentence-case convention.

Links resolve relative to the containing Markdown file: top-level pages use `./`, nested pages use `../` for documentation siblings and `../../` for repository sources. Root-relative-from-docs notation cannot be copied unchanged into nested pages without breaking links.

## Scope Boundaries

Do not create a second copy of root architecture, licence, security policy, privacy policy or roadmap under this directory. The [Architecture reading guide](./architecture.md) and [Security engineering](./security.md) provide contextual implementation detail only. There are no controllers, so the [API index](./api-reference/INDEX.md) explicitly documents the library surface instead of inventing HTTP endpoints.

For a regression, keep the exact source location and test name close to the behavioral explanation. Avoid line-number-only references that drift with mapping-table edits. See [Change guide](./change-guide.md).