
using Amai.Users.Application;
using Amai.Users.Contract;
using Amai.Users.Infrastructure;

namespace Onix.Web;

public static class Inject
{
    public static IServiceCollection AddAllService(
        this IServiceCollection services, IConfiguration configuration)
    {
        /*services.AddWebSiteApplication()
            .AddWebSiteInfrastructure(configuration)
            .AddWebSitePresentation();*/

        services.AddUserService(configuration);
        
        return services;
    }

    private static IServiceCollection AddUserService(
        this IServiceCollection service, IConfiguration configuration)
    {

        service
            .AddUserApplication()
            .AddUserContract()
            .AddUserInfrastructure(configuration);
        
        return service;
    }
}