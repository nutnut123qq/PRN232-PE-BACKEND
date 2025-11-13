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
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                // Convert Render's DATABASE_URL format (postgresql://user:pass@host:port/db)
                // to Npgsql format (Host=host;Database=db;Username=user;Password=pass;Port=port)
                if (databaseUrl.StartsWith("postgresql://") || databaseUrl.StartsWith("postgres://"))
                {
                    var uri = new Uri(databaseUrl);
                    var userInfo = uri.UserInfo.Split(':');
                    var username = userInfo[0];
                    var password = userInfo.Length > 1 ? userInfo[1] : "";
                    var host = uri.Host;
                    var port = uri.Port > 0 ? uri.Port : 5432;
                    var database = uri.AbsolutePath.TrimStart('/');
                    
                    connectionString = $"Host={host};Database={database};Username={username};Password={password};Port={port};SSL Mode=Require;Trust Server Certificate=true";
                }
                else
                {
                    connectionString = databaseUrl;
                }
            }
        }

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}

