using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.SendGrid.Client.Validation.Abstract;

/// <summary>
/// An async thread-safe singleton for a SendGrid validation client
/// </summary>
public interface ISendGridValidationClientUtil : IDisposable, IAsyncDisposable
{
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}