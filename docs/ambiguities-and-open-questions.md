# Ambiguities And Open Questions

## Confirmed Documentation Discrepancies

| Item | Conflicting evidence | Follow-up |
|---|---|---|
| Privacy statement denies cache storage | [Privacy policy](../PRIVACY.md) says no storage in caches; [Implementation](../NuciText.Conversion/NuciTextConverter.cs) retains original/result pairs | Root policy and its corresponding README section need coordinated template-aware correction |
| Blank behavior described as tested | [Root architecture](../ARCHITECTURE.md) says tests cover blank-input behavior; [Tests](../NuciText.Conversion.UnitTests/NuciTextConverterTests.cs) have no null/empty/whitespace cases | Correct root coverage statement or add dedicated tests in a behavior-focused change |
| Output target can read as universal guarantee | Interface XML comments and root purpose describe Windows-1252 conversion; helper preserves unknown code units and never encodes | Define whether the contract is finite approximation or guaranteed representability |
| Cache verification can read as direct test coverage | Root compatibility table refers to behavioral tests and inspection; fixture resets instance per case and never repeats a call | Add dedicated cache tests or explicitly qualify coverage |

Root documents are left unchanged by this documentation addition; these are tracked discrepancies, not silently repaired implementation defects.

## Source-Derived Behavior Needing Contract Decisions

- Terminal U+0120/U+0121 remain when no following character satisfies contextual rules. Is terminal handling intended?
- `a\u030D` becomes U+0227 after its common-map opportunity, then U+00E1 on a later conversion. Is idempotence desired?
- Selected tilde compositions such as `v\u0303` introduce an unmapped precomposed character. Should the encoder target be checked after composition?
- Selected late sequence replacements mention characters already mapped or marks already removed. Some rules appear redundant or unreachable for their literal original sequences; confirm intent before deletion.
- Uppercase and lowercase approximations are deliberately encoded differently in some entries, such as D-stroke. Linguistic rationale is not recorded.
- Comments include tentative alternatives, guessed mappings and mappings untested in games. The current examples establish behavior, not the correctness of those choices in each game or language.

These conclusions come from [Production source](../NuciText.Conversion/NuciTextConverter.cs), not new executed edge-case tests. See [Behaviour](./behaviour/text-conversion.md).

## Verification And Operational Gaps

- The output-set assertion uses a sample allowlist and suppresses whitespace-only mismatches; it is not a strict encoder proof.
- No dedicated null, whitespace, cache-hit, concurrency, unknown-Unicode, idempotence, performance or security tests exist.
- Expected cache lifetime, cardinality and acceptable retained memory are undocumented; no bound or clearing API exists.
- Moq is referenced but unused in the current fixture; whether it should remain is a dependency-maintenance decision.
- No checked-in release process, signing policy, registry verification, SDK pin or dependency lock establishes reproducible publication.
- No external consumer inventory or assigned component owner is present. Interface stability policy beyond existing contribution guidance is unknown.

No root roadmap exists. These questions are a comprehension ledger, not a roadmap or a commitment to change behavior.