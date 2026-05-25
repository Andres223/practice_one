using WebApplication1.Repositories;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Extension;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        
        return services;
    }
}