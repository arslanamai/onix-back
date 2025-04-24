using Amai.Users.Application;
using Amai.Users.Contract;
using Amai.Users.Infrastructure;
using Amai.Users.Presentation;

namespace Amai.Web;

public static class Inject
{
    public static IServiceCollection AddAllService(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        
        services.AddUserService(configuration);
        
        return services;
    }

    private static IServiceCollection AddUserService(
        this IServiceCollection service, IConfiguration configuration)
    {

        service
            .AddUserApplication()
            .AddUserContract()
            .AddUserPresentation()
            .AddUserInfrastructure(configuration);
        
        return service;
    }
}