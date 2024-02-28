using SendGrid;
using System;
using System.Threading.Tasks;

namespace Soenneker.SendGrid.Client.Validation.Abstract;

/// <summary>
/// An async thread-safe singleton for a SendGrid validation client
/// </summary>
public interface ISendGridValidationClientUtil : IDisposable, IAsyncDisposable
{
    ValueTask<SendGridClient> Get();
}
