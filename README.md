[![](https://img.shields.io/nuget/v/soenneker.sendgrid.client.validation.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sendgrid.client.validation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.sendgrid.client.validation/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.sendgrid.client.validation/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.sendgrid.client.validation.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sendgrid.client.validation/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.sendgrid.client.validation/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.sendgrid.client.validation/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.SendGrid.Client.Validation

Provides a cached, authenticated `HttpClient` for SendGrid Email Address Validation.

## Installation

```bash
dotnet add package Soenneker.SendGrid.Client.Validation
```

## Configuration

```json
{
  "SendGrid": {
    "ValidationApiKey": "SG.xxxxxxxxx"
  }
}
```

## Usage

```csharp
using System.Net.Http.Json;
using Soenneker.SendGrid.Client.Validation.Abstract;
using Soenneker.SendGrid.Client.Validation.Registrars;

services.AddSendGridValidationClientUtilAsSingleton();

public sealed class EmailAddressValidator
{
    private readonly ISendGridValidationClientUtil _sendGrid;

    public EmailAddressValidator(ISendGridValidationClientUtil sendGrid)
    {
        _sendGrid = sendGrid;
    }

    public async Task<HttpResponseMessage> Validate(
        string email,
        CancellationToken cancellationToken)
    {
        HttpClient client = await _sendGrid.Get(cancellationToken);
        return await client.PostAsJsonAsync(
            "v3/validations/email",
            new { email },
            cancellationToken);
    }
}
```

The provider sends `SendGrid:ValidationApiKey` as a bearer token. The key must have access to Email Address Validation. Set `SendGrid:ValidationClientBaseUrl` only when routing through a proxy or compatible endpoint. The caller owns each returned `HttpResponseMessage` and should dispose it after reading the response.
