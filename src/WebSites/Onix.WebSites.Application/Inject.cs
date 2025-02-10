using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Onix.Core.Abstraction;
using Onix.WebSites.Application.Commands.Blocks.Add;
using Onix.WebSites.Application.Commands.Blocks.Delete;
using Onix.WebSites.Application.Commands.Blocks.Update;
using Onix.WebSites.Application.Commands.Locations.Add;
using Onix.WebSites.Application.Commands.Locations.Delete;
using Onix.WebSites.Application.Commands.Locations.Update;
using Onix.WebSites.Application.Commands.Products.Add;
using Onix.WebSites.Application.Commands.Products.Delete;
using Onix.WebSites.Application.Commands.Products.Update;
using Onix.WebSites.Application.Commands.WebSites.Create;
using Onix.WebSites.Application.Commands.WebSites.Delete;
using Onix.WebSites.Application.Commands.WebSites.Update;
using Onix.WebSites.Application.Queries.WebSites.GetById;
using Onix.WebSites.Application.Queries.WebSites.GetByIdWithBlocks;
using Onix.WebSites.Application.Queries.WebSites.GetByIdWithFavicon;
using Onix.WebSites.Application.Queries.WebSites.GetByIdWithLocations;
using Onix.WebSites.Application.Queries.WebSites.GetByUrl;

namespace Onix.WebSites.Application;

public static class Inject
{
    public static IServiceCollection AddWebSiteApplication(
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

        services
            .AddValidatorsFromAssembly(assembly)
            .WebSiteCommand()
            .BlockCommand()
            .LocationCommand()
            .ProductCommand()
            .AddQuery();

        return services;
    }

    private static IServiceCollection WebSiteCommand(
        this IServiceCollection service)
    {
        service.AddScoped<CreateWebSiteHandler>();
        service.AddScoped<UpdateWebSiteHandler>();
        service.AddScoped<DeleteWebSiteHandler>();
        
        return service;
    }
    
    private static IServiceCollection BlockCommand(
        this IServiceCollection service)
    {
        service.AddScoped<AddBlockHandler>();
        service.AddScoped<UpdateBlockHandler>();
        service.AddScoped<DeleteBlockHandle>();
        
        return service;
    }

    private static IServiceCollection LocationCommand(
        this IServiceCollection service)
    {
        service.AddScoped<AddLocationHandler>();
        service.AddScoped<UpdateLocationHandler>();
        service.AddScoped<DeleteLocationHandler>();

        return service;
    }

    private static IServiceCollection ProductCommand(
        this IServiceCollection service)
    {
        service.AddScoped<AddProductHandler>();
        service.AddScoped<UpdateProductHandler>();
        service.AddScoped<DeleteProductHandler>();
        
        return service;
    }

    private static IServiceCollection AddQuery(
        this IServiceCollection service)
    {
        service.AddScoped<GetWebSiteByIdHandler>();
        service.AddScoped<GetWebSiteByUrlHandler>();
        service.AddScoped<GetBlocksHandler>();
        service.AddScoped<GetWebSiteByIdWithFaviconHandler>();
        service.AddScoped<GetWebSiteByIdWithLocationsHandler>();

        return service;
    }
}