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
dotnet add package MagicAdminDotnet
```

## ⚡️ Quick Start

Sign up or log in to the [developer dashboard](https://dashboard.magic.link) to receive API keys that will allow your application to interact with Magic's administration APIs.

```cs
using Magic;

var Magic = new MagicAdminSDK('YOUR_SECRET_API_KEY');

// Read the docs to learn about next steps! 🚀
```

## 😀 Examples

**Validate a token and load issuer**

`Issuer` is what should be stored to the database as a [unique ID per user](https://magic.link/docs/introduction/faq#what-is-the-unique-user-id-i-should-save-to-my-database).

```cs
// Using the `MagicAdminSDK` instance created above

var bearerString = Request.Headers[HeaderNames.Authorization];
var token = Magic.Utils.ParseAuthorizationHeader(bearerString);
Magic.Token.Validate(token);
var metadata = await Magic.Users.GetMetadataByToken(token);
```
