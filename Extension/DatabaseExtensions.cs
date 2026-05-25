using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Extension;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseConfig(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );
        
        return services;
    }
}