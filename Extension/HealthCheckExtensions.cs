using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace WebApplication1.Extension;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddHealthChecksConfig(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada");
        }
        
        services.AddHealthChecks()
            .AddNpgSql(
                connectionString,
                name: "postgresql"
            );
        
        return services;
    }
    
    public static IEndpointRouteBuilder MapHealthChecksConfig(
        this IEndpointRouteBuilder endpoints
    )
    {
        endpoints.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var entry = report.Entries.FirstOrDefault();
                
                var response = new
                {
                    status = report.Status.ToString(),
                    name = entry.Key
                };
                
                await context.Response.WriteAsJsonAsync(response);
            }
        }
        );
        return endpoints;
    }
}