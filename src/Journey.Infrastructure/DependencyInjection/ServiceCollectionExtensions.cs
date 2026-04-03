using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Journey.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IHostEnvironment environment)
    {
        var databasePath = Path.GetFullPath(
            Path.Combine(environment.ContentRootPath, "..", "Journey.Infrastructure", "JourneyDatabase.db"));
        var connectionString = $"Data Source={databasePath}";

        services.AddDbContext<JourneyDbContext>(options =>
            options.UseSqlite(connectionString));

        return services;
    }
}