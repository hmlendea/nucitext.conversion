# Privacy and Personal Data

This document describes how NuciText.Conversion handles personal data. The library is a self-contained .NET text conversion library with no data collection, telemetry, or external integrations.

## 📑 Table of Contents

- [What This Document Covers](#what-this-document-covers)
- [Self-Hosted Deployments](#self-hosted-deployments)
- [Data We Handle](#data-we-handle)
- [Processing and Use](#processing-and-use)
- [Storage, Retention, and Deletion](#storage-retention-and-deletion)
- [External Processing and Integrations](#external-processing-and-integrations)
- [Data Protection and Security](#data-protection-and-security)
- [Document Changes](#document-changes)
- [Contact](#contact)

## 🔎 What This Document Covers

This document describes how NuciText.Conversion at https://github.com/hmlendea/NuciText.Conversion handles personal data. It covers the application behaviour and verified integrations described below. Where the software is self-hosted, the instance operator may have separate responsibilities described below.

## 🏠 Self-Hosted Deployments

NuciText.Conversion is a library that can be used in self-hosted applications. This document covers the library itself. Instance operators control their application's configuration, local storage, logs, backups, access controls, retention, and request handling. The library does not send any data to project maintainers or external services.

## 📥 Data We Handle

### Data Provided to the Application

No personal data is requested or provided to the application. The library operates on text strings passed by the calling code.

### Data Generated or Collected by the Application

No personal data is generated or collected automatically. The library performs text conversion operations in memory and returns the result.

### Data Received from Integrations

No personal data is received from integrations or third parties. The library has no built-in integrations.

## 🧭 Processing and Use

The application processes the data described above for these verified functions:
- Text conversion to Windows-1252 encoding — Input text string provided by the caller

## 🗄️ Storage, Retention, and Deletion

The library does not store any data. All operations are performed in memory on the input string provided by the caller. No data is persisted to disk, databases, logs, caches, or backups by the library itself.

## 🔗 External Processing and Integrations

The application has no built-in external data transfer. No external services, recipients, or integrations process or receive data from this library.

| Service or integration | Purpose | Data involved | Configuration or documentation |
|-----------------------|---------|---------------|--------------------------------|
| None | N/A | N/A | N/A |

## 🛡️ Data Protection and Security

The library performs in-memory text processing only. It does not handle secrets, credentials, or sensitive data. No network connections are made. For self-hosted deployments, the instance operator is responsible for the security of their application that uses this library.

The original maintainers are not responsible for changes, modifications, or privacy issues introduced by downstream forks.

## 🔄 Document Changes

Update this document when application data flows, storage, integrations, or deployment responsibilities change. The current version is published at https://github.com/hmlendea/NuciText.Conversion/blob/master/PRIVACY.md.

## 📬 Contact

For questions about application data handling, contact the project maintainers. For a self-hosted instance, contact the instance operator, unless the project explicitly handles the request. Include the library version and deployment context; do not send passwords, access tokens, or other secrets.