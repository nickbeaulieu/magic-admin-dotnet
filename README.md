# Magic Authentication Admin Dotnet SDK

Unofficial library for interacting with Magic in .NET

<!-- [![<MagicHQ>](https://circleci.com/gh/magiclabs/magic-admin-js.svg?style=shield)](https://circleci.com/gh/MagicHQ/magic-admin-js) -->

> The Magic Admin SDK lets developers secure endpoints, manage users, and create middlewares via easy-to-use utilities.

<p align="center">
  <a href="./LICENSE">License</a> ·
  <a href="./CHANGELOG.md">Changelog</a> ·
  <a href="./CONTRIBUTING.md">Contributing Guide</a>
</p>

## 📖 Documentation

<!-- See the [developer documentation](https://docs.magic.link/admin-sdk/node-js) to learn how you can master the Magic Admin SDK in a matter of minutes. -->

## 🔗 Installation

Integrating your .NET application with Magic will require our NuGet package:

```bash
dotnet add package MagicAdminSDK --version 0.1.0
```

## ⚡️ Quick Start

Sign up or log in to the [developer dashboard](https://dashboard.magic.link) to receive API keys that will allow your application to interact with Magic's administration APIs.

```cs
using Magic;

var Magic = new MagicAdminSDK('YOUR_SECRET_API_KEY')

// Read the docs to learn about next steps! 🚀
```
