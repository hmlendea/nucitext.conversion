# Security Policy

This document describes the security policy for NuciText.Conversion, including supported versions, vulnerability reporting, and disclosure expectations.

## 📑 Table of Contents

- [Supported Versions](#supported-versions)
- [Reporting a Vulnerability](#reporting-a-vulnerability)
- [Scope](#scope)
- [Disclosure Policy](#disclosure-policy)
- [Automated Security Scanning](#automated-security-scanning)

## 🛡️ Supported Versions

Use this table to indicate which project versions currently receive security maintenance.

| Version | Distribution Channel | Supported |
|---------|--------------------|-----------|
| Latest version | NuGet | ✅ |
| Preceding versions | Any distribution channel | ❌ |

## 🚨 Reporting a Vulnerability

Please do not disclose suspected vulnerabilities publicly before maintainers have had an opportunity to validate and remediate them.

To report a vulnerability:
- [GitHub Security Advisories](https://github.com/hmlendea/nucitext.conversion/security/advisories)
- Contact the maintainers directly

## 📌 Scope

The original maintainers are not responsible for changes, modifications, or security issues introduced by downstream forks.

The subsequent report categories are in scope for this repository:
- Vulnerabilities in the NuciText.Conversion library code
- Vulnerabilities in the build and release pipeline
- Vulnerabilities in project dependencies

The subsequent categories are out of scope unless explicitly stated to the contrary:
- Security issues in applications that consume this library
- Security issues in downstream forks or modified distributions
- Issues requiring elevated privileges or local access beyond the library's threat model

## 📢 Disclosure Policy

This project follows coordinated disclosure:
1. Vulnerabilities are investigated privately.
2. A remediation plan is prepared and validated.
3. Public disclosure is published after a fix, mitigation, or agreed risk decision is available.
4. Credit is attributed in accordance with reporter preference and project policy.

## 🔍 Automated Security Scanning

This repository runs automated security scans weekly, and on every push to and pull request against `master`. The scan includes:
- **CodeQL code analysis** of the C# source and build, uploaded as SARIF results to GitHub Code Scanning.
- **Vulnerable package detection** across direct and transitive NuGet dependencies.

Results are published to the [Security](https://github.com/hmlendea/nucitext.conversion/security) tab and the [Actions](https://github.com/hmlendea/nucitext.conversion/actions/workflows/security.yml) workflow page.