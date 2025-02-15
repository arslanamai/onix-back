using Microsoft.Extensions.DependencyInjection;
using Onix.Account.Application.Queries.Users.GetByEmail;
using Onix.Account.Application.Queries.Users.GetById;
using Onix.Core.Abstraction;

namespace Onix.Account.Application;

public static class Inject
{
    public static IServiceCollection AddAccountApplication(
        this IServiceCollection services)
    {
        var assembly = typeof(Inject).Assembly;

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(IQueryHandler<,>), typeof(IQueryHandlerWithResult<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
        
        
        return services;
    }
    
    private static IServiceCollection UserCommand(
        this IServiceCollection service)
    {
        
        
        return service;
    }
    
    private static IServiceCollection UserQuery(
        this IServiceCollection service)
    {
        service.AddScoped<GetUserByEmailHandler>();
        service.AddScoped<GetUserByIdHandler>();
        
        return service;
    }
}