using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Core.Application.Common.Interfaces;
using Core.Infrastructure.Data;
using Core.Infrastructure.Repositories;

namespace Core.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Support Render's DATABASE_URL environment variable
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            // Fallback to DATABASE_URL (used by Render and other platforms)
            connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
        }

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}

