using Mapster;
using MapsterMapper;

namespace WebApplication1.Extension;

public static class MapsterExtensions
{
    public static IServiceCollection AddMapsterConfig(
        this IServiceCollection services
    )
    {
        var config = TypeAdapterConfig.GlobalSettings;
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}