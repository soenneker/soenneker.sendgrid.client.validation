using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.SendGrid.Client.Validation.Abstract;

/// <summary>
/// Provides an authenticated HTTP client for SendGrid Email Address Validation.
/// </summary>
public interface ISendGridValidationClientUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached client owned by this provider.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the HTTP client owned by this provider.
    /// </summary>
    new void Dispose();

    /// <summary>
    /// Asynchronously releases the HTTP client owned by this provider.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    new ValueTask DisposeAsync();
}
