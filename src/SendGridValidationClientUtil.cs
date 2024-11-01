using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Extensions.Configuration;
using Soenneker.SendGrid.Client.Validation.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;
using Soenneker.Utils.HttpClientCache.Dtos;

namespace Soenneker.SendGrid.Client.Validation;

/// <inheritdoc cref="ISendGridValidationClientUtil"/>
public class SendGridValidationClientUtil : ISendGridValidationClientUtil
{
    private readonly IHttpClientCache _httpClientCache;

    private readonly HttpClientOptions _options;

    public SendGridValidationClientUtil(IConfiguration configuration, IHttpClientCache httpClientCache)
    {
        _httpClientCache = httpClientCache;

        var apiKey = configuration.GetValueStrict<string>("SendGrid:ValidationApiKey");

        _options = new HttpClientOptions
        {
            BaseAddress = "https://api.sendgrid.com/",
            DefaultRequestHeaders = new System.Collections.Generic.Dictionary<string, string> {{"Authentication", $"bearer {apiKey}"}}
        };
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(SendGridValidationClientUtil), _options, cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _httpClientCache.RemoveSync(nameof(SendGridValidationClientUtil));
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _httpClientCache.Remove(nameof(SendGridValidationClientUtil));
    }
}