using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.SendGrid.Client.Validation.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.SendGrid.Client.Validation.Registrars;

/// <summary>
/// An async thread-safe singleton for a SendGrid validation client
/// </summary>
public static class SendGridValidationClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ISendGridValidationClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddSendGridValidationClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<ISendGridValidationClientUtil, SendGridValidationClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ISendGridValidationClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddSendGridValidationClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<ISendGridValidationClientUtil, SendGridValidationClientUtil>();

        return services;
    }
}