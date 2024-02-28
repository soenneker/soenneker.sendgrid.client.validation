using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using Soenneker.Extensions.Configuration;
using Soenneker.SendGrid.Client.Validation.Abstract;
using Soenneker.Utils.AsyncSingleton;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.SendGrid.Client.Validation;

/// <inheritdoc cref="ISendGridValidationClientUtil"/>
public class SendGridValidationClientUtil: ISendGridValidationClientUtil
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly ILogger<SendGridValidationClientUtil> _logger;

    private readonly AsyncSingleton<SendGridClient> _client;

    public SendGridValidationClientUtil(IConfiguration config, IHttpClientCache httpClientCache, ILogger<SendGridValidationClientUtil> logger)
    {
        _httpClientCache = httpClientCache;
        _logger = logger;

        _client = new AsyncSingleton<SendGridClient>( () =>
        {
            var apiKey = config.GetValueStrict<string>("SendGrid:ValidationApiKey");

            logger.LogDebug("Connecting SendGrid validation client...");

            //HttpClient httpClient = await httpClientCache.Get(nameof(SendGridValidationClientUtil));

            //var options = new SendGridClientOptions { ApiKey = apiKey };

            var client = new SendGridClient(apiKey);

            return client;
        });
    }

    public ValueTask<SendGridClient> Get()
    {
        return _client.Get();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

       // _httpClientCache.RemoveSync(nameof(SendGridValidationClientUtil));

        _client.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        //await _httpClientCache.Remove(nameof(SendGridValidationClientUtil));

        await _client.DisposeAsync();
    }
}
