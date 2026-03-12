using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.SendGrid.Client.Validation.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.SendGrid.Client.Validation;

/// <inheritdoc cref="ISendGridValidationClientUtil"/>
public sealed class SendGridValidationClientUtil : ISendGridValidationClientUtil
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _configuration;

    private const string _clientId = nameof(SendGridValidationClientUtil);
    private static readonly Uri _baseUrl = new Uri("https://api.sendgrid.com/");

    public SendGridValidationClientUtil(IConfiguration configuration, IHttpClientCache httpClientCache)
    {
        _httpClientCache = httpClientCache;
        _configuration = configuration;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        // No closure: state passed explicitly + static lambda
        return _httpClientCache.Get(_clientId, (configuration: _configuration, baseUrl: _baseUrl), static state =>
        {
            var apiKey = state.configuration.GetValueStrict<string>("SendGrid:ValidationApiKey");

            return new HttpClientOptions
            {
                BaseAddress = state.baseUrl,
                DefaultRequestHeaders = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "Authorization", $"Bearer {apiKey}" }
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_clientId);
    }
}