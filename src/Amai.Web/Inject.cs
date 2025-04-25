using Amai.Users.Application;
using Amai.Users.Contract;
using Amai.Users.Infrastructure;
using Amai.Users.Presentation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Amai.Web;

public static class Inject
{
    public static IServiceCollection AddAllService(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddUserService(configuration);
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAuthorization();
        services.AddAuth0Authentication(configuration);
        
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

    private static IServiceCollection AddAuth0Authentication(
        this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var auth0Settings = configuration.GetSection("Auth0");
                options.Authority = auth0Settings["Domain"];
                options.Audience = auth0Settings["Audience"];
            });

        return services;
    }
}