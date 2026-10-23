# Troubleshooting

## Output Cannot Be Encoded As Windows-1252

Inspect the exact output code points, not only rendered glyphs. `NuciText.Conversion.NuciTextConverter.ReplaceUsingMap` preserves unknown code units and final output is not checked by an encoder. A terminal U+0120, unmapped Han text or newly composed U+1E7D can survive. Apply strict host-side encoding validation; do not silently replace unknown characters unless that is your intended policy. See [Behaviour](./behaviour/text-conversion.md).

## Similar-Looking Inputs Produce Different Results

There is no general Unicode normalization. Compare precomposed versus combining spellings and invisible code points. Trace the [Ordered flow](./flows/text-conversion.md), especially whether a character was introduced after its map stage. Preserve code-point sequences in a regression test rather than copying visually indistinguishable text without explanation.

## A Second Conversion Changes Output

The pipeline is not a fixed-point computation. Source-derived example: `a\u030D` becomes U+0227 after the common-map stage, then U+00E1 on a later call. The cache for the original input does not make conversion of its output idempotent. Decide the desired contract before adding repeated passes. See [Invariants](./invariants.md).

## Memory Grows With Traffic

Count distinct original inputs and review converter lifetime. The cache has no expiry or maximum, and retains originals even if output is shorter or empty. Different instances do not share entries. There is no cache-clear API; choose host lifetime accordingly. See [State](./state-and-persistence.md).

## Empty Output Is Unexpected

Null/whitespace-only input returns empty immediately. A nonblank input consisting of removable marks or invisibles can also become empty after conversion and is cached. No result status distinguishes these paths. Do not log raw sensitive text while diagnosing; see [Logging](./logging.md).

## Build Or Test Discovery Fails

Check that the selected SDK can target .NET 10, restore dependencies, then use the [CI commands](./build-and-deployment.md). The test project requires NUnit3TestAdapter as well as NUnit and Microsoft.NET.Test.Sdk. Current observed baseline is 130 cases, not zero. No repository SDK pin makes every machine select the same SDK.

These procedures follow implemented boundaries; they are not claims that any particular build/network failure was observed. Consult [Dependencies](./dependencies.md) for exact versions.