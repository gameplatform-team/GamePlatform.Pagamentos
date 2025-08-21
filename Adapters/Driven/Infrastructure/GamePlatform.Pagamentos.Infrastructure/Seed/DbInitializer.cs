using GamePlatform.Pagamentos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Pagamentos.Infrastructure.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(DataContext context)
    {
        await context.Database.MigrateAsync();
    }
}