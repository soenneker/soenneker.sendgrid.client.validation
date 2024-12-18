using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.SendGrid.Client.Validation.Abstract;

namespace Soenneker.SendGrid.Client.Validation.Registrars;

/// <summary>
/// An async thread-safe singleton for a SendGrid validation client
/// </summary>
public static class SendGridValidationClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ISendGridValidationClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static void AddSendGridValidationClientUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<ISendGridValidationClientUtil, SendGridValidationClientUtil>();
    }

    /// <summary>
    /// Adds <see cref="ISendGridValidationClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static void AddSendGridValidationClientUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<ISendGridValidationClientUtil, SendGridValidationClientUtil>();
    }
}
