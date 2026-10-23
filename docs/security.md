# Security Engineering

The [Root security policy](../SECURITY.md) governs supported versions, vulnerability scope and coordinated disclosure. This page documents implementation-specific boundaries, not a replacement policy. The package's licence metadata is described in the [Project](../NuciText.Conversion/NuciText.Conversion.csproj) and the [Root licence](../LICENSE).

## Trust Boundary

The caller supplies arbitrary strings to `NuciText.Conversion.INuciTextConverter.ToWindows1252`. The library performs no authentication, authorisation, file access, networking or code execution. Fixed source regex patterns are not caller-provided patterns. Unknown characters are not rejected.

## Threats And Practical Controls

| Concern | Evidence | Host-side control |
|---|---|---|
| Memory growth from many distinct inputs | Unbounded original-to-output instance cache | Limit input size/cardinality and select bounded instance lifetime |
| Sensitive data retention | Original text is stored as cache key | Avoid unnecessarily long-lived instances for sensitive workloads; do not promise secure erasure |
| Encoding mismatch | Unknown code units pass through | Validate with the actual strict target encoder |
| Identifier collisions and spoofing | Lossy maps merge script lookalikes and delete marks | Do not use output as an authentication identity, uniqueness proof or security canonical form |
| Injection into downstream formats | No escaping or validation stage | Apply context-specific escaping/validation at the output boundary |
| Resource consumption | Many passes, intermediate allocations, no input limit or explicit regex timeout | Apply workload limits; benchmark real inputs before high-volume sharing |

No adversarial benchmark, fuzzing suite, collision analysis or formal transliteration security guarantee exists in current tests. No regex exploit is claimed by this review.

## Privacy Relationship

The library performs no built-in external data transfer. However, the [Privacy policy](../PRIVACY.md) currently denies cache storage, while [Implementation](../NuciText.Conversion/NuciTextConverter.cs) retains originals and outputs in memory. See [Open questions](./ambiguities-and-open-questions.md). Garbage collection is not deterministic deletion, and host dumps, logs or swap are outside this library's controls.

## Pipeline And Dependencies

Production has no third-party package references. Test/tooling dependencies and GitHub actions still require maintenance; [Dependencies](./dependencies.md) lists declared versions. Local passing tests do not establish dependency vulnerability status or published package provenance.

Report suspected vulnerabilities through the private routes in the [Security policy](../SECURITY.md), not public issue examples containing sensitive input.