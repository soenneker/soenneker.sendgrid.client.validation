using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using Soenneker.Extensions.Configuration;
using Soenneker.SendGrid.Client.Validation.Abstract;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.SendGrid.Client.Validation;

/// <inheritdoc cref="ISendGridValidationClientUtil"/>
public class SendGridValidationClientUtil : ISendGridValidationClientUtil
{
    private readonly AsyncSingleton<SendGridClient> _client;

    public SendGridValidationClientUtil(IConfiguration config, ILogger<SendGridValidationClientUtil> logger)
    {
        _client = new AsyncSingleton<SendGridClient>(() =>
        {
            var apiKey = config.GetValueStrict<string>("SendGrid:ValidationApiKey");

            logger.LogDebug("Connecting SendGrid validation client...");

            var client = new SendGridClient(apiKey);

            return client;
        });
    }

    public ValueTask<SendGridClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _client.DisposeAsync();
    }
}