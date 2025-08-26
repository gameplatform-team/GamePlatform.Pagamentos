using Azure.Messaging.ServiceBus;
using GamePlatform.Pagamentos.Api.BackgroundServices;
using GamePlatform.Pagamentos.Domain.Interfaces.Messaging;
using GamePlatform.Pagamentos.Infrastructure.Messaging;
using Microsoft.Extensions.Options;

namespace GamePlatform.Pagamentos.Api.Configuration;

public static class MessagingConfiguration
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServiceBusOptions>(configuration.GetSection("ServiceBus"));
        services.AddSingleton<IServiceBusPublisher, AzureServiceBusPublisher>();
        services.AddSingleton<ServiceBusClient>(sp =>
        {
            var opts = sp.GetRequiredService<IOptions<ServiceBusOptions>>().Value;
            if (string.IsNullOrWhiteSpace(opts.ConnectionString))
                throw new InvalidOperationException("AzureServiceBus:ConnectionString não configurado.");
            return new ServiceBusClient(opts.ConnectionString);
        });
        services.AddHostedService<GamePurchaseRequestedBackgroundService>();

        return services;
    }
}