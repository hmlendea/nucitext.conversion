# Design Decisions

This is an evidence ledger, not a retrospective claim about author intent. No architecture decision records or design discussions establish historical rationale. The [Implementation](../NuciText.Conversion/NuciTextConverter.cs), [Projects](./dependencies.md) and [Tests](./testing.md) establish choices and consequences.

| Observed choice | Consequence | Rationale status |
|---|---|---|
| Interface plus one sealed implementation | Hosts can depend on a small contract or supply another implementation; subclassing current converter is unavailable | Intent beyond API shape unknown |
| Explicit finite maps and ordered replacements | Supports domain-specific approximations but does not cover arbitrary Unicode | Game-related comments support specialized context, not linguistic correctness |
| Unicode string return | Hosts retain control of byte encoding; method name is not proof of encoded output | Public signature establishes fact; historical naming rationale unknown |
| Single-pass maps | Replacement strings can feed later stages but not recursively the same map | Algorithmic choice observed; performance motivation unproven |
| Per-instance concurrent result cache | Repeated exact inputs skip work; originals/results retained without bound | Memoisation effect observed; expected workload/cardinality undocumented |
| Private per-instance dictionaries, no mutation API | Concurrent readers share fixed rules; instance creation reallocates tables | No explicit immutability design rationale recorded |
| Blank input becomes empty | Null/blank callers receive a simple value rather than an exception | Source branch establishes behavior; coverage absent |
| No I/O or logging | Conversion is isolated from external availability; hosts own operations | Absence verified in production source |
| NUnit exact-output examples | Compatibility expectations are concrete for the sample corpus | Exhaustive coverage or formal encoding proof not established |

## Decisions Requiring Maintainer Input

Before changing behavior, resolve whether complete Windows-1252 representability is a required contract or merely the intent for supported inputs. Also resolve expected idempotence, mapping asymmetry, cache limits and rule provenance. Existing code alone cannot determine the desired answer. See [Open questions](./ambiguities-and-open-questions.md).

For new decisions, record the observed problem, alternatives, intended contract, migration impact and tests; do not describe an inferred preference as established policy. See [Change guide](./change-guide.md).