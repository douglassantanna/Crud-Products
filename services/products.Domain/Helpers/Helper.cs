using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace products.Domain.Helpers;
public static class Helper
{
    public static IServiceCollection AutoMapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}