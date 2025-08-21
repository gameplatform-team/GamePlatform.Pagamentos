using GamePlatform.Pagamentos.Domain.Interfaces;
using GamePlatform.Pagamentos.Infrastructure.Data;
using GamePlatform.Pagamentos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamePlatform.Pagamentos.Application.Configuration;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPagamentoRepository, PagamentoRepository>();
    }
}