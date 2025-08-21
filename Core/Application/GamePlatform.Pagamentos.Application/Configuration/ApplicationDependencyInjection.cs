using GamePlatform.Pagamentos.Application.Interfaces.Services;
using GamePlatform.Pagamentos.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GamePlatform.Pagamentos.Application.Configuration;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IPagamentoService, PagamentoService>();
        services.AddScoped<IUsuarioContextService, UsuarioContextService>();

        return services;
    }
}