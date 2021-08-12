# Magic Authentication Admin .NET SDK

Unofficial library for interacting with Magic in .NET. This package is not officially supported by [Magic](https://magic.link)

> The Magic Admin SDK lets developers secure endpoints, manage users, and create middlewares via easy-to-use utilities.

<p align="center">
  <a href="./LICENSE">License</a> ·
  <a href="./CHANGELOG.md">Changelog</a> ·
  <a href="./CONTRIBUTING.md">Contributing Guide</a>
</p>

## 📖 Documentation

Ported directly from [Magic Admin JS](https://github.com/magiclabs/magic-admin-js). All functionalities (and unit tests!) were replicated in C#

See the [developer documentation](https://docs.magic.link/admin-sdk/node-js) to learn more

## 🔗 Installation

Integrating your .NET application with Magic will require the NuGet package:

```bash
dotnet add package MagicAdminSDK --version 0.1.0
dotnet add package Nethereum.Web3 --version 3.8.0
```

## ⚡️ Quick Start

Sign up or log in to the [developer dashboard](https://dashboard.magic.link) to receive API keys that will allow your application to interact with Magic's administration APIs.

```cs
using Magic;

var Magic = new MagicAdminSDK('YOUR_SECRET_API_KEY');

// Read the docs to learn about next steps! 🚀
```

## Examples
